using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Perspective
    {
        public float aspectRatio { get; set; }
        public float yfov { get; set; }
        public float zfar { get; set; }
        public float znear { get; set; }

        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data

        public Perspective ( float Yfov, float Znear)
        {
            yfov = Yfov;
            znear = Znear;
        }
    }
}
