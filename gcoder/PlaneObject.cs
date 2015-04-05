using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gcoder
{
    public class PlaneObject
    {
        public PlaneObject()
        {
            X = 0;
            Y = 0;
        }
        public PlaneObject(float x, float y)
        {
            X = x;
            Y = y;
        }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
