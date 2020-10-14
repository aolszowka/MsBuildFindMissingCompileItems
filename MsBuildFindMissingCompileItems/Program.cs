// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Ace Olszowka">
//  Copyright (c) Ace Olszowka 2019-2020. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MsBuildFindMissingCompileItems
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MsBuildFindMissingCompileItems.Properties;

    using NDesk.Options;

    /// <summary>
    ///    Toy program to scan a given directory for MSBuild Project Types and
    /// evaluate their Compile tags for missing items.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            string targetDirectory = string.Empty;
            bool showHelp = false;

            OptionSet p = new OptionSet()
            {
                { "<>", Strings.TargetDirectoryArgument, v => targetDirectory = v },
                { "?|h|help", Strings.HelpDescription, v => showHelp = v != null },
            };

            try
            {
                p.Parse(args);
            }
            catch (OptionException)
            {
                Console.WriteLine(Strings.ShortUsageMessage);
                Console.WriteLine($"Try `{Strings.ProgramName} --help` for more information.");
                Environment.Exit(21);
            }

            if (showHelp || string.IsNullOrEmpty(targetDirectory))
            {
                int exitCode = ShowUsage(p);
                Environment.Exit(exitCode);
            }
            else
            {
                if (Directory.Exists(targetDirectory))
                {
                    Environment.ExitCode = PrintToConsole(targetDirectory);
                }
                else
                {
                    string error = string.Format(Strings.InvalidDirectoryArgument, targetDirectory);
                    Console.WriteLine(error);
                    Environment.ExitCode = 9009;
                }
            }
        }

        private static int ShowUsage(OptionSet p)
        {
            Console.WriteLine(Strings.ShortUsageMessage);
            Console.WriteLine();
            Console.WriteLine(Strings.LongDescription);
            Console.WriteLine();
            Console.WriteLine($"               <>            {Strings.TargetDirectoryArgument}");
            p.WriteOptionDescriptions(Console.Out);
            return 21;
        }

        /// <summary>
        /// Prints of the Results of FindMissingCompileItems in Plain Text
        /// </summary>
        /// <returns>The number of projects with missing files</returns>
        static int PrintToConsole(string targetDirectory)
        {
            int projectsWithMissingCompileItems = 0;

            IEnumerable<(string ProjectName, IEnumerable<string> MissingCompileItems)> results = FindMissingCompileItems.Execute(targetDirectory);

            foreach ((string ProjectName, IEnumerable<string> MissingCompileItems) result in results)
            {
                if (result.MissingCompileItems.Any())
                {
                    projectsWithMissingCompileItems++;
                    Console.WriteLine($"~~{result.ProjectName}~~");
                    foreach (string missingItem in result.MissingCompileItems)
                    {
                        Console.WriteLine(missingItem);
                    }
                }
            }

            return projectsWithMissingCompileItems;
        }
    }
}
