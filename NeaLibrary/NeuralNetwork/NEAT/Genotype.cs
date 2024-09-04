using System;
using System.Collections.Generic;
using System.Linq;
using NeaLibrary.DataStructures;
using NeaLibrary.Tools;


namespace NeaLibrary.NeuralNetwork.NEAT
{
    [Serializable]
    public class Genotype : IComparable<Genotype>
    {
        public Neat master { get; set; }
        public RandomHashedSet<NodeGene> Nodes { get; set; }
        public RandomHashedSet<ConnectionGene> Connections { get; set; }
        //this is a container for all our genes
        public Graph? Phenotype;
        public Dictionary<NodeGene, int> NodePhenotypeMap;
        public double fitness { get; set; }
        public Species? specie { get; set; }
        Vector back_prop_delta_avg;
        Vector activation_in_avg;

        public Genotype(Neat m)
        {//new empty genotype
            Nodes = new RandomHashedSet<NodeGene>();
            Connections = new RandomHashedSet<ConnectionGene>();
            NodePhenotypeMap = new Dictionary<NodeGene, int>();
            master = m;
            //Fetch in and out nodes
            InitialiseInputOutputNodes();
            //Need to make a phenotype
            //Phenotype = new Graph(0); //just empty?
            GeneratePhenotype();
        }
        public void AddNode(NodeGene g)
        {
            UpdatePhenotype_AddNode(g);
            if (Nodes.Contains(g)) return;
            Nodes.Add(g);

            //update Phenotype



            //basic sol
            //Used to update map here but should be handled in UpdatePhentope
        }
        public void AddConn(ConnectionGene g)
        {
            if (Connections.Contains(g)) return;
            Connections.Add(g);

            //update Phenotype
            UpdatePhenotype_AddEdge(g.from, g.to, g.weight);
        }
        public ConnectionGene CloneConn(int index)
        {
            return Connections[index].Clone();
        }

        public int Highest_Innov()
        {
            if (Nodes.Count == 0) return -1;
            if (Connections.Count == 0) return Nodes.Max()!.innovation_number;
            int c = Connections.Max()!.innovation_number;
            //int n = Nodes.MaxBy(x=>x.innovation_number).innovation_number;
            //return ((n>c)? n:c);
            return c;
            //Connection has highest innov number because,
            //when you add a node, you split a connection
            //and therefore add another connection with a higher innov n
        }
        public bool Fitter(Genotype b)
        {

            return fitness > b.fitness ? true : false;

        }

        public static double Highest_Fitness(Genotype a, Genotype b)
        {

            return a.fitness > b.fitness ? a.fitness : b.fitness;

        }
        public double Distance(Genotype other)
        {
            //distance between this andother
            //we need a to have highest innov
            Genotype a, b;
            if (Highest_Innov() > other.Highest_Innov())
            {
                a = this;
                b = other;
            }
            else
            {
                a = other;
                b = this;
            }
            int A = 0;
            int B = 0;
            int sim = 0;
            int dis = 0;
            int exc = 0;
            double weight_dif = 0;
            while (A < a.Connections.Count && B < b.Connections.Count)
            {
                ConnectionGene g1 = a.Connections[A];
                ConnectionGene g2 = b.Connections[B];
                //every time we add a new NODE
                //we make new CONNECTIONS
                //so by only looking at CONNECTION innov
                //we know it is the highest

                int in1 = g1.innovation_number;
                int in2 = g2.innovation_number;

                if (in1 == in2)
                {
                    //matching
                    A++;
                    B++;
                    sim++;
                    weight_dif += Math.Abs(g1.weight - g2.weight);
                }
                else if (in1 > in2)
                {
                    //disjoint gene of b. b has this extra gene
                    B++;
                    dis++;
                }
                else
                {
                    //disjoint gene of a. a has extra
                    A++;
                    dis++;
                }
            }
            exc = a.Connections.Count - A; //excess as a has bigger innov
            weight_dif /= (sim!=0)? sim:1;
            int n = Math.Max(a.Connections.Count, b.Connections.Count);
            if (n < 20) n = 1;
            return master.c1 * exc / n + master.c2 * dis / n + master.c3 * weight_dif;
        }


