﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeaLibrary.Tools
{
    public class RandomHolder
    {
        private static Random _instance;

        public static Random Instance
        {
            get { return _instance ?? (_instance = new Random()); }
        }
    }
    public class Globals
    {
       
    }
}
