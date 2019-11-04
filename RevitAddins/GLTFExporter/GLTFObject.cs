using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class GLTFObject
    {
        public Dictionary<string, string> asset { get; set; }
        public Array accessors { get; set; }

        public Array bufferViews { get; set; }
        public List<Buffer> buffers{ get; set; }
        public Array images { get; set; }
        public Array meshes { get; set; }
        public Array materials { get; set; }

        public Array nodes { get; set; }
        public int scene { get; set; }
        public Array scenes { get; set; }
        public Array textures { get; set; }
        public GLTFObject() 
        {
            asset = new Dictionary<string, string>();
        }
    }
}
