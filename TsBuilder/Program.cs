using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.InteropServices;

namespace TsBuilder
{
    public class Program
    {
        private const string BundleConfigFile = "bundlerconfig.json";

        public static async Task Main(string[] args)
        {
            try
            {
                Environment.SetEnvironmentType(args);

                var bundleEntrys = await Configurator.GetBundleFilesAsync(BundleConfigFile);

                if (OS.Type == OSPlatform.Windows)
                    WinConsole.Setup();

                var tsEntrys = Typescript.GetEntrys(bundleEntrys);
                await Bundler.BundleAsync(tsEntrys);

                var compileSuccess = await Typescript.CompileAsync(Environment.Type);

                if (!compileSuccess)
                {
                    return;
                }

                var jsEntrys = Javascript.GetEntrys(bundleEntrys);
                var cssEntrys = Css.GetEntrys(bundleEntrys);

                if (Environment.Type == EnvironmentType.Debug)
                {
                    await Bundler.BundleAsync(jsEntrys);
                    await Bundler.BundleAsync(cssEntrys);
                }
                else if (Environment.Type == EnvironmentType.Prod)
                {
                    var summaryEntrys = new List<BundleEntry>();
                    var jsOtherEntrys = Typescript.CreateJsEntrys(tsEntrys);

                    summaryEntrys.AddRange(cssEntrys);
                    summaryEntrys.AddRange(jsEntrys);
                    summaryEntrys.AddRange(jsOtherEntrys);

                    var successDotnetBundle = await DotnetBundler.BundleAsync(summaryEntrys);

                    if (!successDotnetBundle)
                        return;
                }

                Console.WriteLine("Bundling done !");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }
    }
}
