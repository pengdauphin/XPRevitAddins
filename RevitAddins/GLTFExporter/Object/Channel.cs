using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Channel
    {
        public int sampler { get; set; }
        public Target target { get; set; }

        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data

        public Channel (int Sampler, Target Target)
        {

        }
    }
}
