using System;
using System.Collections.Generic;
using System.Drawing;

namespace gcoder
{
    public class GCodeInstruction
    {
        protected int line;
        protected string id;
        protected Dictionary<char, float> parameters;

        public GCodeInstruction(string id, Dictionary<char, float> parameters, int line)
        {
            this.line = line;
            this.parameters = parameters;
            this.id = id;
        }

        public Dictionary<char, float> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public string Id
        {
            get { return id; }
        }

        public int Line
        {
            get { return line; }
        }

        public StraightMovement ConvertToG00(Tool tool)
        {
            StraightMovement result;
            if (!parameters.ContainsKey('X') && !parameters.ContainsKey('Y') && !parameters.ContainsKey('Z'))
            {
                throw new Exception();
            }
            if (!parameters.ContainsKey('X'))
                parameters.Add('X', 0);
            if (!parameters.ContainsKey('Y'))
                parameters.Add('Y', 0);
            if (!parameters.ContainsKey('Z'))
                parameters.Add('Z', 0);

            PlaneObject destination = new PlaneObject();
            destination.X = parameters['X'];
            destination.Y = parameters['Y'];
            result = new StraightMovement(destination, tool, Pens.Green);
            return result;
        }

        public StraightMovement ConvertToG01(Tool tool)
        {
            StraightMovement result;
            if (!parameters.ContainsKey('X') && !parameters.ContainsKey('Y') && !parameters.ContainsKey('Z'))
            {
                throw new Exception();
            }
            if (!parameters.ContainsKey('X'))
                parameters.Add('X', 0);
            if (!parameters.ContainsKey('Y'))
                parameters.Add('Y', 0);
            if (!parameters.ContainsKey('Z'))
                parameters.Add('Z', 0);

            PlaneObject destination = new PlaneObject();
            destination.X = parameters['X'];
            destination.Y = parameters['Y'];
            result = new StraightMovement(destination, tool, Pens.Red);
            return result;
        }

        public ArcMovement ConvertToG02(Tool tool)
        {
            ArcMovement result;


            PlaneObject destination = new PlaneObject();
            if (!parameters.ContainsKey('X') && !parameters.ContainsKey('Y'))
            {
                throw new Exception();
            }
            if(!parameters.ContainsKey('X'))
                parameters.Add('X', 0);
            if (!parameters.ContainsKey('Y'))
                parameters.Add('Y', 0);
            destination.X = parameters['X'];
            destination.Y = parameters['Y'];

            //if (!parameters.ContainsKey('I') && !parameters.ContainsKey('J') && !parameters.ContainsKey('R'))
            //{
            //    throw new InvalidParamException(line);
            //}
            if (parameters.ContainsKey('R')) 
                result = new ArcMovement(destination, parameters['R'], tool);
            else if (parameters.ContainsKey('I') || parameters.ContainsKey('J'))
            {
                if (!parameters.ContainsKey('I'))
                    parameters.Add('I', 0);
                if (!parameters.ContainsKey('J'))
                    parameters.Add('J', 0);
                if (!parameters.ContainsKey('K'))
                    parameters.Add('K', 0);
                result = new ArcMovement(destination, parameters['I'], parameters['J'], tool);
            }
            else
            {
                throw new InvalidParamException(line);
            }

            //return null;
            return result;
        }
    }

    public class StraightMovement : IDrawable
    {
        private PlaneObject destinationPoint;
        private PlaneObject startPoint;
        private Pen pen;
        public StraightMovement(PlaneObject destination, Tool tool, Pen color)
        {
            destinationPoint = destination;
            startPoint = tool;
            pen = color;
        }

        public void Execute(Tool tool)
        {
            tool.Move(destinationPoint);
        }
        public void Draw(Graphics e, Plane plane)
        {
            e.DrawLine(pen, startPoint.X*plane.Scale + plane.ZeroX, 
                startPoint.Y*plane.Scale + plane.ZeroY,
                destinationPoint.X * plane.Scale + plane.ZeroX, 
                destinationPoint.Y*plane.Scale + plane.ZeroY);
        }
    }

    public class ArcMovement : IDrawable
    {
        double RadianToDegree(double angle) { return angle * (180.0 / Math.PI); }
        private double startAngle;
        private double endAngle;
        private double radius;
        private PlaneObject RectStart;

        public ArcMovement(PlaneObject destination, float i, float j, Tool tool)
        {
            startAngle = 0;
            endAngle = 180;
            radius = Math.Sqrt(i*i + j*j);
            RectStart = new PlaneObject();
           
            //III ćwiartka - done start
            if (i > 0 && j >= 0)
            {
                RectStart.X = -(tool.X + (float)(radius - i));
                RectStart.Y = -(tool.Y + (float)(radius - j));
                startAngle = 180 + RadianToDegree(Math.Atan(j/i));
            }
            //IV ćwiartka - done start
            if (i <= 0 && j > 0)
            {
                RectStart.X = tool.X - (float)(radius - i);
                RectStart.Y = -(tool.Y + (float)(radius - j));
                startAngle = 270 + RadianToDegree(Math.Atan(Math.Abs(i)/j));
            }
            //I ćwiartka - done start
            if (i < 0 && j <= 0)
            {
                RectStart.X = tool.X - (float)(radius - i);
                RectStart.Y = tool.Y - (float)(radius - j);
                startAngle = RadianToDegree(Math.Atan(Math.Abs(j) / Math.Abs(i)));
            }
            //II ćwiartka - done start
            if (i >= 0 && j < 0)
            {
                RectStart.X = tool.X - (float)(radius - i);
                RectStart.Y = tool.Y - (float)(radius - j);
                startAngle = 90+ RadianToDegree(Math.Atan(i / Math.Abs(j)));
            }
            //jesli gorna krawedz kwadratu jest większa od y endpointa to kończymy wcześniej i idziemy x po prostej
            //jestli gorna krawedz jest mniejsza to lecimy do czubka łuku i sobie radzimy

            //if(destination.Y)
        }
        public ArcMovement(PlaneObject destination, float r, Tool tool)
        {
            startAngle = 0;
            endAngle = 180;
            radius = r;
            RectStart = new PlaneObject();

            
                RectStart.X = -(tool.X + (float)(radius));
                RectStart.Y = -(tool.Y + (float)(radius));
                startAngle = 180;
            
        }

        public void Execute(Tool tool)
        {
            tool.Move(new PlaneObject());
        }
        public void Draw(Graphics e, Plane plane)
        {
            e.DrawArc(Pens.BlueViolet, RectStart.X + plane.ZeroX, RectStart.Y + plane.ZeroY, 2 * (float)radius, 2 * (float)radius, (float)startAngle, -(float)endAngle);
            //e.DrawRectangle(Pens.Brown, RectStart.X + plane.ZeroX, RectStart.Y + plane.ZeroY, 2 * (float)radius, 2 * (float)radius);
            //e.DrawLine(Pens.Brown, plane.ZeroX, plane.ZeroY, 50 + plane.ZeroX, -50 + plane.ZeroY);
        }
    }
}
