using NeaLibrary.Data.Other;
using NeaLibrary.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NeaLibrary.NeuralNetwork
{
    public interface INeuralNetwork
    {
        public abstract NNSpecification GetNNSpecification();
        public abstract double Train(IDataSet dataset, int iterations);
        public abstract double Train(IDataSet dataset);
        public abstract Vector BestPrediction(Vector input);

        public abstract Vector AveragePrediction(Vector input);

        public abstract void Pause();
        public abstract void Unpause();
        public abstract void TogglePause();
        public abstract void Terminate();

        public abstract void Save(string path);
        public abstract static INeuralNetwork Load(string path);

        //public abstract Graph ToGraph();

        public abstract event EventHandler<(int, double)> NextGeneration;

        public bool IsCategorical { get; set; }
        public Func<Vector, Vector> CategoryTreshholdFunction { get; set; }


    }
}
