using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    //Not applicable for Revit elements
    //reference if needed in future: https://github.com/KhronosGroup/glTF/tree/master/specification/2.0#reference-indices
    class Skin
    {
        public int[] joints { get; set; }

        public Skin (int[] Joints)
        {
            joints = Joints;
        }
    }
}