        public static Genotype Crossover(Genotype first, Genotype second)
        {
            //Crrossover between this andother
            //we need a to have highest innov
            Genotype a, b, c;

            if (first.Highest_Innov() > second.Highest_Innov())
            {
                a = first;
                b = second;
            }
            else
            {
                a = second;
                b = first;
            }
            c = a.master.EmptyGenotype();
            //child

            Neat master = a.master;

            int A = 0;
            int B = 0;

            void addConnsNodes(ConnectionGene cg)
            {
                c.AddNode(cg.to);
                c.AddNode(cg.from);
            }//helper function
             //Changed my mind, will iterate over node genes?
             //nvm causes key not in dictionary error

            while (A < a.Connections.Count && B < b.Connections.Count)
            {
                ConnectionGene g1 = a.Connections[A];
                ConnectionGene g2 = b.Connections[B];
                //every time we add a new NODE
                //we make new CONNECTIONS
                //so by only looking at CONNECTION innov
                //we know it is the highest

                //also since we are only looking at connections,
                //I will need to add the Nodes myself (*)

                int in1 = g1.innovation_number;
                int in2 = g2.innovation_number;

                if (in1 == in2)
                {
                    //matching


                    //we clone the connections genes for the CHILD
                    //as we do nt want it to be the EXACT SAME
                    //reference as parent
                    //WE DO NOT want them to share EXACT same gene instance, but a copy
                    if (Tools.Tools.RandomDouble(0, 1) > 0.5)
                    {
                        ConnectionGene cg = master.GetConnection(g1);
                        addConnsNodes(cg); //this has to be before next line
                        c.AddConn(cg.Clone());

                    }
                    else
                    {
                        ConnectionGene cg = master.GetConnection(g2);
                        addConnsNodes(cg);
                        c.AddConn(cg.Clone());

                    }
                    A++;
                    B++;

                }
                else if (in1 > in2)
                {
                    //disjoint gene of b. b has this extra gene

                    if (b.Fitter(a))
                    {
                        ConnectionGene cg = b.CloneConn(B);
                        addConnsNodes(cg);
                        c.AddConn(cg);

                    }
                    B++;
                    ////GetConnection(g2).Clone()

                }
                else
                {
                    //disjoint gene of a. a has extra

                    if (a.Fitter(b))
                    {
                        ConnectionGene cg = a.CloneConn(A);
                        addConnsNodes(cg);
                        c.AddConn(cg);

                    }
                    //if(a.Fitter(b)) c.AddConn(master.GetConnection(g1).Clone());
                    A++;
                }
            }

            if (a.Fitter(b))
            {
                //the excess of a if a is fitter
                for (int i = A; i < a.Connections.Count; i++)
                {
                    ConnectionGene cg = a.CloneConn(i);
                    addConnsNodes(cg);
                    c.AddConn(cg);
                }
            }


            return c;


        }

        public void Mutate()
        {
            if (Tools.Tools.RandomDouble() < master.probability_mutate_link) MutateLink();
            if (Tools.Tools.RandomDouble() < master.probability_mutate_node) MutateNode();
            if (Tools.Tools.RandomDouble() < master.probability_mutate_weight_random) WeightRandom();
            if (Tools.Tools.RandomDouble() < master.probability_mutate_weight_shift) WeightShift();
            if (Tools.Tools.RandomDouble() < master.probability_mutate_link_toggle) ToggleLink();
        }

