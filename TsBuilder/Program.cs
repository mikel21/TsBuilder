using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.InteropServices;

namespace TsBuilder
{
    class Program
    {
        private static EnvironmentType Environment { get; set; }
        static async Task Main(string[] args)
        {
            SetEnvironmentType(args);

            var bundleEntrys = await Configurator.GetBundleFilesAsync();

            if (bundleEntrys == null)
            {
                LogGetError("bundles");
                return;
            }

            var tsEntrys = Configurator.GetTsEntrysAsync(bundleEntrys);

            if (OS.Type == OSPlatform.Windows)
                WinConsole.Setup();

            var bundleSuccess = await Typescript.BundleAsync(tsEntrys);

            if (!bundleSuccess)
            {
                return;
            }

            var compileSuccess = await Typescript.CompileAsync(Environment);
            
            if (!compileSuccess)
            {
                return;
            }

            var jsEntrys = Configurator.GetJsEntrysAsync(bundleEntrys);
            var cssEntrys = Configurator.GetCssEntryAsync(bundleEntrys);

            if (Environment == EnvironmentType.Debug)
            {    
                var bundleJsSuccess = await Javascript.BundleAsync(jsEntrys);

                if (!bundleJsSuccess)
                {
                    return;
                }
                
                var bundleCssSuccess = await Css.BundleAsync(cssEntrys);

                if (!bundleCssSuccess)
                {
                    return;
                }
            }
            else if (Environment == EnvironmentType.Prod)
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

        private static void LogGetError(string elements)
        {
            Console.WriteLine($"Error while getting {elements}");
        }

        private static void SetEnvironmentType(string[] args)
        {
            if (args.Length == 0)
            {
                Environment = EnvironmentType.Debug;
                return;
            }

            if (args[0] == "--production")
                Environment = EnvironmentType.Prod;
            else
                Environment = EnvironmentType.Debug;
        }
    }
}
