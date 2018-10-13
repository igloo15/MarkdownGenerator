using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownApi.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        public MemberInfo Info { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public TypeWrapper(PropertyInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public TypeWrapper(FieldInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public TypeWrapper(MethodInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public TypeWrapper(EventInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public TypeWrapper(ConstructorInfo info)
        {
            Info = info;
            FullName = info.Name;
            Name = info.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
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
        /// 
        /// </summary>
        /// <param name="name"></param>
        public TypeWrapper(string name)
        {
            Info = null;
            FullName = name;
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetId()
        {
            if (Info == null)
                return Name;
            return $"{Info.Module.ModuleVersionId}-{Info.MetadataToken}";
        }
    }
}
