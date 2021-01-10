using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Utf8Json;
using TsBuilder.TsConfigModels;

namespace TsBuilder
{
    public static class Typescript
    {
        public static List<BundleEntry> GetEntrys(List<BundleEntry> entrys)
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

        public static async Task<bool> CompileAsync(EnvironmentType environment)
        {
            var configFile = $"tsconfig.{environment.ToString()}.json";
            
            await CreateConfigAsync(environment, configFile);
            var command = GetCommand(configFile);

            var result = await Cmd.ExecuteAsync(command);

            Console.WriteLine(result);

            File.Delete(configFile);

            var existError = result.IndexOf("error") > -1;

            if (existError)
                return false;
            else
                return true;
        }

        private static string GetCommand(string configFile)
        {
            var baseCommand = "tsc";
            string options = $"--project {configFile}";

            return $"{baseCommand} {options}";
        }

        private static async Task CreateConfigAsync(EnvironmentType environment, string configFile)
        {
            var config = new TsConfig(environment);

            var data = JsonSerializer.Serialize(config);

            using (var fileStream = File.Create(configFile))
            {
                await fileStream.WriteAsync(data);
            }
        }

        public static List<BundleEntry> CreateJsEntrys(List<BundleEntry> tsEntrys)
        {
            var result = new List<BundleEntry>();

            foreach(var entry in tsEntrys)
            {
                var outputFile = entry.OutputFile.Replace("ts", "js");
                var inputFile = GetInputFile(outputFile);
                
                var jsEntry = new BundleEntry(inputFile, outputFile);

                result.Add(jsEntry);
            }

            return result;
        }

        private static string GetInputFile(string outputFile)
        {
            var fileNameStartIndex = outputFile.LastIndexOf('/') + 1;
            var fileName = outputFile.Substring(fileNameStartIndex);
            
            return "wwwroot/j/inputsForMin/" + fileName;
        }
    }
}
