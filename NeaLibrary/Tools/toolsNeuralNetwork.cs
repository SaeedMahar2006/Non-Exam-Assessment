using System;
using System.Text.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using NeaLibrary.DataStructures;

namespace NeaLibrary.Tools{
    public static partial class Tools{
        public static Func<double,double> Tanh = x => Math.Tanh(x);
        public static Func<double,double> Derivative_Tanh = x=> 1-Math.Pow(Tanh(x),2);
        public static Func<double,double> Sigmoid = x => 1/(1+Math.Pow(Math.E,-x));
        public static Func<double,double> Sigmoid_Derivative = x => Sigmoid(x)*(1-Sigmoid(x));
        public static Func<double,double> Leaky_ReLu = x => (x>0) ? x : 0.01*x;
        public static Func<double,double> Leaky_ReLu_Derivative = x => (x>0) ? 1 : 0.01;
        public static Func<double,double> ReLu = x => (x>0)?  x:0 ;
        public static Func<double,double> ReLu_Derivative = x => (x>0)?  1:0.01 ;
        public static Func<double, double> ReLu2 = x => (x > -1) ? ((x>1)? 1 : x ) : -1;
        public static Func<double, double> ReLu2_Derivative = x => (x > -1) ? ((x > 1) ? 0 : 1) : 0;
        public static Func<double,double> S_ReLu = x => (x<1)?  ReLu(x): 1;
        public static Func<double,double> S_ReLu_Derivative = x => (x<1)?  ReLu_Derivative(x): 0.01;

        public static Func<double, double> SimpleTreshholdFunction = x =>
        {
            double v=0;
            if (x < -0.5) v = -1; else if (x > 0.7) v = 1; else if (-0.5 < x & x < 0.7) v = 0;
            return v;
        };

        
        public static double MSE(Vector actual, Vector target){
            if (actual.dimension != target.dimension) throw new Exception();
            double r= 0;
            for(int i =0; i<actual.dimension;i++){
                r+=0.5*Math.Pow(target[i]-actual[i],2);
            }
            return r;
        }
        public static double derivative_MSE(double aL_j, double y){
            return aL_j - y;
        }

        public static double Avg_Cost(Vector[] a, Vector[] b ){
            if (a.Length != b.Length) throw new Exception();
            double s=0;
            for (int i=0;i<a.Length;i++){
                s+=MSE(a[i],b[i]);
            }return s/a.Length;
        }

        public static Vector Softmax(Vector rawinput)
        {
            //https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregate?view=net-8.0
            double expsum = rawinput.Aggregate(0.0, (total, v) => total+=Math.Exp(v));
            Vector result = new Vector(rawinput.dimension);
            for (int i = 0; i < rawinput.dimension; i++)
            {
                result[i] = Math.Exp(rawinput[i])/expsum;
            }
            return result;
        }
    }


}