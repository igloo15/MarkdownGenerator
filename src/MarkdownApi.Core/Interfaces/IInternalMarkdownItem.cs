using System;
using System.Collections.Generic;
using System.Text;

namespace igloo15.MarkdownApi.Core.Interfaces
{
    internal interface IInternalMarkdownItem
    {
        void SetLocation(string location);

        void SetFilename(string filename);
    }
}
