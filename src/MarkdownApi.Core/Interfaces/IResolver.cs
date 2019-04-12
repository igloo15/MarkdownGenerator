using System;
using System.Collections.Generic;
using System.Text;

namespace igloo15.MarkdownApi.Core.Interfaces
{
    /// <summary>
    /// Resolves the path/folder of the markdown item and gets a file name
    /// </summary>
    public interface IResolver
    {
        /// <summary>
        /// Calculates the path for the markdown item
        /// </summary>
        /// <param name="item">The item to get a path for</param>
        /// <returns>String path to where to place the markdown item page</returns>
        string GetPath(IMarkdownItem item);

        /// <summary>
        /// Gets the filename for the markdown item
        /// </summary>
        /// <param name="item">The item to get a filename for</param>
        /// <returns>String name of the file that the item's content should be in</returns>
        string GetFileName(IMarkdownItem item);
    }
}
