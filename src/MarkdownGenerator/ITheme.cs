using Igloo15.MarkdownGenerator.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator
{
    interface ITheme
    {
        string GetMethodCode(MethodInfo methodInfo);

        string GetMethodLink(MethodInfo methodInfo);

        string GetMethodTitle(MethodInfo methodInfo);

        string GetMethodPage(MethodInfo methodInfo);


        string GetTypeCode(MarkdownableType t);

        string GetTypeLink(MarkdownableType t);

        string GetTypeTitle(MarkdownableType t);

        string GetTypePage(MarkdownableType t);


        string GetNamespaceTitle(NamespaceGroup group);

        string GetNamespaceLink(NamespaceGroup group);

        string GetNamespacePage(NamespaceGroup group);

        

        string GetEnumLink(Type t);

        string GetEnumTitle(Type t);

        string GetEnumPage(Type t);
    }
}
