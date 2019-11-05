using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace XPRevitAddins.GLTFExporter
{
    class GLTF
    {
        public string[] extensionsUsed { get; set; }
        public string[] extensionsRequired { get; set; }
        public Accessor[] accessors { get; set; }
        public Animation[] animations { get; set; }
        public Asset asset { get; set; }
        public Buffer[] buffers { get; set; }
        public BufferView[] bufferViews { get; set; }

        public Camera[] cameras { get; set; }

        public Image[] images { get; set; }

        public Material[] materials { get; set; }

        public Mesh[] meshes { get; set; }

        public List<Node> nodes { get; set; }

        public Sampler[] samplers { get; set; }

        public int scene { get; set; }

        public List<Scene> scenes { get; set; }
        public Skin[] skins { get; set; }

        public Texture[] textures { get; set; }

        public Extension extensions { get; set; }

        //public typ extras { get; set; } this property is not required, and coulb be any type of data

        //Additional properties are allowed.

        public GLTF(Asset Asset)
        {
            //require property for GLTF init
            asset = Asset;
            scene = 0;
            
            
        }


        
    }


}
