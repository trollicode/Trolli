using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Newtonsoft.Json;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.Connectivity;
using RealTrolli;
using Syncfusion.SfAutoComplete.XForms;
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
	public partial class CreateTrolli : MasterDetailPage
    {
        ExceptionManagement exceptionObject = new ExceptionManagement();
        ObservableCollection<ListProduct> list = new ObservableCollection<ListProduct>();
        ObservableCollection<ListStores> lists = new ObservableCollection<ListStores>();
        ObservableCollection<string> data = new ObservableCollection<string>();

        public int check = 0;
        public string updateStore = "";
        public string updateStoreSuburb = "";
        public CreateTrolli ()
		{
			InitializeComponent ();
            ListOfStore();

            listView.TranslateTo(0, 30, 100);

            listSection.TranslateTo(0, 0, 100);

            //   listSection.TranslateTo(0, 250, 100);
            autoComplete.DataSource = data;
            ItemsCount();
            scheduleButtonEnable();
        }

        /*   public async void selected_store(Object sender, EventArgs e) {
               string store = storePicker.Items[storePicker.SelectedIndex];
               await Navigation.PushAsync(new AddProduct(store));
           }
        */
        public void BackEvent(Object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        public void MenuNavigate(object sender, EventArgs e)
        {
            this.IsPresented = true;
        }
        protected override void OnAppearing()
        {
            try
            {
                listView.ItemsSource = null;
                listView.ItemsSource = SharedUserData.listStore;
                scheduleButtonEnable();
                if (listView.ItemsSource == null)
                {
                    // listValidationText.Text = "There is no Selected Stroes";
                    ListStores listData = new ListStores()
                    {
                        store = "Any Store",
                        storeSuburb = "(No Suburb)",
                        storeDetail = "Any Store (0 Items)"
                    };

                    lists.Add(listData);
                    SharedUserData.listStore = lists;
                    listView.ItemsSource = null;
                    listView.ItemsSource = SharedUserData.listStore;
                }
                else
                {
                    //  listValidationText.Text = "";

                    listView.ItemsSource = null;
                    listView.ItemsSource = SharedUserData.listStore;
                }
                ItemsCount();


                mainGrid.Height = 1;

                listSection.TranslateTo(0, 0, 100);

                listSection1.TranslateTo(0, 0, 100);
                base.OnAppearing();
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
            
        }

        Button b = null;

        Boolean isListOpen = true;
        public void StoreList(Object sender, EventArgs e)
        {
            if (isListOpen)
            {
                //    listView.TranslateTo(0, 280, 100);

                //    storeButton.TranslateTo(0, 220, 100);
                listSection.TranslateTo(0, -50, 100);
                listSection1.TranslateTo(0, -50, 100);
                mainGrid.Height = 370;
                
                isListOpen = false;
            }
            else
            {
                //  listView.TranslateTo(0, 50, 100);
                //  storeButton.TranslateTo(0, 5, 100);
                mainGrid.Height = 1;

                listSection.TranslateTo(0, 0, 100);

                listSection1.TranslateTo(0, 0, 100);
                isListOpen = true;
            }
            //  storeButton.TranslateTo(0, -20, 100);
        }

        bool rewardCardSelection = true;

        bool switcher = true;
        public void RewardCardHandler(Object sender, EventArgs e)
        {
            if (switcher)
            {
                rewardCard.Opacity = 1;
                rewardCard.TextColor = Color.FromHex("#aed582");
                rewardCard.BorderColor = Color.FromHex("#aed582");
                rewardCardSelection = true;
                switcher = false;
            }
            else
            {
                rewardCard.TextColor = Color.White;
                rewardCard.Opacity = 0.33;
                rewardCard.BorderColor = Color.White;
                rewardCardSelection = false;
                switcher = true;

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

        public void ItemsCount()
        {
            int count = 0;
            ObservableCollection<ListStores> listStores = SharedUserData.listStore;

            if (listStores == null)
            {
                storeButton.Text = "(" + count + ") Stores Added";
            }
            else
            {
                listStores = SharedUserData.listStore;
                //listItems.Clear();
                if (listStores != null)
                {
                    foreach (ListStores listData in listStores)
                    {
                            count++;
                    }
                }
            }

                storeButton.Text = "(" + count + ") Stores  Added";
                listView.ItemsSource = null;
                listView.ItemsSource = listStores;
        }

        string updateStoreName = "";
        public void EditHandler(Object sender, SelectedItemChangedEventArgs e) {
            try
            {

                
                b = (Button)sender; //Change var into Button

                AbsoluteLayout relative = (AbsoluteLayout)b.ParentView; //Change var into AbsoluteLayout
             /*   SfAutoComplete viewEntry1 = (SfAutoComplete)relative.Children[2]; //Change var into SfAutoComplete
                viewEntry1.IsVisible = true;
                Entry viewEntry2 = (Entry)relative.Children[3]; //Change var into Entry
                viewEntry2.IsVisible = true;
                Button updateStoreBtn = (Button)relative.Children[4]; //Change var into Button
                updateStoreBtn.IsVisible = true;
                */
                

                Label labelStore1 = (Label)relative.Children[0]; //Change var into Label
           
                Label labelStore2 = (Label)relative.Children[1]; ////Change var into Label
                                                                 //      labelStore2.IsVisible = false;
                updateStore = labelStore1.Text;
                updateStoreSuburb = labelStore2.Text;

                //  autoComplete.Text = labelStore1.Text;
                autoComplete.Focus();
                updateStoreName = labelStore1.Text;
              //  storesSuburb.Text = labelStore2.Text;
                check = 1;
                addStore.Text = "Update Store";

                mainGrid.Height = 1;

                listSection.TranslateTo(0, 0, 100);

                listSection1.TranslateTo(0, 0, 100);
                isListOpen = true;
                // viewEntry1.DataSource = data;
            }
            catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
            //listStore.IsVisible = true;
            // listStoreSuburb.IsVisible = true;
        }

        public void UpdateStoreName(Object sender, EventArgs e) {

            try
            {
                ObservableCollection<ListStores> listStores = SharedUserData.listStore; //Change ObservableCollection into List
                ObservableCollection<ListProduct> listProducts = SharedUserData.addProduct;
                Button b = (Button)sender; //Change var into Button

                AbsoluteLayout relative = (AbsoluteLayout)b.ParentView; //Change var into AbsoluteLayout
                SfAutoComplete viewEntry1 = (SfAutoComplete)relative.Children[2]; //Change var into SfAutoComplete
                Entry viewEntry2 = (Entry)relative.Children[3]; //Change var into Entry

                Label labelStore1 = (Label)relative.Children[0]; //Change var into Label
                labelStore1.IsVisible = false;

                Label labelStore2 = (Label)relative.Children[1]; //Change var into Label
                labelStore2.IsVisible = false;

                foreach (ListStores list in listStores)
                {
                    if (labelStore1.Text == list.store)
                    {
                        list.store = viewEntry1.Text;
                        if (viewEntry2.Text == "")
                        {
                            list.storeSuburb = "(No Suburbs)";

                        }
                        else
                        {
                            list.storeSuburb = viewEntry2.Text;
                        }
                    }
                }

                if (listProducts != null)
                {
                    foreach (ListProduct listProduct in listProducts)
                    {
                        if (labelStore1.Text == listProduct.store)
                        {
                            listProduct.store = viewEntry1.Text;

                            listProduct.detailItem = listProduct.quantity + " " + listProduct.unit + " " + listProduct.items + " " + listProduct.size + " FROM " + viewEntry1.Text;

                        }
                    }
                }
                SharedUserData.listStore = listStores;
                SharedUserData.addProduct = listProducts;
                viewEntry1.IsVisible = false;
                labelStore1.IsVisible = true;
                labelStore2.IsVisible = true;
                listView.ItemsSource = null;
                listView.ItemsSource = SharedUserData.listStore;
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void Discard(Object sender, EventArgs e)
        {
            try
            {
                ObservableCollection<ListProduct> listProducts = SharedUserData.addProduct;
                string output = "";
                foreach (ListProduct list in listProducts)
                {
                    output += JsonConvert.SerializeObject(list) + ",";
                }

                DisplayAlert("", output, "ok");
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

        }

        public async void DeleteHandler(Object sender, EventArgs e)
        {
            
            try
            {
                bool check = await DisplayAlert("", "All your grocery list under this store will also be removed. Are you sure you want to delete this store from the list?", "Yes", "No");

                if (check)
                {
                    ListStores content = (sender as Button).CommandParameter as ListStores;
                    list = SharedUserData.addProduct;

                    if (list != null)
                    {
                        IEnumerable<ListProduct> obsCollection = (IEnumerable<ListProduct>)SharedUserData.addProduct;
                        var lists = new List<ListProduct>(obsCollection);
                        foreach (ListProduct listData in lists)
                        {
                            if (listData.store == content.store)
                            {
                                SharedUserData.addProduct.Remove(listData);
                            }
                        }
                    }

                    int index = SharedUserData.listStore.IndexOf(content);
                    SharedUserData.listStore.RemoveAt(index);

                    listView.ItemsSource = null;
                    listView.ItemsSource = SharedUserData.listStore;
                    ItemsCount();

                }

            }catch(Exception ex)
            {
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

        }
        
        public void ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
          //  var storeTitle = e.SelectedItem as ListStores;
         //   Navigation.PushAsync(new GroceryList(storeTitle.store, storeTitle.storeSuburb));

        }


        int counter;
        public void ItemSelect(Object sender, SelectedItemChangedEventArgs e) {
            // var store = e.SelectedItem as ListStores;

            try
            {
                counter = 0;
                if (e.SelectedItem == null) return;
                ListStores storeTitle = e.SelectedItem as ListStores;

                list = SharedUserData.addProduct;
               // listItems.Clear();
                if (list != null)
                {
                    foreach (ListProduct listData in list)
                    {
                        if (listData.store == storeTitle.store)
                        {
                        //    listItems.Add(listData);
                            counter++;
                        }
                    }
                }


                string trolliTitleVar = trolliTitle.Text;
                if (trolliTitleVar == "")
                {
                    Navigation.PushModalAsync(new AddProduct(storeTitle.store, storeTitle.storeSuburb, counter, "", storeTitle.rewardCard), true);
                }
                else
                {
                    Navigation.PushModalAsync(new AddProduct(storeTitle.store, storeTitle.storeSuburb, counter, trolliTitleVar, storeTitle.rewardCard),true);
                }
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex) {

                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
            /*   if (_oldData == store)
               {
                   store.IsVisible = !store.IsVisible;
               }
               else {
                   if(_oldData != null) {
                       _oldData.IsVisible = false;
                   }
                   store.IsVisible = true;

               }
               _oldData = store;
               //        var product = e.SelectedItem as ListStores;
               listView.ItemsSource = null;
               listView.ItemsSource = ShareUserData.listStore;
             */
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


        
        public void SaveInDraft(Object sender, EventArgs e)
        {
            try
            {
                string UUIDs = "";
                bool connect = CrossConnectivity.Current.IsConnected;
                if (connect)
                {

                    if (CheckField())
                    {
                        ApiCalling apiCall = new ApiCalling();
                        string clientNotificationId = "";
                        OneSignal.Current.IdsAvailable((playerID, pushToken) =>
                        {
                            clientNotificationId = playerID;
                        //  App.Current.MainPage.DisplayAlert("playerId", id.ToString(), "OK");
                        });

                        Dictionary<string, object> userData = SharedUserData.getUserData;
                        string deliveryDateTime = "";
                        UUIDs = Guid.NewGuid().ToString();

                        deliveryDateTime = "Not set";//currentDate.Text + " " + currentTime.Text;

                        TrollyCreation trolly = new TrollyCreation
                        {
                            createdDate = DateTime.Now.ToString("dd/MM/yyyy"),
                            lastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy"),
                            trollyTitle = trolliTitle.Text,
                            trollyDetail = GetItemsInJson(),
                            trolliId = UUIDs,
                            clientId = Convert.ToString(userData["UniqueID"]),
                            status = "Draft",
                            deliveryDateTime = deliveryDateTime,
                            assigneeId = " ",
                            clientNotificationId = clientNotificationId,
                            SBNotificationId = " "


                        };

                        apiCall.TrollyCreation(trolly);
                        SharedUserData.draftRecord = trolly; 
                        //    SharedUserData.listStore = new ObservableCollection<ListStores>();
                        //    SharedUserData.addProduct = new ObservableCollection<ListProduct>();
                        DisplayAlert("", "Trolli saved", "Ok");
                        // Navigation.PushAsync(new FeesCalculation());
                    }


                    else { DisplayAlert("", "Enter the name Trolly Title", "Ok"); }

                }

                else
                {
                    DisplayAlert("Uh Oh!", "It seems like you are not connected to internet. Retry once your connections is back", "Exit");
                    // internetCheck.IsVisible = true;
                    //   RetryVariable.IsVisible = true;
                }

            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }


        public void AddStore(Object sender, EventArgs e) {
            //  Navigation.PushModalAsync(new StoresAdd());
            try
            {
                string storeNamee = autoComplete.Text;

                if (storeNamee == "")
                {
                    DisplayAlert("", "Please Select any Store", "Ok");
                }
                else
                {

                    bool checkStoress = true;
                    foreach (ListStores list in lists)
                    {
                        if (storeNamee == list.store)
                        {
                            checkStoress = false;
                        }
                    }

                    if (check == 1) // True, When user Update the Store Name
                    {
                        if (checkStoress)
                        {
                            /*    ObservableCollection<ListStores> listStores = SharedUserData.listStore;
                                ObservableCollection<ListProduct> listProducts = SharedUserData.addProduct;

                                AbsoluteLayout relative = (AbsoluteLayout)b.ParentView; //Change var into AbsoluteLayout


                                //   Button b2 = (Button)sender; //Change var into Button

                                //   AbsoluteLayout relative2 = (AbsoluteLayout)b2.ParentView; //Change var into AbsoluteLayout
                                //   SfAutoComplete viewEntry1 = // (SfAutoComplete)relative.Children[2]; //Change var into SfAutoComplete
                                //   Entry viewEntry2 = (Entry)relative.Children[3]; //Change var into Entry

                                //  Label labelStore1 = (Label)relative.Children[0]; //Change var into Label
                                // labelStore1.IsVisible = false;

                                //  Label labelStore2 = (Label)relative.Children[1]; //Change var into Label
                                //  labelStore2.IsVisible = false;


                                foreach (ListStores list in listStores)
                                {
                                    if (updateStore.Contains(list.store))
                                    {
                                        list.store = autoComplete.Text;

                                        //  Label labelStore1 = (Label)relative.Children[0];

                                        //  labelStore1.Text = autoComplete.Text;

                                        if (storesSuburb.Text == "")
                                        {
                                            list.storeSuburb = "(No Suburbs)";

                                        }
                                        else
                                        {
                                            list.storeSuburb = storesSuburb.Text;
                                        }
                                    }
                                }

                                if (listProducts != null)
                                {
                                    foreach (ListProduct listProduct in listProducts)
                                    {
                                        if (updateStore.Contains(listProduct.store))
                                        {
                                            listProduct.store = autoComplete.Text;

                                            listProduct.detailItem = listProduct.quantity + " " + listProduct.unit + " " + listProduct.items + " " + listProduct.size + " FROM " + autoComplete.Text;

                                        }
                                    }
                                }
                                SharedUserData.listStore = listStores;
                                SharedUserData.addProduct = listProducts;
                                b = null;
                                //viewEntry1.IsVisible = false;
                                // labelStore1.IsVisible = true;
                                //labelStore2.IsVisible = true;
                                */
                            ObservableCollection<ListStores> listStores = SharedUserData.listStore; //Change ObservableCollection into List
                            ObservableCollection<ListProduct> listProducts = SharedUserData.addProduct;
                         
                                                    
                            foreach (ListStores list in listStores)
                            {
                                if (updateStoreName.Contains(list.store))
                                {
                                    list.store = autoComplete.Text;
                                    if (storesSuburb.Text == "")
                                    {
                                        list.storeSuburb = "(No Suburbs)";

                                    }
                                    else
                                    {
                                        list.storeSuburb = storesSuburb.Text;
                                    }
                                }
                            }

                            if (listProducts != null)
                            {
                                foreach (ListProduct listProduct in listProducts)
                                {
                                    if (updateStoreName.Contains(listProduct.store))
                                    {
                                        listProduct.store = autoComplete.Text;

                                        listProduct.detailItem = listProduct.quantity + " " + listProduct.unit + " " + listProduct.items + " " + listProduct.size + " FROM " + autoComplete.Text;

                                    }
                                }
                            }
                            SharedUserData.listStore = listStores;
                            SharedUserData.addProduct = listProducts;
                            //  viewEntry1.IsVisible = false;
                            //  labelStore1.IsVisible = true;
                            //  labelStore2.IsVisible = true;
                            ItemsCount();
                          //  listView.ItemsSource = null;
                          //  listView.ItemsSource = SharedUserData.listStore;

                            string trolliTitleVar = trolliTitle.Text;
                            if (trolliTitleVar == "")
                            {
                                Navigation.PushModalAsync(new AddProduct(autoComplete.Text, storesSuburb.Text, 0, "", rewardCardSelection),true);
                            }
                            else
                            {
                                Navigation.PushModalAsync(new AddProduct(autoComplete.Text, storesSuburb.Text, 0, trolliTitleVar, rewardCardSelection),true);

                            }

                        }
                        else
                        {
                            DisplayAlert("", "The Store is already exist in below list", "Ok");
                        }
                        check = 0;
                        addStore.Text = "Add Store";
                        autoComplete.Text = "";
                        storesSuburb.Text = "";


                    }

                    else
                    {
                        if (SharedUserData.listStore == null) { }
                        else { lists = SharedUserData.listStore; }

                        string storeName = autoComplete.Text;
                        bool checkStores = true;
                        foreach (ListStores list in lists)
                        {
                            if (storeName == list.store)
                            {
                                checkStores = false;
                            }
                        }

                        if (checkStores)
                        {
                            string storeSuburbs = "(No Suburb)";
                            if (storesSuburb.Text == "")
                            {
                                storesSuburb.Text = "(No Suburb)";

                            }
                            else { storeSuburbs = storesSuburb.Text; }
                            ListStores listData = new ListStores()
                            {
                                store = storeName,
                                storeSuburb = storeSuburbs,
                                storeDetail = storeName + " (0 Items)",
                                rewardCard = rewardCardSelection

                            };

                            lists.Add(listData);
                            SharedUserData.listStore = lists;


                            listView.ItemsSource = null;
                            listView.ItemsSource = SharedUserData.listStore;

                            string trolliTitleVar = trolliTitle.Text;
                            if (trolliTitleVar == "") {
                                Navigation.PushModalAsync(new AddProduct(storeName, storesSuburb.Text, 0, "", rewardCardSelection),true);
                            }
                            else
                            {
                                Navigation.PushModalAsync(new AddProduct(storeName, storesSuburb.Text, 0, trolliTitleVar, rewardCardSelection),true);

                            }

                        }
                        else
                        {
                            DisplayAlert("", "The Store is already exist in below list", "Ok");
                        }

                        autoComplete.Text = "";
                        storesSuburb.Text = "";
                        if (listView.ItemsSource == null)
                        {
                            //  listValidationText.Text = "There is no Selected Stroes";
                        }
                        else
                        {
                            //   listValidationText.Text = "";
                        }
                    }

                }
                scheduleButtonEnable();
            }
            catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public async void ListOfStore() //List of Stores
        {
            try
            {
                data.Add("Australia Post");
                data.Add("Big W");
                data.Add("Chemist Warehouse");
                data.Add("My Chemist");
                data.Add("Amcal Pharmacy");
                data.Add("Coles");
                data.Add("ALDI");
                data.Add("Woolworths");
                data.Add("IGA");
                data.Add("The Reject Shop");
                data.Add("Priceline");
                data.Add("Kmart");
                data.Add("Bunnings Warehouse");
            }
            catch(Exception ex)
            {
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public bool CheckField()
        {

            try
            {
                if (trolliTitle.Text == "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
                return false;
            }
        }


        public void SaveTrolly(Object sender, EventArgs e){ //Save Trolli Button

            if (CheckField())
            {
                string trolliTitleVar = trolliTitle.Text;
                Navigation.PushAsync(new ScheduledTrolly(trolliTitleVar),true);
            }
            else
            {
                DisplayAlert("", "Please Enter Trolli Title", "OK");
            }
        }
    }
}