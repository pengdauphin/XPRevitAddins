using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Values
    {
        public int bufferView { get; set; }
        public int byteOffset { get; set; }
        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data

        public Values (int BufferView)
        {
            bufferView = BufferView;
        }
    }
}
