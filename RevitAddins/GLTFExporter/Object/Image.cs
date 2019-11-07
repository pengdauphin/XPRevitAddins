using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Image
    {
        public string uri { get; set; } //Format: uriref

        /*
         Allowed values:
                        "image/jpeg"
                        "image/png"
        */
        public string mimeType { get; set; }
        public int bufferView { get; set; }

        public string name { get; set; }

        public Extension Extensions { get; set; }

        //public typ extras { get; set; } this property is not required, and coulb be any type of data
    }
}
