using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using NeaLibrary.DataStructures;
namespace WinFormsApp2.MainControls
{
    public partial class GraphDisplay : UserControl
    {
        public event EventHandler Exit;
        Graph? graph;
        Pen blackPen = new Pen(Color.Black);
        Pen redPen = new Pen(Color.Red);
        Brush redBrush = new SolidBrush(Color.Red);
        public GraphDisplay()
        {
            InitializeComponent();
        }
        public void SetGraph(Graph graph)
        {
            this.graph = graph;
        }

        public void Render()
        {
            if (graph == null)
            {

            }
            else
            {
                panel1.Invalidate();
                Update();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            void DrawLine(int to, int from, bool black = false, float penwidth = 1.4f)
            {
                try
                {
                    //graph shouldsnt be null when this is caleld
                    double width = panel1.Width;
                    double height = panel1.Height;
                    //blackPen.Width = 1.4f;
                    int x1 = (int)(graph.NodesCoordinates[to].Item1 * width); //simple truncation
                    int y1 = (int)(graph.NodesCoordinates[to].Item2 * height);
                    int x2 = (int)(graph.NodesCoordinates[from].Item1 * width);
                    int y2 = (int)(graph.NodesCoordinates[from].Item2 * height);

                    Pen p;
                    if (black) { p = blackPen; }
                    else
                    {
                        p = new Pen(colourscale(graph[to, from]));
                    }
                    p.DashStyle = DashStyle.Solid;
                    p.StartCap = LineCap.ArrowAnchor;
                    p.Width = penwidth;
                    e.Graphics.DrawLine(p, new Point(x1, y1), new Point(x2, y2));
                }
                catch { }
            }


            //https://stackoverflow.com/questions/55601338/get-a-color-value-within-a-gradient-based-on-a-value
            //https://stackoverflow.com/questions/3722307/is-there-an-easy-way-to-blend-two-system-drawing-color-values
            Color ColourInterpolate(Color c1, Color c2, double fraction)
            {
                byte r = (byte)(c2.R * fraction + (1 - fraction) * (c1.R));
                byte g = (byte)(c2.G * fraction + (1 - fraction) * (c1.G));
                byte b = (byte)(c2.B * fraction + (1 - fraction) * (c1.B));
                return Color.FromArgb(r, g, b);
            }

            Color colourscale(double value)
            {//red  yellow   green    blue
                if (value < -2)
                {
                    return Color.Red;
                }
                else if (value >= -2 && value < -0.5)
                {
                    return ColourInterpolate(Color.Red, Color.Yellow, (value + 2) / 1.5);   // --2 = +2
                }
                else if (value >= -0.5 && value < 0.5)
                {
                    return ColourInterpolate(Color.Yellow, Color.Green, (value + 0.5));  // - - 0.5     /1
                }
                else if (value >= 0.5 && value < 2)
                {
                    return ColourInterpolate(Color.Green, Color.Blue, (value + 0.5));
                }
                else
                {
                    return Color.Blue;
                }
            }

            //DateTimeOffset.

            void DrawCircle(int node, double r = 4)
            {
                //graph shouldsnt be null when this is caleld
                double width = panel1.Width;
                double height = panel1.Height;
                blackPen.Width = 1;
                try
                {
                    int x1 = (int)(graph.NodesCoordinates[node].Item1 * width); //simple truncation
                    int y1 = (int)(graph.NodesCoordinates[node].Item2 * height);

                    Vector r_v = new Vector(2);
                    r_v[0] = -1;
                    r_v[1] = -1;
                    r_v = (r) * r_v;

                    x1 = (int)(x1 + r_v[0]);
                    y1 = (int)(y1 + r_v[1]);

                    e.Graphics.FillEllipse(redBrush, (int)x1, (int)y1, (int)r * 2, (int)r * 2);
                }
                catch { }
            }
            if (graph != null)
            {
                //foreach (KeyValuePair<int,(double,double)> v in graph.Nodes)
                //{

                //}
                //foreach (var v in graph.Edges())
                //{

                //}

                for (int from = 0; from < graph.NodeCount; from++)
                {
                    DrawCircle(from);
                    for (int to = 0; to < graph.NodeCount; to++)
                    {
                        if (graph.adjacencyMatrix[to, from] != 0) DrawLine(to, from);
                    }
                }
            }
        }

        private void ExitB_Click(object sender, EventArgs e)
        {
            Exit(this, EventArgs.Empty);
        }
    }
}
