using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gcoder
{
    public interface IMovable
    {
        void Move(PlaneObject vector);
    }

    public interface IDrawable
    {
        void Draw(Graphics e, Plane plane);
    }
    public class Tool : PlaneObject, IMovable, IDrawable
    {
        private int diameter;

        public Tool(int diameter)
        {
            this.diameter = diameter;
            X = 0;
            Y = 0;
        }

        public void Draw(Graphics e, Plane plane)
        {
            RectangleF rect = new RectangleF(X * plane.Scale - (diameter*plane.Scale) / 2 + plane.ZeroX,
                Y * plane.Scale - (diameter * plane.Scale) / 2 + plane.ZeroY, 
                diameter*plane.Scale, 
                diameter*plane.Scale);
            e.DrawEllipse(Pens.Red, rect);
        }

        public void Move(PlaneObject newPlace)
        {
            X = newPlace.X;
            Y = newPlace.Y;
        }
    }
}
