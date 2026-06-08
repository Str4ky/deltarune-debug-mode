using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UndertaleModLib;
using UndertaleModLib.Models;
using UndertaleModLib.Util;
using static UndertaleModLib.Models.UndertaleSound;
using static UndertaleModLib.UndertaleData;
using ImageMagick;
using ImageMagick.Drawing;

public class TextureInfo
{
    public string Source;
    public int Width;
    public int Height;
    public int TargetX;
    public int TargetY;
    public int BoundingWidth;
    public int BoundingHeight;
    public MagickImage Image;
}

public enum SpriteType
{
    Sprite,
    Background,
    Font,
    Unknown
}

public enum SplitType
{
    Horizontal,
    Vertical,
}

public enum BestFitHeuristic
{
    Area,
    MaxOneAxis,
}

public struct Rect
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

public class Node
{
    public Rect Bounds;
    public TextureInfo Texture;
    public SplitType SplitType;
}

public class Atlas
{
    public int Width;
    public int Height;
    public List<Node> Nodes;
}

public class Packer
{
    public List<TextureInfo> SourceTextures;
    public StringWriter Log;
    public StringWriter Error;
    public int Padding;
    public int AtlasSize;
    public bool DebugMode;
    public BestFitHeuristic FitHeuristic;
    public List<Atlas> Atlasses;

    public Packer()
    {
        SourceTextures = new List<TextureInfo>();
        Log = new StringWriter();
        Error = new StringWriter();
    }

    public void Process(string _SourceDir, string _Pattern, int _AtlasSize, int _Padding, bool _DebugMode)
    {
        Padding = _Padding;
        AtlasSize = _AtlasSize;
        DebugMode = _DebugMode;
        //1: scan for all the textures we need to pack
        ScanForTextures(_SourceDir, _Pattern);
        List<TextureInfo> textures = new List<TextureInfo>();
        textures = SourceTextures.ToList();
        //2: generate as many atlasses as needed (with the latest one as small as possible)
        Atlasses = new List<Atlas>();
        while (textures.Count > 0)
        {
            Atlas atlas = new Atlas();
            atlas.Width = _AtlasSize;
            atlas.Height = _AtlasSize;
            List<TextureInfo> leftovers = LayoutAtlas(textures, atlas);
            if (leftovers.Count == 0)
            {
                // we reached the last atlas. Check if this last atlas could have been twice smaller
                while (leftovers.Count == 0)
                {
                    atlas.Width /= 2;
                    atlas.Height /= 2;
                    leftovers = LayoutAtlas(textures, atlas);
                }
                // we need to go 1 step larger as we found the first size that is too small
                // if the atlas is 0x0 then it should be 1x1 instead
                if (atlas.Width == 0)
                {
                    atlas.Width = 1;
                }
                else
                {
                    atlas.Width *= 2;
                }
                if (atlas.Height == 0)
                {
                    atlas.Height = 1;
                }
                else
                {
                    atlas.Height *= 2;
                }
                leftovers = LayoutAtlas(textures, atlas);
            }
            Atlasses.Add(atlas);
            textures = leftovers;
        }
    }

    public void SaveAtlasses(string _Destination)
    {
        int atlasCount = 0;
        string prefix = _Destination.Replace(Path.GetExtension(_Destination), "");
        string descFile = _Destination;

        StreamWriter tw = new StreamWriter(_Destination);
        tw.WriteLine("source_tex, atlas_tex, x, y, width, height");
        foreach (Atlas atlas in Atlasses)
        {
            string atlasName = $"{prefix}{atlasCount:000}.png";

            // 1: Save images
            using (MagickImage img = CreateAtlasImage(atlas))
                TextureWorker.SaveImageToFile(img, atlasName);

            // 2: save description in file
            foreach (Node n in atlas.Nodes)
            {
                if (n.Texture != null)
                {
                    tw.Write(n.Texture.Source + ", ");
                    tw.Write(atlasName + ", ");
                    tw.Write((n.Bounds.X).ToString() + ", ");
                    tw.Write((n.Bounds.Y).ToString() + ", ");
                    tw.Write((n.Bounds.Width).ToString() + ", ");
                    tw.WriteLine((n.Bounds.Height).ToString());
                }
            }
            ++atlasCount;
        }
        tw.Close();
        tw = new StreamWriter(prefix + ".log");
        tw.WriteLine("--- LOG -------------------------------------------");
        tw.WriteLine(Log.ToString());
        tw.WriteLine("--- ERROR -----------------------------------------");
        tw.WriteLine(Error.ToString());
        tw.Close();
    }

