using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Buffer
    {
        public int byteLength { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public Extension extensions { get; set; }
        //public any extras { get; set; }


        public Buffer(int ByteLength)
        {
            byteLength = ByteLength;
            
        }
    }
}
