// -----------------------------------------------------------------------
// <copyright file="FindMissingCompileItems.cs" company="Ace Olszowka">
//  Copyright (c) Ace Olszowka 2019-2020. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MsBuildFindMissingCompileItems
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    static class FindMissingCompileItems
    {
        internal static XNamespace msbuildNS = @"http://schemas.microsoft.com/developer/msbuild/2003";

        /// <summary>
        ///    Given a Directory, Scan for all supported Project Types,
        /// Returning a named Tuple that contains the ProjectName and an
        /// IEnumerable of missing Compile Reference Items. If there are
        /// no items in the IEnumerable then the project did not have any
        /// missing Compile Items.
        /// </summary>
        /// <param name="targetDirectory">The Directory to scan.</param>
        /// <returns>
        /// A named Tuple that contains the ProjectName and an IEnumerable
        /// of missing Compile Reference Items.
        /// </returns>
        internal static IEnumerable<(string ProjectName, IEnumerable<string> MissingCompileItems)> Execute(string targetDirectory)
        {
            IEnumerable<string> projectsToScan = GetProjectsInDirectory(targetDirectory);

            return projectsToScan.AsParallel().Select(projectToScan => GetMissingCompileItemsForProject(projectToScan));
        }

        /// <summary>
        ///    Scans a single project for missing compile items
        /// </summary>
        /// <param name="projectToScan">The project to scan for missing items.</param>
        /// <returns>
        ///    A Tuple where the first item is the name of the project and the
        /// second is an IEnumerable of missing projects. If this IEnumerable
        /// is empty then the project had no missing items.
        /// </returns>
        public static (string ProjectName, IEnumerable<string> MissingCompileItems) GetMissingCompileItemsForProject(string projectToScan)
        {
            ConcurrentBag<string> missingCompileReferenceItems = new ConcurrentBag<string>();

            try
            {
                IEnumerable<string> compileReferences = GetCompileFileReferences(projectToScan);

                Parallel.ForEach(compileReferences, compileReference =>
                {
                    if (!File.Exists(compileReference))
                    {
                        missingCompileReferenceItems.Add(compileReference);
                    }
                }
                );
            }
            catch (Exception ex)
            {
                missingCompileReferenceItems.Add($"Failed to Load Project: {ex.Message}");
            }

            return (ProjectName: projectToScan, MissingCompileItems: missingCompileReferenceItems);
        }

        /// <summary>
        ///    For a given MsBuild Project File, Return all Compile Items with
        /// their fully qualified Path.
        /// </summary>
        /// <param name="projectFile">The MsBuild Project File to read</param>
        /// <returns>An IEnumerable of the fully qualified paths to all Compile Items</returns>
        public static IEnumerable<string> GetCompileFileReferences(string projectFile)
        {
            XDocument projXml = XDocument.Load(projectFile);

            IEnumerable<string> compileIncludeRelativePaths =
                projXml
                .Descendants(msbuildNS + "Compile")
                .Select(compileReferenceNode => compileReferenceNode.Attribute("Include").Value);

            string targetProjectDirectory = Path.GetDirectoryName(projectFile);

            foreach (string compileIncludeRelativePath in compileIncludeRelativePaths)
            {
                string absolutePath = Path.Combine(targetProjectDirectory, compileIncludeRelativePath);
                string resolvedPath = Path.GetFullPath(new Uri(absolutePath).LocalPath);
                yield return resolvedPath;
            }
        }

        /// <summary>
        /// Gets all Project Files that are understood by this
        /// tool from the given directory and all subdirectories.
        /// </summary>
        /// <param name="targetDirectory">The directory to scan for projects.</param>
        /// <returns>All projects that this tool supports.</returns>
        static IEnumerable<string> GetProjectsInDirectory(string targetDirectory)
        {
            HashSet<string> supportedFileExtensions = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
            {
                ".csproj",
                ".fsproj",
                ".synproj",
                ".vbproj",
            };

            return
                Directory
                .EnumerateFiles(targetDirectory, "*proj", SearchOption.AllDirectories)
                .Where(currentFile => supportedFileExtensions.Contains(Path.GetExtension(currentFile)));
        }
    }
}
