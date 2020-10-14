# MsBuildFindMissingCompileItems
Utility to find project files that have missing Compile Items

## Background
In most MSBuild based Project Systems the  `<Compile>` item is used to "Represent the source files for the compiler". See [Visual Studio Docs: Common MSBuild project items](https://docs.microsoft.com/en-ie/visualstudio/msbuild/common-msbuild-project-items?view=vs-2019#compile)

By convention most MSBuild based Project Systems will use `<Compile>` as an input and the produced binary as an output when evaluating if an incremental build should be used. See [Visual Studio Docs: Incremental builds](https://docs.microsoft.com/en-ie/visualstudio/msbuild/incremental-builds?view=vs-2019) for a more detailed explanation of `Inputs` and `Outputs`

Unfortunately when Project files contain invalid `<Compile>` elements this usually forces a rebuild. This commonly happens when a developer is editing the Project file outside the purview of Visual Studio.

This tool allows you to quickly scan an entire directory for all MSBuild Project Types and validate that their `<Compile>` items exist.

## When To Use This Tool
If you suspect that rebuilds are happening even when no code has changed run this tool to see if an invalid `<Compile>` item is in the project.

## Usage
```
Usage: MsBuildFindMissingCompileItems.exe directory

Scans given directory for MsBuild Projects, evaluating each project's Compile Tags reporting any missing items.
Invalid Command/Arguments. Valid commands are:

[directory]    - [READS] Spins through the specified directory and all
                 subdirectories for Project files; prints any projects
                 which have Compile items that are missing along with
                 the file paths that were invalid.
```

## Hacking
The most likely change you will want to make is changing the supported project files. Because this tooling is operating on file names any extension is supported.

See `FindMissingCompileItems.GetProjectsInDirectory(string)` for the place to modify this.

## Contributing
Pull requests and bug reports are welcomed so long as they are MIT Licensed.

## License
This tool is MIT Licensed.

## Third Party Licenses
This project uses other open source contributions see [LICENSES.md](LICENSES.md) for a comprehensive listing.