    private void ScanForTextures(string _Path, string _Wildcard)
    {
        DirectoryInfo di = new(_Path);
        FileInfo[] files = di.GetFiles(_Wildcard, SearchOption.AllDirectories);
        foreach (FileInfo fi in files)
        {
            (int width, int height) = TextureWorker.GetImageSizeFromFile(fi.FullName);
            if (width == -1 || height == -1)
                continue;

            if (width <= AtlasSize && height <= AtlasSize)
            {
                TextureInfo ti = new();

                MagickReadSettings settings = new()
                {
                    ColorSpace = ColorSpace.sRGB,
                };
                MagickImage img = new(fi.FullName);
                //imagesToCleanup.Add(img);

                ti.Source = fi.FullName;
                ti.BoundingWidth = (int)img.Width;
                ti.BoundingHeight = (int)img.Height;

                // GameMaker doesn't trim tilesets. I assume it didn't trim backgrounds too
                ti.TargetX = 0;
                ti.TargetY = 0;
                if (GetSpriteType(ti.Source) != SpriteType.Background)
                {
                    img.BorderColor = MagickColors.Transparent;
                    img.BackgroundColor = MagickColors.Transparent;
                    img.Border(1);
                    IMagickGeometry? bbox = img.BoundingBox;
                    if (bbox is not null)
                    {
                        ti.TargetX = bbox.X - 1;
                        ti.TargetY = bbox.Y - 1;
                        // yes, .Trim() mutates the image...
                        // it doesn't really matter though since it isn't written back or anything
                        img.Trim();
                    }
                    else
                    {
                        // Empty sprites should be 1x1
                        ti.TargetX = 0;
                        ti.TargetY = 0;
                        img.Crop(1, 1);
                    }
                    img.ResetPage();
                }
                ti.Width = (int)img.Width;
                ti.Height = (int)img.Height;
                ti.Image = img;

                SourceTextures.Add(ti);

                Log.WriteLine($"Added {fi.FullName}");
            }
            else
            {
                Error.WriteLine($"{fi.FullName} is too large to fix in the atlas. Skipping!");
            }
        }
    }

    private void HorizontalSplit(Node _ToSplit, int _Width, int _Height, List<Node> _List)
    {
        Node n1 = new Node();
        n1.Bounds.X = _ToSplit.Bounds.X + _Width + Padding;
        n1.Bounds.Y = _ToSplit.Bounds.Y;
        n1.Bounds.Width = _ToSplit.Bounds.Width - _Width - Padding;
        n1.Bounds.Height = _Height;
        n1.SplitType = SplitType.Vertical;
        Node n2 = new Node();
        n2.Bounds.X = _ToSplit.Bounds.X;
        n2.Bounds.Y = _ToSplit.Bounds.Y + _Height + Padding;
        n2.Bounds.Width = _ToSplit.Bounds.Width;
        n2.Bounds.Height = _ToSplit.Bounds.Height - _Height - Padding;
        n2.SplitType = SplitType.Horizontal;
        if (n1.Bounds.Width > 0 && n1.Bounds.Height > 0)
            _List.Add(n1);
        if (n2.Bounds.Width > 0 && n2.Bounds.Height > 0)
            _List.Add(n2);
    }

    private void VerticalSplit(Node _ToSplit, int _Width, int _Height, List<Node> _List)
    {
        Node n1 = new Node();
        n1.Bounds.X = _ToSplit.Bounds.X + _Width + Padding;
        n1.Bounds.Y = _ToSplit.Bounds.Y;
        n1.Bounds.Width = _ToSplit.Bounds.Width - _Width - Padding;
        n1.Bounds.Height = _ToSplit.Bounds.Height;
        n1.SplitType = SplitType.Vertical;
        Node n2 = new Node();
        n2.Bounds.X = _ToSplit.Bounds.X;
        n2.Bounds.Y = _ToSplit.Bounds.Y + _Height + Padding;
        n2.Bounds.Width = _Width;
        n2.Bounds.Height = _ToSplit.Bounds.Height - _Height - Padding;
        n2.SplitType = SplitType.Horizontal;
        if (n1.Bounds.Width > 0 && n1.Bounds.Height > 0)
            _List.Add(n1);
        if (n2.Bounds.Width > 0 && n2.Bounds.Height > 0)
            _List.Add(n2);
    }

