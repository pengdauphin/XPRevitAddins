using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Accessor
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
        public bool normalized { get; set; }
        public int count { get; set; }
        /*type
        * Allowed values:
                            "SCALAR"
                            "VEC2"
                            "VEC3"
                            "VEC4"
                            "MAT2"
                            "MAT3"
                            "MAT4"
        * */
        public string type { get; set; }
        public Array max { get; set; } //spec require data type as Array of number [1-16]
        public Array min { get; set; } //spec require data type as Array of number [1-16]
        public Sparse sparse { get; set; }

        public string name { get; set; } 
        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data

        public Accessor (int ComponentType, int Count, string Type)
        {
            componentType = ComponentType;
            count = Count;
            type = Type;
        }
    }
}
