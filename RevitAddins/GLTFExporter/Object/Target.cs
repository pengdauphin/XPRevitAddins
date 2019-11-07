using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Target
    {
        public int node { get; set; }

        /*path
         Allowed values:
                        "translation"
                        "rotation"
                        "scale"
                        "weights"
        */
        public string path { get; set; }
        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data

        public Target (string Path)
        {
            path = Path;
        }
    }
}
