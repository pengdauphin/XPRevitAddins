using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Sampler
    {
        /*
         Allowed values:
                        9728 NEAREST
                        9729 LINEAR
             */
        public int magFilter { get; set; }
        /*
         Allowed values:
                        9728 NEAREST
                        9729 LINEAR
                        9984 NEAREST_MIPMAP_NEAREST
                        9985 LINEAR_MIPMAP_NEAREST
                        9986 NEAREST_MIPMAP_LINEAR
                        9987 LINEAR_MIPMAP_LINEAR
             */
        public int minFilter { get; set; }
        /*
         Allowed values:
                        33071 CLAMP_TO_EDGE
                        33648 MIRRORED_REPEAT
                        10497 REPEAT
             */
        public int wrapS { get; set; }
        /*
         Allowed values:
                        33071 CLAMP_TO_EDGE
                        33648 MIRRORED_REPEAT
                        10497 REPEAT
             */
        public int wrapT { get; set; }
        public string name { get; set; }

        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data
    }
}
