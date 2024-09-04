using System;

namespace NeaLibrary.NeuralNetwork.NEAT
{
    [Serializable]
    public class Gene
    {
        //this isnt to be used for anything
        //just a container for innovation number
        public int innovation_number { get; set; }

        public Gene(int innovation)
        {
            innovation_number = innovation;
        }
        public override int GetHashCode()
        {
            return innovation_number;
        }

    }
    [Serializable]
    public class NodeGene : IComparable<NodeGene>
    {
        public int innovation_number { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        Gene gene;
        public NodeGene(int innovation_numberr)
        {
            gene = new Gene(innovation_numberr);  //do i need a gene class. consider both struct?
            innovation_number = innovation_numberr;
        }

        public override bool Equals(object? obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            NodeGene c = (NodeGene)obj;
            return c.gene == gene;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {

            return gene.GetHashCode();
        }
        public int CompareTo(NodeGene? b)
        {
            if (b == null) return 0; //is this ok
            return innovation_number - b.innovation_number;
        }

    }
    [Serializable]
    public class ConnectionGene : IComparable<ConnectionGene>
    {
        public int innovation_number { get; set; }
        public NodeGene from { get; }
        public NodeGene to { get; }
        public double weight { get; set; }
        public bool enabled = true;

        public int SplitIndex { get; set; }

        public ConnectionGene(NodeGene From, NodeGene To)
        {
            to = To;
            from = From;
            SplitIndex = -1;
        }
        public ConnectionGene Clone()
        {
            ConnectionGene cg = new ConnectionGene(from, to);  //BUG WAS HERE, wrong way from and to
            cg.weight = weight;
            cg.enabled = enabled;
            cg.innovation_number = innovation_number;
            return cg;
        }

        public override bool Equals(object? obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            ConnectionGene c = (ConnectionGene)obj;
            if (c.to == to && c.from == from) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return to.GetHashCode() << 20 + from.GetHashCode();
            // so like (to node innov n) 00000000 (from node innov n)
        }
        public int CompareTo(ConnectionGene? b)
        {
            //   if less than zero   this before other

            if (b == null) return 0; //is this ok
            return innovation_number - b.innovation_number;
        }
    }
}