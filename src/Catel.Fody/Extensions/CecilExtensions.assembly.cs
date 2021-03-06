﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CecilExtensions.assembly.cs" company="Catel development team">
//   Copyright (c) 2008 - 2017 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Catel.Fody
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Mono.Cecil;

    public static partial class CecilExtensions
    {
        public static Version GetVersion(this AssemblyDefinition assemblyDefinition)
        {
            return assemblyDefinition.Name.Version;

            //var stringVersion = "0.0.0.0";

            //var assemblyVersionAttributeName = typeof(AssemblyVersionAttribute).FullName;
            //var assemblyFileVersionAttributeName = typeof(AssemblyFileVersionAttribute).FullName;

            //var attribute = assemblyDefinition.CustomAttributes.FirstOrDefault(x => x.AttributeType.FullName == assemblyVersionAttributeName);
            //if (attribute == null)
            //{
            //    attribute = assemblyDefinition.CustomAttributes.FirstOrDefault(x => x.AttributeType.FullName == assemblyFileVersionAttributeName);
            //}

            //if (attribute != null)
            //{
            //    stringVersion = (string)attribute.ConstructorArguments.First().Value;
            //}

            //var version = new Version(stringVersion);
            //return version;
        }

        public static bool IsNetStandardLibrary(this AssemblyDefinition assemblyDefinition)
        {
            return assemblyDefinition.MainModule.FileName.IndexOf("netstandard", 0, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static AssemblyDefinition Resolve(this IAssemblyResolver assemblyResolver, string name)
        {
            return assemblyResolver.Resolve(new AssemblyNameReference(name, null));
        }
    }
}