using ListViewWithSubListView.Models;
using ListViewWithSubListView.ViewModels;
using OneSignalAPI;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace ListViewWithSubListView.Views
{
    public partial class Hotels : ContentPage
    {

        private HotelsGroupViewModel ViewModel
        {
            get { return (HotelsGroupViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        int quantityCounter = 1;
        public void IncrementQuantity(Object sender, EventArgs e)
        {
            quantityCounter++;
            quantitys.Text = "" + quantityCounter;
        }

        public void DecrementQuantity(Object sender, EventArgs e)
        {
            quantityCounter--;
            quantitys.Text = "" + quantityCounter;
        }

        bool rightSide = false;
        public void HandlerEvent(Object sender, EventArgs e)
        {
            // ListOfStore();
            if (!rightSide)
            {
                rightSlide.Width = 320;
                rightSide = true;
            }
            else
            {
                rightSlide.Width = 40;
                rightSide = false;

            }
            // DisplayAlert("", "Hello World", "Ok");
        }


        private List<Hotels> ListHotel = new List<Hotels>();

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (ViewModel.Items.Count == 0)
                {
                    ViewModel.LoadHotelsCommand.Execute(null);
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.Message);
            }
        }

        public void ItemTaped(Object sender, ItemTappedEventArgs e)
        {
            var name = e.Item as RoomViewModel;
            itemName.Text = name.RoomName;
            rightSlide.Width = 40;
            rightSide = false;


            // DisplayAlert("", "Hello"+name.RoomName, "Ok");
        }
        ListStores listStore = null;
        public Hotels(HotelsGroupViewModel viewModel, ListStores list)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
            listStore = list;
            storeTitle.Text = list.store;
            ListProduct();
            CheckList();
            statesPicker.SelectedIndex = 0;
            Unit.SelectedIndex = 3;
        }


        ObservableCollection<ListProduct> list = null;
        ObservableCollection<string> data = new ObservableCollection<string>();

        public void ItemsCount()
        {


            int count = 0;
            ObservableCollection<ListProduct> listProduct = SharedUserData.addProduct;
            if (listProduct == null)
            {
                //   storeButton.Text = "(" + count + ") Items Added";
            }
            else
            {
                listProduct = SharedUserData.addProduct;
                //    listItems.Clear();
                if (listProduct != null)
                {
                    foreach (ListProduct listData in listProduct)
                    {
                        if (listData.store == "") //storeName.Text
                        {
                            //listItems.Add(listData);
                            count++;
                        }
                    }
                    //listView.ItemsSource = null;
                    //listView.ItemsSource = listItems;

                    //storeButton.Text = "(" + count + ") Items Added";



                }

            }
        }

        public void ViewProduct(Object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ViewProductsList());
        }

        public void AddProduct(Object sender, EventArgs e)
        {
            try
            {
                string itemProduct = itemName.Text;

                int quantity = Convert.ToInt32(quantitys.Text);
                string sizes = "";
                sizes = statesPicker.Items[statesPicker.SelectedIndex];
                string unit = Unit.Items[Unit.SelectedIndex];// "Pack(s)";
                //  string storeSuburbs = storeSuburb.Text;
                string descriptions = note.Text;
                ListProduct ob = new ListProduct()
                {
                    items = itemProduct,
                    unit = unit,
                    quantity = quantity,
                    size = sizes,
                    store = listStore.store,
                    storeSuburb = listStore.storeSuburb,
                    description = descriptions,
                    detailItem = quantity + " " + unit + " " + itemProduct + " " + sizes + " FROM " + listStore.store,
                    rewardCard = false

                };
                list.Add(ob);

                SharedUserData.addProduct = list;

                string item = quantity + " " + unit + " " + itemProduct + " FROM " + listStore.store;


                itemName.Text = "";
                // storeSuburb.Text = "";
                note.Text = "";
                quantitys.Text = "1";
                statesPicker.IsVisible = true;
                // SizeEntry.IsVisible = false;
                statesPicker.SelectedIndex = 0;
                Unit.SelectedIndex = 3;
                //SizeEntryChange = 0;
                // Unit.SelectedIndex = 3;
                ItemsCount();
            } catch (Exception ex)
            {

            }
        }
    


        public void ListProduct()
        {
            //  string itemsName = "";
            //  string content = await _client.GetStringAsync(url);
            //  List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);

            //  itemsName = Convert.ToString(posts[""]);
            try
            {
                data.Add("Soap");
                data.Add("Apple Juice");
                data.Add("Coffee");
                data.Add("Milk");
                data.Add("Barbeque Sauce");
                data.Add("Cheery Pie Filling");
                data.Add("Chicken Noodle Soup, regular");
                data.Add("Green Beans, canned");
                data.Add("Milk Chocolate Bar");
                data.Add("Pasta, Egg Noodles");
                itemName.DataSource = data;
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }


        public void CheckList()
        {
            try
            {
                if (SharedUserData.addProduct == null)
                {
                    list = new ObservableCollection<ListProduct>();
                }
                else
                {
                    list = SharedUserData.addProduct;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

        }

        public void UnFocusedEvent(Object sender, FocusEventArgs e)
        {
            DisplayAlert("", "Hello World", "Ok");
        }


    }
}
