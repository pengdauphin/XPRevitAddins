using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Orthographic
    {
        public float xmag { get; set; }
        public float ymag { get; set; }
        public float zfar { get; set; }
        public float znear { get; set; }


        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data

        public Orthographic (float Xmag, float Ymag, float Zfar, float Znear)
        {
            xmag = Xmag;
            ymag = Ymag;
            zfar = Zfar;
            znear = Znear;
        }
    }
}
