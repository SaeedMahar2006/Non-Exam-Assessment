using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeaLibrary.Data.Other
{
    [Serializable]
    public class NNSpecification
    {
        public int InputDimension;
        public int OutputDimension;
        public string? LastTrainedOn;
        public InputMapCache? InputMapCacheDescription;
        public NNSpecification(int inputDimension, int outputDimension, string? lastTrainedOn, InputMapCache? inputDescription)
        {
            InputDimension = inputDimension;
            OutputDimension = outputDimension;
            LastTrainedOn = lastTrainedOn;
            InputMapCacheDescription = inputDescription;
        }
        public override string ToString()
        {
            return String.Format("Input Dimension {0}\n" +
                "Output Dimension {1}\n" +
                "Last Trained On {2}\n" +
                "InputDescripton {3}\n", InputDimension, OutputDimension, LastTrainedOn, InputMapCacheDescription) ;
        }
    }
}
