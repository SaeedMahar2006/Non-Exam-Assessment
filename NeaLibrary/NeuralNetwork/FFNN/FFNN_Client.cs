using System;
using System.Collections.Concurrent;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using NeaLibrary.Data.Other;
using NeaLibrary.DataStructures;


namespace NeaLibrary.NeuralNetwork.FFNN
{
    [Serializable]
    public class FFNN_Client : INeuralNetwork
    {
        public NNSpecification Specification;

        private double learning_rate;
        public double GetLearningRate() => learning_rate;
        //public double SetLearningRate(double val) { learning_rate = val; }
        private Vector topology;
        public Vector GetTopology() => topology;
        //public Vector SetTopology(Vector topology) { this.topology = topology; }
        private object clientsLock = new object();
        private List<Network> clients;
        public List<Network> GetClients() => clients;
        //public List<Network> SetLearningRate(double val) { learning_rate = val; }

        //public bool Dynamic = true;

        private int generations = 1;
        private double lowest_error = 1;

        private object PauseLock = new object();
        private bool Paused = false;


        //private object LearningRateLock = new object();
        object workingLock = new object();


        [NonSerialized]
        bool _Working = false;
        
        public bool Working { get => _Working; set => _Working = value; }

        //[NonSerialized]
        private object lowestErrLock = new object();

        private Network? bestFFNN;

        [field: NonSerialized]
        public event EventHandler<(int, double)> NextGeneration;



        //ParallelOptions _ParallelSpecifier = new ParallelOptions();
        //CancellationTokenSource cts = new CancellationTokenSource();


