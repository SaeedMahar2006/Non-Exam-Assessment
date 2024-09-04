using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeaLibrary.Data.Other
{
    public class SettingsItem
    {
        public string Value { get; set; }
        public string TypeName { get; set; }
        public bool Categorical { get; set; }
        public object[] Categories { get; set; }
        public bool PathSelector { get; set; }
        public string FileExtension { get; set; }
        public bool IsNumeric { get; set; }
        public double[] BoundsAndStep { get; set; }
        public bool IsRegex { get; set; }
        public string Regex { get; set; }

        public SettingsItem(string value, string type, bool categorical, object[] categories, bool pathSelector, string fileExtension, bool isNumeric, double[] boundsAndStep, bool isRegex, string regex)
        {
            Value = value;
            TypeName = type;//typeof(testc.GetType()).FullName!;
            Categorical = categorical;
            Categories = categories;
            PathSelector = pathSelector;
            FileExtension = fileExtension;
            PathSelector=pathSelector;
            FileExtension=fileExtension;
            IsNumeric = isNumeric;
            BoundsAndStep = boundsAndStep;
            IsRegex = isRegex;
            Regex = regex;
        }
    }
}
