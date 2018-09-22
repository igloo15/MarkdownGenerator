using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableMethod : IMarkdownable
    {
        public string FolderPath { get; private set; }

        public string FilePath { get; private set; }

        public MethodInfo InternalMethod { get; private set; }

        public bool IsStatic { get; private set; }

        public bool IsExtension => InternalMethod.GetCustomAttributes<System.Runtime.CompilerServices.ExtensionAttribute>(false).Any();

        public string Name => InternalMethod.Name;

        public Options Config { get; private set; }

        public IEnumerable<XmlDocumentComment> Comments { get; }

        public string Summary { get; private set; }

        public MemberInfo Info => InternalMethod;

        public MarkdownableMethod(MethodInfo info, bool isStatic, IEnumerable<XmlDocumentComment> comments)
        {
            InternalMethod = info;
            IsStatic = isStatic;
            Comments = comments;

            Summary = Comments.FirstOrDefault(a => (a.MemberName == InternalMethod.Name || a.MemberName.StartsWith(InternalMethod.Name + "`"))
                && info.GetParameters().All(b => a.Parameters.ContainsKey(b.Name))
            )?.Summary ?? "";
        }
        
        public void Build(string destination, Options config)
        {
            Config = config;
            FolderPath = destination;
            FilePath = InternalMethod.GetFilePath(destination);

            if(Config.MethodPages)
            {
                if (!Directory.Exists(destination))
                    Directory.CreateDirectory(destination);

                string content = string.Empty;
                if (IsStatic)
                    content = Config.CurrentTheme.StaticMethodPart.GetPage(this);
                else
                    content = Config.CurrentTheme.MethodPart.GetPage(this);

                File.WriteAllText(FilePath, content);
            }
            
        }
    }
}
