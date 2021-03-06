﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MsCoreReferenceFinder.cs" company="Catel development team">
//   Copyright (c) 2008 - 2013 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Catel.Fody
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil;

    public class MsCoreReferenceFinder
    {
        public const string CompilerGeneratedAttributeTypeName = "System.Runtime.CompilerServices.CompilerGeneratedAttribute";
        public const string GeneratedCodeAttributeTypeName = "System.CodeDom.Compiler.GeneratedCodeAttribute";
        public const string DebuggerNonUserCodeAttributeTypeName = "System.Diagnostics.DebuggerNonUserCodeAttribute";

        private readonly ModuleWeaver _moduleWeaver;
        private readonly IAssemblyResolver _assemblyResolver;

        private readonly IDictionary<string, TypeReference> _typeReferences = new Dictionary<string, TypeReference>();

        // Types
        public TypeReference XmlQualifiedName;
        public TypeReference XmlSchemaSet;

        public TypeReference GeneratedCodeAttribute;
        public TypeReference CompilerGeneratedAttribute;
        public TypeReference DebuggerNonUserCodeAttribute;

        public MsCoreReferenceFinder(ModuleWeaver moduleWeaver, IAssemblyResolver assemblyResolver)
        {
            _moduleWeaver = moduleWeaver;
            _assemblyResolver = assemblyResolver;
        }

        public void Execute()
        {
            var xmlDefinition = _assemblyResolver.Resolve("System.Xml");
            if (xmlDefinition != null)
            {
                var xmlTypes = xmlDefinition.MainModule.Types;

                XmlQualifiedName = (from t in xmlTypes
                                    where string.Equals(t.FullName, "System.Xml.XmlQualifiedName")
                                    select t).FirstOrDefault();

                XmlSchemaSet = (from t in xmlTypes
                                where string.Equals(t.FullName, "System.Xml.Schema.XmlSchemaSet")
                                select t).FirstOrDefault();
            }
            else
            {
                FodyEnvironment.LogInfo("System.Xml not referenced, disabling xml-related features");
            }

            GeneratedCodeAttribute = GetCoreTypeReference(GeneratedCodeAttributeTypeName);
            CompilerGeneratedAttribute = GetCoreTypeReference(CompilerGeneratedAttributeTypeName);
            DebuggerNonUserCodeAttribute = GetCoreTypeReference(DebuggerNonUserCodeAttributeTypeName);
        }

        public TypeReference GetCoreTypeReference(string typeName)
        {
            if (!_typeReferences.ContainsKey(typeName))
            {
                var types = GetTypes();
                _typeReferences[typeName] = types.FirstOrDefault(x => string.Equals(x.Name, typeName) || string.Equals(x.FullName, typeName));
            }

            return _typeReferences[typeName];
        }

        private IEnumerable<TypeReference> GetTypes()
        {
            var msCoreLibDefinition = _assemblyResolver.Resolve("mscorlib");
            var msCoreTypes = msCoreLibDefinition.MainModule.Types.Cast<TypeReference>().ToList();

            var objectDefinition = msCoreTypes.FirstOrDefault(x => string.Equals(x.Name, "Object"));
            if (objectDefinition == null)
            {
                if (msCoreLibDefinition.IsNetStandardLibrary())
                {
                    msCoreTypes.AddRange(GetNetStandardTypes());
                }
                else
                {
                    msCoreTypes.AddRange(GetWinRtTypes());
                }
            }
            else
            {
                msCoreTypes.AddRange(GetDotNetTypes());
            }

            return msCoreTypes.OrderBy(x => x.FullName);
        }

        private IEnumerable<TypeReference> GetDotNetTypes()
        {
            var allTypes = new List<TypeReference>();

            allTypes.AddRange(GetTypesFromAssembly("System"));

            return allTypes;
        }

        private IEnumerable<TypeReference> GetWinRtTypes()
        {
            var allTypes = new List<TypeReference>();

            allTypes.AddRange(GetTypesFromAssembly("System.Runtime"));
            allTypes.AddRange(GetTypesFromAssembly("System.Diagnostics.Debug"));
            allTypes.AddRange(GetTypesFromAssembly("System.Diagnostics.Tools"));

            return allTypes;
        }

        private IEnumerable<TypeReference> GetNetStandardTypes()
        {
            // Load all assemblies, it's slower but then we are sure we have all types
            var allTypes = new List<TypeReference>();

            foreach (var assembly in _moduleWeaver.ModuleDefinition.AssemblyReferences)
            {
                var resolvedAssembly = _assemblyResolver.Resolve(assembly);
                if ((resolvedAssembly != null) && resolvedAssembly.IsNetStandardLibrary())
                {
                    allTypes.AddRange(resolvedAssembly.MainModule.Types);
                }
            }

            return allTypes;
        }

        private IEnumerable<TypeReference> GetTypesFromAssembly(string assemblyName)
        {
            var assembly = _assemblyResolver.Resolve(assemblyName);
            if (assembly is null)
            {
                return Array.Empty<TypeReference>();
            }

            var systemTypes = assembly.MainModule.Types;
            return systemTypes;
        }
    }
}
