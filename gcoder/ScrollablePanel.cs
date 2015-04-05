using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gcoder
{
    internal class ScrollablePanel : Panel
    {
        public Point panelAnchor;
        public ScrollablePanel()
        {
            this.AutoScroll = true;
            this.DoubleBuffered = true;
            panelAnchor = new Point(0, 0);
        }

        private Bitmap bitmap;
        public Bitmap Bitmap 
        {
            get { return bitmap; }
            set
            {
                bitmap = value;
                if (value == null) this.AutoScrollMinSize = new Size(0, 0);
                else
                {
                    var size = value.Size;
                    using (var gr = this.CreateGraphics())
                    {
                        size.Width = (int)(size.Width * gr.DpiX / value.HorizontalResolution);
                        size.Height = (int)(size.Height * gr.DpiY / value.VerticalResolution);
                    }
                    //this.AutoScrollMinSize = size;
                    //this.VerticalScroll.Value = 0;
                    //this.HorizontalScroll.Value = 0;
                }
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                e.Graphics.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
                if (bitmap != null)
                    e.Graphics.DrawImage(bitmap, panelAnchor.X, panelAnchor.Y);
                base.OnPaint(e);
            }
            catch (Exception)
            {
                MessageBox.Show("Can't render plane.");
            }
        }
    }
}