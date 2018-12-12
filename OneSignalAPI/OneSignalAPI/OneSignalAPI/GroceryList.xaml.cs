using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
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
	public partial class GroceryList : ContentPage
	{

        ObservableCollection<ListProduct> list = new ObservableCollection<ListProduct>();
        ObservableCollection<ListProduct> listItems = new ObservableCollection<ListProduct>();
        ObservableCollection<ListProduct> listProduct = new ObservableCollection<ListProduct>();

        string titleStore = "";
        string storeSuburb = "";
        string storeItemCount = "";
        public int counter = 0;

        public GroceryList (string title, string storeSuburbs)
		{
			InitializeComponent ();
            this.Title = title;
            titleStore = title;
            storeSuburb = storeSuburbs;
		}

        protected override void OnAppearing()
        {
            try
            {
                counter = 0;
                list = SharedUserData.addProduct;
                listItems.Clear();
                if (list != null)
                {
                    foreach (ListProduct listData in list)
                    {
                        if (listData.store == titleStore)
                        {
                            listItems.Add(listData);
                            counter++;
                        }
                    }
                  listView.ItemsSource = null;
                  listView.ItemsSource = listItems;
                }
                storeItemCount = titleStore + " (" + counter + " Items)";
                this.Title = storeItemCount;
                UpdateStoreList();
                base.OnAppearing();
            }catch(Exception ex)
            {
               DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void UpdateStoreList() {

            try
            {
                ObservableCollection<ListStores> lists = SharedUserData.listStore;

                foreach (ListStores list in lists)
                {
                    if (titleStore.Contains(list.store))
                    {
                        ListStores update = new ListStores()
                        {
                            store = list.store,
                            storeSuburb = list.storeSuburb,
                            storeDetail = storeItemCount
                        };
                        lists[lists.IndexOf(list)] = update;
                        //   int index = lists.IndexOf(list);
                        //   lists.RemoveAt(index);
                        //   lists.Insert(index, update);
                    }

                }
            }catch(Exception ex)
            {

               // DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

        }


        public void AddItems(object sender, EventArgs e) {
            try
            {
         //       Navigation.PushAsync(new AddProduct(titleStore, storeSuburb, counter));
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }
//        ObservableCollection<ListStores> listStore = new ObservableCollection<ListStores>();

        public async void DeleteHandler(Object sender, EventArgs e){
            try
            {
                bool check = await DisplayAlert("", "Are you sure Delete this Item from list", "Yes", "Cancel");

                if (check)
                {
                    listProduct = SharedUserData.addProduct;
                    ListProduct content = (sender as Button).CommandParameter as ListProduct;
                    listProduct.Remove(content);
                   

                    SharedUserData.addProduct = listProduct;
                    OnAppearing();
                }

                 /*   list = SharedUserData.addProduct;
                    listItems.Clear();
                    if (list != null)
                    {
                        foreach (ListProduct listData in list)
                        {
                            if (listData.store == titleStore)
                            {
                                listItems.Add(listData);
                            }
                        }

                        listView.ItemsSource = null;
                        listView.ItemsSource = listItems;
                    }
                   */ 
                
            }
            catch(Exception ex)
            {

              await  DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void EditHandler(Object sender, EventArgs e)
        {
            try
            {

                listProduct = SharedUserData.addProduct;
                ListProduct content = (sender as Button).CommandParameter as ListProduct;
                int index = listProduct.IndexOf(content);
                Navigation.PushModalAsync(new AddProduct(content, true, index, counter, content.rewardCard));
            }
            catch (Exception ex) {

                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public async void CloseStore(Object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopAsync();
            }catch(Exception ex)
            {
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

    }
}