        public void MutateLink()
        {
            if (Nodes.Count == 0) { Console.WriteLine("dont be dum"); return; }
            for (int i = 0; i < 100; i++)
            {
                NodeGene a = Nodes.GetRandom();
                NodeGene b = Nodes.GetRandom();
               
                if (a.x == b.x)
                {
                    continue;
                }

                ConnectionGene c;
                if (a.x >  b.x)
                {//avoid going backwards
                    c = new ConnectionGene(b, a);

                    //from b to a
                }
                else
                {
                    c = new ConnectionGene(a, b);
                    
                    //from a to b
                }
                if (Connections.Contains(c))
                {
                    //if such one exists
                    continue;
                }


                c = master.MakeConnection(c.from, c.to); //we replaced it with one from global neat pool
                //   here /\   /\    /\    Make rather than Get, but make creates a new one anf does all checking
                //        ||   ||    ||
                
                c.weight = Tools.Tools.RandomDouble(-master.weight_random_strength, master.weight_random_strength);
                //random weight
                Connections.AddSorted(c);

                //Since we did not call this.AddConn()
                UpdatePhenotype_AddEdge(c.from, c.to, c.weight);

                return;
            }

        }
        public void MutateNode()
        {
            if (Connections.Count == 0)
            {

                return;
            }
            ConnectionGene c = Connections.GetRandom();
            if (c == null) return;
            
            NodeGene from = c.from;
            NodeGene to = c.to;

            NodeGene mid;
            int replaced = master.GetSplitIndex(from, to);
            if (replaced == -1)
            {
                mid = master.MakeNode();
                master.SetSplitIndex(from, to, mid.innovation_number);
            }
            else
            {
                mid = master.GetNode(replaced);
            }

            mid.x = (to.x + from.x) / 2;
            mid.y = (to.y + from.y) / 2;

            ConnectionGene con1 = master.MakeConnection(from, mid);
            ConnectionGene con2 = master.MakeConnection(mid, to);

            con1.weight = 1;
            con2.weight = c.weight;
            con2.enabled = c.enabled;

            c.enabled = false;

           

            Connections.Remove(c);
            //TODO   what should i do about removing connections from phenotype?
            //set it to 0?
            UpdatePhenotype_RemoveEdge(c);


            AddNode(mid);    //mid is new so safe

            AddConn(con1);
            AddConn(con2);
            //used to be Connections.Add

        }
        public void WeightShift()
        {

            if (Connections.Count > 0)
            {
                ConnectionGene con = Connections[Tools.Tools.RandomInt(0, Connections.Count - 1)];
                if (con != null)
                {
                    con.weight += Tools.Tools.RandomDouble(-master.weight_shift_strength, master.weight_shift_strength);
                    UpdatePhenotype_UpdateEdge(con, con.weight);
                }
            }
        }
        public void WeightRandom()
        {
            if (Connections.Count > 0)
            {
                ConnectionGene con = Connections[Tools.Tools.RandomInt(0, Connections.Count - 1)];
                if (con != null)
                {
                    con.weight = Tools.Tools.RandomDouble(-master.weight_random_strength, master.weight_random_strength);
                    UpdatePhenotype_UpdateEdge(con, con.weight);
                }
            }
        }
        public void ToggleLink()
        {
            if (Connections.Count == 0)
            {
            
                return;
            }
            ConnectionGene con = Connections[Tools.Tools.RandomInt(0, Connections.Count - 1)];
            if (con != null)
            {
                con.enabled = !con.enabled;
            }
        }

        public Graph ToGraph()
        {
            Graph g = new Graph(Nodes.Count);
            //ALREADY CREATES NODES
            int i = 0;

            if (Nodes.Count != 0)
            {
                foreach (NodeGene ng in Nodes)
                {
                    NodePhenotypeMap[ng] = i;
                    g.NodesCoordinates.Add(i, (ng.x, ng.y));
                    i++;
                }
            }
            if (Connections.Count != 0)
            {
                foreach (ConnectionGene cg in Connections)
                {
                    g.AddEdge(NodePhenotypeMap[cg.to], NodePhenotypeMap[cg.from], cg.enabled ? cg.weight : 0);
                    //will the order of list be important?
                    //set disabled to 0
                    //using nodephenotypemap    was before Node.IndexOf(cg.to/from)
                }
            }

            return g;
        }

