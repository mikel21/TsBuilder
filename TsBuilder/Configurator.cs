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
        private static string BundleConfigDirectory;
        private const string BundleConfigFile = "bundlerconfig.json";

        public static async Task<Dictionary<string, object>> GetSettingsAsync(string file)
        {
            Dictionary<string, object> result = null;

            try
            {
                using (var fileStream = File.OpenRead("settings.json"))
                {
                    result = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(fileStream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error {0}", e.Message);
                return null;
            }

            return result;
        }

        public static async Task<List<BundleEntry>> GetBundleFilesAsync()
        {
            List<BundleEntry> result = null;

            try
            {
                using (var fileStream = File.OpenRead(BundleConfigFile))
                {
                    result = await JsonSerializer.DeserializeAsync<List<BundleEntry>>(fileStream);
                    BundleConfigDirectory = Path.GetDirectoryName(fileStream.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error {0}", e.Message);
                return null;
            }

            return result;
        }

        public static List<BundleEntry> GetTsEntrysAsync(List<BundleEntry> entrys)
        {
            var result = new List<BundleEntry>();

            foreach (var entry in entrys)
            {
                if (entry.OutputFile.IndexOf(".ts") > -1)
                {
                    result.Add(entry);
                }
            }

            return result;
        }

        public static List<BundleEntry> GetJsEntrysAsync(List<BundleEntry> entrys)
        {
            var result = new List<BundleEntry>();

            foreach (var entry in entrys)
            {
                if (entry.OutputFile.IndexOf(".js") > -1)
                {
                    result.Add(entry);
                }
            }

            return result;
        }

        public static List<BundleEntry> GetCssEntryAsync(List<BundleEntry> entrys)
        {
            var result = new List<BundleEntry>();

            foreach (var entry in entrys)
            {
                if (entry.OutputFile.IndexOf(".css") > -1)
                {
                    result.Add(entry);
                }
            }

            return result;
        }

        public static string GetBundleConfigDirectory()
        {
            return BundleConfigDirectory;
        }
    }
}