    private TextureInfo FindBestFitForNode(Node _Node, List<TextureInfo> _Textures)
    {
        TextureInfo bestFit = null;
        float nodeArea = _Node.Bounds.Width * _Node.Bounds.Height;
        float maxCriteria = 0.0f;
        foreach (TextureInfo ti in _Textures)
        {
            switch (FitHeuristic)
            {
                // Max of Width and Height ratios
                case BestFitHeuristic.MaxOneAxis:
                    if (ti.Width <= _Node.Bounds.Width && ti.Height <= _Node.Bounds.Height)
                    {
                        float wRatio = (float)ti.Width / (float)_Node.Bounds.Width;
                        float hRatio = (float)ti.Height / (float)_Node.Bounds.Height;
                        float ratio = wRatio > hRatio ? wRatio : hRatio;
                        if (ratio > maxCriteria)
                        {
                            maxCriteria = ratio;
                            bestFit = ti;
                        }
                    }
                    break;
                // Maximize Area coverage
                case BestFitHeuristic.Area:
                    if (ti.Width <= _Node.Bounds.Width && ti.Height <= _Node.Bounds.Height)
                    {
                        float textureArea = ti.Width * ti.Height;
                        float coverage = textureArea / nodeArea;
                        if (coverage > maxCriteria)
                        {
                            maxCriteria = coverage;
                            bestFit = ti;
                        }
                    }
                    break;
            }
        }
        return bestFit;
    }

    private List<TextureInfo> LayoutAtlas(List<TextureInfo> _Textures, Atlas _Atlas)
    {
        List<Node> freeList = new List<Node>();
        List<TextureInfo> textures = new List<TextureInfo>();
        _Atlas.Nodes = new List<Node>();
        textures = _Textures.ToList();
        Node root = new Node();
        root.Bounds.Width = _Atlas.Width;
        root.Bounds.Height = _Atlas.Height;
        root.SplitType = SplitType.Horizontal;
        freeList.Add(root);
        while (freeList.Count > 0 && textures.Count > 0)
        {
            Node node = freeList[0];
            freeList.RemoveAt(0);
            TextureInfo bestFit = FindBestFitForNode(node, textures);
            if (bestFit != null)
            {
                if (node.SplitType == SplitType.Horizontal)
                {
                    HorizontalSplit(node, bestFit.Width, bestFit.Height, freeList);
                }
                else
                {
                    VerticalSplit(node, bestFit.Width, bestFit.Height, freeList);
                }
                node.Texture = bestFit;
                node.Bounds.Width = bestFit.Width;
                node.Bounds.Height = bestFit.Height;
                textures.Remove(bestFit);
            }
            _Atlas.Nodes.Add(node);
        }
        return textures;
    }

    private MagickImage CreateAtlasImage(Atlas _Atlas)
    {
        MagickImage img = new(MagickColors.Transparent, (uint)_Atlas.Width, (uint)_Atlas.Height);

        foreach (Node n in _Atlas.Nodes)
        {
            if (n.Texture is not null)
            {
                MagickImage sourceImg = n.Texture.Image;
                using IMagickImage<byte> resizedSourceImg = TextureWorker.ResizeImage(sourceImg, n.Bounds.Width, n.Bounds.Height);
                img.Composite(resizedSourceImg, n.Bounds.X, n.Bounds.Y, CompositeOperator.Copy);
            }
        }

        return img;
    }
}

public static SpriteType GetSpriteType(string path)
{
    string folderPath = Path.GetDirectoryName(path);
    string folderName = new DirectoryInfo(folderPath).Name;
    string lowerName = folderName.ToLower();

    if (lowerName == "backgrounds" || lowerName == "background")
    {
        return SpriteType.Background;
    }
    else if (lowerName == "fonts" || lowerName == "font")
    {
        return SpriteType.Font;
    }
    else if (lowerName == "sprites" || lowerName == "sprite")
    {
        return SpriteType.Sprite;
    }
    return SpriteType.Unknown;
}

public class Rectangle
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public void Size(int width, int height)
    {
        this.Width = width;
        this.Height = height;
    }
}

public class TextureInfoPolice
{
    public string Source;
    public int Width;
    public int Height;
}

