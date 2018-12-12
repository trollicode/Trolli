using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeaderFooterXaml : ContentPage
	{
        class Monkey
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
        class MonkeyVM
        {
            public List<Monkey> Monkeys { get; set; }
            public string Intro { get { return "Monkey Header"; } }
            public string Summary { get { return " There were " + Monkeys.Count + " monkeys"; } }
        }

        public HeaderFooterXaml ()
		{
			InitializeComponent ();
            var monkeys = new List<Monkey> {
                new Monkey {Name="Xander", Description="Writer"},
                new Monkey {Name="Rupert", Description="Engineer"},
                new Monkey {Name="Tammy", Description="Designer"},
                new Monkey {Name="Blue", Description="Speaker"},
            };
            BindingContext = new MonkeyVM { Monkeys = monkeys };
        }
	}
}