using NeaLibrary.DataStructures;
using System.Runtime.Serialization.Formatters.Binary;

namespace NeaLibrary.NeuralNetwork.FFNN
{
    [Serializable]
    public class Network
    {


        private int n_layers { get; set; } //not count input 
        //    weights , bias
        //List<Matrix> layers;  //array is faster than list
        private double learning_rate = 0.1;
        public double GetLearningRate() => learning_rate;
        public void SetLearningRate(double v) { learning_rate = v; }
        private Matrix[] layers { get; set; }
        public Matrix[] GetLayers() => layers;
        private Vector[] biases { get; set; }

        private Vector topology;
        public Vector GetTopology() => topology;
        public void SetTopology(Vector topology) { this.topology = topology; }
        private Vector[] neuron_values { get; set; }
        private Vector[] activations { get; set; }
        private Vector[] deltas { get; set; }

        private Vector[] training_deltas { get; set; }
        private Matrix[] training_weights { get; set; }
        private Vector[] training_bias { get; set; }

        private double fitness { get; set; }
        public double GetFitness() => fitness;

        public Network(Vector topography)
        {     // n of input nodes    n of hidden layers   .....   n of output
            if (topography.dimension < 2)
            {
                //wrong
                Console.WriteLine("Wrong use");
                throw new Exception();
            }

            topology = topography;

            n_layers = topography.dimension - 1;
            layers = new Matrix[n_layers];
            biases = new Vector[n_layers];//les biases

            activations = new Vector[n_layers];
            neuron_values = new Vector[n_layers];
            deltas = new Vector[n_layers];

            training_deltas = new Vector[n_layers];
            training_weights = new Matrix[n_layers];
            training_bias = new Vector[n_layers];

            //create the hidden layers    and (<=)  the out layer
            for (int n = 1; n <= n_layers; n++)
            {
                //the previous layers n of nodes
                int prev = (int)topography[n - 1];
                //Console.Write($"{prev} => ");
                //this layers n of nodes
                int thsnodes = (int)topography[n];
                //Console.WriteLine($"{thsnodes}");
                //matrix to represent the weights
                Matrix weights = new Matrix(thsnodes, prev);
                training_weights[n - 1] = new Matrix(thsnodes, prev);
                //randomise weights
                weights.Randomise(-1, 1);// idk what range to pick
                //add to the layers List
                layers[n - 1] = weights;  //array starts from 0 innih
            }

            //vector[] bias initialisation
            double max = 0.1;
            double min = -0.1;
            for (int n = 0; n < n_layers; n++)
            {
                biases[n] = new Vector((int)topography[n + 1]);
                training_bias[n] = new Vector((int)topography[n + 1]);
                training_deltas[n] = new Vector((int)topography[n + 1]);
                // layer of biases, now loop through each 

                for (int b = 0; b < biases[n].dimension; b++)
                {
                    biases[n][b] = Tools.Tools.RandomDouble(min, max);//rand.NextDouble() * (max - min) + min;
                }
            }
        }



      

        /// <summary>
        /// Pass forward pass from input to an output
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Vector representing the output</returns>
        public Vector Forward(Vector input)
        {
            Vector temp = input; //shallow or no?
            for (int l = 0; l < n_layers; l++)
            {
                temp = layers[l] * temp;  // left multiply by matrix     columns match the current vector    rows match next vector
                //      m  by  n  *   n  by  p
                temp += biases[l];
                neuron_values[l] = temp;
                temp = temp.PutThrough(Tools.Tools.Tanh);
                activations[l] = temp;
            }
            return temp;
        }
        
