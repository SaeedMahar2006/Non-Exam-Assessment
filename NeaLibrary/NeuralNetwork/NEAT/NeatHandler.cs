using NeaLibrary.Data;
using NeaLibrary.Data.Other;
using NeaLibrary.DataStructures;
using System;
using System.CodeDom;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace NeaLibrary.NeuralNetwork.NEAT
{
    [Serializable]
    public class NEAT_Handler : INeuralNetwork
    {
        public Neat? client;
        
        public NNSpecification Specification;

        //in case weird GUI glitch and user does change multiple/same value too fast and cause corruption
        object ParameterLock = new object();
        

        private bool working = false;

        public bool GetWorkingStatus() => working;
        public void SetWorkingStatus(bool s) => working = s;
        bool isCategorical;

        bool INeuralNetwork.IsCategorical { get => isCategorical; set => isCategorical = value; }

        [field: NonSerialized]
        Func<Vector, Vector> categoryThreshholdFunc = (x)=>x.PutThrough(Tools.Tools.SimpleTreshholdFunction);



        // reference to https://stackoverflow.com/questions/7693391/nonserialized-on-property

        //[: NonSerialized]
        Func<Vector, Vector> INeuralNetwork.CategoryTreshholdFunction { get => categoryThreshholdFunc; set => categoryThreshholdFunc = value; }

        [field: NonSerialized]
        public event EventHandler<(int, double)>? NextGeneration;

        [field: NonSerialized]
        public IDataSet? dataset;



        public NEAT_Handler(int input, int output, int n_creatures)
        {
            client = new Neat(input,output,n_creatures);
            Specification = new NNSpecification(client.Input, client.Output, null, null);
        }

        /// <summary>
        /// Actions valid for change parameter method
        /// </summary>
        public enum Action
        {
            //weight_shift_strength,
            //weight_random_strength,
            probability_mutate_link,
            probability_mutate_node,
            probability_mutate_weight_shift,
            probability_mutate_weight_random,
            probability_mutate_link_toggle,
            specie_distance,
            kill_rate,
            c1,
            c2,
            c3,

            weight_shift_strength,

            creature_count

            //weight_shift_strength_decrease,
            //weight_random_strength_decrease,
            //probability_mutate_link_decrease,
            //probability_mutate_node_decrease,
            //probability_mutate_weight_shift_decrease,
            //probability_mutate_weight_random_decrease,
            //probability_mutate_link_toggle_decrease,
            //specie_distance_decrease,
            //kill_rate_decrease,

            //pause,
            //start
        }


        /// <summary>
        /// method to change the parameters using the Action enum
        /// </summary>
        /// <param name="action"></param>
        /// <param name="val"></param>
        public void ChangeParameter(Action action, double val)
        {
            if (client == null) return;
            lock (ParameterLock) {
                switch (action)
                {
                    case Action.probability_mutate_weight_shift:
                        client.weight_shift_strength = val;
                        break;
                    case Action.probability_mutate_weight_random:
                        client.probability_mutate_weight_random = val;
                        break;
                    case Action.probability_mutate_link:
                        client.probability_mutate_link = val;
                        break;
                    case Action.probability_mutate_node:
                        client.probability_mutate_node = val;
                        break;

                    case Action.specie_distance:
                        client.specie_distance = val;
                        break;
                    case Action.kill_rate:
                        client.kill_rate = val;
                        break;

                    case Action.c1:
                        client.c1 = val;
                        break;
                    case Action.c2:
                        client.c2 = val;
                        break;
                    case Action.c3:
                        client.c3 = val;
                        break;
                    case Action.weight_shift_strength:
                        client.weight_shift_strength = val;
                        break;

                    case Action.creature_count:
                        client.max_clients = (int)val;
                        break;
                }
            }


        }



        /// <summary>
        /// the private implemetation for the exposed interface method
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="iters"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /*async Task<double>*/private  double _Train(IDataSet dataset/*double target_acc,*/, int iters=int.MaxValue )
        {
            if (client == null) throw new Exception("No neural network");
            working = true;
            int i = 0;
            double acc = 0;
            //bool c1 = false;  redundant. experimenting with momentum
            //bool c2 = false;

            if (dataset != null)
            {
                if (dataset.First().Item1.dimension != client.Input || dataset.First().Item2.dimension != client.Output)
                {
                    throw new ArgumentException("Different dimensionality input or output");
                }
            }
            else
            {
                throw new ArgumentNullException("Dataset not given");
            }

            Specification.InputMapCacheDescription = dataset.GetInputMapCache();
            Specification.LastTrainedOn = dataset.GetHashCode().ToString();

            do
            {
                client.Train(dataset);
                Genotype best_guy = client.GetBest();
                acc = best_guy.RateFitness(dataset.RandomBatch(100));
                client.Best_Dead.Add(best_guy);
                if (client.Best_Dead.Count > 10) { client.Best_Dead.Remove(client.Best_Dead.Min()); }
                i++;
                //if (!c1 && acc > target_acc * 0.9)
                //{
                //    Console.WriteLine("Change 1");
                //    c1 = true;
                //    client.probability_mutate_link *= 0.5;
                //    client.probability_mutate_node *= 0.5;
                //    client.probability_mutate_weight_shift *= 3;
                //    client.specie_distance *= 0.8;
                //}
                //if (!c2 && acc > target_acc * 0.95)
                //{
                //    Console.WriteLine("Change 2");
                //    c2 = true;
                //    client.probability_mutate_link *= 0.2;
                //    client.probability_mutate_node *= 0.2;
                //    client.probability_mutate_weight_shift *= 2;
                //    client.probability_mutate_weight_shift *= 4;
                //    client.weight_shift_strength *= 0.5;
                //}
                //if (client.generations > 50)
                //{
                //    for (int xx = 0; xx < 10; xx++)
                //    {
                //        best_guy.improve(dataset.RandomBatch(100));
                //        double t = best_guy.RateFitness(dataset.RandomBatch(200));
                //        //Tools.Tools.write_to_file(t.ToString(),"log.txt");
                //    }
                //}
                Console.WriteLine($"Best acc {acc}, iteration {i}");
                client.best_acc = acc;
                client.motionless_check.Insert_at_start_Same_Length(client.best_acc);
                double first_der = Tools.Tools.first_deriv_from_values(client.motionless_check);
                client.accel_check.Insert_at_start_Same_Length(first_der);
                double accel = Tools.Tools.first_deriv_from_values(client.accel_check);

                OnNextGeneration((client.generations, client.best_acc));

                //if ((motion=first_der)<0)
                //{
                //    //revive
                //    foreach(Genotype g in Best_Dead)
                //    {
                //        Creatures.Add(g);
                //        revivals++;
                //    }//then respeciation happens, these genomes should be at the top
                //}
                //if (accel==0)
                //{
                //    probability_mutate_weight_shift *= 1.01;
                //    probability_mutate_weight_random *= 1.005;
                //}

            } while (i < iters /* && acc < target_acc*/ && !client.PAUSED);
            working = false;
            return acc;
        }


        //public async Task<double> TrainAsync(double target_acc, int MaxIter = 1000)
        //{

        //}
        protected void OnNextGeneration((int, double) e)
        {
            EventHandler<(int, double)>? handler = NextGeneration;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        public int NumberOfCreatures()
        {
            if (client == null) throw new Exception("No neural network");
            return client.Creatures.Count();
        }

        /// <summary>
        /// Pauses the training, which escapes the train method and all train methods will terminate. The only way to terminate training method without max iterations specified
        /// </summary>
        public void Pause()
        {
            if (client == null) throw new Exception("No neural network");
            client.PAUSED = true;
            
        }
        /// <summary>
        /// Unpause the model, if model wont train when calling Train method, consider calling this first
        /// </summary>
        public void Unpause()
        {
            if (client == null) throw new Exception("No neural network");
            client.PAUSED = false;
        }

        public void TogglePause()
        {
            if (client == null) throw new Exception("No neural network");
            if (client.PAUSED) Unpause();
            else Pause();

        }

        /// <summary>
        /// just a wrapper on pause
        /// </summary>
        public void Terminate()
        {
            Pause();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Pause status</returns>
        public bool GetPaused()
        {
            if (client == null) throw new Exception("No neural network");
            return client.PAUSED;
        }

        public Vector BestPrediction(Vector input)
        {
            if (client == null) throw new Exception("No neural network");
            if(isCategorical) return categoryThreshholdFunc( client.GetBest().Forwards(input));
            return client.GetBest().Forwards(input);
        }

        /// <summary>
        /// take the mean of all the genotypes and return this as output
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Vector AveragePrediction(Vector input)
        {
            if (client == null) throw new Exception("No neural network");
            Vector sum = new Vector( client.Output);
            foreach (Genotype g in client.Creatures)
            {
                sum += g.Forwards(input);
            }
            sum =  (1 / client.Creatures.Count())*sum;
            return sum;
        }

        /// <summary>
        /// Get specification about input/output sizes and more
        /// </summary>
        /// <returns>NNSpecification</returns>
        public NNSpecification GetNNSpecification()
        {
            return Specification;
        }

        /// <summary>
        /// train for a specified number of generations
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        public double Train(IDataSet dataset, int iterations)
        {
            return _Train(dataset, iters:iterations);
        }
        /// <summary>
        /// train without stopping (until paused) or Int32.MaxValue (practical infinity)
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public double Train(NeaLibrary.DataStructures.IDataSet dataset)
        {
            return _Train(dataset);
        }

        public void Save(string path)
        {
            if (client == null) throw new ArgumentNullException("No associated NEAT instance");
            lock (client.ClientLock)
            {
                Tools.Tools.Serialize(path, this);
            }
        }

        public static INeuralNetwork Load(string path)
        {
            NEAT_Handler? n = null;
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();

            #pragma warning disable
            n = (NEAT_Handler)formatter.Deserialize(fs);
            fs.Close();
            return n;
        }


    }
}