        protected void OnNextGeneration((int, double) e)
        {
            EventHandler<(int, double)> handler = NextGeneration;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void SetLearningRate(double rate)
        {
            learning_rate = rate;
            lock (clientsLock)
            {
                foreach (Network n in clients)
                {
                    n.SetLearningRate(rate);
                }
            }
        }

        public void Pause()
        {
            lock (PauseLock)
            {
                Paused = true;
            }
        }
        public void Unpause()
        {
            lock (PauseLock)
            {
                Paused = false;
            }
        }
        public void TogglePause()
        {
            lock (PauseLock)
            {
                Paused = !Paused;
            }
        }
        public void Terminate()
        {
            Paused = true;
            //cts.Cancel();
        }
        public bool GetPause()
        {
            //if (Paused == null) Paused = false ;
            return Paused;
        }

        public Graph ToGraph()
        {
            int n = (int)topology.Aggregate((sum, v) => { sum += v; return sum; });
            Graph g = new Graph(n);
            double BORDER = 0.05;
            double SPAN = 1 - 2 * BORDER;
            double xstep = SPAN / topology.dimension;
            int node = 0;
            Dictionary<(int, int), int> nodeLayerMap = new Dictionary<(int, int), int>();
            for (int layer = 0; layer < topology.dimension; layer++)
            {
                double ystep = SPAN / topology[layer];
                for (int i = 0; i < topology[layer]; i++)
                {
                    g.NodesCoordinates.Add(node, (xstep * layer + BORDER, ystep * i + BORDER));
                    nodeLayerMap.Add((layer, i), node);
                    node++;
                }
            }
            for (int layer = 0; layer < topology.dimension - 1; layer++)
            {
                for (int from = 0; from < topology[layer]; from++)
                {
                    for (int to = 0; to < topology[layer + 1]; to++)
                    {
                        try
                        {
                            g[nodeLayerMap[(layer, from)], nodeLayerMap[(layer + 1, to)]] = clients.First().GetLayers()[layer][to, from];   //layers[0]  matches   0  to  1           n m   m p  =>  n p       cols match first layer, rows match second layer
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            return g;
        }
        [field: NonSerialized]
        IDataSet? ds;

        //public bool IsCategorical { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [field: NonSerialized]
        private Func<Vector, Vector> _CategoryTreshholdFunction = (Vector x) => x;
        //[field: NonSerialized]
        private bool _IsCategorical = false;
        public Func<Vector, Vector> CategoryTreshholdFunction { get => _CategoryTreshholdFunction; set => _CategoryTreshholdFunction=value; }
        public bool IsCategorical { get => _IsCategorical; set => _IsCategorical=value; }
        //public Func<Vector, Vector> CategoryTreshholdFunction { get => (x)=>x; set => CategoryTreshholdFunction=value; }


        public FFNN_Client(Vector Topology, double LearningRate, int Clients, IDataSet? ds = null)
        {
            learning_rate = LearningRate;
            topology = Topology;
            clients = new List<Network>();
            for (int i = 0; i < Clients; i++)
            {
                clients.Add(new Network(topology));
            }
            //this.ds = ds;

            Specification = new NNSpecification((int)topology.First(), (int)topology.Last(), "", ds == null ? null : ds.GetInputMapCache());
            //_ParallelSpecifier.CancellationToken = cts.Token;
            //_ParallelSpecifier.MaxDegreeOfParallelism = System.Environment.ProcessorCount;   //TODO test if this makes things faster

        }

        void UpdateNNSpecification(IDataSet ds)
        {
            if (ds == null) return;
            this.ds = ds;
            Specification.LastTrainedOn = ds.ToString();
            Specification.InputMapCacheDescription = ds.GetInputMapCache();
        }



        /// <summary>
        /// Uses Parallel.ForEach to train each network
        /// </summary>
        /// <param name="backprops_per_iteration"></param>
        /// <param name="dataset"></param>
        /// <exception cref="Exception"></exception>
        private void TrainAll_ParallelForEach(IDataSet dataset, int backprops_per_iteration = 1)
        {
            
            if (Working) return;
            lock (workingLock)
            {
                Working = true;
            }


            double er;

            if (dataset == null && ds == null) throw new Exception("Provide a dataset");
            UpdateNNSpecification(dataset);
            ds = dataset; //comes after the updateNNspecification bc no point updating spec to the same one of same dataset
            generations++;
            lowest_error = 1000000; //TODO add a better way
            lock (clientsLock)
            {
                Parallel.ForEach(clients, //_ParallelSpecifier ,
                    x =>
                    {
                        er = x.Train(ds);
                        if (er < lowest_error)
                        {
                            lock (lowestErrLock)
                            {
                                lowest_error = er;
                                bestFFNN = x;
                            }
                        }
                    }
                );
            }
            OnNextGeneration((generations, lowest_error));
            lock (workingLock)
            {
                Working = false;
            }

        }
        
        public Network GetBest()
        {
            return clients.First(x => x.GetFitness() == clients.Max(x => x.GetFitness()));
        }



        public void Save(string path)
        {
            Tools.Tools.Serialize(path, this);
        }
        public static FFNN_Client Load(string path)
        {
            FFNN_Client? n = null;
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
#pragma warning disable
            n = (FFNN_Client)formatter.Deserialize(fs);
            fs.Close();
            return n;
        }
        public void SetDataset(IDataSet ds)
        {
            this.ds = ds;
        }


        public double Train(IDataSet dataset)
        {
            TrainAll_ParallelForEach(dataset: dataset);
            return lowest_error;
        }
        public double Train(IDataSet dataset, int iterations)
        {
            TrainAll_ParallelForEach(dataset: dataset, backprops_per_iteration:iterations);
            return lowest_error;
        }

        public Vector BestPrediction(Vector input)
        {
            if(IsCategorical)return CategoryTreshholdFunction( GetBest().Forward(input));
            return GetBest().Forward(input);
        }

        public Vector AveragePrediction(Vector input)
        {
            Vector result = new Vector(Specification.OutputDimension);
            foreach(Network n in clients)
            {
                result+=n.Forward(input);
            }result = (1/clients.Count)*result;
            if (IsCategorical) return CategoryTreshholdFunction(result);
            return result;
        }

        public NNSpecification GetNNSpecification()
        {
            return Specification;
        }

        static INeuralNetwork INeuralNetwork.Load(string path)
        {
            return Load(path);
        }

        [OnDeserialized]
        public void onDeserialised(StreamingContext cx)
        {
            Working = false;
        }
    }
}