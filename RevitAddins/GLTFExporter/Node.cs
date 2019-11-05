using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Node
    {
        //public int camera { get; set; }
        public List<int> children { get; set; }

        //public int skin { get; set; }
        public List<float> matrix { get; set; }
        //public int mesh { get; set; }
        public List<float> rotation { get; set; }

        public List<float> scale { get; set; }
        public List<float> translation { get; set; }
        public List<float> weights { get; set; }
        public string name { get; set; }
        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data
    }
}
