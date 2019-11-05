using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Texture
    {
        public int sampler { get; set; }
        public int source { get; set; }
        public string name { get; set; }
        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data

    }
}