        public void InitialiseInputOutputNodes()
        {
            for (int i = 0; i < master.Input + master.Output; i++)
            {
                NodeGene ng = master.GetNode(i);
                Nodes.Add(ng);
                NodePhenotypeMap.Add(ng, i);
                //GeneratePhenotype();
                //Phenotype.Nodes.Add(i, (ng.x,ng.y));
                //The crude method rather than AddNode cause phenotype not yet initiated
                //the index i should be fine as we are looping from start
                //and filling empty genome with initial genes
                //input at start and output stacked on, everything else after


                //it is the FIRST node. but stored in the adj matrix in index i
                //so to compensate in the update edge i will make sure to -1
                //IGNORE /\
            }
        }


        //whats faster? making a new matrix or upscaling existing one?
        //I think upscale one would be more efficient. as in to graph method we search for correct node in Nodes
        public void GeneratePhenotype()
        {
            Phenotype = ToGraph();
            // for (int i = 0; i < master.Output; i++)
            // {
            //     //Phenotype[master.Input+i,master.Input+i] = 1;
            //     //so that it remembers results
            //     //set the self loop of output nodes to 1

            //     //gonna use total sum instead
            // }
        }
        public void UpdatePhenotype_AddNode(NodeGene ng)
        {
            if (NodePhenotypeMap.ContainsKey(ng)) return;
            Phenotype!.AddNode(); //will make new node of index   +1
            NodePhenotypeMap.Add(ng, Phenotype.NodeCount - 1); //so no need +1 here
            Phenotype.NodesCoordinates.Add(Phenotype.NodeCount - 1, (ng.x, ng.y));
            //  so for eg 5th node. but in array it is in place 4
            //thus -1
            //all the meddle with Connections should be handled in Mutate func
        }
        public void UpdatePhenotype_AddEdge(NodeGene From, NodeGene To, double weight)
        {
            Phenotype!.AddEdge(NodePhenotypeMap[To], NodePhenotypeMap[From], weight);
        }
        public void UpdatePhenotype_UpdateEdge(ConnectionGene cg, double weight)
        {
            Phenotype!.adjacencyMatrix[NodePhenotypeMap[cg.to], NodePhenotypeMap[cg.from]] = weight;

            //TODO    the -1?
        }
        public void UpdatePhenotype_RemoveEdge(ConnectionGene cg)
        {
            UpdatePhenotype_UpdateEdge(cg, 0);
            //since there arent multiple of same edge, we just change the edge in the matrix
            //nodes arent removed
        }

        public void Print()
        {
            string specieText = specie == null ? "null" : specie.GetHashCode().ToString();
            Console.WriteLine($"Creature {GetHashCode()} in  NEAT {master.GetHashCode()}, specie {specieText}");
            Console.WriteLine($"Fitness: {fitness}\nComplexity: {Nodes.Count} Nodes, {Connections.Count} Connections");
            if (Phenotype != null)
            {
                Console.WriteLine("==Phenotype==");
                Phenotype.Print();
            }
        }


        public Vector Forwards(Vector input)
        {
            if (Phenotype == null) GeneratePhenotype();
            if (input.dimension != master.Input) throw new Exception("Mismatching input dimensions");

            Phenotype.Forward(input, Nodes.Count - master.Input - master.Output + 1);

            
            Vector r = new Vector(master.Output);
            for (int i = 0; i < master.Output; i++)
            {
                r[i] = Tools.Tools.S_ReLu(Phenotype.total_activation_accumulation[master.Input + i]);   //todo
                                                                                                        //read off the output nodes
            }
            return r;
        }
        public double RateFitness(Vector input, Vector output)
        {
            fitness = 1 - Tools.Tools.MSE(Forwards(input), output);
            return fitness;
        }

        public double RateFitness(Vector[] inputs, Vector[] outputs)
        {
            if (inputs.Length != outputs.Length) Console.WriteLine("Mismatching training set");
            double error = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                error += Tools.Tools.MSE(Forwards(inputs[i]), outputs[i]);
            }
            if (inputs.Length == 0) throw new Exception("Give a set with values");
            fitness = 1 - error / inputs.Length;