        /// <summary>
        /// Backpropagation, single pass.
        /// To be used after a Network.Forward()
        /// </summary>
        /// <param name="target">Takes in the label</param>
        public void Backward(Vector target)
        {

            //last layer aka output
            int l = n_layers - 1;

            Vector deltaL = new Vector((int)topology[l + 1]);
            Vector nabla_Cost = new Vector((int)topology[l + 1]);//starts from input, so +1, but 0 index
            Vector σ_gradient = new Vector((int)topology[l + 1]);
            for (int i = 0; i < topology[l + 1]; i++)
            {

                nabla_Cost[i] = Tools.Tools.derivative_MSE(activations[l][i], target[i]); //derivative of cost f for 1 example
                                                                                          // ∇ Cₐ   = [ partial derivatives ]
                σ_gradient[i] = Tools.Tools.Derivative_Tanh(neuron_values[l][i]);
                //     σ'(zᴸ)
            }
            deltaL = nabla_Cost ^ σ_gradient; //hadamard product, more loops than necessary but just go with it
                                              //      δᴸ = ∇C * σ'(zᴸ)
            deltas[l] = deltaL;//want last one
            void recursion(int l, Vector delta)
            {// l is current, working back to l-1
                if (l >= 0)
                {
                    Vector new_delta = new Vector((int)topology[l + 1]);// l+1 but then -1 for last layer
                    Vector σ_gradient = new Vector((int)topology[l + 1]);
                    σ_gradient = neuron_values[l - 1 + 1].PutThrough(Tools.Tools.Derivative_Tanh);
                    //for(int i=0;i<topology[l];i++){//new grad vector of activation. of the size l-1 layer
                    //σ_gradient.vector[i]= tools.Derivative_Tanh(neuron_values[l-1].vector[i]);
                    //σ'(zˡ)
                    //  }
                    new_delta = layers[l + 1].Transpose() * delta ^ σ_gradient;
                    deltas[l] = new_delta;

                    recursion(l - 1, new_delta);

                }
                //done recursion
                return;
            }

            recursion(l - 1, deltaL);


        }

        private double Rate(Vector input, Vector target)
        {
            //RATES ERROR
            return Tools.Tools.MSE(Forward(input), target);
        }


        public void Improve(Vector input, Vector target)
        {
            Forward(input);
            Backward(target);


            void reccur(int l)
            {
                if (l >= 0)
                {
                    //biases   dC/dbˡⱼ   =  δˡⱼ

                    biases[l] += -1 * learning_rate * deltas[l];
                    //vectors line up

                    if (l != 0)
                    {
                        //use last activations
                        for (int k = 0; k < layers[l].GetRows(); k++)
                        {
                            for (int j = 0; j < layers[l].GetColumns(); j++)
                            {
                                layers[l][k, j] += -1 * learning_rate * deltas[l][k] * activations[l - 1][j];
                                //dC / dwˡₖⱼ = δˡₖ * aˡ⁻¹ⱼ 
                            }
                        }
                    }
                    else
                    {
                        //use inputs
                        for (int k = 0; k < layers[l].GetRows(); k++)
                        {
                            for (int j = 0; j < layers[l].GetColumns(); j++)
                            {
                                layers[l][k, j] += -1 * learning_rate * deltas[l][k] * input[j];
                                //dC / dwˡₖⱼ = δˡₖ * aˡ⁻¹ⱼ 
                            }
                        }
                    }

                }
                return;
            }
            reccur(n_layers - 1);
        }