public class NodePolice
{
    public Rectangle Bounds = new Rectangle();
    public TextureInfoPolice Texture;
    public SplitType SplitType;
}

public class AtlasPolice
{
    public int Width;
    public int Height;
    public List<NodePolice> NodePolices;
}

public class PackerPolice
{
    public List<TextureInfoPolice> SourceTextures;
    public StringWriter Log;
    public StringWriter Error;
    public int Padding;
    public int AtlasPoliceSize;
    public bool DebugMode;
    public BestFitHeuristic FitHeuristic;
    public List<AtlasPolice> AtlasPoliceses;

    public PackerPolice()
    {
        SourceTextures = new List<TextureInfoPolice>();
        Log = new StringWriter();
        Error = new StringWriter();
    }

    public void ProcessPolice(string _SourceDir, string _Pattern, int _AtlasPoliceSize, int _Padding, bool _DebugMode)
    {
        Padding = _Padding;
        AtlasPoliceSize = _AtlasPoliceSize;
        DebugMode = _DebugMode;
        //1: scan for all the textures we need to pack
        ScanForTextures(_SourceDir, _Pattern);
        List<TextureInfoPolice> textures = new List<TextureInfoPolice>();
        textures = SourceTextures.ToList();
        //2: generate as many atlasses as needed (with the latest one as small as possible)
        AtlasPoliceses = new List<AtlasPolice>();
        while (textures.Count > 0)
        {
            AtlasPolice atlas = new AtlasPolice();
            atlas.Width = _AtlasPoliceSize;
            atlas.Height = _AtlasPoliceSize;
            List<TextureInfoPolice> leftovers = LayoutAtlasPolice(textures, atlas);
            if (leftovers.Count == 0)
            {
                // we reached the last atlas. Check if this last atlas could have been twice smaller
                while (leftovers.Count == 0)
                {
                    atlas.Width /= 2;
                    atlas.Height /= 2;
                    leftovers = LayoutAtlasPolice(textures, atlas);
                }
                // we need to go 1 step larger as we found the first size that is to small
                atlas.Width *= 2;
                atlas.Height *= 2;
                leftovers = LayoutAtlasPolice(textures, atlas);
            }
            AtlasPoliceses.Add(atlas);
            textures = leftovers;
        }
    }

    public void SaveAtlasPoliceses(string _Destination)
    {
        int atlasCount = 0;
        string prefix = _Destination.Replace(Path.GetExtension(_Destination), "");
        string descFile = _Destination;
        StreamWriter tw = new StreamWriter(_Destination);
        tw.WriteLine("source_tex, atlas_tex, x, y, width, height");
        foreach (AtlasPolice atlas in AtlasPoliceses)
        {
            string atlasName = $"{prefix}{atlasCount:000}.png";
            //1: Save images
            MagickImage img = CreateAtlasPoliceImage(atlas);
            //DPI fix start
            img.Density = new Density(96, 96);
            //DPI fix end
            img.Write(atlasName, MagickFormat.Png);
            //2: save description in file
            foreach (NodePolice n in atlas.NodePolices)
            {
                if (n.Texture != null)
                {
                    tw.Write(n.Texture.Source + ", ");
                    tw.Write(atlasName + ", ");
                    tw.Write((n.Bounds.X).ToString() + ", ");
                    tw.Write((n.Bounds.Y).ToString() + ", ");
                    tw.Write((n.Bounds.Width).ToString() + ", ");
                    tw.WriteLine((n.Bounds.Height).ToString());
                }
            }
            ++atlasCount;
        }
        tw.Close();
        tw = new StreamWriter(prefix + ".log");
        tw.WriteLine("--- LOG -------------------------------------------");
        tw.WriteLine(Log.ToString());
        tw.WriteLine("--- ERROR -----------------------------------------");
        tw.WriteLine(Error.ToString());
        tw.Close();
    }

    private void ScanForTextures(string _Path, string _Wildcard)
    {
        DirectoryInfo di = new DirectoryInfo(_Path);
        FileInfo[] files = di.GetFiles(_Wildcard, SearchOption.AllDirectories);
        foreach (FileInfo fi in files)
        {
            var img = new MagickImage(fi.FullName);
            if (img != null)
            {
                if (img.Width <= AtlasPoliceSize && img.Height <= AtlasPoliceSize)
                {
                    TextureInfoPolice ti = new TextureInfoPolice();

                    ti.Source = fi.FullName;
                    ti.Width = (int)img.Width;
                    ti.Height = (int)img.Height;

                    SourceTextures.Add(ti);

                    Log.WriteLine("Added " + fi.FullName);
                }
                else
                {
                    Error.WriteLine(fi.FullName + " is too large to fix in the atlas. Skipping!");
                }
            }
        }
    }

