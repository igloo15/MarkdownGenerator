using igloo15.MarkdownApi.Core.MarkdownItems;
using igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace igloo15.MarkdownApi.Core.Builders
{
  internal class MarkdownTypeBuilder
    {
        public List<MarkdownNamespace> BuildTypes(MarkdownType[] types, ILookup<string, XmlDocumentComment> comments)
        {
            List<MarkdownNamespace> namespaces = new List<MarkdownNamespace>();

            foreach (var type in types)
            {
                var tempNamespace = new MarkdownNamespace(type.InternalType.Namespace);

                var myNamespace = MarkdownRepo.TryGetOrAdd(type.InternalType.Namespace, tempNamespace);

                var typeComments = comments[type.FullName];


                if (!type.InternalType.IsEnum)
                {
                    var builtType = BuildType(type, myNamespace, typeComments);

                    myNamespace.Types.Add(builtType);
                }
                else
                {
                    var enumType = BuildEnum(type, myNamespace, typeComments);

                    myNamespace.Enums.Add(enumType);
                }

                namespaces.Add(myNamespace);
            }

            return namespaces;
        }

        public MarkdownType BuildType(MarkdownType type, MarkdownNamespace namespaceItem, IEnumerable<XmlDocumentComment> comments)
        {
            Constants.Logger?.LogTrace("Building Markdown Item for Type {typeName}", type.Name);
            MarkdownRepo.TryAdd(type);

            type.NamespaceItem = namespaceItem;

            type.Summary = comments.FirstOrDefault(x => x.MemberName == type.Name
                    || x.MemberName.StartsWith(type.Name))?.Summary ?? "";

            Constants.Logger?.LogTrace("Getting Markdown Fields for Type {typeName}", type.Name);
            BuildFields(type, comments, type.GetFields().Together(type.GetStaticFields()).ToArray());

            Constants.Logger?.LogTrace("Getting Markdown Properties for Type {typeName}", type.Name);
            BuildProperties(type, comments, type.GetProperties().Together(type.GetStaticProperties()).ToArray());

            Constants.Logger?.LogTrace("Getting Markdown Methods for Type {typeName}", type.Name);
            BuildMethods(type, comments, type.GetMethods().Together(type.GetStaticMethods()).ToArray());

            Constants.Logger?.LogTrace("Getting Markdown Events for Type {typeName}", type.Name);
            BuildEvents(type, comments, type.GetEvents().Together(type.GetStaticEvents()).ToArray());

            Constants.Logger?.LogTrace("Getting Markdown Constructors for Type {typeName}", type.Name);
            BuildConstructors(type, comments, type.GetConstructors().Together(type.GetStaticConstructors()).ToArray());

            Constants.Logger?.LogTrace("Completed Building Markdown Type {typeName}", type.Name);
            return type;
        }

        public void BuildFields(MarkdownType type, IEnumerable<XmlDocumentComment> comments, MarkdownField[] infos)
        {
            Constants.Logger?.LogTrace("Found {itemCount} Markdown Fields for Type {typeName}", infos.Length, type.Name);
            foreach (var item in infos)
            {
                item.ParentType = type;
                type.Fields.Add(item);
                MarkdownRepo.TryAdd(item);
                item.Summary = comments.FirstOrDefault(x => x.MemberName == item.Name
                    || x.MemberName.StartsWith(item.Name + "`"))?.Summary ?? "";
            }
        }

        public void BuildProperties(MarkdownType type, IEnumerable<XmlDocumentComment> comments, MarkdownProperty[] infos)
        {
            Constants.Logger?.LogTrace("Found {itemCount} Markdown Properties for Type {typeName}", infos.Length, type.Name);
            foreach (var item in infos)
            {
                item.ParentType = type;
                type.Properties.Add(item);
                MarkdownRepo.TryAdd(item);
                item.Summary = comments.FirstOrDefault(x => x.MemberName == item.Name
                    || x.MemberName.StartsWith(item.Name + "`"))?.Summary ?? "";
            }
        }

        public void BuildMethods(MarkdownType type, IEnumerable<XmlDocumentComment> comments, MarkdownMethod[] infos)
        {
            Constants.Logger?.LogTrace("Found {itemCount} Markdown Comments for Type {typeName}", infos.Length, type.Name);
            var memberComments = comments.Where(c => c.MemberType == MemberType.Method);
            foreach (var item in infos)
            {
                item.ParentType = type;
                type.Methods.Add(item);
                MarkdownRepo.TryAdd(item);

               item.Summary = memberComments.Where(mc => mc.MemberName == item.InternalItem.GetCommentName()).FirstOrDefault(a => MethodCommentFilter(a, item))?.Summary ?? "";
            }
        }

        private bool MethodCommentFilter(XmlDocumentComment comment, MarkdownMethod method)
        {
            return method.InternalItem.IsMatchOnMethod(comment);
        }

        public void BuildEvents(MarkdownType type, IEnumerable<XmlDocumentComment> comments, MarkdownEvent[] infos)
        {
            Constants.Logger?.LogTrace("Found {itemCount} Markdown Events for Type {typeName}", infos.Length, type.Name);
            foreach (var item in infos)
            {
                item.ParentType = type;
                type.Events.Add(item);
                MarkdownRepo.TryAdd(item);
                item.Summary = comments.FirstOrDefault(x => x.MemberName == item.Name
                    || x.MemberName.StartsWith(item.Name + "`"))?.Summary ?? "";
            }
        }

        public void BuildConstructors(MarkdownType type, IEnumerable<XmlDocumentComment> comments, MarkdownConstructor[] infos)
        {
            Constants.Logger?.LogTrace("Found {itemCount} Markdown Constructors for Type {typeName}", infos.Length, type.Name);
            foreach (var item in infos)
            {
                item.ParentType = type;
                type.Constructors.Add(item);
                MarkdownRepo.TryAdd(item);
                item.Summary = comments.FirstOrDefault(x => ConstructorCommentFilter(x, item))?.Summary ?? "";
            }
        }

        private bool ConstructorCommentFilter(XmlDocumentComment comment, MarkdownConstructor constructor)
        {
            return constructor.InternalItem.IsMatchOnMethod(comment);
        }

        public MarkdownEnum BuildEnum(MarkdownType type, MarkdownNamespace namespaceItem, IEnumerable<XmlDocumentComment> comments)
        {
            Constants.Logger?.LogTrace("Building Markdown Item for Enum {enumName}", type.Name);
            MarkdownEnum me = new MarkdownEnum();

            me.NamespaceItem = namespaceItem;
            me.InternalType = type.InternalType;
            me.Comments = comments;

            me.Summary = comments.FirstOrDefault(x => x.MemberName == me.Name
                    || x.MemberName.StartsWith(me.Name + "`"))?.Summary ?? "";

            MarkdownRepo.TryAdd(me);

            Constants.Logger?.LogTrace("Completed Building Markdown Enum {typeName}", me.Name);
            return me;
        }
    }
}