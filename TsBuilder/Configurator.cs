using System;
using System.Collections.Generic;
using System.Text;
using Utf8Json;
using System.Threading.Tasks;
using System.IO;

namespace TsBuilder
{
    public static class Configurator
    {
        public static async Task<List<BundleEntry>> GetBundleFilesAsync(string bundleConfigFile)
        {
            List<BundleEntry> result = null;

            using (var fileStream = File.OpenRead(bundleConfigFile))
            {
                result = await JsonSerializer.DeserializeAsync<List<BundleEntry>>(fileStream);
            }

            return result;
        }
    }
}
