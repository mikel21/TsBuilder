using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsBuilder;
using Xunit;

namespace TsBuilderTest
{
    public class BundlerTest
    {
        private const string rootFolder = @"..\..\..\Bundler\Typescript\";

        [Fact]
        public async Task TsBundleSuccess()
        {
            // Arrange
            var entrys = new List<BundleEntry>() {
                new BundleEntry(){InputFiles = new string[]{rootFolder + "home.ts"}, OutputFile = rootFolder + "resultBundle.ts"},
                new BundleEntry(){InputFiles = new string[]{rootFolder + "IMenuInfluential.ts", rootFolder + "IMenuItemEventable.ts", rootFolder + "menu.ts", rootFolder + "search.ts", rootFolder + "site.ts"}, 
                    OutputFile = rootFolder + "resultBundle2.ts" }
            };
            var expectedBundles = await GetExpectedBundles();

            // Act
            await Bundler.BundleAsync(entrys);
            var resultBundles = await GetResultBundles(entrys);

            // Assert
            Assert.Equal(expectedBundles, resultBundles);
        }

        [Fact]
        public async Task SourceFileNotExist()
        {
            // Arrange
            var entrys = new List<BundleEntry>() {
                new BundleEntry(){InputFiles = new string[]{rootFolder + "home1.ts"}, OutputFile = rootFolder + "resultBundle.ts"},
                new BundleEntry(){InputFiles = new string[]{rootFolder + "IMenuInfluential.ts", rootFolder + "IMenuItemEventable.ts", rootFolder + "menu.ts", rootFolder + "search.ts", rootFolder + "site.ts"},
                    OutputFile = rootFolder + "resultBundle2.ts" }
            };

            // Act
            Func<Task> action = async () => await Bundler.BundleAsync(entrys);

            // Assert
            var exception = await Assert.ThrowsAsync<FileNotFoundException>(action);
        }

        private async Task<List<string>> GetExpectedBundles()
        {
            var sources = new string[] { "expectedBundle.ts", "expectedBundle2.ts" };
            var result = new List<string>();

            foreach (var source in sources)
            {
                var text = await File.ReadAllTextAsync(rootFolder + source);
                result.Add(text);
            }

            return result;
        }

        private async Task<List<string>> GetResultBundles(List<BundleEntry> entrys)
        {
            var result = new List<string>();

            foreach (var entry in entrys)
            {
                var text = await File.ReadAllTextAsync(entry.OutputFile);
                result.Add(text.Trim('\r', '\n'));
            }

            return result;
        }
    }
}
