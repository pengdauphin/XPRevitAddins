using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Material
    {
        public string name { get; set; }
        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data

        public PBRMetallicRoughness pbrMetallicRoughness { get; set; }
        public NormalTexture normalTexture { get; set; }

        public OcclusionTexture occlusionTexture { get; set; }

        public EmissiveTexture emissiveTexture { get; set; }
        public int[] emissiveFactor { get; set; }

        public string alphaMode { get; set; }

        public int alphaCutoff { get; set; }
        public bool doubleSided { get; set; }

    }
}
