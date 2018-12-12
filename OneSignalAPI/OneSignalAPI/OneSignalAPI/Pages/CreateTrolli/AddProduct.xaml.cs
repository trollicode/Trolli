using Newtonsoft.Json;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProduct : ContentPage
    {

        ExceptionManagement exceptionObject = new ExceptionManagement();
        ObservableCollection<string> data = new ObservableCollection<string>();
        static HttpClient _client = new HttpClient();
        ObservableCollection<ListProduct> list = null;

        //private static string url = "https://jsonplaceholder.typicode.com/users";
        static int SizeEntryChange = 0;
        bool updateState = false;
        int updateIndex = -1;
        string stores;
        string storeSuburbs;
        string trolliTitle = "";
        bool rewardCardSelection = false;
        //  AutoCompleteTextView autoComplete1;



        public AddProduct(string store, string storeSuburb, int counterTrolli, string trolliTitle, bool rewardCard)
        {
          
            InitializeComponent();
            scheduleButtonEnable();

            // this.BackgroundColor = new Color(0, 0, 0, 0.4);
            storeName.Text = store;
          //  counter.Text = ""+counterTrolli;
           // this.Title = store;
            stores = store;
            storeSuburbs = storeSuburb;
            CheckList();
            ListProduct();
            Size.SelectedItem = "Standard";
            Unit.SelectedItem = "Pc(s)";
            Quantity.Text = "1";
            //autoComplete.DisplayMemberPath = "Name";

            this.trolliTitle = trolliTitle;

            autoComplete.DataSource = data;
            listView.TranslateTo(0, 30, 100);
          //  storeButton.TranslateTo(0, -20, 100);

            listSection.TranslateTo(0, 0, 100);
            listSection1.TranslateTo(0, 0, 100);
            rewardCardSelection = rewardCard;
            ItemsCount();

        }
        public AddProduct(ListProduct obj, bool updateStates, int index, int count, bool rewardCard)
        {
            InitializeComponent();
            storeName.Text = "Update Item";
            autoComplete.Text = obj.items;
            Quantity.Text = ""+obj.quantity;
            Size.SelectedItem = obj.size;
            Unit.SelectedItem = obj.unit;
            stores = obj.store;
            storeSuburbs = obj.storeSuburb;
            description.Text = obj.description;
            updateState = updateStates;
           // counter.Text = ""+count;
            updateItemButton.Text = "Update Item";
            updateIndex = index;
            CheckList();
            ListProduct();
            autoComplete.DataSource = data;
            rewardCardSelection = rewardCard;
            // Quantity.Text = ""+obj.quantity;
            scheduleButtonEnable();
        }


        Boolean isListOpen = true;
        public void StoreList(Object sender, EventArgs e)
        {
            if (isListOpen)
            {
                //    listView.TranslateTo(0, 280, 100);

                //    storeButton.TranslateTo(0, 220, 100);

                listSection.TranslateTo(0, -50, 200);

                listSection1.TranslateTo(0, -50, 200);
                listGrid.Height = 370;
                 isListOpen = false;
            }
            else
            {
                //  listView.TranslateTo(0, 50, 100);
                //  storeButton.TranslateTo(0, 5, 100);

                listSection.TranslateTo(0, 0, 300);
                listSection1.TranslateTo(0, 0, 300);

                listGrid.Height = 1;
                isListOpen = true;
            }
            //  storeButton.TranslateTo(0, -20, 100);
        }



        public void ChangeValueHandler(object Sender, SelectedItemChangedEventArgs e){
            try
            {
                if (Size.Items[Size.SelectedIndex] == "Custom Cell")
                {
                    SizeEntryChange = 1;
                    SizeEntry.IsVisible = true;
                    SizeEntry.Focus();
                    Size.IsVisible = false;

                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        ObservableCollection<ListProduct> listProduct = new ObservableCollection<ListProduct>();

        public void EditHandler(Object sender, EventArgs e) {

            listProduct = SharedUserData.addProduct;
            ListProduct obj = (sender as Button).CommandParameter as ListProduct;
            int index = listProduct.IndexOf(obj);
            //      AddProduct(content, true, index, counter);


          //  storeName.Text = "Update Item";
            autoComplete.Text = obj.items;
            Quantity.Text = "" + obj.quantity;
            Size.SelectedItem = obj.size;
            Unit.SelectedItem = obj.unit;
            stores = obj.store;
            storeSuburbs = obj.storeSuburb;
            description.Text = obj.description;
            updateState = true;
            // counter.Text = ""+count;
            updateItemButton.Text = "Update Item";
            updateIndex = index;

            CheckList();
            ListProduct();

            listSection.TranslateTo(0, 0, 100);
            listSection1.TranslateTo(0, 0, 100);

            listGrid.Height = 1;



        }

        public async void DeleteHandler(Object sender, EventArgs e) {
            try
            {
                bool check = await DisplayAlert("", "Are you sure Delete this Item from list", "Yes", "No");

                if (check)
                {
                    listProduct = SharedUserData.addProduct;
                    ListProduct content = (sender as Button).CommandParameter as ListProduct;
                    listProduct.Remove(content);


                    SharedUserData.addProduct = listProduct;
                    ItemsCount();
                }

            }
            catch (Exception ex)
            {
              await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
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
                data.Add("Apple Juice");
                data.Add("Coffee");
                data.Add("Milk");
                data.Add("Barbeque Sauce");
                data.Add("Cheery Pie Filling");
                data.Add("Chicken Noodle Soup, regular");
                data.Add("Green Beans, canned");
                data.Add("Milk Chocolate Bar");
                data.Add("Pasta, Egg Noodles");
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public bool EntryValidation() {

            try
            {
                bool check = true;
                string errorInfo = "";
                if (autoComplete.Text == "")
                {
                    check = false;
                    errorInfo += "Item Name \n";
                }
                if (Quantity.Text == "")
                {
                    check = false;
                    errorInfo += "Quantity \n";
                }
                if (Size.SelectedIndex == -1)
                {
                    check = false;
                    errorInfo += "Item Size\n";

                }
                if (Unit.SelectedIndex == -1)
                {
                    check = false;
                    errorInfo += "Item Unit\n";

                }

                if (description.Text == "")
                {
                    description.Text = " ";
                }

                if (check == false)
                {
                    DisplayAlert("Following required values are missing", errorInfo, "Ok");
                }

                return check;
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                return false;
            }
        }

        public void scheduleButtonEnable()
        {
            if (SharedUserData.addProduct == null)
            {
                scheduleBtn.IsEnabled = false;
            }
            else
            {
                scheduleBtn.IsEnabled = true;

            }
        }



        public void ItemsCount() {


            int count = 0;
            ObservableCollection<ListProduct> listProduct = SharedUserData.addProduct;
            if (listProduct == null)
            {
                storeButton.Text = "(" + count + ") Items Added";
            }
            else
            {
                listProduct = SharedUserData.addProduct;
                listItems.Clear();
                if (listProduct != null)
                {
                    foreach (ListProduct listData in listProduct)
                    {
                        if (listData.store == storeName.Text)
                        {
                            listItems.Add(listData);
                            count++;
                        }
                    }
                    listView.ItemsSource = null;
                    listView.ItemsSource = listItems;

                    storeButton.Text = "(" + count + ") Items Added";



                }

            }
        }



        public async void AddProductEvent(Object sender, EventArgs e)
        {
            try
            {

                if (!updateState)
                {
                    if (EntryValidation())
                    {
                       // int counters = Convert.ToInt16(counter.Text);
                     //   counters++;
                       // counter.Text = "" + counters;
                        string itemProduct = autoComplete.Text;

                        int quantity = Convert.ToInt32(Quantity.Text);
                        string sizes = "";
                        if (SizeEntryChange == 0)
                        {
                            sizes = Size.Items[Size.SelectedIndex];
                        }
                        else { sizes = SizeEntry.Text; }

                        string unit = Unit.Items[Unit.SelectedIndex];
                        //  string storeSuburbs = storeSuburb.Text;
                        string descriptions = description.Text;
                        ListProduct ob = new ListProduct()
                        {
                            items = itemProduct,
                            unit = unit,
                            quantity = quantity,
                            size = sizes,
                            store = stores,
                            storeSuburb = storeSuburbs,
                            description = descriptions,
                            detailItem = quantity + " " + unit + " " + itemProduct + " " + sizes + " FROM " + stores,
                            rewardCard = rewardCardSelection

                        };
                        list.Add(ob);

                        SharedUserData.addProduct = list;

                        string item = quantity + " " + unit + " " + itemProduct + " FROM " + stores;


                        autoComplete.Text = "";
                        // storeSuburb.Text = "";
                        description.Text = "";
                        Quantity.Text = "1";
                        Size.IsVisible = true;
                        SizeEntry.IsVisible = false;
                        Size.SelectedIndex = 3;
                        SizeEntryChange = 0;
                        Unit.SelectedIndex = 3;
                        ItemsCount();
                     //   Plugin.Toast.CrossToastPopUp.Current.ShowToastMessage("Items Successfully Add in Cart");

                    }
                }
                else
                {
                   
                    string itemProduct = autoComplete.Text;

                    int quantity = Convert.ToInt32(Quantity.Text);
                    string sizes = "";
                    if (SizeEntryChange == 0)
                    {
                        sizes = Size.Items[Size.SelectedIndex];
                    }
                    else { sizes = SizeEntry.Text; }

                    string unit = Unit.Items[Unit.SelectedIndex];
                    //  string storeSuburbs = storeSuburb.Text;
                    string descriptions = description.Text;
                    ListProduct ob = new ListProduct()
                    {
                        items = itemProduct,
                        unit = unit,
                        quantity = quantity,
                        size = sizes,
                        store = stores,
                        storeSuburb = storeSuburbs,
                        description = descriptions,
                        detailItem = quantity + " " + unit + " " + itemProduct + " " + sizes + " FROM " + stores,
                        rewardCard = rewardCardSelection

                    };
                    list.RemoveAt(updateIndex);
                    list.Insert(updateIndex,ob);
                    SharedUserData.addProduct = list;

                    storeName.Text = ob.store;
                    updateItemButton.Text = "Add Item";
                    updateState = false;
                    Plugin.Toast.CrossToastPopUp.Current.ShowToastMessage("Item Successfully Update");

                    autoComplete.Text = "";
                    // storeSuburb.Text = "";
                    description.Text = "";
                    Quantity.Text = "1";
                    Size.IsVisible = true;
                    SizeEntry.IsVisible = false;
                    Size.SelectedIndex = 3;
                    SizeEntryChange = 0;
                    Unit.SelectedIndex = 3;
                    ItemsCount();

                    //await Navigation.PopModalAsync();
                }

                int counter;
                counter = 0;
                list = SharedUserData.addProduct;
                listItems.Clear();
                if (list != null)
                {
                    foreach (ListProduct listData in list)
                    {
                        if (listData.store == stores)
                        {
                            listItems.Add(listData);
                            counter++;
                        }
                    }
                    //listView.ItemsSource = null;
                    //listView.ItemsSource = listItems;
                }
                storeItemCount = stores + " (" + counter + " Items)";

                UpdateStoreList();

                scheduleButtonEnable();
                //  listView.ItemsSource = SharedUserData.addProduct;
            }
            catch (Exception ex)
            {
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");

            }
            //list.Add(item);
          //  DisplayAlert("Test", itemProduct, "ok");
        }




        public void CheckList() {
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


        ObservableCollection<ListProduct> listItems = new ObservableCollection<ListProduct>();

        string storeItemCount = "";
        public async void SaveEvent(Object sender, EventArgs e)
        {
               if (trolliTitle == "")
               {
                await DisplayAlert("", "Trolli title is empty, please name this trolli", "Ok");
                await Navigation.PopModalAsync();
               }
               else
               {
                   //schduleGrid.IsVisible = true;
                   //listSection.IsVisible = false;
                   await Navigation.PushAsync(new ScheduledTrolly(trolliTitle));
               }
        //  await  DisplayAlert("", GetItemsInJson(), "ok");
            }

        public string GetItemsInJson()
        {
            try
            {
                ObservableCollection<ListProduct> listProducts = SharedUserData.addProduct;
                string output = "";
                foreach (ListProduct list in listProducts)
                {
                    output += JsonConvert.SerializeObject(list) + ",";
                }

                return output;
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                return "";
            }
        }

        public void BackEvent(Object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }


        public void UpdateStoreList()
        {

            try
            {
                ObservableCollection<ListStores> lists = SharedUserData.listStore;

                foreach (ListStores list in lists)
                {
                    if (stores.Contains(list.store))
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

                SharedUserData.listStore = lists;
            }
            catch (Exception ex)
            {
                // DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

        }
    }

}
  