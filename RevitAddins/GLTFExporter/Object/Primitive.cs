using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Primitive
    {
        public Attribute attributes { get; set; }
        public int indices { get; set; }
        public int material { get; set; }
        public int mode { get; set; }
        public Target[] targets { get; set; }

        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data

        public Primitive (Attribute Attribute)
        {
            attributes = Attribute;
        }
    }
}
