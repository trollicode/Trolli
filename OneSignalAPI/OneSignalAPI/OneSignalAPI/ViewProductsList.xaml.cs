using OneSignalAPI.BeanClass;
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
	public partial class ViewProductsList : ContentPage
	{
		public ViewProductsList ()
		{
			InitializeComponent ();
            if (SharedUserData.addProduct == null) { }
            else
            {
                listView.ItemsSource = null;
                listView.ItemsSource = SharedUserData.addProduct;
            }
        }
	}
}