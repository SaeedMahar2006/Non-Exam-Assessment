using NeaLibrary.Data.Other;
using System;
using System.Collections;
using System.Collections.Generic;

using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NeaLibrary.DataStructures
{
    public interface IDataSet : IEnumerable<(Vector, Vector)>
    {
        ///<summary>just an interface so that we can train on live data or old</summary>
        public abstract IEnumerable<(Vector, Vector)> Batch(int size, int start = 0);
        public abstract IEnumerable<(Vector, Vector)> NextBatch(int size, int start = 0);
        //public abstract IEnumerable<(Vector,Vector)> RandomBatch(int size);
        public virtual IEnumerable<(Vector, Vector)> RandomBatch(int size)
        {
            for (int i = 0; i < size; i++)
            {
                int r = Tools.Tools.RandomInt(0, Count() - 1);//since inclusive      -1??? CHECK
                yield return this[r];
            }
        }
        public abstract (Vector,Vector) this[int n]{
            get;
            set;
        }

        public abstract void Save(string location);
        public static IDataSet Load(string location)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        ///<summary>number of elements, -1 for uknown</summary>
        public abstract int Count();

        public abstract InputMapCache GetInputMapCache();
    }

}
