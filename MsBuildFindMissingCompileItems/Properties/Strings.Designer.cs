﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MsBuildFindMissingCompileItems.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MsBuildFindMissingCompileItems.Properties.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Show this message and exit.
        /// </summary>
        internal static string HelpDescription {
            get {
                return ResourceManager.GetString("HelpDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The provided directory `{0}` is invalid..
        /// </summary>
        internal static string InvalidDirectoryArgument {
            get {
                return ResourceManager.GetString("InvalidDirectoryArgument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Scans given directory for MsBuild Projects, evaluating each project&apos;s Compile
        ///Tags reporting any missing items.
        ///
        ///Arguments:.
        /// </summary>
        internal static string LongDescription {
            get {
                return ResourceManager.GetString("LongDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage: C:\DirectoryWithProjects [-xml].
        /// </summary>
        internal static string ShortUsageMessage {
            get {
                return ResourceManager.GetString("ShortUsageMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The directory to scan for MSBuild Projects.
        /// </summary>
        internal static string TargetDirectoryArgument {
            get {
                return ResourceManager.GetString("TargetDirectoryArgument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Produce Output in XML Format.
        /// </summary>
        internal static string XmlOutputFlag {
            get {
                return ResourceManager.GetString("XmlOutputFlag", resourceCulture);
            }
        }
    }
}
