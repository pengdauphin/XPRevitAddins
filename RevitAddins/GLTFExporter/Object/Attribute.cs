using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Attribute
    {
        public int NORMAL { get; set; }
        public int POSITION { get; set; }
        public int TANGENT { get; set; }
        public int TEXCOORD_0 { get; set; }
        public int TEXCOORD_1 { get; set; }
        public int COLOR_0 { get; set; }
        public int JOINTS_0 { get; set; }
        public int WEIGHTS_0 { get; set; }
    }
}
