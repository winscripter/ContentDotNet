# Building instructions
To build ContentDotNet, you can do so either via Visual Studio, the build-all.[py/bat]
script, or via the .NET CLI.

Before proceeding, you **must** have the .NET 8 SDK installed. You can download it [here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) if you
don't have it installed.

Then, clone the repository:
1. If you have Git installed, type this in the Terminal:
```
git clone https://github.com/winscripter/ContentDotNet
```
2. If you don't have Git installed, visit `https://github.com/winscripter/ContentDotNet`. Under the green `Code` button, click on it, and in the dropdown, click `Download ZIP`, then extract it.

## 1. Via Visual Studio
Make sure you have Visual Studio 2022 17.9 (or later) installed. To see what version of
Visual Studio you have:

1. Open Visual Studio 2022
2. Click 'Continue without Code' in the 'Open recent' window. It's under the 'Create a new project' button
3. Go to Help -&gt; About Microsoft Visual Studio
4. Make sure it says Version 17.9 or newer (i.e. 17.10, 17.11, 17.10.1, etc)

If your Visual Studio 2022 is too old (below Version 17.9, such as 17.8 or 17.7), you'll
need to update it. To do this, make sure you have a plenty of time and a good network connection
because it typically takes a long time for Visual Studio to update (much like how long it takes
for it to install). To update it, click Help -&gt; Check for Updates.

Assuming that all prerequisites are followed:
1. Open Visual Studio
2. Click `Continue without Code` under `Create a new Project`
3. Drag and drop the ContentDotNet.sln file in the cloned repository onto Visual Studio, and give it some time to load up
4. Once everything loads up, click `Build` -&gt; `Build Solution`. Then, just give it about 30 seconds if not a minute, and it should be good to go!

## 2. Via command line
In the cloned repository, open the terminal (or cmd.exe on Windows).

If you have Python installed OR you aren't on Windows, type `python build-all.py`.
Otherwise, if you don't have Python installed and you're on Windows, type `build-all.bat`.

## 3. Via .NET CLI
In the cloned repository, open the terminal (or cmd.exe on Windows).
Then, just type `dotnet build`. That's it!
