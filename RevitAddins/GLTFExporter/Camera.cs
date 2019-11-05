using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    //Camera might or might not needed. 
    class Camera
    {
        public Orthographic orthographic { get; set; }
        public Perspective perspective { get; set; }
        /*might need to turn this property to enum
         * Allowed values:
                            "perspective"
                            "orthographic"
         */
        public string type { get; set; } 

        public string name { get; set; }
        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data
    }
}