    private void HorizontalSplit(NodePolice _ToSplit, int _Width, int _Height, List<NodePolice> _List)
    {
        NodePolice n1 = new NodePolice();
        n1.Bounds.X = _ToSplit.Bounds.X + _Width + Padding;
        n1.Bounds.Y = _ToSplit.Bounds.Y;
        n1.Bounds.Width = _ToSplit.Bounds.Width - _Width - Padding;
        n1.Bounds.Height = _Height;
        n1.SplitType = SplitType.Vertical;
        NodePolice n2 = new NodePolice();
        n2.Bounds.X = _ToSplit.Bounds.X;
        n2.Bounds.Y = _ToSplit.Bounds.Y + _Height + Padding;
        n2.Bounds.Width = _ToSplit.Bounds.Width;
        n2.Bounds.Height = _ToSplit.Bounds.Height - _Height - Padding;
        n2.SplitType = SplitType.Horizontal;
        if (n1.Bounds.Width > 0 && n1.Bounds.Height > 0)
            _List.Add(n1);
        if (n2.Bounds.Width > 0 && n2.Bounds.Height > 0)
            _List.Add(n2);
    }

    private void VerticalSplit(NodePolice _ToSplit, int _Width, int _Height, List<NodePolice> _List)
    {
        NodePolice n1 = new NodePolice();
        n1.Bounds.X = _ToSplit.Bounds.X + _Width + Padding;
        n1.Bounds.Y = _ToSplit.Bounds.Y;
        n1.Bounds.Width = _ToSplit.Bounds.Width - _Width - Padding;
        n1.Bounds.Height = _ToSplit.Bounds.Height;
        n1.SplitType = SplitType.Vertical;
        NodePolice n2 = new NodePolice();
        n2.Bounds.X = _ToSplit.Bounds.X;
        n2.Bounds.Y = _ToSplit.Bounds.Y + _Height + Padding;
        n2.Bounds.Width = _Width;
        n2.Bounds.Height = _ToSplit.Bounds.Height - _Height - Padding;
        n2.SplitType = SplitType.Horizontal;
        if (n1.Bounds.Width > 0 && n1.Bounds.Height > 0)
            _List.Add(n1);
        if (n2.Bounds.Width > 0 && n2.Bounds.Height > 0)
            _List.Add(n2);
    }

    private TextureInfoPolice FindBestFitForNodePolice(NodePolice _NodePolice, List<TextureInfoPolice> _Textures)
    {
        TextureInfoPolice bestFit = null;
        float NodePoliceArea = _NodePolice.Bounds.Width * _NodePolice.Bounds.Height;
        float maxCriteria = 0.0f;
        foreach (TextureInfoPolice ti in _Textures)
        {
            switch (FitHeuristic)
            {
                // Max of Width and Height ratios
                case BestFitHeuristic.MaxOneAxis:
                    if (ti.Width <= _NodePolice.Bounds.Width && ti.Height <= _NodePolice.Bounds.Height)
                    {
                        float wRatio = (float)ti.Width / (float)_NodePolice.Bounds.Width;
                        float hRatio = (float)ti.Height / (float)_NodePolice.Bounds.Height;
                        float ratio = wRatio > hRatio ? wRatio : hRatio;
                        if (ratio > maxCriteria)
                        {
                            maxCriteria = ratio;
                            bestFit = ti;
                        }
                    }
                    break;
                // Maximize Area coverage
                case BestFitHeuristic.Area:
                    if (ti.Width <= _NodePolice.Bounds.Width && ti.Height <= _NodePolice.Bounds.Height)
                    {
                        float textureArea = ti.Width * ti.Height;
                        float coverage = textureArea / NodePoliceArea;
                        if (coverage > maxCriteria)
                        {
                            maxCriteria = coverage;
                            bestFit = ti;
                        }
                    }
                    break;
            }
        }
        return bestFit;
    }

