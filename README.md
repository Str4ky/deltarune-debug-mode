# <img src="https://github.com/user-attachments/assets/d4adee43-9ac0-4a28-a464-32bad31baebb" width="32" align="absmiddle" alt="icon" /> Deltarune Custom Debug Mode

Custom debug mode scripts to use with Undertale Mod Tool for Deltarune.\
Only works with paid version of the game and the LTS demo from the Steam beta.

Initially created for the french fan translation team [Deltarune FR](https://deltarune-fr.com).

#### (‼️Ce readme est également disponible en français‼️)

[![English](https://img.shields.io/badge/Read_in-English-blue)](README.md)
[![Français](https://img.shields.io/badge/Voir_en-Français-red)](README.fr-FR.md)

<img width="768" height="432" alt="image" src="https://github.com/user-attachments/assets/eca4d41e-cba4-42eb-8cbf-86e623d0b4e4" />

---

## 📖 Installation

* Download the `scripts.zip` file from the [releases](https://github.com/Str4ky/deltarune-debug-mode/releases) menu.
* Extract the zip file.

### Installation with UTMT GUI

* In the `Scripts` tab  from Undertale Mod Tool, press on  `Run other scripts...` .
* Select the right `debug_mode_chap[1-5].csx` file based on the currently opened chapter in Undertale Mod Tool.
* Save the modified data.win file once as appeared the popup saying that the debug mode has been successfully installed.
* Repeat for each chapters.

### Installation with UTMT CLI

* With a terminal, execute this command:\
`<UTMT CLI path> load <entry data.win path> <.csx script path> -o <output data.win path>`.

Ex. for chapter 2 : `"...\UndertaleModCli.exe" load ".../DELTARUNE/chapter2_windows/data.win" ".../debug_mode_chap2.csx" -o ".../DELTARUNE/chapter2_windows/data.win"`

* Repeat for each chapters.

---

## 🛠️ Main shortcuts and debug mode features

#### General
| Key(s) | Description |
| :--- | :--- |
| <kbd>F10</kbd> | Toggle debug mode |
| <kbd>D</kbd> | Open debug menu |
| <kbd>S</kbd> | Save game |
| <kbd>L</kbd> | Load last save |
| <kbd>R</kbd> | Reload current room |
| <kbd>Backspace</kbd> + <kbd>R</kbd> | Restart game |
| <kbd>O</kbd> | Toggle between 30, 60 and 120 FPS |
| <kbd>`</kbd> | Set game to 150 FPS |
| <kbd>P</kbd> | Set game to 1 FPS |

#### Navigation
| Key(s) | Description |
| :--- | :--- |
| <kbd>Insert</kbd> | Go to next room |
| <kbd>Suppr</kbd> | Go to previous room |
| <kbd>Middle click</kbd> | Open room editor |
| <kbd>Backspace</kbd> | Skip intro sequence (Only Chapter 1) |

#### Battles
| Key(s) | Description |
| :--- | :--- |
| <kbd>W</kbd> | Skip battle |
| <kbd>Shift</kbd> + <kbd>W</kbd> | Skip battle with recruit |
| <kbd>V</kbd> | Skip enemy's turn |
| <kbd>Alt</kbd> + <kbd>P</kbd> | Disable battle dialog autoskip |

#### Cheat
| Key(s) | Description |
| :--- | :--- |
| <kbd>G</kbd> | Toggle *Godmode* |
| <kbd>H</kbd> | Fully restore party PV |
| <kbd>T</kbd> | Fill and empty TP bar |
| <kbd>M</kbd> + <kbd>1</kbd> | Add 100 D$ to inventory |
| <kbd>M</kbd> + <kbd>2</kbd> | Remove 100 $ to inventory |

## More functions

Some debug functions already implemented as been left in each chapters.

For those, controls are still the one they have been assigned to and have been untouched.

## Contribution

If you have any suggestions or encounter bugs, feel free to let us know!
