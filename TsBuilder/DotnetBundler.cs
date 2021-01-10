using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Utf8Json;
using System.IO;

namespace TsBuilder
{
    public static class DotnetBundler
    {
        private const string BundleConfigFile = "bundleconfig.json";
        public static async Task<bool> BundleAsync(List<BundleEntry> entrys)
        {
            var success = await CreateBundleFileAsync(entrys);

            if (!success)
                return false;

            var command = "dotnet bundle";

            var result = await Cmd.ExecuteAsync(command);

            Console.WriteLine(result);

            File.Delete(BundleConfigFile);

            return true;
        }

        private static async Task<bool> CreateBundleFileAsync(List<BundleEntry> entrys)
        {
            try
            {
                var data = JsonSerializer.Serialize(entrys);

                using (var fileStream = File.Create(BundleConfigFile))
                {
                    await fileStream.WriteAsync(data);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while create bundle", e.Message);
                return false;
            }
        }
    }
}
