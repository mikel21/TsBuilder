using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace TsBuilder
{
    public static class Bundler
    {
        public static async Task BundleAsync(List<BundleEntry> entrys)
        {
            if (!entrys.Any())
                return;
            
            foreach (var entry in entrys)
            {
                await File.WriteAllTextAsync(entry.OutputFile, string.Empty);
                await CopyToEndOfFileAsync(entry.InputFiles, entry.OutputFile);
            }
        }

        private static async Task CopyToEndOfFileAsync(string[] sourceFiles, string destination)
        {
            foreach (var source in sourceFiles)
            {
                var sourceText = await File.ReadAllTextAsync(source);
                await File.AppendAllTextAsync(destination, sourceText + System.Environment.NewLine);
            }
        }
    }
}