    private List<TextureInfoPolice> LayoutAtlasPolice(List<TextureInfoPolice> _Textures, AtlasPolice _AtlasPolice)
    {
        List<NodePolice> freeList = new List<NodePolice>();
        List<TextureInfoPolice> textures = new List<TextureInfoPolice>();
        _AtlasPolice.NodePolices = new List<NodePolice>();
        textures = _Textures.ToList();
        NodePolice root = new NodePolice();
        root.Bounds.Size(_AtlasPolice.Width, _AtlasPolice.Height);
        root.SplitType = SplitType.Horizontal;
        freeList.Add(root);
        while (freeList.Count > 0 && textures.Count > 0)
        {
            NodePolice NodePolice = freeList[0];
            freeList.RemoveAt(0);
            TextureInfoPolice bestFit = FindBestFitForNodePolice(NodePolice, textures);
            if (bestFit != null)
            {
                if (NodePolice.SplitType == SplitType.Horizontal)
                {
                    HorizontalSplit(NodePolice, bestFit.Width, bestFit.Height, freeList);
                }
                else
                {
                    VerticalSplit(NodePolice, bestFit.Width, bestFit.Height, freeList);
                }
                NodePolice.Texture = bestFit;
                NodePolice.Bounds.Width = bestFit.Width;
                NodePolice.Bounds.Height = bestFit.Height;
                textures.Remove(bestFit);
            }
            _AtlasPolice.NodePolices.Add(NodePolice);
        }
        return textures;
    }

    private MagickImage CreateAtlasPoliceImage(AtlasPolice _AtlasPolice)
    {
        var atlas = new MagickImage(MagickColors.Transparent, (uint)_AtlasPolice.Width, (uint)_AtlasPolice.Height);

        foreach (NodePolice n in _AtlasPolice.NodePolices)
        {
            if (n.Texture != null)
            {
                using (var src = new MagickImage(n.Texture.Source))
                {
                    atlas.Composite(src, n.Bounds.X, n.Bounds.Y, CompositeOperator.Over);
                }

                if (DebugMode)
                {
                    // Desenhar retângulo e texto com Drawables
                    var drawables = new Drawables()
                    .FillColor(MagickColors.Black)
                    .Rectangle(n.Bounds.X, n.Bounds.Y,
                            n.Bounds.X + n.Bounds.Width, n.Bounds.Y + 15)
                    .FillColor(MagickColors.White)
                    .FontPointSize(12)
                    .Text(n.Bounds.X, n.Bounds.Y + 12, Path.GetFileNameWithoutExtension(n.Texture.Source));

                    drawables.Draw(atlas);
                }
            }
        else if (DebugMode)
        {
            var drawables = new Drawables()
                .FillColor(MagickColors.DarkMagenta)
                .Rectangle(n.Bounds.X, n.Bounds.Y,
                        n.Bounds.X + n.Bounds.Width, n.Bounds.Y + n.Bounds.Height);

            string label = $"{n.Bounds.Width}x{n.Bounds.Height}";
            drawables.FillColor(MagickColors.Black)
                .Rectangle(n.Bounds.X, n.Bounds.Y,
                        n.Bounds.X + 50, n.Bounds.Y + 15)
                .FillColor(MagickColors.White)
                .FontPointSize(12)
                .Text(n.Bounds.X, n.Bounds.Y + 12, label);

            drawables.Draw(atlas);
        }
        }

        return atlas;
    }
}


