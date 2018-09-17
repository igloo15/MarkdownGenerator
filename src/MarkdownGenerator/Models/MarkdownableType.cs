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

        

        public string Namespace => InternalType.Namespace;
        public string Name => InternalType.Name;
        public string FullName => InternalType.FullName;
        public bool IsStatic => false;
        public bool IsEnum => InternalType.IsEnum;

        public MarkdownableMethod[] Methods => this.GetMethods(_comments[FullName]);
        public MarkdownableMethod[] StaticMethods => this.GetStaticMethods(_comments[FullName]);
        public MarkdownableProperty[] Properties => this.GetProperties(_comments[FullName]);
        public MarkdownableProperty[] StaticProperties => this.GetStaticProperties(_comments[FullName]);
        public MarkdownableField[] Fields => this.GetFields(_comments[FullName]);
        public MarkdownableField[] StaticFields => this.GetStaticFields(_comments[FullName]);
        public MarkdownableEvent[] Events => this.GetEvents(_comments[FullName]);
        public MarkdownableEvent[] StaticEvents => this.GetStaticEvents(_comments[FullName]);

        public string BeautifyName => Beautifier.BeautifyTypeWithLink(InternalType, GenerateTypeRelativeLinkPath);

        private Options _config;
        private readonly ILookup<string, XmlDocumentComment> _comments;

        public MarkdownableType(Type type, ILookup<string, XmlDocumentComment> commentLookup)
        {
            InternalType = type;
            _comments = commentLookup;
        }

        void BuildTable<T>(MarkdownBuilder mb, string label, T[] array, IEnumerable<XmlDocumentComment> docs, Func<T, string> type, Func<T, string> name, Func<T, string> finalName)
        {
            
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
            

        }

        public string GetLink()
        {
            return GenerateTypeRelativeLinkPath(InternalType);
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string GetReturnOrType()
        {
            throw new NotImplementedException();
        }

        public string GetSummary()
        {
            throw new NotImplementedException();
        }

        public string GetCode()
        {
            var mb = new MarkdownBuilder();
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

            return mb.ToString();
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

            var desc = _comments[InternalType.FullName].FirstOrDefault(x => x.MemberType == MemberType.Type)?.Summary ?? "";

            if (!String.IsNullOrEmpty(desc))
            {
                mb.AppendLine(desc);
            }
            
            mb.Append(GetCode());
            

            mb.AppendLine();

            if (InternalType.IsEnum)
            {
                BuildEnumTable(mb);
            }
            else
            {

                BuildTable(mb, "Fields", _comments[InternalType.FullName], Fields, new[] { "Type", "Name", "Summary" });
                BuildTable(mb, "Properties", _comments[InternalType.FullName], Properties, new[] { "Type", "Name", "Summary" });

                //1 -Type 2-Name 3-finalName
                BuildTable(mb, "Fields", Fields, _comments[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalField.FieldType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Properties", Properties, _comments[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalProperty.PropertyType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Events", Events, _comments[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalEvent.EventHandlerType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Methods", Methods, _comments[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalMethod.ReturnType, GenerateTypeRelativeLinkPath), x => x.Name, x => Beautifier.ToMarkdownMethodInfo(x.InternalMethod, GenerateTypeRelativeLinkPath));
                BuildTable(mb, "Static Fields", StaticFields, _comments[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalField.FieldType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Static Properties", StaticProperties, _comments[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalProperty.PropertyType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Static Methods", StaticMethods, _comments[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalMethod.ReturnType, GenerateTypeRelativeLinkPath), x => x.Name, x => Beautifier.ToMarkdownMethodInfo(x.InternalMethod, GenerateTypeRelativeLinkPath));
                BuildTable(mb, "Static Events", StaticEvents, _comments[InternalType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalEvent.EventHandlerType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
            }

            return mb.ToString();
        }

        private void BuildTable(MarkdownBuilder mb, string label, IEnumerable<XmlDocumentComment> comments, IMarkdownable[] items, string[] headers)
        {
            if (items.Any())
            {
                mb.Header(2, label);
                mb.AppendLine();


                var seq = items.OrderBy(x => x.Name);

                var data = seq.Select(item2 =>
                {
                    var summary = comments.FirstOrDefault(x => x.MemberName == item2.GetName()
                        || x.MemberName.StartsWith(item2.GetName() + "`"))?.Summary ?? "";

                    return new[] {
                        item2.GetReturnOrType(),
                        item2.GetDetailed(),
                        summary
                    };
                });

                mb.Table(headers, data);
                mb.AppendLine();
            }
        }

        private void BuildEnumTable(MarkdownBuilder mb)
        {
            var docs = _comments[InternalType.FullName];
            var enums = Enum.GetNames(InternalType)
                    .Select(x => new {
                        Name = x,
                        //Value = ((Int32)Enum.Parse(type),
                        Value = x
                    })
                    .OrderBy(x => x.Value)
                    .ToArray();

            if (enums.Any())
            {
                mb.AppendLine($"##\tEnum");
                mb.AppendLine();

                string[] head = new[] { "Value", "Name", "Summary" };
                
                var data = enums.Select(item =>
                {
                    var summary = docs.FirstOrDefault(x => x.MemberName == item.Name
                    || x.MemberName.StartsWith(item.Name + "`"))?.Summary ?? "";


                    return new[] {
                        item.Value,
                        item.Name,
                        summary
                    };
                });

                mb.Table(head, data);
                mb.AppendLine();
            }
        }

        public void Build(string destination, Options config)
        {
            _config = config;
            if (_config.TypePages)
            {
                var content = BuildPage();

                File.WriteAllText(Path.Combine(destination, Name + ".md"), content);
            }

            Methods.Together(StaticMethods).Foreach(m => m.Build(Path.Combine(destination, _config.MethodFolderName), config));

            Properties.Together(StaticProperties).Foreach(p => p.Build(Path.Combine(destination, _config.PropertyFolderName), config));

            Fields.Together(StaticFields).Foreach(p => p.Build(Path.Combine(destination, _config.FieldFolderName), config));

            Events.Together(StaticEvents).Foreach(p => p.Build(Path.Combine(destination, _config.EventFolderName), config));

        }
    }
}
