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

        public Buffer(int ByteLength, string Url)
        {
            byteLength = ByteLength;
            url = Url;
        }
    }
}
