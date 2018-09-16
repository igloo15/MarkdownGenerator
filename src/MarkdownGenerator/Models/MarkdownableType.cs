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

        readonly ILookup<string, XmlDocumentComment> commentLookup;

        public string Namespace => InternalType.Namespace;
        public string Name => InternalType.Name;
        public string FullName => InternalType.FullName;
        public bool IsStatic => false;

        public MarkdownableMethod[] Methods => this.GetMethods();
        public MarkdownableMethod[] StaticMethods => this.GetStaticMethods();
        public MarkdownableProperty[] Properties => this.GetProperties();
        public MarkdownableProperty[] StaticProperties => this.GetStaticProperties();
        public MarkdownableField[] Fields => this.GetFields();
        public MarkdownableField[] StaticFields => this.GetStaticFields();
        public MarkdownableEvent[] Events => this.GetEvents();
        public MarkdownableEvent[] StaticEvents => this.GetStaticEvents();

        public string BeautifyName => Beautifier.BeautifyTypeWithLink(InternalType, GenerateTypeRelativeLinkPath);

        private Options _config;

        public MarkdownableType(Type type, ILookup<string, XmlDocumentComment> commentLookup, Options config)
        {
            InternalType = type;
            this.commentLookup = commentLookup;
            _config = config;
        }

        void BuildTable<T>(MarkdownBuilder mb, string label, T[] array, IEnumerable<XmlDocumentComment> docs, Func<T, string> type, Func<T, string> name, Func<T, string> finalName)
        {
            if (array.Any())
            {
                mb.AppendLine($"##\t{label}");
                mb.AppendLine();

                string[] head = (InternalType.IsEnum)
                    ? new[] { "Value", "Name", "Summary" }
                    : new[] { "Type", "Name", "Summary" };

                IEnumerable<T> seq = array;
                if (!InternalType.IsEnum)
                {
                    seq = array.OrderBy(x => name(x));
                }

                var data = seq.Select(item2 =>
                {
                    var summary = docs.FirstOrDefault(x => x.MemberName == name(item2)
                    || x.MemberName.StartsWith(name(item2) + "`"))?.Summary ?? "";
                    return new[] {
                        //MarkdownBuilder.MarkdownCodeQuote(),
                        type(item2),
                        finalName(item2),
                        summary };
                });

                mb.Table(head, data);
                mb.AppendLine();
            }
        }


        string GenerateTypeRelativeLinkPath(Type type)
        {
            if (type.Name == "void")
                return string.Empty;
            if (type.Name == "String")
                return string.Empty;
            if (type.Namespace.StartsWith("System"))
                return string.Empty;
            var localNamescape = this.Namespace;
            var linkNamescape = type.Namespace;
            var RelativeLinkPath = $"{(string.Join("/", localNamescape.Split('.').Select(a => "..")))}/{linkNamescape.Replace('.', '/')}/{type.Name}.md";
            return RelativeLinkPath;
        }

        public void GenerateMethodDocuments(string namescapeDirectoryPath)
        {
            var methods = this.GetMethods();
            var comments = commentLookup[FullName];

            foreach (var method in methods.Select(i => i.InternalMethod))
            {
                
                
            }

        }

        public string GetLink()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string GetReturn()
        {
            throw new NotImplementedException();
        }

        public string GetSummary()
        {
            throw new NotImplementedException();
        }

        public string GetCode()
        {
            throw new NotImplementedException();
        }

        public string GetDetailed()
        {
            throw new NotImplementedException();
        }

        public string GetExample()
        {
            throw new NotImplementedException();
        }

        public string BuildPage()
        {
            var mb = new MarkdownBuilder();

            mb.HeaderWithCode(1, Beautifier.BeautifyTypeWithLink(InternalType, GenerateTypeRelativeLinkPath, false));
            mb.AppendLine();

            var desc = commentLookup[InternalType.FullName].FirstOrDefault(x => x.MemberType == MemberType.Type)?.Summary ?? "";
            if (desc != "")
            {
                mb.AppendLine(desc);
            }
            {
                var sb = new StringBuilder();

                var stat = (InternalType.IsAbstract && InternalType.IsSealed) ? "static " : "";
                var abst = (InternalType.IsAbstract && !InternalType.IsInterface && !InternalType.IsSealed) ? "abstract " : "";
                var classOrStructOrEnumOrInterface = InternalType.IsInterface ? "interface" : InternalType.IsEnum ? "enum" : InternalType.IsValueType ? "struct" : "class";

                sb.AppendLine($"public {stat}{abst}{classOrStructOrEnumOrInterface} {Beautifier.BeautifyType(InternalType, true)}");
                var impl = string.Join(", ", new[] { InternalType.BaseType }.Concat(InternalType.GetInterfaces()).Where(x => x != null && x != typeof(object) && x != typeof(ValueType)).Select(x => Beautifier.BeautifyType(x)));
                if (impl != "")
                {
                    sb.AppendLine("    : " + impl);
                }

                mb.Code("csharp", sb.ToString());
            }

            mb.AppendLine();

            if (InternalType.IsEnum)
            {
                var enums = Enum.GetNames(InternalType)
                    .Select(x => new {
                        Name = x,
                        //Value = ((Int32)Enum.Parse(type),
                        Value = x
                    })
                    .OrderBy(x => x.Value)
                    .ToArray();

                BuildTable(mb, "Enum", enums, commentLookup[InternalType.FullName], x => x.Value, x => x.Name, x => x.Name);
            }
            else
            {
                BuildTable(mb, "Fields", this.GetFields(), commentLookup[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalField.FieldType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Properties", this.GetProperties(), commentLookup[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalProperty.PropertyType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Events", this.GetEvents(), commentLookup[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalEvent.EventHandlerType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Methods", this.GetMethods(), commentLookup[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalMethod.ReturnType, GenerateTypeRelativeLinkPath), x => x.Name, x => Beautifier.ToMarkdownMethodInfo(x.InternalMethod, GenerateTypeRelativeLinkPath));
                BuildTable(mb, "Static Fields", this.GetStaticFields(), commentLookup[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalField.FieldType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Static Properties", this.GetStaticProperties(), commentLookup[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalProperty.PropertyType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Static Methods", this.GetStaticMethods(), commentLookup[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalMethod.ReturnType, GenerateTypeRelativeLinkPath), x => x.Name, x => Beautifier.ToMarkdownMethodInfo(x.InternalMethod, GenerateTypeRelativeLinkPath));
                BuildTable(mb, "Static Events", this.GetStaticEvents(), commentLookup[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalEvent.EventHandlerType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
            }

            return mb.ToString();
        }

        public void Build(string destination)
        {
            if(_config.TypePages)
            {
                var content = BuildPage();

                File.WriteAllText(Path.Combine(destination, Name + ".md"), content);
            }

            Methods.Together(StaticMethods).Foreach(m => m.Build(Path.Combine(destination, _config.MethodFolderName)));

            Properties.Together(StaticProperties).Foreach(p => p.Build(Path.Combine(destination, _config.PropertyFolderName)));

            Fields.Together(StaticFields).Foreach(p => p.Build(Path.Combine(destination, _config.FieldFolderName)));

            Events.Together(StaticEvents).Foreach(p => p.Build(Path.Combine(destination, _config.EventFolderName)));

        }
    }
}
