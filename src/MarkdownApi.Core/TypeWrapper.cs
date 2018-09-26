using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownApi.Core
{
    public class TypeWrapper
    {
        public MemberInfo Info { get; set; }

        public string FullName { get; private set; }

        public string Name { get; private set; }

        public TypeWrapper(PropertyInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        public TypeWrapper(FieldInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        public TypeWrapper(MethodInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        public TypeWrapper(EventInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        public TypeWrapper(ConstructorInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        public TypeWrapper(Type info)
        {
            Info = info;

            if (info.FullName == null)
                FullName = "";
            else
                FullName = info.FullName;

            Name = info.Name;
        }

        public TypeWrapper(string name)
        {
            Info = null;
            FullName = name;
            Name = name;
        }

        public string GetId()
        {
            if (Info == null)
                return Name;
            return $"{Info.Module.ModuleVersionId}-{Info.MetadataToken}";
        }
    }
}
