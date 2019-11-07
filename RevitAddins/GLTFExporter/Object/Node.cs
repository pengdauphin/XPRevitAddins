using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Node
    {

        /// <summary>
        /// Might need to conver this class to dynamic object
        /// </summary>
        //public int camera { get; set; } = 0; //will fix later to get camera from view
        public List<int> children { get; set; }

        //public int skin { get; set; } = 0; // not appicable to Revit elements. Skip for this code
        public List<float> matrix { get; set; }
        public int mesh { get; set; } = 0;
        public List<float> rotation { get; set; }

        public List<float> scale { get; set; }
        public List<float> translation { get; set; }
        public List<float> weights { get; set; }
        public string name { get; set; }
        public Extension extensions { get; set; }
        //public typ extras { get; set; } this property is not required, and coulb be any type of data
    }
}
