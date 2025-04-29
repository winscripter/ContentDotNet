# Copyright (c) 2023-2025, winscripter

import subprocess

def check_dotnet_installed():
    result = subprocess.run("where dotnet", shell=True, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    return result.returncode == 0

def clean_solution(solution_file):
    try:
        result = subprocess.run(f"dotnet build {solution_file}", shell=True, stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
        return result.returncode == 0
    except Exception as e:
        print(f"Error occurred: {e}")
        return False

def main():
    print("Building...")
    if not check_dotnet_installed():
        print(".NET is not installed.")
        return

    solution_file = "ContentDotNet.sln"
    if clean_solution(solution_file):
        print("Success.")
    else:
        print("Failed.")

if __name__ == "__main__":
    main()
