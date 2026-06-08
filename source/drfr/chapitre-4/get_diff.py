with open("_strings_fr.txt") as f:
    fr = [l[:-1] for l in f.readlines()]

with open("_strings_en.txt") as f:
    en = [l[:-1] for l in f.readlines()]

new_fr = []
new_en = []
for i in range(len(fr)):
    if (fr[i] != en[i]):
        new_fr.append(fr[i])
        new_en.append(en[i])

with open("strings_fr.txt", "w") as f:
    f.write("\n".join(new_fr))

with open("strings_en.txt", "w") as f:
    f.write("\n".join(new_en))
