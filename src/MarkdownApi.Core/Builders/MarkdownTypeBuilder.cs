﻿using Igloo15.MarkdownApi.Core.MarkdownItems;
using Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;
using System.Collections.Generic;
using System.Linq;

namespace Igloo15.MarkdownApi.Core.Builders
{
    internal class MarkdownTypeBuilder
    {

        public List<MarkdownNamespace> BuildTypes(MarkdownType[] types, ILookup<string, XmlDocumentComment> comments)
        {
            List<MarkdownNamespace> namespaces = new List<MarkdownNamespace>();

            foreach(var type in types)
            {

                var tempNamespace = new MarkdownNamespace(type.InternalType.Namespace);

                var myNamespace = MarkdownRepo.TryGetOrAdd(type.InternalType.Namespace, tempNamespace);

                var typeComments = comments[type.FullName];


                if(!type.InternalType.IsEnum)
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
            MarkdownRepo.TryAdd(type);

            type.NamespaceItem = namespaceItem;

            type.Summary = comments.FirstOrDefault(x => x.MemberName == type.Name
                    || x.MemberName.StartsWith(type.Name + "`"))?.Summary ?? "";


            BuildFields(type, comments, type.GetFields().Together(type.GetStaticFields()).ToArray());
            BuildProperties(type, comments, type.GetProperties().Together(type.GetStaticProperties()).ToArray());
            BuildMethods(type, comments, type.GetMethods().Together(type.GetStaticMethods()).ToArray());
            BuildEvents(type, comments, type.GetEvents().Together(type.GetStaticEvents()).ToArray());
            var constructors = type.GetConstructors();
            BuildConstructors(type, comments, type.GetConstructors().ToArray());

            return type;
        }

        public void BuildFields(MarkdownType type, IEnumerable<XmlDocumentComment> comments, MarkdownField[] infos)
        {
            foreach(var item in infos)
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
            foreach (var item in infos)
            {
                item.ParentType = type;
                type.Methods.Add(item);
                MarkdownRepo.TryAdd(item);

                item.Summary = comments.FirstOrDefault(a => (a.MemberName == item.Name || a.MemberName.StartsWith(item.Name + "`"))
                && item.InternalItem.GetParameters().All(b => a.Parameters.ContainsKey(b.Name))
                    )?.Summary ?? "";
            }
        }

        public void BuildEvents(MarkdownType type, IEnumerable<XmlDocumentComment> comments, MarkdownEvent[] infos)
        {
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
            foreach (var item in infos)
            {
                item.ParentType = type;
                type.Constructors.Add(item);
                MarkdownRepo.TryAdd(item);
                item.Summary = comments.FirstOrDefault(x => x.MemberName == item.Name
                    || x.MemberName.StartsWith(item.Name + "`"))?.Summary ?? "";
            }
        }

        public MarkdownEnum BuildEnum(MarkdownType type, MarkdownNamespace namespaceItem, IEnumerable<XmlDocumentComment> comments)
        {
            MarkdownEnum me = new MarkdownEnum();

            me.NamespaceItem = namespaceItem;
            me.InternalType = type.InternalType;
            me.Comments = comments;

            me.Summary = comments.FirstOrDefault(x => x.MemberName == me.Name
                    || x.MemberName.StartsWith(me.Name + "`"))?.Summary ?? "";

            MarkdownRepo.TryAdd(me);

            return me;
        }
    }
}