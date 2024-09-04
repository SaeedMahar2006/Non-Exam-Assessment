using System;
using NeaLibrary.Tools;
namespace NeaLibrary.DataStructures
{
    [Serializable]
    public class Graph{
        public Matrix adjacencyMatrix;  //REFER TO MY BOOK
        public Matrix incidenceMatrix; //NOT IMPLEMETED
        public Matrix Laplacian; //NOT IMPLEMENTED
        public Vector nodeStates; // REFER TO MY BOOK
        public int EdgeCount;
        public int NodeCount;

        public Dictionary<int, (double,double)> NodesCoordinates = new Dictionary<int, (double, double)>();

        public Vector total_activation_accumulation;
        public Vector total_sum_accumulation;

        //public int maxSize;
        public Vector deltas_accumulation;
        public Graph(int size,int maxEdge=-1){
            if(maxEdge==-1) maxEdge=size*(size-1); //Simple and bidirected there and back so no div by 2
            //wont actually need this many for NEAT

            //CREATES AN EMPTY GRAPH WITH size NODES
            //NO CONNECTIONS YET
            EdgeCount = 0;
            NodeCount = size;
            //maxSize = size;
            adjacencyMatrix = new Matrix(size,size);
            incidenceMatrix = new Matrix(size,maxEdge);
            Laplacian = new Matrix(size,size);
            nodeStates = new Vector(size);
            total_activation_accumulation = new Vector(size);

        }
        public void AddEdge(int to, int from, double weight, bool directed=true){
            if((to>NodeCount) | (from>NodeCount)) throw new Exception();

            if(directed){ adjacencyMatrix[to,from] = weight;}
            else{ adjacencyMatrix[to,from] = weight;adjacencyMatrix[from,to] = weight;}   
            EdgeCount++;
        }
        public void SetEdge(int to, int from, double weight, bool directed = true)
        {
            if ((to > NodeCount) | (from > NodeCount)) throw new Exception();

            if (directed) { adjacencyMatrix[to, from] = weight; }
            else { adjacencyMatrix[to, from] = weight; adjacencyMatrix[from, to] = weight; }
        }
        public void AddNode(double value=0){
            //if() throw new Exception();

                adjacencyMatrix = adjacencyMatrix.Upscale(1,1);
                nodeStates = nodeStates.Upscale(1);

                nodeStates[nodeStates.dimension-1] = value;
                //maxSize++;
                NodeCount++;
        }
        public void SetNode(int i, double val){
            if(i>NodeCount) throw new Exception();
            nodeStates[i] = val;
        }
        public void Update(){
            nodeStates = adjacencyMatrix * nodeStates;
            total_sum_accumulation += nodeStates;

            nodeStates = nodeStates.PutThrough(Tools.Tools.S_ReLu);

        }
        public void ResetAccumulations() {

            total_activation_accumulation = new Vector(nodeStates.dimension);
            deltas_accumulation = new Vector(nodeStates.dimension);
            total_sum_accumulation = new Vector(nodeStates.dimension);
        }

        public void Forward(Vector input, int cycles)
        {
            ResetAccumulations();//is fine here too, fine here too since 1 forward then 1 back
            nodeStates = 0 * nodeStates; //zero everything
            for (int i=0;i<input.dimension;i++)
            {
                nodeStates[i] = input[i];
                total_activation_accumulation[i] = input[i];
            }
            for (int n=0;n<cycles;n++){
                Update();
                total_activation_accumulation += nodeStates;
            }
          //all the accumulations for 1 forward
        }
        public void Backward(int node, double error)
        {
            Console.WriteLine(node);
            //works only for acyclic

            //we got the delta cost / delta a _node

            //see all the ones leading in
            for (int in_node=0; in_node<NodeCount;in_node++)
            {
                if (in_node==node) { continue; }  //self loop for "remembering" shouldnt count
                if (adjacencyMatrix[node, in_node]!=0)
                {
                    
                    double weight = adjacencyMatrix[node, in_node]; //    perhaps consider to chnaging to leaky relu?
                    deltas_accumulation[in_node] += error * weight * Tools.Tools.S_ReLu_Derivative(weight * total_activation_accumulation[in_node]);
                    Backward(in_node, deltas_accumulation[in_node]);
                }
            }
            //all the accumulations for 1 backward
        }


        public void Print(){
            Console.WriteLine($"Printing graph {this.GetHashCode()}\nNodes: {NodeCount}\nEdges: {EdgeCount}");
            Console.WriteLine("Adjacency matrix:");
            adjacencyMatrix.Print();
            Console.WriteLine("\nNode States:");
            nodeStates.Print();
            Console.WriteLine("\n");

        }


        public double this[int to,int from]{
            get{
                return adjacencyMatrix[to,from];
            }set{
                adjacencyMatrix[to,from]=value;
            }
        }
        public double this[int Node]{
            get{
                return nodeStates[Node];
            }set{
                nodeStates[Node]=value;
            }
        }

    }
}