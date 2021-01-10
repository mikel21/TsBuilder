using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TsBuilder
{
    public class BundleEntry
    {
        public BundleEntry() {}
        public BundleEntry(string inputFile, string outputFile)
        {
            OutputFile = outputFile;
            InputFiles = new string[] { inputFile };
        }

        [DataMember(Name = "outputFileName")]
        public string OutputFile { get; set; }

        [DataMember(Name = "inputFiles")]
        public string[] InputFiles { get; set; }
    }
}
