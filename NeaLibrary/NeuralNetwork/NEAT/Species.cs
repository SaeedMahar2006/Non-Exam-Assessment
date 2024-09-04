using System;
using System.Linq;
using NeaLibrary.DataStructures;

namespace NeaLibrary.NeuralNetwork.NEAT
{
    [Serializable]
    public class Species : IComparable<Species>
    {
        ///<summary>This is a class to represent the species</summary>
        public RandomHashedSet<Genotype> Members;
        Genotype Representitive;
        Neat master;
        double average_fitness;
        public Species(Genotype representative)
        {
            Members = new RandomHashedSet<Genotype>();
            Representitive = representative;
            representative.specie = this;
            Members.Add(Representitive);
            master = representative.master;
        }
        public bool put(Genotype creature)
        {
            if (creature.Distance(Representitive) < master.specie_distance)
            {
                Members.Add(creature);
                return true;
            }
            return false;
        }
        public void force_put(Genotype creature)
        {
            Members.Add(creature);
        }
        public void go_extinct()
        {
            foreach (Genotype g in Members)
            {
                g.specie = null;
            }
        }
        public double take_average()
        {
            double sum = 0;
            if (Members.Count == 0) return 0;
            sum = Members.Sum(x => x.fitness);
            return sum / Members.Count;
        }
        public void reset()
        {
            Genotype newrep = Members.GetRandom();
            go_extinct();
            Members.Clear();
            newrep.specie = this;
            average_fitness = 0;
            //this.Representative
            Representitive = newrep;
            force_put(newrep);
        }
        public void kill(double rate)
        {
            double victims = rate * Members.Count;
            for (int i = 0; i < victims; i++)
            {
                Genotype victm = Members.Min();
                victm.specie = null;
                Members.Remove(victm);
            }
        }
        public Genotype breed()
        {
            Genotype a = Members.GetRandom();
            Genotype b = Members.GetRandom();
            Genotype c = a.fitness > b.fitness ? Genotype.Crossover(a, b) : Genotype.Crossover(b, a);
            force_put(c);
            return c;
        }

        public int CompareTo(Species? other)
        {
            if (other == null) throw new Exception();
            return average_fitness.CompareTo(other.average_fitness);
        }
    }
}