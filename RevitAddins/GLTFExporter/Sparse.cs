using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPRevitAddins.GLTFExporter
{
    class Sparse
    {
        public int count { get; set; }

        public Values values { get; set; }
        public Indices indices { get; set; }
        public Extension extensions { get; set; }
        //public any extraa { get; set; } this property is not required, and the type could be any


        public Sparse(int Count, Indices Indices, Values Values)
        {
            count = Count;
            indices = Indices;
            values = Values;
        }
    }
}
