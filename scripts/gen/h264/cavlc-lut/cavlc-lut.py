# Copyright (c) 2023-2025, winscripter
# Created: 4/27/2025, last edited: 4/27/2025
#
# Note: You should run this python script with the
# working directory being the root of the repository.
#

def lut_bitsizes(s: list[list[str]]) -> str:
    bld = ""
    for x in s:
        xn = []
        for e in x:
            if str(len(e.strip())) == "0":
                continue

            xn.append(str(len(e.strip())))
            
        bld += ',\t'.join(xn) + ', '
        bld += '\n'
    return bld

def lut_gen(s: list[list[str]]) -> str:
    bld = ""
    for x in s:
        for e in range(len(x)):
            if e == 0 or e == 1:
                bld += x[e]
                bld += ',\t'
            else:
                bld += '0b'
                bld += x[e]
                bld += ',\t'
        bld += '\r\n'
    return bld

def lut_open() -> list[list[str]]:
    res = []
    with open("scripts/gen/h264/cavlc-lut/cavlc-lut.txt", "r") as file:
        txt = file.readlines()
        for e in txt:
            curr = []
            if e.strip() == "":
                continue

            for x in e.strip().split(' '):
                curr.append(x.strip())
            res.append(curr)
    return res

lut = lut_open()
ask = input("Enter 1 to generate LUT; 2 to generate sizes: ")
if ask != "1" and ask != "2":
    print("Bad input.")
    exit()

if ask == "1":
    print(lut_gen(lut))
else:
    print(lut_bitsizes(lut))

