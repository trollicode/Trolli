using OneSignalAPI.BeanClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StoresAdd : ContentPage
	{
        ObservableCollection<string> data = new ObservableCollection<string>();
        ObservableCollection<ListStores> list = new ObservableCollection<ListStores>();

        public StoresAdd ()
		{
			InitializeComponent ();
            ListOfStore();
            autoComplete.DataSource = data;

        }
        public async void SelectionChange(object sender, SelectedItemChangedEventArgs e) {

            //ShareUserData.listStore.Add(new ListStores {listStore = autoComplete.Text});



            if (SharedUserData.listStore == null) { }
            else { list = SharedUserData.listStore; }

            
            string storeName = autoComplete.Text;
            ListStores listData = new ListStores()
            {
                store = storeName
            };
            list.Add(listData);
            SharedUserData.listStore = list;

            await Navigation.PopModalAsync();

        }

        public void ListOfStore()
        {
            data.Add("Barneys New York");
            data.Add("Bealls (Florida)");
            data.Add("Belk");
            data.Add("The Bon-Ton");
            data.Add("Boscov's");
            data.Add("Dillard's");
            data.Add("J. C. Penney");
            data.Add("Central Park Mall");
            data.Add("Mid City Centre");

        }
	}
}