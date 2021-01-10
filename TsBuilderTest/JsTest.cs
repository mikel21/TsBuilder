using System;
using System.Collections.Generic;
using System.Text;
using TsBuilder;
using Xunit;

namespace TsBuilderTest
{
    public class JsTest
    {
        [Fact]
        public void GetEntrysSuccess()
        {
            // Arrange
            var entrys = new List<BundleEntry>() {
                new BundleEntry(){InputFiles = new string[]{"Content/s/site.css", "Content/s/menu.css", "Content/s/jqModal.css", "Content/s/jquery.tooltipster.css"}, OutputFile = "wwwroot/s/site.css"},
                new BundleEntry(){InputFiles = new string[]{"Content/s/error.css"}, OutputFile = "wwwroot/s/error.css"},
                new BundleEntry(){InputFiles = new string[]{"Content/j/jQueryPlugins/jquery.js", "Content/j/jQueryPlugins/jsrender.js", "Content/j/jQueryPlugins/jqModal.js", "Content/j/jQueryPlugins/jquery.tooltipster.js"}, OutputFile = "wwwroot/j/jquery.plus.plugins.js"},
                new BundleEntry(){InputFiles = new string[]{"Content/j/ts/home.ts"}, OutputFile = "wwwroot/j/home.ts"},
                new BundleEntry(){InputFiles = new string[]{"Content/j/ts/interfaces/IMenuInfluential.ts", "Content/j/ts/interfaces/IMenuItemEventable.ts", "Content/j/ts/menu.ts", "Content/j/ts/search.ts", "Content/j/ts/site.ts"}, OutputFile = "wwwroot/j/site.ts"}
            };
            var expected = new List<BundleEntry>() {
                new BundleEntry() { InputFiles = new string[] { "Content/j/jQueryPlugins/jquery.js", "Content/j/jQueryPlugins/jsrender.js", "Content/j/jQueryPlugins/jqModal.js", "Content/j/jQueryPlugins/jquery.tooltipster.js" }, OutputFile = "wwwroot/j/jquery.plus.plugins.js" }
            };

            // Act
            var result = Javascript.GetEntrys(entrys);

            // Assert
            Assert.Equal(expected.Count, result.Count);

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].OutputFile, result[i].OutputFile);
                Assert.Equal(expected[i].InputFiles, result[i].InputFiles);
            }
        }
    }
}
