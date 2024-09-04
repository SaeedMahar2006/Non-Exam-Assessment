using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeaLibrary.Data.Other
{
    [Serializable]
    public class InputMapCache
    {
        public int InputDimension;
        public Dictionary<int, string> InputDescription;
        public List<string> Normalise;
        public List<string> Relative;
        public string Value;
        public InputMapCache(int inputDimension, Dictionary<int, string> inputDescription, List<string> normalise, List<string> relative, string value)
        {
            InputDimension = inputDimension;
            InputDescription = inputDescription;
            Normalise = normalise;
            Relative = relative;
            Value = value;
        }
        public override string ToString()
        {
            string r = "";
            foreach(KeyValuePair<int,string> kvp in InputDescription)
            {
                r += $"{kvp.Key.ToString()}  {kvp.Value}  ({((Normalise.Contains(kvp.Value))?"Normalised":"")},{((Relative.Contains(kvp.Value)) ? "Relativised" : "")})\n";
            }return r;
        }
    }
}
