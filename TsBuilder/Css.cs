using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TsBuilder
{
    public static class Css
    {
        public static async Task<bool> BundleAsync(List<BundleEntry> entrys)
        {
            try
            {
                return await Bundler.BundleAsync(entrys);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while bundling css: {0}", e.Message);
                return false;
            }
        }
    }
}
