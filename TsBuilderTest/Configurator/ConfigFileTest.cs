using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TsBuilder;
using Xunit;
using Utf8Json;
using System.Linq;

namespace TsBuilderTest
{
    public class ConfigFileTest
    {
        [Fact]
        public async Task ConfigFileIsValid()
        {
            // Arrange
            var configFile = @"..\..\..\Configurator\validConfig.json";
            var expectedBundles = new List<BundleEntry>() {
                new BundleEntry(){InputFiles = new string[]{"Content/s/site.css", "Content/s/menu.css", "Content/s/jqModal.css", "Content/s/jquery.tooltipster.css"}, OutputFile = "wwwroot/s/site.css"},
                new BundleEntry(){InputFiles = new string[]{"Content/s/error.css"}, OutputFile = "wwwroot/s/error.css"},
                new BundleEntry(){InputFiles = new string[]{"Content/j/jQueryPlugins/jquery.js", "Content/j/jQueryPlugins/jsrender.js", "Content/j/jQueryPlugins/jqModal.js", "Content/j/jQueryPlugins/jquery.tooltipster.js"}, OutputFile = "wwwroot/j/jquery.plus.plugins.js"},
                new BundleEntry(){InputFiles = new string[]{"Content/j/ts/home.ts"}, OutputFile = "wwwroot/j/home.ts"},
                new BundleEntry(){InputFiles = new string[]{"Content/j/ts/interfaces/IMenuInfluential.ts", "Content/j/ts/interfaces/IMenuItemEventable.ts", "Content/j/ts/menu.ts", "Content/j/ts/search.ts", "Content/j/ts/site.ts"}, OutputFile = "wwwroot/j/site.ts"}
            };

            // Act
            var result = await Configurator.GetBundleFilesAsync(configFile);

            // Assert
            Assert.Equal(expectedBundles.Count, result.Count);

            for (var i = 0; i < expectedBundles.Count; i++)
            {
                Assert.Equal(expectedBundles[i].OutputFile, result[i].OutputFile);
                Assert.Equal(expectedBundles[i].InputFiles, result[i].InputFiles);
            }
        }

        [Fact]
        public async Task ConfigFileIsNotValid()
        {
            // Arrange
            var configFile = @"..\..\..\Configurator\notValidConfig.json";

            // Act
            Func<Task> action = async () => await Configurator.GetBundleFilesAsync(configFile);

            // Assert
            var exception = await Assert.ThrowsAsync<JsonParsingException>(action);
        }

        [Fact]
        public async Task ConfigFileNotFound()
        {
            // Arrange
            var configFile = "some-config.txt";

            // Act
            Func<Task> action = async () => await Configurator.GetBundleFilesAsync(configFile);

            // Assert
            var exception = await Assert.ThrowsAsync<FileNotFoundException>(action);
        }
    }
}
