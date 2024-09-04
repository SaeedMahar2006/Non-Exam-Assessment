using System;
using NeaLibrary.Tools;
using NeaLibrary.DataStructures;
using System.Collections.Concurrent;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace NeaLibrary.NeuralNetwork.NEAT
{

    //testing saves 234
    [Serializable]
    public class Neat
    {

        public bool PAUSED = false;

        //public int InputDimension, OutputDimension;

        public int Input;
        public int Output;
        public int max_clients;
        public double best_acc;
        int highest_innovation_number;

        public int generations = 0;

        //parameters
        public double weight_shift_strength = 0.1;
        public double weight_random_strength = 2;
        public double probability_mutate_link = 0.3;
        public double probability_mutate_node = 0.05;
        public double probability_mutate_weight_shift = 0.1;
        public double probability_mutate_weight_random = 0.01;
        public double probability_mutate_link_toggle = 0.01;

        public double specie_distance = 5;
        public double kill_rate = 0;

        public int revivals = 0;

        public RandomHashedSet<NodeGene> Global_Nodes;
        public RandomHashedSet<ConnectionGene> Global_Connectons;
        public RandomHashedSet<Genotype> Creatures;
        public RandomHashedSet<Species> Species;

  

        public double c1 = 0.4;
        public double c2 = 0.3;
        public double c3 = 0.35;


        [field:NonSerialized]
        public SortedSet<Genotype> Best_Dead = new SortedSet<Genotype>();//in case we need to revive them



        public Vector motionless_check = new Vector(5);
        public Vector accel_check = new Vector(10);
        public double motion = 0;

        public object ClientLock = new object(); //lock Creatures, Species and other collections during LifeCycle and operations like GetBest().
        //Gave an error when calling GetBest and LifeCycle happening at same time

    
        public Neat(int input, int output, int n_creatures)
        {
            Reset(input, output, n_creatures);//this is not null, compiler is just wrong
        }

        public void Reset(int input, int output, int n_creatures)
        {
            Input = input;
            Output = output;
            highest_innovation_number = -1;
            //so that first one starts at 0
            Global_Connectons = new RandomHashedSet<ConnectionGene>();
            Global_Nodes = new RandomHashedSet<NodeGene>();
            Species = new RandomHashedSet<Species>();

            double BORDER = 0.05;
            double SPACE = 1 - 2 * BORDER;

            for (int i = 0; i < input; i++)
            {
                NodeGene inNode = MakeNode();
                inNode.x = BORDER;
                inNode.y = BORDER + SPACE * i / input;
            }
            for (int o = 0; o < output; o++)
            {
                NodeGene outNode = MakeNode();
                outNode.x = 1 - BORDER;
                outNode.y = BORDER + SPACE * o / output;
            }
            Creatures = new RandomHashedSet<Genotype>();
            for (int i = 0; i < n_creatures; i++)
            {
                //Console.WriteLine($"{i}th creature");
                //Creatures[i] = EmptyGenotype();
                Genotype instance = EmptyGenotype();
                Creatures.Add(instance, instance);
            }
            max_clients = n_creatures;
            Console.WriteLine($"Reset NEAT instance {GetHashCode()}\n{max_clients} clients\n{input} => {output}");
        }
        public NodeGene MakeNode()
        {
            //new node gene
            NodeGene ng = new NodeGene(highest_innovation_number + 1);
            highest_innovation_number++;
            Global_Nodes.Add(ng, ng);
            return ng;
        }
        public NodeGene GetNode(int innovation_number)
        {
            //if(innovation_number>Global_Nodes.Count) return Global_Nodes[Global_Nodes.Count-1];
            //return Global_Nodes[innovation_number]; //TODO is it innovation number her?
            return Global_Nodes.First(x => x.innovation_number == innovation_number);
        } //TODO, this is not so efficient

        // public NodeGene MakeNodeFromSplit(ConnectionGene cg){
        //     if(cg.SplitIndex==-1) return MakeNode();
        //     NodeGene ng = new NodeGene(cg.SplitIndex);
        //     return ng;
        // }
        public int GetSplitIndex(NodeGene from, NodeGene to)
        {
            ConnectionGene cg = new ConnectionGene(from, to);
            if (Global_Connectons.Contains(cg))
            {
                cg = GetConnection(cg);
                return cg.SplitIndex;
            }
            else
            {
                return -1;
            }
        }
        public void SetSplitIndex(NodeGene from, NodeGene to, int val)
        {
            ConnectionGene cg = new ConnectionGene(from, to);
            GetConnection(cg).SplitIndex = val;
        }
        public ConnectionGene MakeConnection(NodeGene node1, NodeGene node2)
        {
            ///<summary>
            /// n1  ->  n2
            ///Despite the name Make,
            ///this method returns the link if suxh exists,
            ///or makes a new one and stores it in Global_Connections if not
            ///</summary>
            ConnectionGene cg = new ConnectionGene(node1, node2);
            if (Global_Connectons.Contains(cg))
            {
                ConnectionGene existing = Global_Connectons.GetValue(cg);
                cg.innovation_number = existing.innovation_number;
                cg.SplitIndex = existing.SplitIndex;
                //already acts as clone

            }
            else
            {
                //new conn and add to global

                cg.innovation_number = highest_innovation_number + 1;
                highest_innovation_number++;
                Global_Connectons.Add(cg, cg);
                            }
            return cg;
        }
        public ConnectionGene GetConnection(ConnectionGene g)
        {
            return Global_Connectons.GetValue(g);
        }


        public Genotype EmptyGenotype()
        {
            Genotype g = new Genotype(this);
            //moved adding inout and output nodes to Genotype
            return g;
        }
        public void Evolve()
        {
            foreach (Genotype creature in Creatures)
            {

                creature.Mutate();

            }
          

        }

        public void Respeciate()
        {
            genspecies();
        }
        private void genspecies()
        {
            foreach (Species s in Species)
            {
                s.reset();
            }
        }
        private void PutCreaturesInSpecies()
        {
            //ConcurrentBag<Genotype> specieless = new ConcurrentBag<Genotype>();
            //Parallel.ForEach(Creatures, c =>
            //{
            //    if (c.specie != null) return;
            //    bool found = false;
            //    foreach (Species s in Species)
            //    {
            //        if (s.put(c))
            //        {
            //            found = true;
            //            break;
            //        }
            //    }
            //    if (!found)
            //    {
            //        //Species.Add(new Species(c));
            //        specieless.Add(c);  //so its safe with threading
            //    }
            //}
            //);
            foreach (Genotype c in Creatures)
            {
                if (c.specie != null) continue;
                bool found = false;
                foreach (Species s in Species)
                {
                    if (s.put(c))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Species.Add(new Species(c));
                    //specieless.Add(c);  //so its safe with threading
                }
            }
            //if (Species.Count==0) {
            //    Species.Add(new Species(Creatures[0]));
            //}
            //There exists a problem, if many genotypes need to go to same species

            //foreach (Genotype c in specieless)
            //{     //i dont think parallel would suit, writing to dictionary only 1 writer anyway
            //    Species.Add(new Species(c));
            //}
            foreach (Species s in Species)
            {
                s.take_average();
            }
        }
        private void remove_extinct_species()
        {
            for (int i = Species.Count - 1; i >= 0; i--)
            {
                if (Species[i].Members.Count <= 1)
                {
                    Species[i].go_extinct();
                    Species.Remove(i);
                }
            }
        }
        private void Breed()
        {
            int spaces = 0;
            for (int c = 0; c < Creatures.Count; c++)
            {
                if (Creatures[c].specie == null)
                {
                    spaces++;
                    Creatures.Remove(c);
                }
            }

            if (Species.Count == 0)
            {
                Species.Add(new Species(Creatures.GetRandom()));
                return;
            }

            for (int i = 0; i < spaces; i++)
            {
                Creatures.Add(Species.GetRandomBiased().breed());
            }
        }
        public void Kill(double kill = -1)
        {
            if (kill == -1) kill = kill_rate;
            foreach (Species s in Species)
            {
                s.kill(kill);
            }
        }
        public void LifeCycle()
        {
            lock (ClientLock)
            {
                genspecies();
                PutCreaturesInSpecies();
                Kill();
                remove_extinct_species();
                PutCreaturesInSpecies();
                Breed();
                Evolve();
                generations++;
            }

        }

        public void Train(Vector[] ins, Vector[] outs)
        {

            foreach (Genotype creature in Creatures)
            {
                creature.RateFitness(ins, outs);
            }

            LifeCycle();
        }
        public void Train(IDataSet ds)
        {

            foreach (Genotype creature in Creatures)
            {
                creature.RateFitness(ds.RandomBatch(Int32.Parse(Tools.Tools.GetGlobalVar("Random_Batch_Items_Count"))));
            }

            LifeCycle();
        }
        public void Train(IDataSet ds, double target_acc, int MaxIter = 9999)
        {
            int i = 0;
            double acc = 0;
            bool c1 = false;
            bool c2 = false;
            do
            {
                Train(ds);
                Genotype best_guy = GetBest();
                acc = best_guy.RateFitness(ds.RandomBatch( Int32.Parse(Tools.Tools.GetGlobalVar("Random_Batch_Items_Count")) ));
                lock (ClientLock)
                {
                    Best_Dead.Add(best_guy);
                    if (Best_Dead.Count > 10) { Best_Dead.Remove(Best_Dead.Min()); }
                }
                i++;
                if (!c1 && acc > target_acc * 0.9)
                {
                    Console.WriteLine("Change 1");
                    c1 = true;
                    probability_mutate_link *= 0.5;
                    probability_mutate_node *= 0.5;
                    probability_mutate_weight_shift *= 3;
                    specie_distance *= 0.8;
                }
                if (!c2 && acc > target_acc * 0.95)
                {
                    Console.WriteLine("Change 2");
                    c2 = true;
                    probability_mutate_link *= 0.2;
                    probability_mutate_node *= 0.2;
                    probability_mutate_weight_shift *= 2;
                    probability_mutate_weight_shift *= 4;
                    weight_shift_strength *= 0.5;
                }
                if (generations > 50)
                {
                    for (int xx = 0; xx < 10; xx++)
                    {
                        best_guy.Improve(ds.RandomBatch(Int32.Parse(Tools.Tools.GetGlobalVar("Random_Batch_Items_Count"))));
                        double t = best_guy.RateFitness(ds.RandomBatch(Int32.Parse(Tools.Tools.GetGlobalVar("Random_Batch_Items_Count"))));
                        //Tools.Tools.write_to_file(t.ToString(),"log.txt");
                    }
                }
                Console.WriteLine($"Best acc {acc}, iteration {i}");
                best_acc = acc;
                motionless_check.Insert_at_start_Same_Length(best_acc);
                double first_der = Tools.Tools.first_deriv_from_values(motionless_check);
                accel_check.Insert_at_start_Same_Length(first_der);
                double accel = Tools.Tools.first_deriv_from_values(accel_check);
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

            } while (i < MaxIter && acc < target_acc);
        }
        public Vector BestPrediction(Vector inp)
        {
            lock (ClientLock)
            {
                return Creatures.Max().Forwards(inp);
            }
        }
        public Graph GetCreatureAsGraph(int i)
        {
            return Creatures[i].ToGraph();
        }
        public Genotype GetBest()
        {
            lock (ClientLock)
            {
                return Creatures.Max();
            }
        }
        public void PrintBest()
        {
            Creatures.Max().Print();
        }

        public void Save(string path)
        {
            Tools.Tools.Serialize(path, this);
        }
        public static Neat? Load(string path)
        {
            Neat? n = null;
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
#pragma warning disable
            n = (Neat)formatter.Deserialize(fs);
            fs.Close();
            return n;
        }

        [OnDeserializing]
        public void Deserializing(StreamingContext context)
        {
            //Console.WriteLine(context);
            ////debug code
            //Console.WriteLine(max_clients);
            //Console.WriteLine(Global_Nodes);
            //Console.WriteLine(Global_Connectons);
            //Console.WriteLine(Creatures);

        }
        [OnDeserialized] public void OnDeserialized(StreamingContext context)
        {
            Best_Dead = new SortedSet<Genotype>();
        }
        [OnSerializing] public void OnSerializing(StreamingContext context)
        {

        }

    }
}