using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class PBRMetallicRoughness
    {
        public int[] baseColorFactor { get; set; } //number [4]

        public BaseColorTexture baseColorTexture { get; set; }

        public int metallicFactor { get; set; }
        public int roughnessFactor { get; set; }

        public MetallicRoughnessTexture metallicRoughnessTexture { get; set; }

        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data
    }
}
