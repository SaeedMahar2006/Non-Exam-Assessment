using System.Collections.Generic;
using System.Collections;
using System.Linq;
namespace NeaLibrary.DataStructures
{
    public class HashMap<TKey, TVal>
    {
        /*
        C# is confusing with Hashset, Dictionary, Hashtable. So i made my own
        implementaion. It takes in a TKey and maps it to TVal

        TKey gets hashed and gives position in the array where TVal is
        if not, then linear probing is used. 
        */
        private int size;
        public int Size { get { return size; } }
        public int Count { get { return count; } }
        private int count=0;

        TVal?[] hashtable;
        public HashMap(int Size){
            hashtable = new TVal?[Size];
            size=Size;
            count=0;
        }
        public bool Add(TKey k, TVal v){
            if (count<size){
                int hash=k.GetHashCode();
                hash = hash%size;
                hash += size;
                hash = hash % size;
                if(!Occupied(hash)){
                    hashtable[hash] = v;
                }else{
                    //linear probing for a free space
                    int probed = 0;
                    do{
                        hash++;
                        hash = hash%size;
                        if( !Occupied(hash) ){
                            hashtable[hash] = v;
                        }
                        probed++;
                    } while (Occupied(hash) && probed<size);
                }

                count++;
                return true;

            }else{
                //full
                return false;
            }
        }
        public bool Contains(TKey k, TVal v)
        {
            int hash=k.GetHashCode() % size;
            
            if (hashtable[hash]!=null && hashtable[hash].Equals(v))  //still required as collision's item may have been removed
            {
                return true;
            }
            else
            {
                //linear probing for a free space
                int probed = 0;
                do
                {
                    hash++;
                    hash = hash % size;
                    if (!Occupied(hash))
                    {
                        hashtable[hash] = v;
                    }
                    probed++;
                } while (Occupied(hash) && probed < size);
                return false;
            }
        }
        public bool Occupied(TKey key)
        {
            int hash = key.GetHashCode();
            hash = hash % size;
            return !( hashtable[hash].Equals(default(TVal))) ;
        }
        public bool Occupied(int n)
        {
            if (hashtable[n]==null) return false;
            return !(hashtable[n].Equals(default(TVal)));
        }
        public void Remove(TKey k, TVal v){
            int hash=k.GetHashCode();
            hash = hash%size;
            if(hashtable[hash].Equals(v)){
                hashtable[hash] = default;
            }else{
                //linear probing for correct entry
                int probed = 0;
                do{
                    hash++;
                    hash = hash%size;
                    if(!Occupied(hash)){
                        hashtable[hash] = v;
                    }
                    probed++;
                }while(Occupied(hash) && probed<size);
        }
    }

}
}