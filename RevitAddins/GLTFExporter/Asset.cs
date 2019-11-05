using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{

    class Asset
    {
        public string copyright { get; set; }
        public string generator { get; set; }
        public string version { get; set; }
        public string minVersion { get; set; }
        public Extension extensions { get; set; }
        //public any extras { get; set; }

        public Asset (string Version)
        {
            version = Version;
        }

        private string Toregexp(string _String)
        {
            //string pattern = '^([0-9]+)\\.([0-9]+)$';


            return "";
        }
    }
}
