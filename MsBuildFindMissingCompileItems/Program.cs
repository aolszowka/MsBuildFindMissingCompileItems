// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Ace Olszowka">
//  Copyright (c) Ace Olszowka 2019. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MsBuildFindMissingCompileItems
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            int errorCode = 0;

            if (args.Any())
            {
                string command = args.First().ToLowerInvariant();

                if (command.Equals("-?") || command.Equals("/?") || command.Equals("-help") || command.Equals("/help"))
                {
                    errorCode = ShowUsage();
                }
                else
                {
                    if (Directory.Exists(command))
                    {
                        string targetPath = command;
                        errorCode = PrintToConsole(command);
                    }
                    else
                    {
                        string error = string.Format("The specified path `{0}` is not valid.", command);
                        Console.WriteLine(error);
                        errorCode = 1;
                    }
                }
            }
            else
            {
                // This was a bad command
                errorCode = ShowUsage();
            }

            Environment.Exit(errorCode);
        }

        private static int ShowUsage()
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine("Scans given directory for MsBuild Projects, evaluating each project's Compile Tags reporting any missing items.");
            message.AppendLine("Invalid Command/Arguments. Valid commands are:");
            message.AppendLine();
            message.AppendLine("[directory]    - [READS] Spins through the specified directory and all\n" +
                               "                 subdirectories for Project files; prints any projects\n" +
                               "                 which have Compile items that are missing along with\n" +
                               "                 the file paths that were invalid.");
            Console.WriteLine(message);
            return 21;
        }

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
