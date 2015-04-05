using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gcoder
{
    public class Plane
    {
        private int width;
        private int height;
        private int originWidth;
        private int originHeight;
        private int zeroX;
        private int zeroY;
        private int originZeroX;
        private int originZeroY;

        public Bitmap bitmap;
        private Tool tool;
        private List<GCodeInstruction> gcodes;

        public Plane(int width, int height, int zeroX, int zeroY, List<GCodeInstruction> gcodes)
        {
            originWidth = this.width = width + 40;
            originHeight = this.height = height + 40;
            originZeroX = this.zeroX = zeroX + 20;
            originZeroY = this.zeroY = zeroY + 20;

            this.bitmap = new Bitmap(this.width, this.height);

            this.tool = new Tool(30);
            
            this.gcodes = gcodes;

            this.Scale = 1;
        }

        public void DrawAxis()
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                //graphics.TranslateTransform(0, bitmap.Height);
                //graphics.ScaleTransform(1, -1);

                graphics.DrawLine(Pens.RoyalBlue, 0, zeroY, width, zeroY);
                graphics.DrawLine(Pens.RoyalBlue, zeroX, 0, zeroX, height);
            }
        }
        public void DrawTool()
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                //graphics.TranslateTransform(0, bitmap.Height);
                //graphics.ScaleTransform(1, -1);
                tool.Draw(graphics, this);
            }
        }

        public void Execute(int line)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                //graphics.TranslateTransform(0, bitmap.Height);
                //graphics.ScaleTransform(1, -1);

                var instructionsInLine = gcodes.FindAll(x => x.Line == line);
                foreach (var instruction in instructionsInLine)
                {
                    if (instruction.Id == "00")
                    {
                        var move = instruction.ConvertToG00(tool);
                        move.Draw(graphics, this);
                        move.Execute(tool);
                    }
                    if (instruction.Id == "01")
                    {
                        var move = instruction.ConvertToG01(tool);
                        move.Draw(graphics, this);
                        move.Execute(tool);
                    }
                    if (instruction.Id == "02")
                    {
                        var move = instruction.ConvertToG02(tool);
                        move.Draw(graphics, this);
                        move.Execute(tool);

                    }
                }
            }

        }

        public void Execute()
        {
            for (int i = 1; i <= gcodes.Last().Line; i++)
            {
                Execute(i);
            }
        }


        public void Clear()
        {
            Graphics.FromImage(bitmap).Clear(Color.Transparent);
        }

        public void Reset()
        {
            tool.Move(new PlaneObject());
            Clear();
        }

        public int ZeroX
        {
            get { return zeroX; }
            set { zeroX = value; }
        }

        public int ZeroY
        {
            get { return zeroY; }
            set { zeroY = value; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height;  }
        }

        public float Scale { get; set; }

        public void Zoom()
        {
            //originWidth = this.width = width + 40;
            //originHeight = this.height = height + 40;
            //originZeroX = this.zeroX = zeroX + 20;
            //originZeroY = this.zeroY = zeroY + 20;

            this.width = (int)((originWidth) * Scale) + 40;
            this.height = (int)((originHeight * Scale) + 40);
            ZeroX = (int)((originZeroX) * Scale) + 20;
            ZeroY = (int)((originZeroY) * Scale) + 20;

            bitmap = new Bitmap((int)(width), (int)(height));

            //Plane tempPlane = new Plane((int)(originWidth * Scale), (int)(originHeight * Scale), (int)(originZeroX * Scale), (int)(originZeroY * Scale), gcodes);
            //ZeroX = (int)(tempPlane.ZeroX);
            //ZeroY = (int)(tempPlane.ZeroY);
            //bitmap = tempPlane.bitmap;

            
            //bitmap = 
        }
    }
}
