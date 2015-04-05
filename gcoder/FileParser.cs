using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Serialization;

namespace gcoder
{
    internal class FileParser
    {
        private List<string> lines;
        private char[] parametersArray = {'X', 'Y', 'Z', 'A', 'B', 'C', 'I', 'J', 'K', 'R'};

        private float minimalX, maximalX;
        private float minimalY, maximalY;
        private float drawingWidth, drawingHeight;
        private string path;
        public float MinimalX
        {
            get { return minimalX; }
        }

        public float MaximalX
        {
            get { return maximalX; }
        }

        public float MinimalY
        {
            get { return minimalY; }
        }

        public float MaximalY
        {
            get { return maximalY; }
        }

        public float DrawingHeight
        {
            get { return drawingHeight; }
        }

        public float DrawingWidth
        {
            get { return drawingWidth; }
        }

        public List<string> Lines
        {
            get { return lines; }
        }

        public FileParser(string path)
        {
            this.path = path;
            minimalX = 0;
            maximalX = 0;
            drawingWidth = 0;
            drawingHeight = 0;

            lines = new List<string>();

            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                lines.Add(line);

                if (lines.Count > 10000) throw new TooBigFileException();
            }

            fs.Close();
            sr.Close();
        }

        public List<GCodeInstruction> Parse()
        {
            List<GCodeInstruction> instructions = new List<GCodeInstruction>();
            int lineNumber = 1;
            try
            {
                foreach (var line in lines)
                {
                    var gcodes = Regex.Split(line, @"([G][0-9 A-FH-Z.-]+)").Where(s => s != String.Empty);

                    foreach (var gcode in gcodes) //(G23 X234 Y324) (G24)(G34)(G11)
                    {
                        GCodeInstruction instruction;
                        string id = gcode.Substring(1, 2);

                        Dictionary<char, float> parametersDict = new Dictionary<char, float>();

                        var parameters = gcode.Split(new[] {' ', '\n'}, StringSplitOptions.RemoveEmptyEntries);

                        //weź parametry
                        foreach (var parameter in parameters)
                        {
                            if (parametersArray.Contains(parameter[0]))
                            {
                                parametersDict.Add(parameter[0],
                                    float.Parse(parameter.Substring(1), CultureInfo.InvariantCulture));
                            }
                        }

                        instruction = new GCodeInstruction(id, parametersDict, lineNumber);
                        instructions.Add(instruction);
                    }

                    lineNumber++;
                }
            }
            catch (Exception exception)
            {
                throw new FileLoadFailureException("Error parsing a file. Check line number " + lineNumber);
            }

            if(instructions.Count == 0)
                throw new EmptyFileException(path);

            CalculateDrawingSize(instructions);
            return instructions;
        }

        private void CalculateDrawingSize(List<GCodeInstruction> program)
        {
            foreach (var item in program)
            {
                if (item.Parameters != null && item.Parameters.Count != 0)
                {
                    if (item.Parameters.ContainsKey('X'))
                    {
                        if (item.Parameters['X'] < minimalX)
                            minimalX = item.Parameters['X'];
                        if (item.Parameters['X'] > maximalX)
                            maximalX = item.Parameters['X'];
                    }
                    if (item.Parameters.ContainsKey('Y'))
                    {
                        if (item.Parameters['Y'] < minimalY)
                            minimalY = item.Parameters['Y'];
                        if (item.Parameters['Y'] > maximalY)
                            maximalY = item.Parameters['Y'];
                    }
                }
            }

            drawingWidth = Math.Abs(minimalX - maximalX);
            drawingHeight = Math.Abs(minimalY - maximalY);
        }

        public void AddLine(GCodeInstruction instruction)
        {
            string line = "G";
            line += instruction.Id + " ";
            foreach (var parameter in instruction.Parameters)
            {
                line += parameter.Key.ToString(CultureInfo.InvariantCulture) + parameter.Value.ToString(CultureInfo.InvariantCulture) + " ";
            }

            lines.Insert(instruction.Line-1, line);

        }

        public void SaveFile(string path)
        {
            FileStream fs = new FileStream(path,FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);

            foreach (var line in lines)
            {
                sw.WriteLine(line);
            }

            sw.Close();
            fs.Close();
        }
    }
}