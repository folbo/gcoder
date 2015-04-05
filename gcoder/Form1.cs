using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace gcoder
{
    public partial class Form1 : Form
    {
        public enum ApplicationMode
        {
            ViewMode,
            EditMode
        };

        public ApplicationMode CurrentMode { get; set; }

        private FileParser parser;
        private Plane plane;

        private Point? mouseStartPoint;

        private bool isScaled;

        public Form1()
        {
            InitializeComponent();
            dragRadioButton.Checked = true;
            CurrentMode = ApplicationMode.ViewMode;
            //renderPanel.Hide();



            renderPanel.MouseMove += renderPanel_MouseMove;
            renderPanel.MouseClick += renderPanel_MouseClick;
            renderPanel.MouseWheel += renderPanel_MouseWheel;
            renderPanel.MouseHover += renderPanel_MouseHover;
            this.Resize += OnResize;
            this.ClientSizeChanged += OnResize;

            isScaled = false;

            
        }

        void renderPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if(CurrentMode == ApplicationMode.EditMode && plane != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    var oldScale = plane.Scale;

                    var parameters = new Dictionary<char, float>();
                    parameters.Add('X', (e.X - plane.ZeroX - renderPanel.panelAnchor.X) / oldScale);
                    parameters.Add('Y', (e.Y - plane.ZeroY - renderPanel.panelAnchor.Y )/oldScale); //- bo symetria układu współrzędnych względem osi x
                    var newInstruction = new GCodeInstruction("01", parameters, listBox1.SelectedIndex + 2);
                    parser.AddLine(newInstruction);

                    var gcodes = parser.Parse();
                    
                    plane = new Plane((int) parser.DrawingWidth, (int) parser.DrawingHeight,
                        (int) Math.Abs(parser.MinimalX), (int) Math.Abs(parser.MinimalY), gcodes);

                    plane.Scale = oldScale;
                    plane.Zoom();

                    renderPanel.Bitmap = plane.bitmap;
                    var newSelectedItem = listBox1.SelectedIndex + 1;
                    
                    plane.DrawAxis();
                    plane.Execute();

                    listBox1.Items.Insert(newSelectedItem, parser.Lines[newSelectedItem]);

                    listBox1.SelectedIndex = newSelectedItem;
                    renderPanel.Refresh();
                }
            }
        }

        void renderPanel_MouseHover(object sender, EventArgs e)
        {
            var asd = sender as ScrollablePanel;
            if(asd!=null)
                asd.Focus();
        }

        private void OnResize(object sender, EventArgs eventArgs)
        {
            tableLayoutPanel1.Width = ClientSize.Width - panel3.Width;
            tableLayoutPanel1.Height = ClientSize.Height - panel1.Height;
            isScaled = false;
        }

        private Point? clickPosition;
        void renderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (CurrentMode == ApplicationMode.ViewMode && plane != null)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (!clickPosition.HasValue)
                        {
                            clickPosition = e.Location;
                        }
                        else
                        {
                            Point offset = new Point(e.Location.X - clickPosition.Value.X,
                                e.Location.Y - clickPosition.Value.Y);
                            Debug.WriteLine(offset.X + " " + offset.Y);
                            renderPanel.panelAnchor.X += offset.X;
                            renderPanel.panelAnchor.Y += offset.Y;
                            clickPosition = e.Location;
                            renderPanel.Refresh();
                            isScaled = false;
                        }
                    }
                    else
                    {
                        clickPosition = null;
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Cannot move drawing!");
            }
            
        }
        
        void renderPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta > 0)
                {
                    renderPanel.panelAnchor.X = (int)(-0.1*e.Location.X + 1*renderPanel.panelAnchor.X);
                    renderPanel.panelAnchor.Y = (int)(-0.1*e.Location.Y + 1*renderPanel.panelAnchor.Y);
                    plane.Scale += 0.1f;
                }
                else
                {
                    renderPanel.panelAnchor.X = (int)(0.1 * e.Location.X + 1 * renderPanel.panelAnchor.X);
                    renderPanel.panelAnchor.Y = (int)(0.1 * e.Location.Y + 1 * renderPanel.panelAnchor.Y);
                    plane.Scale -= 0.1f;
                    if (plane.Scale < 0.1)
                        plane.Scale = 0.1f;
                }


                //.panelAnchor.X = (int)(e.Location.X - renderPanel.panelAnchor.X - (e.Location.X - renderPanel.panelAnchor.X) * plane.Scale);
               // renderPanel.panelAnchor.Y = (int)(e.Location.Y - renderPanel.panelAnchor.Y - (e.Location.Y - renderPanel.panelAnchor.Y) * plane.Scale);

                plane.Zoom();
                Debug.WriteLine(renderPanel.panelAnchor.X + " "+ renderPanel.panelAnchor.Y);
                plane.DrawAxis();
                renderPanel.Bitmap = plane.bitmap;
                listBox1_SelectedIndexChanged(sender, e);
                isScaled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Zoom scale is invalid.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    string path = openFileDialog1.FileName;
                    parser = new FileParser(path);
                    var gcodes = parser.Parse();
                    plane = new Plane((int) parser.DrawingWidth, (int) parser.DrawingHeight,
                        (int) Math.Abs(parser.MinimalX), (int) Math.Abs(parser.MinimalY), gcodes);

                    renderPanel.Bitmap = plane.bitmap;

                    //renderPanel.Show();

                    plane.DrawAxis();
                    plane.Execute();
                    listBox1.Items.Clear();
                    foreach (var line in parser.Lines)
                    {
                        listBox1.Items.Add(line);
                    }
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;

                    fileOpenedLabel.Text = "Opened " + path;
                }
                catch (FileLoadFailureException exception)
                {
                    MessageBox.Show(exception.Message);
                }
                catch (EmptyFileException exception)
                {
                    MessageBox.Show(exception.Message);
                }
                catch (FileNotFoundException exception)
                {
                    MessageBox.Show("File not found.");
                }
                catch (InvalidParamException exception)
                {
                    MessageBox.Show(exception.Message);
                }
                catch (TooBigFileException exception)
                {
                    MessageBox.Show(exception.Message);
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong while preparing file.");
                }
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            //parser.Parse();
            try
            {
                //var gcodes = parser.Parse();
               // plane = new Plane((int) parser.DrawingWidth, (int) parser.DrawingHeight, (int) Math.Abs(parser.MinimalX),
                    //(int) Math.Abs(parser.MinimalY), gcodes);
                //renderPanel.Bitmap = plane.bitmap;

                listBox1.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("No File Loaded!");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            plane.Reset();
            plane.DrawAxis();
            for (int i = 1; i <= listBox1.SelectedIndex+1; i++)
                plane.Execute(i);
            plane.DrawTool();
            renderPanel.Refresh();
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
                listBox1.SelectedIndex--;
        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < listBox1.Items.Count-1)
                listBox1.SelectedIndex++;
        }

        
        private void runButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count == 0)
                {
                    throw new Exception();
                }
                if (timer1.Enabled)
                {
                    runButton.Text = "Run";
                    timer1.Stop();
                }
                else
                {
                    runButton.Text = "Pause";
                    timer1.Start();
                    
                    if (listBox1.Items.Count == listBox1.SelectedIndex + 1)
                    {
                        listBox1.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No File Loaded!");
            }
            /*for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                Thread.Sleep(40);
            }*/
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != listBox1.SelectedIndex +1)
            {
                listBox1.SelectedIndex++;
            }
            else
            {
                runButton.Text = "Run";
                timer1.Stop();
            }
        }

        private void autosizeButton_Click(object sender, EventArgs e)
        {
            if (plane == null) return;
            if (isScaled == true) return;
            renderPanel.panelAnchor.X = 0;
            renderPanel.panelAnchor.Y = 0;
            if(plane.Width > renderPanel.Width || plane.Height > renderPanel.Height)
            {
                while (plane.Width > renderPanel.Width || plane.Height > renderPanel.Height)
                {
                    plane.Scale -= 0.01f;


                    plane.Zoom();
                    plane.DrawAxis();
                    renderPanel.Bitmap = plane.bitmap;
                    listBox1_SelectedIndexChanged(sender, e);
                }
                plane.DrawAxis();
                renderPanel.Bitmap = plane.bitmap;
                listBox1_SelectedIndexChanged(sender, e);
                isScaled = true;
            }

            else if (plane.Width < renderPanel.Width || plane.Height < renderPanel.Height)
            {
                while (plane.Width < renderPanel.Width && plane.Height < renderPanel.Height)
                {
                    plane.Scale += 0.01f;

                    plane.Zoom();
                    plane.DrawAxis();
                    renderPanel.Bitmap = plane.bitmap;
                    listBox1_SelectedIndexChanged(sender, e);
                }
                plane.DrawAxis();
                renderPanel.Bitmap = plane.bitmap;
                listBox1_SelectedIndexChanged(sender, e);
                isScaled = true;
            }

            //plane.Zoom();
            
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result =  saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var path = saveFileDialog1.FileName;
                    parser.SaveFile(path);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No file to save! First open or create one!");
            }

        }

        private void dragRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (dragRadioButton.Checked)
            {
                CurrentMode = ApplicationMode.ViewMode;
            }
            else
            {
                CurrentMode = ApplicationMode.EditMode;
            }
        }
    }
}
