using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TsBuilder
{
    public static class Javascript
    {
        public static List<BundleEntry> GetEntrys(List<BundleEntry> entrys)
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
    }
}
