using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Igloo15.MarkdownApi.Core
{
    /// <summary>
    /// Public Extensions are extension methods meant to be used by anyone with dependency on the MarkdownApi.Core
    /// </summary>
    public static class PublicExtensions
    {

        /// <summary>
        /// Will return a relative link from one markdown item to the other
        /// </summary>
        /// <param name="from">The starting place where you need to link from</param>
        /// <param name="dest">The place you need the link to take you</param>
        /// <returns>The file url or empty string if no file exists</returns>
        public static string To(this IMarkdownItem from, IMarkdownItem dest)
        {
            if (dest.FileName == null)
                throw new FileNotFoundException("Specified markdown item does not have a file to link to", dest.FileName);

            return from.Location.AddRoot().UpdatedRelativePath(dest.Location.AddRoot()).CombinePath(dest.FileName).AddRoot();
        }
    }
}
