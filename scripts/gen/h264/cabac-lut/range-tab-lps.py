class GenElement:
    def __init__(self, idx, data):
        self.idx = idx
        self.data = data

def process_line(line: str) -> GenElement:
    splitted = line.split(' ')
    if len(splitted) != 10:
        raise 'Line must have 10 elements'
    first_idx = int(splitted[0])
    first_pool = []
    for i in range(4):
        first_pool.append(splitted[1 + i])
    second_idx = int(splitted[5])
    second_pool = []
    for i in range(4):
        second_pool.append(splitted[6 + i])
    return [GenElement(first_idx, first_pool), GenElement(second_idx, second_pool)]

def process_document() -> None:
    with open("scripts/gen/h264/cabac-lut/range-tab-lps.txt", "r") as lps:
        with open("scripts/gen/h264/cabac-lut/range-tab-lps-output.txt", "a") as output:
            lines = lps.readlines()
            gen_elements = []
            for x in lines:
                for processed in process_line(x.strip()):
                    gen_elements.append(processed)
            sorted_gen_elements = sorted(gen_elements, key= lambda x: x.idx)
            for element in sorted_gen_elements:
                builder = "        /*"
                builder += str(element.idx)
                builder += "*/ "
                builder += f"{{ {element.idx}, new[] {{ {element.data[0]}, {element.data[1]}, {element.data[2]}, {element.data[3]} }} }},"
                output.write(builder + "\n")
                
print("Working...")
process_document()
print("SUCCESS!")
