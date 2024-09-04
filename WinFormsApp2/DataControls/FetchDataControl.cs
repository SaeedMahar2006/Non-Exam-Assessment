
using NeaLibrary.Data;
using NeaLibrary.Data.Other;
//using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace WinFormsApp2.DataControls
{
    public partial class FetchDataControl : UserControl
    {
        MultiMap<int, string> sourcesInfo = new MultiMap<int, string>();
        Regex parameterMatcher = new Regex(@"\{(0|[1-9][0-9]*)\}"); //tested and verified on regex storm net tester
        //matches curly brackets with natural number including 0 inside. does NOT match {000123} which is correct
        //does NOT match {-123} which is correct
        Dictionary<string, string> ArgumentEditorControlName_to_Values = new Dictionary<string, string>();
        Dictionary<string, ArgumentEditorControl> CurlyBracesFromParameters_to_ArgumentEditorControl = new Dictionary<string, ArgumentEditorControl>();
        string empty_request;
        string final;

        int source;
        List<string> for_worker_args = new List<string>();

        List<BackgroundWorker> workers = new List<BackgroundWorker>();
        object manageWorkersLock = new object();

        public event EventHandler FinishedCreating;

        //BackgroundWorker fetcher_worker = new BackgroundWorker();
        //Queue<>
        public FetchDataControl()
        {
            InitializeComponent();
            loadSources();
        }

        void loadSources()
        {
            sourcesInfo.Clear();
            SourceSelectorUpDown.Items.Clear();
            SQL_Driver sQL_Driver = new SQL_Driver(NeaLibrary.Tools.Tools.GetGlobalVar("db_dir"));
            using (SQLiteDataReader r = SQL_Driver.Query(sQL_Driver.conn, "SELECT * FROM Sources"))
            {
                while (r.Read())
                {
                    try
                    {
                        int sourceId = r.GetInt32(0);
                        sourcesInfo.Add(sourceId);
                        for (int i = 1; i < r.FieldCount; i++)
                        {
                            //Type t = r.GetFieldType(i);
                            object v = r.GetValue(i);
                            sourcesInfo.Add(sourceId, v.ToString()); // sources v should never be null, ignore warning
                        }
                        SourceSelectorUpDown.Items.Add(sourceId);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        private void OnValueChnaged(object sender, EventArgs e)
        {
            final = empty_request;
            //values[((ArgumentEditorControl)sender).Name] = ((ArgumentEditorControl)sender).GetInfo();
            foreach (string k in ArgumentEditorControlName_to_Values.Keys)
            {
                ArgumentEditorControlName_to_Values[k] = ((ArgumentEditorControl)CurlyBracesFromParameters_to_ArgumentEditorControl[k]).GetInfo();
                if (ArgumentEditorControlName_to_Values[k] != "") final = final.Replace(k, ArgumentEditorControlName_to_Values[k]);
            }
            richTextBox1.Text = final.ToString();
            richTextBox1.Invalidate();
            for_worker_args.Clear();
            for (int i = 0; i < ArgumentEditorControlName_to_Values.Count; i++)
            {
                try
                {
                    for_worker_args.Add(ArgumentEditorControlName_to_Values[$"{{{i}}}"]);
                }
                catch (KeyNotFoundException)
                {
                    break;
                }
            }
        }

        private void SourceSelectorUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            CurlyBracesFromParameters_to_ArgumentEditorControl.Clear();
            //selected item can not be null if this called
            if (SourceSelectorUpDown.SelectedItem == null) throw new Exception("No item is selected");
            source = (int)SourceSelectorUpDown.SelectedItem;
            ArgumentP.Controls.Clear();
            ArgumentEditorControlName_to_Values.Clear();
            ArgumentP.RowCount = 1;
            ArgumentP.ColumnCount = 1;
            string sourcepinfo = sourcesInfo[(int)SourceSelectorUpDown.SelectedItem][7];
            Dictionary<int, SourceParameterInfo> sp = new Dictionary<int, SourceParameterInfo>();
            List<int> covered_args = new List<int>();
            if (sourcepinfo != "{}")
            {
                JObject temp = JObject.Parse(sourcepinfo);
                foreach (KeyValuePair<string, JToken?> o in temp)
                {
                    //key should be the parameter position
                    try
                    {
                        if (o.Value == null) continue; //the try loops should save us but defensive programming is good 
                        SourceParameterInfo t = o.Value.ToObject<SourceParameterInfo>(); //warning if he item can not be converted, if thats the case exception is handled by try statemnet
                        sp.Add(Convert.ToInt32(o.Key), t);
                        //control_tracker
                    }
                    catch { }

                }
            }
            foreach (Match m in parameterMatcher.Matches(sourcesInfo[(int)SourceSelectorUpDown.SelectedItem][1]))
            {
                SourceParameterInfo DEFAULT = new SourceParameterInfo();
                DEFAULT.Name = m.Value;
                DEFAULT.IsRegex = false;
                DEFAULT.IsCategorical = false;
                DEFAULT.Regex = "";
                DEFAULT.Categories = new string[0];
                ArgumentEditorControl a = new ArgumentEditorControl();
                //a.ValueChanged += OnValueChnaged;
                ArgumentP.RowCount++;
                int n = Convert.ToInt32(m.Value.Substring(1, m.Value.Length - 2));
                if (sp.ContainsKey(n) && !covered_args.Contains(n))
                {
                    a.setArgumentLabelText($"{m.Value} => {sp[n].Name}");
                    a.SetInfo(sp[n]);
                    covered_args.Add(n);

                    a.Name = m.Value;
                    ArgumentEditorControlName_to_Values.Add(a.Name, "");
                    ArgumentP.Controls.Add(a, 0, ArgumentP.RowCount - 1);
                    a.Show();
                    a.ValueChanged += OnValueChnaged;
                    CurlyBracesFromParameters_to_ArgumentEditorControl.Add(m.Value, a);
                }
                else if (!sp.ContainsKey(n) && !covered_args.Contains(n))
                {
                    a.setArgumentLabelText(m.Value);
                    a.SetInfo(DEFAULT);
                    covered_args.Add(n);

                    a.Name = m.Value;
                    ArgumentEditorControlName_to_Values.Add(a.Name, "");
                    ArgumentP.Controls.Add(a, 0, ArgumentP.RowCount - 1);
                    a.Show();
                    a.ValueChanged += OnValueChnaged;
                    CurlyBracesFromParameters_to_ArgumentEditorControl.Add(m.Value, a);
                }
                //if (!covered_args.Contains(n))
                //{

                //}
            }
            Label label = new Label();
            label.AutoSize = true;
            label.UseMnemonic = false;
            label.Text = String.Join("", sourcesInfo[(int)SourceSelectorUpDown.SelectedItem][0], sourcesInfo[(int)SourceSelectorUpDown.SelectedItem][1]);
            empty_request = String.Join("", sourcesInfo[(int)SourceSelectorUpDown.SelectedItem][0], sourcesInfo[(int)SourceSelectorUpDown.SelectedItem][1]);
            ArgumentP.Controls.Add(label, 0, 0);
            label.Show();
            ArgumentP.RowStyles.Clear();
        }

        BackgroundWorker ManageWorkers()
        {
            lock (manageWorkersLock)
            {
                List<BackgroundWorker> to_free = new List<BackgroundWorker>();
                bool one_unbusy = false;
                BackgroundWorker worker = null;
                if (workers.Count != 0)
                {
                    foreach (BackgroundWorker w in workers)
                    {
                        if (w.IsBusy)
                        {

                        }
                        else
                        {
                            if (one_unbusy)
                            {
                                to_free.Add(w);
                            }
                            else
                            {
                                worker = w;
                            }
                            one_unbusy = true;
                        }
                    }
                    foreach (BackgroundWorker w in to_free)
                    {
                        w.Dispose();
                        workers.Remove(w);
                    }
                }
                if (!one_unbusy)
                {
                    worker = new BackgroundWorker();
                    workers.Add(worker);
                }
                return worker!; //shouldnt be null by this point
            }
        }

        private void SubmitB_Click(object sender, EventArgs e)
        {
            ManageWorkers();

            BackgroundWorker fetcher_worker = ManageWorkers();
            fetcher_worker.DoWork += workerWork;
            fetcher_worker.ProgressChanged += (sender, e) =>
            {
                //CandleChart = e.Result as Chart;

            };
            fetcher_worker.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Error != null)
                {
                    MessageBox.Show("Error encountered while fetching: " + e.Error.ToString());
                }
            };

            if (!fetcher_worker.IsBusy)
            {
                //SubmitB.Enabled = false;
                fetcher_worker.RunWorkerAsync(argument: new Tuple<int, List<string>, bool>(source, for_worker_args, UseCalcCB.Checked));
            }
            else MessageBox.Show("Worker busy before even assigned work, you can't have reached this point");
        }
        void workerWork(object s, DoWorkEventArgs e)
        {
            Tuple<int, List<string>, bool> h = e.Argument as Tuple<int, List<string>, bool>;
            NeaLibrary.Data.Data_Handler dh = new Data_Handler();
            string? table;
            try { table = dh.Fetch(h.Item1, h.Item2.ToArray()); }
            catch (NullReferenceException ex)
            {
                throw new Exception("Null Reference Encountered, perhaps this is not a valid request." +
                    "\nPlease check input" + ex.ToString());
            }
            if (h.Item3)
            {
                //calc
                try
                {
                    //shouldnt be by this point but check so compiler is happy
                    if (table != null) Calculator.Calculate(table);
                }
                catch (Exception ex)
                {
                    throw new Exception("Calculator error. Other values were saved." +
                    "\nPlease check DB and Calculator" + ex.ToString());
                }
            }
        }



        private void ExitB_Click(object sender, EventArgs e)
        {
            lock (manageWorkersLock)
            {

                if (workers.Any((worker) => worker.IsBusy)) { MessageBox.Show("Unfinished workers"); return; }
                try { FinishedCreating(this, EventArgs.Empty); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
