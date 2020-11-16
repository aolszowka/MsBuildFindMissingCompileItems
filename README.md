# MsBuildFindMissingCompileItems
![CI - Master](https://github.com/aolszowka/MsBuildFindMissingCompileItems/workflows/CI/badge.svg?branch=master)

Utility to find project files that have missing Compile Items

## Background
In most MSBuild based Project Systems the  `<Compile>` item is used to "Represent the source files for the compiler". See [Visual Studio Docs: Common MSBuild project items](https://docs.microsoft.com/en-ie/visualstudio/msbuild/common-msbuild-project-items?view=vs-2019#compile)

By convention most MSBuild based Project Systems will use `<Compile>` as an input and the produced binary as an output when evaluating if an incremental build should be used. See [Visual Studio Docs: Incremental builds](https://docs.microsoft.com/en-ie/visualstudio/msbuild/incremental-builds?view=vs-2019) for a more detailed explanation of `Inputs` and `Outputs`

Unfortunately when Project files contain invalid `<Compile>` elements this usually forces a rebuild. This commonly happens when a developer is editing the Project file outside the purview of Visual Studio.

This tool allows you to quickly scan an entire directory for all MSBuild Project Types and validate that their `<Compile>` items exist.

## When To Use This Tool
If you suspect that rebuilds are happening even when no code has changed run this tool to see if an invalid `<Compile>` item is in the project.

## Usage
There are now two ways to run this tool:

1. (Compiled Executable) Invoke the tool via `MsBuildFindMissingCompileItems` and pass the arguments.
2. (Dotnet Tool) Install this tool using the following command `dotnet tool install MsBuildFindMissingCompileItems` (assuming that you have the nuget package in your feed) then invoke it via `dotnet project-findmissingcompileitems`

In both cases the flags to the tooling are identical:

```
Usage: C:\DirectoryWithProjects [-xml]

Scans given directory for MsBuild Projects, evaluating each project's Compile
Tags reporting any missing items.

Arguments:

               <>            The directory to scan for MSBuild Projects
      --xml                  Produce Output in XML Format
  -?, -h, --help             Show this message and exit
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
