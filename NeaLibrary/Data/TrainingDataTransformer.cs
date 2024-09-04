using System;
using System.Collections.Generic;
using System.Linq;
using NeaLibrary.DataStructures;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NeaLibrary.Data
{
    public class TrainingDataTransformer : IEnumerable<Vector>
    {
        IEnumerable<Vector> input;
        List<int> normalise_fields;
        Func<double, double> Normaliser;
        List<int> smooth_fields;
        //int[] smoother_steps;
        const int SMOOTHER_STEPS = 3;

        Func<IEnumerable<double>, double> smoother;
        List<int> relative;
        public TrainingDataTransformer(IEnumerable<Vector> input, List<int> relative , List<int> normalise_fields, Func<double, double> Normaliser, List<int> smooth_fields, Func<IEnumerable<double>, double> smoother, int smoother_steps)
        {
            this.input = input;
            this.normalise_fields = normalise_fields;
            this.Normaliser = Normaliser;
            this.smooth_fields = smooth_fields;
            this.smoother = smoother;
            this.relative = relative;
        }

        public IEnumerator<Vector> GetEnumerator()
        {
            Vector previous = new Vector(input.First().dimension);
            int count = 0;
            Dictionary<int, Queue<double>> for_smoother = new Dictionary<int, Queue<double>>();
            foreach (Vector v in input)
            {
                Vector r = new Vector(v.dimension);
                
                for (int n=0;n<v.dimension;n++)
                {
                    r[n] = v[n]; //default values
                    if (normalise_fields.Contains(n))
                    {
                        r[n] = Normaliser(v[n]);
                    }
                    if (smooth_fields.Contains(n))
                    {
                        if(!for_smoother.ContainsKey(n)) for_smoother[n] = new Queue<double>();
                        r[n] = smoother(for_smoother[n]);
                        if (for_smoother[n].Count >=  SMOOTHER_STEPS) {
                            for_smoother[n].Dequeue();
                        }
                        for_smoother[n].Enqueue(v[n]);
                    }
                    if (relative.Contains(n))
                    {
                        if (count != 0)
                        {
                            r[n] = v[n] / previous[n];
                        }
                        else
                        {
                            r[n] = 1; //SENSIBLE DEFAULT, since this is first value there is nothing before to compare to
                        }
                    }
                }
                count++;
                previous = v;
                yield return r;
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
