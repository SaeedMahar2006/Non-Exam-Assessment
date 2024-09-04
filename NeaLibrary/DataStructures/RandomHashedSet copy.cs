using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NeaLibrary.Tools;
namespace NeaLibrary.DataStructures
{
    public class RandomHashedSetINDEV<T>:IEnumerable<T> where T:IComparable<T>{

        Hashtable hashset;
        List<T> values;
        public int Count;

        public RandomHashedSetINDEV(){
            hashset = new Hashtable();
            values = new List<T>();
            Count=0;
        }
        public bool Contains(T key){
            return hashset.Contains(key);
        }

        public void Add(T key,T val){
            if(!this.Contains(val)){
                hashset.Add(key,val);
                values.Add(val);
                Count++;
            }
        }
        public void Add(T val){
            this.Add(val,val);
        }
        public void Remove(T val){
            if(this.Contains(val)){
                hashset.Remove(val);
                values.Remove(val);
                Count--;
            }
        }
        public void Remove(int i){
            if(i<0) return;
            if(i>=Count) return;
            T val = values[i];
            Remove(val);
        }

        public T? GetValue(T key){
            return (T?)hashset[key];
        }
        public T GetValue(int index){
            return values[index];
        }
        public void AddSorted(T val){   //fix this horrible disgrace
            if(this.Count==0){ Add(val,val);}
            else{
            for(int i = 0; i<this.Count; i++){
                if((val.CompareTo(values[i])<=0)|(i==Count-1)){   //   <0
                    values.Insert(i, val);
                    hashset.Add(val,val);
                    Count++;
                    return;
                }
            }
            }
        }

        public T GetRandom(){
            if(Count==0) throw new Exception();
            return values[Tools.Tools.RandomInt(0, values.Count-1)];
        }
        public int IndexOf(T val){
            return values.IndexOf(val);
        }
        public T Max(){
            //should REALLY be last
            return values.Last<T>();
        }
        public T this[int i]{
            get{
                return values[i];
            }
        }

        
        public IEnumerator<T> GetEnumerator(){
            return values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator(){
            return values.GetEnumerator();
        }
    }    
}