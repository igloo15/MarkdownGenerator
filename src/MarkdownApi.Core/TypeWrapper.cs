using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace igloo15.MarkdownApi.Core
{
    /// <summary>
    /// A wrapper around a type 
    /// </summary>
    public class TypeWrapper
    {
        /// <summary>
        /// Provide the base MemberInfo
        /// </summary>
        public MemberInfo Info { get; set; }

        /// <summary>
        /// Provides the full name of the wrapped type
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// The short name of the wrapped type
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Constructs a TypeWrapper with a PropertyInfo
        /// </summary>
        /// <param name="info">The property info</param>
        public TypeWrapper(PropertyInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        /// <summary>
        /// Constructs a TypeWrapper with FieldInfo
        /// </summary>
        /// <param name="info">The field info</param>
        public TypeWrapper(FieldInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        /// <summary>
        /// Constructs a TypeWrapper with a MethodInfo
        /// </summary>
        /// <param name="info">The method info</param>
        public TypeWrapper(MethodInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        /// <summary>
        /// Constructs a TypeWrapper with an Event Info
        /// </summary>
        /// <param name="info">The event info</param>
        public TypeWrapper(EventInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        /// <summary>
        /// Constructs a Typewrapper with a ConstructorInfo
        /// </summary>
        /// <param name="info">The constructor info</param>
        public TypeWrapper(ConstructorInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        /// <summary>
        /// Constructs a TypeWrapper with a basic type
        /// </summary>
        /// <param name="info">The basic type</param>
        public TypeWrapper(Type info)
        {
            Info = info;

            if (info.FullName == null)
                FullName = "";
            else
                FullName = info.FullName;

            Name = info.Name;
        }

        /// <summary>
        /// Constructs a type wrapper with just a string name
        /// </summary>
        /// <param name="name">The string name</param>
        public TypeWrapper(string name)
        {
            Info = null;
            FullName = name;
            Name = name;
        }

        /// <summary>
        /// Returns the id of the type wrapper based on the MemberInfo or the Name
        /// </summary>
        /// <returns>The id of this typeinfo</returns>
        public string GetId()
        {
            if (Info == null)
                return Name;
            return $"{Info.Module.ModuleVersionId}-{Info.MetadataToken}";
        }
    }
}
