﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace OneSignalAPI
{
    public class VeggieModel
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool IsReallyAVeggie { get; set; }
        public string Image { get; set; }
        public VeggieModel()
        {
        }
    }

    public class GroupedVeggieModel : ObservableCollection<VeggieModel>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}
