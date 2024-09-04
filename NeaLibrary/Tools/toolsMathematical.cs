using System;
using System.Linq;
using NeaLibrary.DataStructures;
namespace NeaLibrary.Tools
{
    public partial class Tools{
    //functions
    public static IDataSet Vector_XOR_Example(int len, bool noise=true, double strength=0.005){
        Vector[] ins = new Vector[len];
        Vector[] outs = new Vector[len];
        DataSet ds = new DataSet();
        for (int i = 0; i < len; i++)
        {  
            ins[i] = new Vector(2);
            ins[i][0]= Tools.RandomInt(0,1);
            ins[i][1]= Tools.RandomInt(0,1);
            outs[i] = new Vector(1);
            outs[i][0] = (ins[i][0]==ins[i][1])? 0 : 1;

            ins[i] = VectorRangeNoisify(ins[i], strength);
            outs[i] = VectorRangeNoisify(outs[i], strength);

                ds.Add(ins[i], outs[i]);
        }
        return ds;
    }
        public static IDataSet Vector_XOR_Example2(int len, bool noise=true, double strength=0.005){
        Vector[] ins = new Vector[len];
        Vector[] outs = new Vector[len];
            DataSet ds = new DataSet();
            for (int i = 0; i < len; i++)
        {  
            ins[i] = new Vector(2);
            int[] choice=  new int[]{-1,1};
            ins[i][0]= choice[Tools.RandomInt(0,1)];
            ins[i][1]= choice[Tools.RandomInt(0,1)];
            outs[i] = new Vector(1);
            outs[i][0] = (ins[i][0]==ins[i][1])? -1 : 1;

            ins[i] = VectorRangeNoisify(ins[i], strength);
            outs[i] = VectorRangeNoisify(outs[i], strength);
                ds.Add(ins[i], outs[i]);
            }
        return ds;
    }
    //Vectorial
    public static Vector RandomVector(int dimension, double min, double max){
            Vector r=new Vector(dimension);
            for(int i=0;i<dimension;i++){
                r[i] = Tools.RandomDouble(min,max);
            }
            return r;
        }
        public static Vector[] SplitVectorEvenly(Vector v, int chunks){
            int chunksize = v.dimension/chunks;
            Vector[] r = new Vector[chunks];
            for(int i =0; i<chunks-1; i++){
                r[i] = new Vector(chunksize);
            }
            r[r.Length-1] = new Vector(v.dimension%chunks);
            for(int i =0; i<chunks*chunksize; i++){
                int chunk=i/chunksize;
                r[chunk][i%chunksize]=v[i];
            }
            for(int i=chunks*chunksize;i<v.dimension;i++){
                r[r.Length-1][i] = v[i];
            }

            return r;
        }



        public static double first_deriv_from_values(Vector v)
        {
            if (v.dimension<=1) { return 0; }
            Vector difs = new Vector(v.dimension-1);
            for (int i=0;i<v.dimension-1;i++)
            {
                difs[i] = v[i+1]-v[i];
            }
            return difs.Sum() / difs.dimension;
        }


        public static Vector VectorNoisify(Vector v, double strength=0.01){
            return strength*v;
        }
        public static Vector VectorRangeNoisify(Vector v, double range=0.01){
            Vector r= new Vector(v.dimension);
            for(int i=0;i<v.dimension;i++){
                r[i] = v[i] + RandomDouble(-range,range);
            }
            return r;
        }

        public static bool isPeak(double a, double b, double c)
        {
            if (a>=b) return false;
            if (c>=b) return false;
            return true;
        }
        public static bool isThrough(double a, double b, double c)
        {
            if (a <= b) return false;
            if (c <= b) return false;
            return true;
        }

        /// <summary>
        /// 1 - max
        /// 0 - neither
        /// -1 - min
        /// credit: this code was inspired by a stack overflow answer. see link in comment below
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<(int ,double,int)> ExtractMinimaMaxima(List<double> values)
        {
            if (values.Count < 3) yield break;
            for (int i=1;i<values.Count-1;i++)
            {
                if (isPeak(values[i - 1], values[i], values[i + 1])) yield return (1, values[i], i);
                else if (isThrough(values[i - 1], values[i], values[i + 1])) yield return (-1, values[i], i);
                else yield return (0, values[i], i);
            }
        }

        //public static IEnumerable<Tuple<int, double>> LocalMaxima(IEnumerable<double> source, int windowSize)   //https://stackoverflow.com/questions/5269000/finding-local-maxima-over-a-dynamic-range
        //{
        //    // Round up to nearest odd value
        //    windowSize = windowSize - windowSize % 2 + 1;
        //    int halfWindow = windowSize / 2;

        //    int index = 0;
        //    var before = new Queue<double>(Enumerable.Repeat(double.NegativeInfinity, halfWindow));
        //    var after = new Queue<double>(source.Take(halfWindow + 1));

        //    foreach (double d in source.Skip(halfWindow + 1).Concat(Enumerable.Repeat(double.NegativeInfinity, halfWindow + 1)))
        //    {
        //        double curVal = after.Dequeue();
        //        if (before.All(x => curVal > x) && after.All(x => curVal >= x))
        //        {
        //            yield return Tuple.Create(index, curVal);
        //        }
        //        before.Enqueue(curVal);
        //        before.Dequeue();
        //        after.Enqueue(d);
        //        index++;
        //    }
        //}
    }
}