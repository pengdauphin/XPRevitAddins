using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Indices
    {
        public int bufferView { get; set; } //Minimum: >= 0
        public int byteOffset { get; set; } //Minimum: >= 0
        /*component type
         * Allowed values:
                            5120 BYTE
                            5121 UNSIGNED_BYTE
                            5122 SHORT
                            5123 UNSIGNED_SHORT
                            5125 UNSIGNED_INT
                            5126 FLOAT
         * */
        public int componentType { get; set; } //
        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data

        public Indices(int BufferView, int ComponentType)
        {
            bufferView = BufferView;
            componentType = ComponentType;
        }
    }
}
