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
        public MethodInfo InternalMethod { get; private set; }

        public bool IsStatic { get; private set; }

        public string Name => InternalMethod.Name;

        private Options _config;

        public MarkdownableMethod(MethodInfo info, bool isStatic)
        {
            InternalMethod = info;
            IsStatic = isStatic;
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
            var sb = new StringBuilder();

            string generateTypeRelativeLinkPath(Type type)
            {
                var RelativeLinkPath = $"{(string.Join("/", type.Namespace.Split('.').Select(a => "..")))}/../{type.Namespace.Replace('.', '/')}/{type.Name}.md";
                return RelativeLinkPath;
            }
            var isExtension = InternalMethod.GetCustomAttributes<System.Runtime.CompilerServices.ExtensionAttribute>(false).Any();
            var seq = InternalMethod.GetParameters().Select(x =>
            {
                var suffix = x.HasDefaultValue ? (" = " + (x.DefaultValue ?? $"null")) : "";
                return $"{Beautifier.BeautifyTypeWithLink(x.ParameterType, generateTypeRelativeLinkPath)} " + x.Name + suffix;
            });
            sb.AppendLine($"#\t{InternalMethod.DeclaringType.Name}.{InternalMethod.Name} Method ({(isExtension ? "this " : "")}{string.Join(", ", seq)})");

            var parameters = InternalMethod.GetParameters();

            var comment = comments.FirstOrDefault(a =>
            (a.MemberName == InternalMethod.Name ||
            a.MemberName.StartsWith(InternalMethod.Name + "`"))
            &&
            parameters.All(b => a.Parameters.ContainsKey(b.Name))
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
                        sb.AppendLine($"-\tType: {Beautifier.BeautifyTypeWithLink(parameter.ParameterType, generateTypeRelativeLinkPath)}");
                        if (comment.Parameters.ContainsKey(parameter.Name))
                            sb.AppendLine($"-\t{comment.Parameters[parameter.Name]}");
                    }
                }
                if (!string.IsNullOrEmpty(comment.Returns))
                {
                    sb.AppendLine($"");
                    sb.AppendLine("##\tReturn Value");
                    sb.AppendLine($"-\tType: {Beautifier.BeautifyTypeWithLink(method.ReturnType, generateTypeRelativeLinkPath)}");
                    sb.AppendLine($"-\t{comment.Returns}");
                }

                sb.AppendLine($"");
                sb.AppendLine("##\tRemarks");
                sb.AppendLine($"-\t{comment.Summary}");
            }

            return sb.ToString();
        }

        public void Build(string destination)
        {
            if(_config.MethodPages)
            {
                if (!Directory.Exists(destination))
                    Directory.CreateDirectory(destination);

                var content = BuildPage();

                File.WriteAllText(Path.Combine(destination, $"{InternalMethod.DeclaringType.Name}-{InternalMethod.MetadataToken}.md"), content);
            }
            
        }
    }
}
