using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableType : IMarkdownable
    {
        public Type InternalType { get; private set; }

        public string FolderPath { get; private set; }
        public string FilePath { get; private set; }

        public string Namespace => InternalType.Namespace;
        public string Name => InternalType.Name;
        public string FullName => InternalType.FullName;
        public bool IsStatic => false;
        public bool IsEnum => InternalType.IsEnum;
        public IEnumerable<XmlDocumentComment> Comments => _comments[FullName];

        public MarkdownableMethod[] Methods => this.GetMethods(Comments);
        public MarkdownableMethod[] StaticMethods => this.GetStaticMethods(Comments);
        public MarkdownableProperty[] Properties => this.GetProperties(Comments);
        public MarkdownableProperty[] StaticProperties => this.GetStaticProperties(Comments);
        public MarkdownableField[] Fields => this.GetFields(Comments);
        public MarkdownableField[] StaticFields => this.GetStaticFields(Comments);
        public MarkdownableEvent[] Events => this.GetEvents(Comments);
        public MarkdownableEvent[] StaticEvents => this.GetStaticEvents(Comments);

        //public string BeautifyName => Beautifier.BeautifyTypeWithLink(InternalType, GenerateTypeRelativeLinkPath);

        public Options Config { get; private set; }

        private readonly ILookup<string, XmlDocumentComment> _comments;

        public MarkdownableType(Type type, ILookup<string, XmlDocumentComment> commentLookup)
        {
            InternalType = type;
            _comments = commentLookup;
        }

        void BuildTable<T>(MarkdownBuilder mb, string label, T[] array, IEnumerable<XmlDocumentComment> docs, Func<T, string> type, Func<T, string> name, Func<T, string> finalName)
        {
            
        }     

        

        

        public void Build(string destination, Options config)
        {
            Config = config;
            FolderPath = destination;
            FilePath = Path.Combine(destination, Name + ".md");
            if (Config.TypePages)
            {
                string content = string.Empty;

                if (IsEnum)
                    content = Config.CurrentTheme.EnumPart.GetPage(this);
                else
                    content = Config.CurrentTheme.TypePart.GetPage(this);

                File.WriteAllText(FilePath, content);
            }

            Methods.Together(StaticMethods).Foreach(m => m.Build(Path.Combine(destination, Config.MethodFolderName), config));

            Properties.Together(StaticProperties).Foreach(p => p.Build(Path.Combine(destination, Config.PropertyFolderName), config));

            Fields.Together(StaticFields).Foreach(p => p.Build(Path.Combine(destination, Config.FieldFolderName), config));

            Events.Together(StaticEvents).Foreach(p => p.Build(Path.Combine(destination, Config.EventFolderName), config));

        }

        
    }
}
