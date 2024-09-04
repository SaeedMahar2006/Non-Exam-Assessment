using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


public class MultiMap<TKey,TVal>
{

    Dictionary<TKey, List<TVal>> _dictionary =
        new Dictionary<TKey, List<TVal>>();
    
    public void Add(TKey key, TVal value)
    {
        // Add a key.
        List<TVal> list;
        if (this._dictionary.TryGetValue(key, out list))
        {
            list.Add(value);
        }
        else
        {
            list = new List<TVal>();
            list.Add(value);
            this._dictionary[key] = list;
        }
    }
    public void Add(TKey key)
    {
        // Add a key.
        List<TVal> list;
        if (this._dictionary.TryGetValue(key, out list))
        {
            Console.WriteLine("Already exists key");
        }
        else
        {
            list = new List<TVal>();
            this._dictionary[key] = list;
        }
    }

    public void Clear()
    {
        _dictionary.Clear();
    }
    
    public IEnumerable<TKey> Keys
    {
        get
        {
            return this._dictionary.Keys;
        }
    }
    
    public List<TVal> this[TKey key]
    {
        get
        {
            List<TVal> list;
            if (!this._dictionary.TryGetValue(key, out list))
            {
                list = new List<TVal>();
                this._dictionary[key] = list;
            }
            return list;
        }
    }
}