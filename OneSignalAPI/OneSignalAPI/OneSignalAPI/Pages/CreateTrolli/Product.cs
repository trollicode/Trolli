using OneSignalAPI.BeanClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace OneSignalAPI
{
    public class Product : ObservableCollection<ListProduct>
    {
        public string Name { get; set; }

        public bool IsVisible { get; set; }
        public ObservableCollection<ListProduct> list { get; set; }
    }
}
