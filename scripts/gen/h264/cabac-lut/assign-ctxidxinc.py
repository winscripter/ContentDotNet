# Copyright (c) 2023-2025, winscripter
# Created: 5/21/2025, last edited: 5/21/2025
#
# Note: You should run this python script with the
# working directory being the root of the repository.
#

class GenElement:
    """
      Represents a single line to be generated.
    """
    def __init__(self, index, data):
        """
          Performs initialization
        """
        self.index = index
        self.data = data

def parse_gen_elements(e: str) -> list[GenElement]:
    splitted = e.split(' ')
    ge_index_first = int(splitted[0])
    ge_pool_first = []
    for x in range(3):
        ge_pool_first.append(splitted[1 + x])
    ge_index_second = int(splitted[4])
    ge_pool_second = []
    for x in range(3):
        ge_pool_second.append(splitted[5 + x])
    return [GenElement(ge_index_first, ge_pool_first), GenElement(ge_index_second, ge_pool_second)]

def process_document() -> None:
    print("Working...")
    with open("scripts/gen/h264/cabac-lut/assign-ctxidxinc.txt", "r", encoding="utf-8") as input:
        with open("scripts/gen/h264/cabac-lut/assign-ctxidxinc-output.txt", "a") as output:
            lines = input.readlines()
            gen_elems = []
            for x in lines:
                clean_line = x.strip().replace("\u2212", "-").replace("−", "-")
                this_line_processed = parse_gen_elements(clean_line)
                gen_elems.extend(this_line_processed)
            sorted_gen_elements = sorted(gen_elems, key= lambda x: x.index)
            for e in sorted_gen_elements:
                output.write(f"    case {e.index}:\n")
                output.write(f"        ctxIdxInc = (mode == 1) ?(isFrame ? {e.data[0]} : {e.data[1]}) : {e.data[2]};\n")
                output.write(f"        break;\n")
                output.write("\n")
        
    print("SUCCESS!")

process_document()
