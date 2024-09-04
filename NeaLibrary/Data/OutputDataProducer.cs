
using NeaLibrary.DataStructures;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace NeaLibrary.Data
{
    public class OutputDataProducer : IEnumerable<Vector>
    {
        private double safety = 0.01;
        private int look_forward;
        private int look_at;
        private IEnumerable<Vector> input;
        private int[] ValueColumns;
        private int maj_voting;
        private int CHUNK_SIZE = 5;

        Dictionary<int, List<double>> valuable_window = new Dictionary<int, List<double>>();
        List<(Vector,Vector)> window = new List<(Vector,Vector)>();
        Vector prev_bought;
        Vector profit;

        /// <summary>
        /// Initiates a dictionary, the key is which entry in the vector it is. and the list represents the previous values of the time series of vectors
        /// List is more convenient in this case just because it has more function than c# Queue implements
        /// </summary>

        void InitiateValueWindow()
        {
           valuable_window.Clear();
            foreach (int i in ValueColumns)
            {
                valuable_window[i] = new List<double>();
            }
        }

        /// <summary>
        /// This implements the required Queue functionality. Items too far back in time series are discarded
        /// Newest items arrive at the end
        /// </summary>
        /// <param name="v"></param>
        void MoveWindow(Vector v)
        {
            window.Add((v,new Vector(1)));
            window.RemoveAt(0);
            foreach (int i in ValueColumns)
            {
                valuable_window[i].Add(v[i]);
                valuable_window[i].RemoveAt(0);
            }
        }
        
        /// <summary>
        /// Finds peaks and throughs. then labels the corresponding item with a +1 to mean buy (through)
        /// -1 to sell (peak)
        /// 0 neither
        /// </summary>
        void FindPeaks()
        {
            foreach (int i in ValueColumns)
            {
                foreach ((int, double, int) tp in Tools.Tools.ExtractMinimaMaxima(valuable_window[i]))
                {
                    if (tp.Item1 == 1)
                    {//peak  so  sell
                        window[tp.Item3].Item2[0] = -1;
                        if (prev_bought[0] != 0) { profit += window.Last().Item1 - prev_bought; }
                    }else if (tp.Item1 == -1)
                    {//buy
                        window[tp.Item3].Item2[0] = 1;
                        prev_bought = window[tp.Item3].Item1;
                    }
                    else
                    {
                        //if (window[tp.Item3].Item2[0]==0&& window[tp.Item3].Item2[1]==0)
                        //{

                        //}
                        //leave as is as the initial value is 0 anyway
                    }
                }
            }
        }

        void FillWindow(IEnumerator<Vector> iter)
        {
            prev_bought = new Vector(input.First().dimension);
            profit = new Vector(input.First().dimension);
            if (window.Count==0)
            {
                //IEnumerator<Vector> iter = input.GetEnumerator();
                for (int i = 0; i<CHUNK_SIZE; i++)
                {
                    try
                    {
                        iter.MoveNext();
                        window.Add((iter.Current ,new Vector(1)));
                        foreach (int v in ValueColumns)
                        {
                            valuable_window[v].Add(iter.Current[v]);
                        }
                    }catch { }
                }
            }
            else if (window.Count == CHUNK_SIZE)
            {
                // if the window is already filled do nothing 
            }
            else
            {
                //window is bigger than it should be. this wouldnt happen? so do nothing
                throw new Exception("Window for OutputDataProducer overfilled, this cant have happened naturally");
                //Console.WriteLine();
            }
        }

        public IEnumerator<Vector> GetEnumerator() 
        {
            //List<Vector> all_inputs= new List<Vector>();
            //List<Vector> all_outputs= new List<Vector>();
            //Dictionary<int,List<double>> all_valuable = new Dictionary<int,List<double>>();
            //foreach (int i in ValueColumns)
            //{
            //    all_valuable.Add(i, new List<double>());
            //}
            //foreach (Vector v in input)
            //{
            //    all_inputs.Add(v);
            //    foreach (int i in ValueColumns)
            //    {
            //        all_valuable[i].Add(v[i]);
            //    }
            //}
            //foreach (int i in ValueColumns)
            //{//should run just ONCE

            //    foreach ((int, double, int) tp in Tools.Tools.ExtractMinimaMaxima(all_valuable[i]))
            //    {
            //        Vector ret=new Vector(3);
            //        if (tp.Item1==1)
            //        {
            //            //maximum so sell
            //            ret[0] = 1;
            //        }
            //        else if(tp.Item1==-1){
            //            //minimum so buy
            //            ret[1] = 1;
            //        }
            //        else
            //        {
            //            ret[2] = 1; //hold
            //        }
            //        all_outputs.Add(ret);
            //    }
            //}
            //return all_outputs.GetEnumerator();

            

            IEnumerator<Vector> iter = input.GetEnumerator();
            InitiateValueWindow();
            FillWindow(iter);
            FindPeaks();
            //foreach ((Vector,Vector) v in window)
            //{

            //    yield return v.Item2; 
            //}

            for (int i=0;i<window.Count-1;i++) //yield all items upto penultimate
            {
                yield return window[i].Item2;
            }

            //budged is there so we can move the window twice. This way we always yield return the penultimate item in the window. This allows us to determine whether it is a max or min

            //case indicates what happens at the end of the time series

            int case_= 0;
            //bool budged=false;
            while (true)
            {
                try
                {   
                    iter.MoveNext();
                    MoveWindow(iter.Current);
                }
                catch { case_=1; break; }
                //try
                //{
                //    if (!budged)
                //    {
                //        iter.MoveNext();
                //        MoveWindow(iter.Current);
                //    }
                //}
                //catch { case_ = 2;break; }

                FindPeaks(); //determines whether to buy or sell

                yield return window[CHUNK_SIZE - 1 - 1].Item2; //return penultimate item


                //if (!budged)
                //{
                //    yield return window[CHUNK_SIZE - 1 - 1].Item2; //second to last
                //    budged = true;
                //}
                //else
                //{
                //    yield return window[CHUNK_SIZE - 1-1].Item2; //2nd to last but budged????! what this is the same as above..       still return penultimate item, just dont set that it wasw budged
                //}

            }
            //if (case_ == 2)
            //{
            //    FindPeaks();
            //    yield return window[CHUNK_SIZE - 1 - 1].Item2; //second to last
            //}
            yield return window.Last().Item2;  //case 2 can not happen anymore. redundant code. Now we just return last item (going to be 0 for sure since at the end)
            //Console.WriteLine(profit);

            //Queue<Vector> sliding_window = new Queue<Vector>(look_forward);
            //Queue<Vector> output = new Queue<Vector>(look_forward);

            //Dictionary<int, List<double>> sliding_window_=new Dictionary<int, List<double>>();
            //foreach (int i in ValueColumns)
            //{
            //    sliding_window_.Add(i,new List<double>());
            //}

            //Vector profit = new Vector(input.First().dimension);
            //List<Vector> bought = new List<Vector>();
            //List<Vector> sold = new List<Vector>();
            //Vector prev_bought = new Vector(input.First().dimension);
            //Vector prev_sold = new Vector(input.First().dimension);
            //int countr = 0;

            //foreach (Vector v in input)
            //{
            //    Vector ret = new Vector(3);
            //    output.Enqueue(ret);
            //    int state = 2;//2
            //    sliding_window.Enqueue(v);


            //    if (sliding_window.Count == look_forward)
            //    {
            //        Vector avg = sliding_window.Aggregate((x, y) => x + y) / look_forward;

            //        Vector older = sliding_window.Dequeue();
            //        Vector newer = sliding_window.Peek();

            //        int voting_increased = 0;
            //        int voting_decreased = 0;




            //        foreach (int i in ValueColumns)
            //        {
            //            sliding_window_[i].Add(v[i]);
            //            if (sliding_window_[i].Count>CHUNK_SIZE) sliding_window_[i].RemoveAt(0);



            //            //if (look_forward>1) {
            //            //    if (/*avg[i] / older[i] > newer[i]/older[i] &&*/ avg[i] / older[i] > (1+safety) ) //some future period upward gain bigger than 1 day gain?
            //            //    {
            //            //        voting_increased++;
            //            //    }
            //            //    if (/*avg[i] / older[i] > newer[i]/older[i] &&*/ avg[i] / older[i] > (1 + safety)) //some future period upward gain bigger than 1 day gain?
            //            //    {
            //            //        voting_decreased++;
            //            //    }
            //            //}
            //            //else
            //            //{

            //            foreach((bool,double,int) vvals in Tools.Tools.ExtractMinimaMaxima(sliding_window_[i])){
            //                if (vvals.Item1)
            //                {

            //                }
            //                else
            //                {

            //                }
            //            }

            //            if (/*newer[i] / older[i]>(1+safety) &&*/newer[i] / older[i] > 1 && newer[i] > prev_bought[i] * (1 + safety)) //if value appreciated
            //            {
            //                voting_increased++;

            //            }
            //            else if (newer[i] / older[i] < (1 - safety / 2) || newer[i] < prev_bought[i] * (1 - safety / 2))  //if value depreciated since last bought
            //            {
            //                voting_decreased++;
            //            }
            //            //}
            //        }

            //        if (voting_increased > maj_voting && voting_increased >
            //            voting_decreased)
            //        {
            //            state = 0; //buy
            //            prev_bought = newer;
            //            bought.Add(newer);
            //        }
            //        else if (voting_decreased > maj_voting || voting_increased > voting_decreased)//CHANGE HERE maybe a seprate maj voting for decrease
            //        {
            //            state = 1; //sell
            //            prev_sold = newer;
            //            List<Vector> soldd = new List<Vector>();

            //            foreach (Vector selling_at in bought)
            //            {
            //                if (selling_at[1] < newer[1]) { profit += newer - selling_at; soldd.Add(selling_at); }
            //            }
            //            foreach (Vector vsold in soldd)
            //            {
            //                bought.Remove(vsold);
            //            }
            //        }
            //        else
            //        {
            //            state = 2; //hold
            //        }


            //        //if (newer[look_at] > (older[look_at]*(1+safety) ))
            //        //{
            //        //    state = 0; //buy
            //        //}else if (newer[look_at] < (older[look_at] * (1 - safety/2)))
            //        //{
            //        //    state=2;//sell
            //        //}

            //    }

            //    ret[state] = 1;
            //    yield return ret;

            //    //yield return ret;   why twice? accident?
            //}
            ////List<Vector> all = input.ToList();

            ////int index = 0;

            ////List<Vector> window = new List<Vector>();



            //yield break;
        }



        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public OutputDataProducer(double safety, int look_forward,IEnumerable<Vector> input, IEnumerable<int> ValueColumns, int maj_voting) // input old to new
        {
            this.safety = safety;
            this.look_forward=look_forward;
            this.input = input;
            this.ValueColumns = ValueColumns.ToArray();
            this.maj_voting=maj_voting;
        }
    }
}
