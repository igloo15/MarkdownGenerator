using Igloo15.MarkdownGenerator.Models;
using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Igloo15.MarkdownGenerator.Themes.Default
{
    internal class DefaultMethodPart : IThemePart<MarkdownableMethod>
    {
        private DefaultTheme defaultTheme;

        public DefaultMethodPart(DefaultTheme defaultTheme)
        {
            this.defaultTheme = defaultTheme;
        }

        public ITheme RootTheme => defaultTheme;

        public string GetCode(MarkdownableMethod value)
        {
            throw new System.NotImplementedException();
        }

        public string GetDetailed(MarkdownableMethod value)
        {
            return value.Name;
        }

        public string GetExample(MarkdownableMethod value)
        {
            throw new System.NotImplementedException();
        }

        public string GetLink(MarkdownableMethod value)
        {
            throw new System.NotImplementedException();
        }

        public string GetName(MarkdownableMethod value)
        {
            return value.Name;
        }
        
        public string GetReturnOrType(MarkdownableMethod value)
        {
            return Beautifier.BeautifyTypeWithLink(value.InternalMethod.ReturnType, value.FilePath);
        }

        public string GetSummary(MarkdownableMethod value)
        {
            return value.Comments.FirstOrDefault(x => x.MemberName == value.Name)?.Summary ?? "";
        }

        public string[] GetTableHeaders()
        {
            return new[] { "Return", "Name", "Summary" };
        }

        public string GetPage(MarkdownableMethod value)
        {
            var sb = new StringBuilder();

            var InternalMethod = value.InternalMethod;


            string generateTypeRelativeLinkPath(Type type)
            {
                var RelativeLinkPath = $"{(string.Join("/", type.Namespace.Split('.').Select(a => "..")))}/../{type.Namespace.Replace('.', '/')}/{type.Name}.md";
                return RelativeLinkPath;
            }

            var seq = InternalMethod.GetParameters().Select(x =>
            {
                var suffix = x.HasDefaultValue ? (" = " + (x.DefaultValue ?? $"null")) : "";
                return $"{Beautifier.BeautifyTypeWithLink(x.ParameterType, generateTypeRelativeLinkPath(x.ParameterType))} " + x.Name + suffix;
            });

            sb.AppendLine($"#\t{InternalMethod.Name} Method ({(value.IsExtension ? "this " : "")}{string.Join(", ", seq)})");

            var parameters = InternalMethod.GetParameters();

            var comment = value.Comments.FirstOrDefault(a =>
                (a.MemberName == InternalMethod.Name || a.MemberName.StartsWith(InternalMethod.Name + "`"))
                && parameters.All(b => a.Parameters.ContainsKey(b.Name))
            );

            if (comment != null)
            {

                if (comment.Parameters != null && comment.Parameters.Count > 0)
                {
                    sb.AppendLine($"");
                    sb.AppendLine("##\tParameters");


                    foreach (var parameter in parameters)
                    {
                        sb.AppendLine($"");
                        sb.AppendLine($"###\t{parameter.Name}");
                        sb.AppendLine($"-\tType: {Beautifier.BeautifyTypeWithLink(parameter.ParameterType, generateTypeRelativeLinkPath(parameter.ParameterType))}");
                        if (comment.Parameters.ContainsKey(parameter.Name))
                            sb.AppendLine($"-\t{comment.Parameters[parameter.Name]}");
                    }
                }
                if (!string.IsNullOrEmpty(comment.Returns))
                {
                    sb.AppendLine($"");
                    sb.AppendLine("##\tReturn Value");
                    sb.AppendLine($"-\tType: {Beautifier.BeautifyTypeWithLink(InternalMethod.ReturnType, generateTypeRelativeLinkPath(InternalMethod.ReturnType))}");
                    sb.AppendLine($"-\t{comment.Returns}");
                }

                sb.AppendLine($"");
                sb.AppendLine("##\tRemarks");
                sb.AppendLine($"-\t{comment.Summary}");
            }

            return sb.ToString();
        }
    }
}