        /// <summary>
        /// Training method, both Forward and Backward on each training cycle
        /// 
        /// </summary>
        /// <param name="dataset">IDataSet instance</param>
        /// <param name="bacth_size">Average of how many examples to be used to work out error</param>
        /// <returns></returns>
        public double Train(IDataSet dataset/*, int bacth_size = -1*/)
        {
            /*if (bacth_size == -1) */int bacth_size = Int32.Parse(Tools.Tools.GetGlobalVar("Random_Batch_Items_Count"));
            foreach ((Vector, Vector) tup in dataset.RandomBatch(bacth_size))
            {
                Forward(tup.Item1);
                Backward(tup.Item2);

                for (int x = 0; x < n_layers; x++)
                {


                    training_deltas[x] += deltas[x];

                    training_bias[x] += -1 * learning_rate * deltas[x];


                    //work out required weight changes
                    if (x != 0)
                    {
                        //use last activations

                        for (int k = 0; k < layers[x].GetRows(); k++)
                        {
                            for (int j = 0; j < layers[x].GetColumns(); j++)
                            {
                                training_weights[x][k, j] += -1 * learning_rate * deltas[x][k] * activations[x - 1][j];
                                //dC / dwˡₖⱼ = δˡₖ * aˡ⁻¹ⱼ 
                            }
                        }
                    }
                    else
                    {
                        //use inputs
                        for (int k = 0; k < layers[x].GetRows(); k++)
                        {
                            for (int j = 0; j < layers[x].GetColumns(); j++)
                            {
                                training_weights[x][k, j] += -1 * learning_rate * deltas[x][k] * tup.Item1[j];
                                //dC / dwˡₖⱼ = δˡₖ * aˡ⁻¹ⱼ           not this one, its the inpputs as activation one
                            }
                        }
                    }
                }// done matrix
            }
            for (int i = 0; i < n_layers; i++)
            {
                training_bias[i] /= dataset.Count();
                training_deltas[i] /= dataset.Count();
                training_weights[i] /= dataset.Count();

                biases[i] += training_bias[i];
                layers[i] += training_weights[i];
            }
            //rate accuracy
            double error = 0;
            int n = 1;

            foreach ((Vector, Vector) v in dataset.RandomBatch(bacth_size)) { error = (error * (n - 1) + Rate(v.Item1, v.Item2)) / n; n++; }//hmm? Rate vEctor[] func?            TODO
            return error;
        }
        /// <summary>
        /// Training method, both Forward and Backward on each training cycle
        /// Specify accuracy to reach or maximum number of training iterations
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="target_acc"></param>
        /// <param name="iterations_per_loop"></param>
        /// <param name="loops"></param>
        /// <returns></returns>
        public double TrainLoop(IDataSet ds, double target_acc, int iterations_per_loop = 1, int loops = 1000)
        {

            double er = 0;
            int iters = 0;
            do
            {
                for (int n = 0; n < iterations_per_loop; n++)
                {
                    er = Train(ds, 100);  //DS and BatchSize

                }
                iters++;
            } while (1 - er < target_acc & iters < loops);
            return er;
        }
        /// <summary>
        /// Training method, both Forward and Backward on each training cycle
        /// </summary>
        /// <param name="ds">IDataSet instance</param>
        /// <param name="iterationsperloop"> iterations per training cycle</param>
        /// <param name="loops">training cycles</param>
        /// <returns></returns>
        public double Train(IDataSet ds, int iterationsperloop = 100, int loops = 1000)
        {

            double er = 0;
            int iters = 0;
            do
            {
                for (int n = 0; n < iterationsperloop; n++)
                {
                    er = Train(ds, Int32.Parse(Tools.Tools.GetGlobalVar("Random_Batch_Items_Count")) );  //DS and BatchSize

                }
                iters++;
            } while (iters < loops);
            return er;
        }

       

        //Mutate
        public void Mutate(float chance, float weightval, float biasval)
        {//chance and max value to mutate by
            foreach (Matrix layer in layers)
            {
                //foreach layer
                for (int n = 0; n < layer.GetRows(); n++)
                {
                    for (int m = 0; m < layer.GetColumns(); m++)
                    {
                        //mutate a little
                        if (Tools.Tools.RandomDouble(0, 1) < chance) layer[n, m] += Tools.Tools.RandomDouble(-weightval, weightval);//rand.NextDouble() * weightval*2-weightval;
                    }
                }
            }
            //muate bias
            for (int i = 0; i < biases.Length; i++)
            {
                for (int n = 0; n < biases[i].dimension; n++)
                {
                    if (Tools.Tools.RandomDouble(0, 1) < chance) biases[i][n] += Tools.Tools.RandomDouble(-biasval, biasval);//rand.NextDouble() * biasval*2-biasval;
                }
            }
        }

        public Network Crossover(Network other)
        {
            //very simple, probably will make things worse
            Network r = new Network(topology);
            for (int w = 0; w < layers.Length; w++)
            {
                r.layers[w] = (layers[w] + other.layers[w]) / 2;
            }
            for (int b = 0; b < biases.Length; b++)
            {
                r.biases[b] = (biases[b] + other.biases[b]) / 2;
            }
            //simple average of both
            return r;
        }


        public void Save(string path)
        {
            Tools.Tools.Serialize(path, this);

        
        }
#nullable enable
        public static Network? Load(string path)
        {
            Network? n = null;
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();

            // Deserialize the hashtable from the file and
            // assign the reference to the local variable.
#pragma warning disable
            n = (Network)formatter.Deserialize(fs);
            //string text = System.IO.File.ReadAllText(path);
            //return JsonSerializer.Deserialize<Network>(text);
            fs.Close();
            return n;
        }
#nullable disable
        public void DeepCopy(Network target)
        {
            /*this.layers.CopyTo(target.layers,0);
            //should copy our layer matrix array
            //to the target's
            //is size of array an issue?
            this.biases.CopyTo*/
            //assuming same sizes and topography
            for (int x = 0; x < layers.Length; x++)
            {
                target.layers[x] = layers[x].Copy_To();
            }
            for (int x = 0; x < biases.Length; x++)
            {
                for (int y = 0; y < biases[x].dimension; y++)
                {
                    target.biases[x][y] = biases[x][y];
                }
            }
        }
    }

}