using igloo15.MarkdownApi.Core;
using igloo15.MarkdownApi.Core.MarkdownItems;
using igloo15.MarkdownApi.Core.Themes;
using igloo15.MarkdownApi.Core.Themes.Default;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;

namespace igloo15.MarkdownApi.Tests
{
    /// <summary>
    /// The Test Class
    /// </summary>
    [TestClass]
    public class MarkdownParsingTest
    {
        private MarkdownProject _project;

        /// <summary>
        /// MarkdownParsingTest Class
        /// </summary>
        public MarkdownParsingTest()
        {
        }

        /// <summary>
        /// The test setup
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            _project = MarkdownApiGenerator.GenerateProject("./igloo15.MarkdownApi.Tests.dll", null);

            _project.Build(new DefaultTheme(new DefaultOptions
            {
                BuildNamespacePages = true,
                BuildTypePages = true,
                RootFileName = "README.md",
                RootTitle = "API",
                RootSummary = "The Root Page Summary",
                ShowParameterNames = true
            }),
                @".\testdocs"
            );
        }

        /// <summary>
        /// The ParseCountTest
        /// </summary>
        [TestMethod]
        public void ParseCountTest()
        {
            Assert.AreEqual(37, _project.AllItems.Count);
        }

        /// <summary>
        /// The SummaryCheckTest
        /// </summary>
        [TestMethod]
        public void SummaryCheckTest()
        {
            var results = _project.AllItems.Where(i => i.Value.ItemType != MarkdownItemTypes.Namespace && string.IsNullOrEmpty(i.Value.Summary));
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            foreach (var result in results)
            {
                sb.AppendLine(result.Value.FullName);
            }

            Assert.AreEqual(0, results.Count(), sb.ToString());
        }
    }
}