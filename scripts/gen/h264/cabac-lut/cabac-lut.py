# Copyright (c) 2023-2025, winscripter
# Created: 5/4/2025, last edited: 5/4/2025
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
    if len(splitted) == 9:
        ge_index = int(splitted[0])
        ge_pool = []
        for i in range(8):
            ge_pool.append(splitted[1 + i])
        return [GenElement(ge_index, ge_pool)]
    elif len(splitted) > 9:
        ge_index_first = int(splitted[0])
        ge_pool_first = []
        for i in range(8):
            ge_pool_first.append(splitted[1 + i])
        ge_index_second = int(splitted[9])
        ge_pool_second = []
        for i in range(8):
            idx = 10 + i
            if idx >= len(splitted):
                ge_pool_second.append(0)
            else:
                ge_pool_second.append(splitted[idx])
        return [GenElement(ge_index_first, ge_pool_first), GenElement(ge_index_second, ge_pool_second)]

def process_document() -> None:
    print("Working...")
    with open("scripts/gen/h264/cabac-lut/cabac-lut.txt", "r", encoding="utf-8") as input:
        with open("scripts/gen/h264/cabac-lut/cabac-output.txt", "a") as output:
            lines = input.readlines()
            gen_elems = []
            for x in lines:
                clean_line = x.strip().replace("\u2212", "-").replace("−", "-")
                this_line_processed = parse_gen_elements(clean_line)
                gen_elems.extend(this_line_processed)
            sorted_gen_elements = sorted(gen_elems, key= lambda x: x.index)
            for sorted_gen_elem in sorted_gen_elements:
                builder = "        /*"
                builder += str(sorted_gen_elem.index)
                builder += "*/ "
                for pool_data in sorted_gen_elem.data:
                    builder += str(pool_data) + ", "
                output.write(builder + "\n")
        
    print("SUCCESS!")

process_document()