            return fitness;
        }
        public double RateFitness(IDataSet ds)
        {
            double error = 0;
            foreach ((Vector, Vector) data in ds)
            {
                error += Tools.Tools.MSE(Forwards(data.Item1), data.Item2);
            }
            if (ds.Count() == -1)
            {
                throw new Exception("Uknown length data set");
            }
            fitness = 1 - error / ds.Count();
            return fitness;
        }
        public double RateFitness(IEnumerable<(Vector, Vector)> Data)
        {
            double error = 0;
            foreach ((Vector, Vector) data in Data)
            {
                error += Tools.Tools.MSE(Forwards(data.Item1), data.Item2);
            }
            if (Data.Count() == -1)
            {
                throw new Exception("Uknown length data set");
            }
            fitness = 1 - error / Data.Count();
            return fitness;
        }
        public (Vector, Vector) Backwards(Vector input, Vector target)
        {
            //Phenotype.resetaccumulations();// for this 1 example, this is why there exists the other vectors in this class     rather it is done in the forwards method
            Vector r = Forwards(input);

            Vector nabla_Cost = new Vector(master.Output);
            Vector σ_gradient = new Vector(master.Output);
            for (int i = 0; i < master.Output; i++)
            {

                nabla_Cost[i] = Tools.Tools.derivative_MSE(r[i], target[i]); //derivative of cost f for 1 example
                                                                             // ∇ Cₐ   = [ partial derivatives ]
                σ_gradient[i] = Tools.Tools.S_ReLu_Derivative(r[i]);
                //     σ'(zᴸ)
            }
            Vector deltaL = nabla_Cost ^ σ_gradient; //hadamard product, more loops than necessary ?


            for (int n = 0; n < master.Output; n++)
            {
                Phenotype.Backward(master.Input + n, deltaL[n]);  //no need to divide here, since effects from multiple output neurons still increasse a nodes error value

            }
            Vector delta = Phenotype.deltas_accumulation;
            for (int i = 0; i < master.Output; i++)
            {
                delta[master.Input + i] = deltaL[i];
            }
            return (delta, Phenotype.total_activation_accumulation);
        }

        public void improve(Vector[] ins, Vector[] outs)
        {
            back_prop_delta_avg = new Vector(Phenotype.NodeCount);
            activation_in_avg = new Vector(Phenotype.NodeCount);

            for (int n = 0; n < outs.Length; n++)
            {
                Vector temp1;
                Vector temp2;
                (temp1, temp2) = Backwards(ins[n], outs[n]);//for 1 example
                back_prop_delta_avg += temp1;//accumulations of examples
                activation_in_avg += temp2;
            }
            back_prop_delta_avg = back_prop_delta_avg / outs.Length;
            activation_in_avg = activation_in_avg / outs.Length;

            foreach (ConnectionGene cg in Connections)
            {
                cg.weight += -1 * back_prop_delta_avg[NodePhenotypeMap[cg.to]] * activation_in_avg[NodePhenotypeMap[cg.from]];
                Phenotype.SetEdge(NodePhenotypeMap[cg.to], NodePhenotypeMap[cg.from], cg.weight);
            }

        }
        public void Improve(IEnumerable<(Vector, Vector)> data)
        {
            back_prop_delta_avg = new Vector(Phenotype.NodeCount);
            activation_in_avg = new Vector(Phenotype.NodeCount);

            for (int n = 0; n < data.Count(); n++)
            {
                Vector temp1;
                Vector temp2;
                (temp1, temp2) = Backwards(data.ElementAt(n).Item1, data.ElementAt(n).Item2);//for 1 example
                back_prop_delta_avg += temp1;//accumulations of examples
                activation_in_avg += temp2;
            }
            back_prop_delta_avg = back_prop_delta_avg / data.Count();
            activation_in_avg = activation_in_avg / data.Count();

            foreach (ConnectionGene cg in Connections)
            {
                cg.weight += -1 * back_prop_delta_avg[NodePhenotypeMap[cg.to]] * activation_in_avg[NodePhenotypeMap[cg.from]];
                Phenotype.SetEdge(NodePhenotypeMap[cg.to], NodePhenotypeMap[cg.from], cg.weight);
            }

        }
#nullable enable
        public int CompareTo(Genotype? obj)
        {
            return fitness.CompareTo(obj.fitness);
        }
#nullable disable
    }
}