using NeaLibrary.Data;
using NeaLibrary.DataStructures;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Windows.Forms;
using WindowsFormsApp1;
using WindowsFormsApp1.NEATControls;
using WinFormsApp2.DataControls;
using WinFormsApp2.FFNNControls;
using WinFormsApp2.MainControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Dictionary<Control, TabPage> ControlPageMap = new Dictionary<Control, TabPage>();
        public Form1()
        {
            InitializeComponent();
            ClearControlTabs();

        }

        private void AddControlToNewTab(Control c, bool show = true)
        {
            TabPage newPage = new TabPage(c.Name);
            ControlTabs.TabPages.Insert(0, newPage);
            newPage.Controls.Add(c);
            c.Show();
            ControlPageMap.Add(c, newPage);
            if (show)
            {
                ControlTabs.SelectedTab = newPage;
            }
        }
        private void RemoveControlFromTab(Control c)
        {
            c.Hide();
            TabPage pg = ControlPageMap[c];
            pg.Controls.Remove(c);
            ControlPageMap.Remove(c);
            ControlTabs.TabPages.Remove(pg);
            c.Dispose();
        }
        private void ClearControlTabs()
        {

            ControlTabs.TabPages.Clear();

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void New_Click(object sender, EventArgs e)
        {
            CreateSelector s = new CreateSelector();
            //splitContainer1.Panel2.Controls.Add(s);
            //s.Show();

            s.Name = "Selector";
            AddControlToNewTab(s);



            s.ChoiceMade += new EventHandler(Handle_CreateSelector_Choice);
            void Handle_CreateSelector_Choice(object sender, EventArgs e)
            {
                //MessageBox.Show($"OMG YES u chose {s.Selected}");
                s.Hide();
                switch (s.Selected)
                {
                    case CreateSelector.Choice.NEAT:
                        New_CreateNeat();
                        break;
                    case CreateSelector.Choice.FFNN:
                        New_CreateFFNN();
                        break;
                    case CreateSelector.Choice.DataSet:
                        New_CreateDataSet();
                        break;
                    case CreateSelector.Choice.DataTable:
                        New_CreateDataTable();
                        break;
                    case CreateSelector.Choice.Invalid:
                        //Must have clicked exit no need to do anything
                        break;
                }
                RemoveControlFromTab(s);
            }
        }
        private void New_CreateNeat()
        {
            CreateNEATControl s = new CreateNEATControl();
            AddControlToNewTab(s);
            s.FinishedCreating += new EventHandler(Handle_NeatCreator_Created);
            void Handle_NeatCreator_Created(object sender, EventArgs e)
            {
                RemoveControlFromTab(s);
            }
        }
        private void New_CreateFFNN()
        {
            FFNNCreateControl s = new FFNNCreateControl();
            AddControlToNewTab(s);
            s.FinishedCreating += new EventHandler(Handle_FFNNCreator_Created);
            void Handle_FFNNCreator_Created(object sender, EventArgs e)
            {
                RemoveControlFromTab(s);
            }
        }
        private void New_CreateDataSet()
        {
            DataSetCreateControl s = new DataSetCreateControl();
            AddControlToNewTab(s);
            s.FinishedCreating += new EventHandler(Handle_DatasetCreator_Created);
            void Handle_DatasetCreator_Created(object sender, EventArgs e)
            {
                RemoveControlFromTab(s);
            }
        }


        private void New_CreateDataTable()
        {
            FetchDataControl s = new FetchDataControl();
            s.FinishedCreating += (s, e) =>
            {
                RemoveControlFromTab((FetchDataControl)s!); //dont like the !
            };
            AddControlToNewTab(s);

        }

        private void OpenB_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = false;
                ofd.Filter = " Data Set (*.ads;*.ds;*dbds)|*.ads;*.ds;*.dbds|FFNN (*.ffnn)|*.ffnn|NEAT (*.neat)|*.neat";
                DialogResult result = ofd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(ofd.FileName))
                {
                    //.ads   ArrayDataSet
                    //.ds DataSet
                    //.dbds  DB_DataSet
                    string path = ofd.FileName;
                    string c = ofd.FileName.Split('.').Last();
                    switch (c)
                    {
                        case "ads":
                            DataSetViewer dsva = new DataSetViewer();
                            DataSetViewerEventHandler(dsva);
                            dsva.ProgramaticAssign(path);
                            AddControlToNewTab(dsva);
                            break;
                        case "ds":
                            DataSetViewer dsvb = new DataSetViewer();
                            DataSetViewerEventHandler(dsvb);
                            dsvb.ProgramaticAssign(path);
                            AddControlToNewTab(dsvb);
                            break;
                        case "dbds":
                            DataSetViewer dsvc = new DataSetViewer();
                            DataSetViewerEventHandler(dsvc);
                            dsvc.ProgramaticAssign(path);
                            AddControlToNewTab(dsvc);
                            break;
                        case "ffnn":
                            FFNNControl fnncontrol = new FFNNControl();
                            fnncontrol.FFNN_Programatic_Assign(ofd.FileName);
                            fnncontrol.GraphDisplayRequested += OnGraphDisplayRequested;
                            AddControlToNewTab(fnncontrol);
                            fnncontrol.Close += (s, e) =>
                            {
                                fnncontrol.Hide();
                                RemoveControlFromTab(fnncontrol);
                            };
                            break;
                        case "neat":
                            NEATControl nEATControl = new NEATControl();
                            nEATControl.GraphDisplayRequested += OnGraphDisplayRequested;
                            AddControlToNewTab(nEATControl);
                            nEATControl.Neat_Programatic_Assign(ofd.FileName);
                            nEATControl.Close += (s, e) =>
                            {
                                nEATControl.Hide();
                                RemoveControlFromTab(nEATControl);
                            };
                            break;
                    }

                }
            }
        }
        void OnGraphDisplayRequested(object? s, GraphDisplay e)
        {
            e.Exit += (sender, eventsargs) => RemoveControlFromTab(e);
            AddControlToNewTab(e);
            e.Render();
        }

        private void DataSetViewerEventHandler(DataSetViewer con)
        {
            con.Exit += (s, e) =>
            {
                RemoveControlFromTab(con);
            };
            con.RequestTabForStockChart += (sender, e) =>
            {
                StockChart sc = new StockChart();
                sc.dataset = e;
                AddControlToNewTab(sc);
                sc.InitialiseCharts();
                sc.SetTokens(sc.dataset.tables);
                sc.Exit += (sender, e) =>
                {
                    sc.Hide();
                    RemoveControlFromTab(sc);
                };
            };
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void SettingsB_Click(object sender, EventArgs e)
        {
            SettingsControl a = new SettingsControl();
            a.Exit += (s, e) => { RemoveControlFromTab(a); };
            AddControlToNewTab(a);
        }

        private void PredictionB_Click(object sender, EventArgs e)
        {
            MakePredictionControl s = new MakePredictionControl();
            s.Name = "Prediction";
            s.VectorViewRequested += (s, v) =>
            {
                VectorViewer vectorViewer = new VectorViewer();
                vectorViewer.Display(v);
                vectorViewer.Name = "Vector Viewer";
                AddControlToNewTab(vectorViewer);
                vectorViewer.Show();
                vectorViewer.Close += (snder, ev) =>
                {
                    RemoveControlFromTab(vectorViewer);
                };
            };

            AddControlToNewTab(s);
            s.Show();
            s.Exit += (sender, ev) =>
            {
                RemoveControlFromTab(s);
            };
        }



        private void credits_Click(object sender, EventArgs e)
        {
            //this method code attributed to https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/

            string url = @"https://github.com/SaeedMahar2006";
            try
            {
                Process.Start(url);
            }
            catch
            {
                // comment from source // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                //else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))// not needed this is windows forms
                //{
                //    Process.Start("xdg-open", url);
                //}
                //else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                //{
                //    Process.Start("open", url);
                //}
                else
                {
                    throw;
                }
            }

        }

    }
}