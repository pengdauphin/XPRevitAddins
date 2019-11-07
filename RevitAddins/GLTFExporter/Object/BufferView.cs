using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class BufferView
    {
        public int buffer { get; set; }
        public int byteOffset { get; set; }
        public int byteLength { get; set; }
        public int byteStride { get; set; }
        public int target { get; set; }
        public string name { get; set; }
        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data

        public BufferView(int Buffer, int ByteLength)
        {
            buffer = Buffer;
            byteLength = ByteLength;
        }
    }
}