public static class ResourceImporter
{
	public static UndertaleData Data {get; set; }
	public static void importFontFolder(string importFolder)
	{
		if (importFolder == null)
			throw new ScriptException("Dossier d'import non sélectionné.");

		string packagerDirPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Packager");
		string sourcePath = importFolder;
		string searchPattern = "*.png";
		string outName = Path.Combine(packagerDirPath, "atlas.txt");
		int textureSize = 2048;
		int border = 2;
		bool debug = false;

		Directory.CreateDirectory(packagerDirPath);
		PackerPolice packer = new PackerPolice();
		packer.ProcessPolice(sourcePath, searchPattern, textureSize, border, debug);
		packer.SaveAtlasPoliceses(outName);

		int lastTextPage = Data.EmbeddedTextures.Count - 1;
		int lastTextPageItem = Data.TexturePageItems.Count - 1;

		static void fontUpdate(UndertaleFont newFont, string sourcePath, UndertaleData Data)
		{
			using (StreamReader reader = new StreamReader(Path.Combine(sourcePath, $"glyphs_{newFont.Name.Content}.csv")))
			{
				newFont.Glyphs.Clear();
				string line;
				int head = 0;
				bool hadError = false;
				while ((line = reader.ReadLine()) != null)
				{
					string[] s = line.Split(';');

					// Skip blank lines like ";;;;;;;"
					if (s.All(x => x.Length == 0))
						continue;

					try
					{
						if (head == 1)
						{
							newFont.RangeStart = UInt16.Parse(s[0]);
							head++;
						}

						if (head == 0)
						{
							String namae = s[0].Replace("\"", "");
							newFont.DisplayName = Data.Strings.MakeString(namae);
							newFont.EmSize = UInt16.Parse(s[1]);
							newFont.Bold = Boolean.Parse(s[2]);
							newFont.Italic = Boolean.Parse(s[3]);
							newFont.Charset = Byte.Parse(s[4]);
							newFont.AntiAliasing = Byte.Parse(s[5]);
							newFont.ScaleX = UInt16.Parse(s[6]);
							newFont.ScaleY = UInt16.Parse(s[7]);
							head++;
						}

						if (head > 1)
						{
							newFont.Glyphs.Add(new UndertaleFont.Glyph()
							{
								Character = UInt16.Parse(s[0]),
								SourceX = UInt16.Parse(s[1]),
								SourceY = UInt16.Parse(s[2]),
								SourceWidth = UInt16.Parse(s[3]),
								SourceHeight = UInt16.Parse(s[4]),
								Shift = Int16.Parse(s[5]),
								Offset = Int16.Parse(s[6]),
							});
							newFont.RangeEnd = UInt32.Parse(s[0]);
						}
					}
					catch
					{
						hadError = true;
					}
				}

				if (hadError)
				{
					hadError = false;
				}
			}
		}
		string prefix = outName.Replace(Path.GetExtension(outName), "");
		int atlasCount = 0;
		foreach (AtlasPolice atlas in packer.AtlasPoliceses)
		{
			string atlasName = $"{prefix}{atlasCount:000}.png";
			UndertaleEmbeddedTexture texture = new UndertaleEmbeddedTexture();
			texture.Name = new UndertaleString($"Texture {++lastTextPage}");
			texture.TextureData.Image = GMImage.FromPng(File.ReadAllBytes(atlasName)); // TODO: generate other formats
			Data.EmbeddedTextures.Add(texture);
			foreach (NodePolice n in atlas.NodePolices)
			{
				if (n.Texture != null)
				{
					UndertaleTexturePageItem texturePageItem = new UndertaleTexturePageItem();
					texturePageItem.Name = new UndertaleString($"PageItem {++lastTextPageItem}");
					texturePageItem.SourceX = (ushort)n.Bounds.X;
					texturePageItem.SourceY = (ushort)n.Bounds.Y;
					texturePageItem.SourceWidth = (ushort)n.Bounds.Width;
					texturePageItem.SourceHeight = (ushort)n.Bounds.Height;
					texturePageItem.TargetX = 0;
					texturePageItem.TargetY = 0;
					texturePageItem.TargetWidth = (ushort)n.Bounds.Width;
					texturePageItem.TargetHeight = (ushort)n.Bounds.Height;
					texturePageItem.BoundingWidth = (ushort)n.Bounds.Width;
					texturePageItem.BoundingHeight = (ushort)n.Bounds.Height;
					texturePageItem.TexturePage = texture;
					Data.TexturePageItems.Add(texturePageItem);
					string spriteName = Path.GetFileNameWithoutExtension(n.Texture.Source);

					UndertaleFont font = null;
					font = Data.Fonts.ByName(spriteName);

					if (font == null)
					{
						UndertaleString fontUTString = Data.Strings.MakeString(spriteName);
						UndertaleFont newFont = new UndertaleFont();
						newFont.Name = fontUTString;

						fontUpdate(newFont, sourcePath, Data);
						newFont.Texture = texturePageItem;
						Data.Fonts.Add(newFont);
						continue;
					}

					fontUpdate(font, sourcePath, Data);
					font.Texture = texturePageItem;
					UndertaleSprite.TextureEntry texentry = new UndertaleSprite.TextureEntry();
					texentry.Texture = texturePageItem;
				}
			}
			atlasCount++;
		}
	}
}

ResourceImporter.Data = Patcher.Data;
