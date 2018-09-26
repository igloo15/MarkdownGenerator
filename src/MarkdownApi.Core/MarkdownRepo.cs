using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.MarkdownItems;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownApi.Core
{
    internal static class MarkdownRepo
    {

        private static Dictionary<string, IMarkdownItem> Items { get; } = new Dictionary<string, IMarkdownItem>();


        public static bool TryAdd(IMarkdownItem item)
        {
            return TryAdd(item.TypeInfo.GetId(), item);
        }

        private static bool TryAdd(string name, IMarkdownItem item)
        {
            if (!Items.ContainsKey(name))
            {
                Items.Add(name, item);
                return true;
            }

            Console.WriteLine($"Item {name} already exists");
            return false;
        }

        public static Dictionary<string, IMarkdownItem> GetLookup()
        {
            return Items;
        }

        public static MarkdownNamespace TryGetOrAdd(string name, IMarkdownItem item)
        {
            if(!Items.ContainsKey(name))
            {
                TryAdd(name, item);
            }

            return Items[name] as MarkdownNamespace;
        }

    }
}
