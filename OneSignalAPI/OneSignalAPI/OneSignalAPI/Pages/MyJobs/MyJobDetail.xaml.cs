using Newtonsoft.Json;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.Connectivity;
using Plugin.Geolocator;
using RealTrolli;
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
	public partial class MyJobDetail : MasterDetailPage
    {
        TrollyCreation items = null;
        public MyJobDetail (TrollyCreation ob)
		{
			InitializeComponent ();
            items = ob;
            title.Text = ob.trollyTitle;
            StatusUpdate(ob.status);
            GetConnection();
        }

        public void StatusUpdate(string status)
        {
            if (status == "Open")
            {
                statusOpen.IsVisible = true;
                statusOpenBox.IsVisible = true;

                statusOpen.Text = "OPEN";
            }
            else if (status == "Assign")
            {
                statusAssignBox.IsVisible = true;

                statusAssign.IsVisible = true;
                assignSBProfile.IsVisible = true;
                statusAssign.Text = "ASSIGN";
            }
            else if (status == "Completed")
            {
                statusCompleteBox.IsVisible = true;
                statusComplete.IsVisible = true;
                statusComplete.Text = "COMPLETED";

            }
            else if (status == "Cancelled by Shopper")
            {
                statusOpen.IsVisible = false;
                statusOpenBox.IsVisible = false;

                statusAssignBox.IsVisible = false;
                statusAssign.IsVisible = false;
                assignSBProfile.IsVisible = false;

                statusCompleteBox.IsVisible = false;
                statusComplete.IsVisible = false;


                statusCancel.IsVisible = true;
                statusCancel.Text = "Cancelled by Shopper";
            }
        }

        public void MenuNavigate(Object sender, EventArgs e)
        {

            this.IsPresented = true;
        }


        private void GetConnection()
        {

            bool connect = CrossConnectivity.Current.IsConnected;
            if (connect)
            {
                LoadData(items);
            }
            else
            {
                DisplayAlert("Uh Oh!", "It seems like you are not connected to internet. Retry once your connections is back", "Exit");
             //   internetCheck.IsVisible = true;
             //   RetryVariable.IsVisible = true;
            }

        }

        public void RetryEvent(Object sender, EventArgs e)
        {
            try
            {

               // internetCheck.IsVisible = false;
               // RetryVariable.IsVisible = false;

                GetConnection();
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }


            }




        protected override void OnAppearing()
        {
            try
            {
                if (items.status == "Assign")
                {
                    statusButton.IsVisible = true;
                    statusButton.Text = "Cancel Job";
                }
                else if (items.status == "Completed")
                {

                    statusButton.IsEnabled = false;
                    jobCompletedButton.IsEnabled = false;

                }
                else if (items.status == "Cancelled by Shopper")
                {
                    statusButton.IsEnabled = false;

                }

                base.OnAppearing();
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public async void CancelJobHandler(object sender, EventArgs e)
        {
            try
            {
                bool connect = CrossConnectivity.Current.IsConnected;

                var locator = CrossGeolocator.Current;
                if (connect)
                {
                    if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                    {
                        await DisplayAlert("", "Trolli app requires access to location services, in order to function properly. If you do not allow location access, some features may not work as expected.", "Ok");
                    }
                    else
                    {
                        if (items.status == "Assign")
                        {
                            bool check = await DisplayAlert("", "Are you sure to Cancel the Job?", "Yes", "No");

                            if (check)
                            {
                                ApiCalling apiCall = new ApiCalling();
                                TrollyCreation trolly = new TrollyCreation
                                {
                                    createdDate = items.createdDate,
                                    lastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy"),
                                    trollyTitle = items.trollyTitle,
                                    trollyDetail = items.trollyDetail,
                                    trolliId = items.trolliId,
                                    clientId = items.clientId,//Convert.ToString(userData["simNumber"]),
                                    status = "Open",
                                    deliveryDateTime = items.deliveryDateTime,
                                    assigneeId = " ",
                                    SBNotificationId = " ",
                                    clientNotificationId = items.clientNotificationId,
                                    stripeChargeId = items.stripeChargeId
                                };
                                StatusUpdate("Cancelled by Shopper");
                                // statusButton.Text = "Re-Open Job";
                                items.status = "Cancelled by Shopper";
                                apiCall.TrollyCreation(trolly);
                                apiCall.SendNotificationToClient(items.clientNotificationId);
                                apiCall.SendNotificationWhenSBCancelled(items.trolliId, items.clientNotificationId, items.SBNotificationId);

                                OnAppearing();
                            }
                        }
                }
                }
                else
                {
                   await DisplayAlert("Uh Oh!", "It seems like you are not connected to internet. Retry once your connections is back", "Exit");
                 //   internetCheck.IsVisible = true;
                 //   RetryVariable.IsVisible = true;
                }

            }
            catch (Exception ex)
            {
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
        }
        int itemsCount = 0;
        public void JobCompleted(Object sender, EventArgs e)
        {
            try
            {
                bool connect = CrossConnectivity.Current.IsConnected;
                if (connect)
                {
                    Navigation.PushAsync(new EnterGroceryAmount(items, itemsCount));
                }
                else
                {
                    DisplayAlert("Uh Oh!", "It seems like you are not connected to internet. Retry once your connections is back", "Exit");
                 //   internetCheck.IsVisible = true;
                //    RetryVariable.IsVisible = true;
                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
            
        }

       


        public void ClientProfile(Object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new ClientProfile(items.clientId));
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }



        List<string> listData = new List<string>();
        public void LoadData(TrollyCreation trollyCreation)
        {
            try
            {
                //   if (e.SelectedItem == null) return;
                //   TrollyCreation trollyDetail = (sender as Button).CommandParameter as TrollyCreation;
                ObservableCollection<ListProduct> dataItems = new ObservableCollection<ListProduct>(); 

                ObservableCollection<ListStores> listStores = new ObservableCollection<ListStores>();
                SharedUserData.addProduct = null;
                string trollyData = "[" + trollyCreation.trollyDetail + "]";
                List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(trollyData);
                //   listView.ItemsSource = posts;

                bool check = true;
                foreach (Dictionary<string, object> i in posts)
                {

                    itemsCount++;
                    ListProduct ob = new ListProduct()
                    {
                        detailItem = Convert.ToString(i["detailItem"]),
                        description = Convert.ToString(i["description"]),
                        items = Convert.ToString(i["items"]),
                        quantity = Convert.ToInt32(i["quantity"]),
                        size = Convert.ToString(i["size"]),
                        store = Convert.ToString(i["store"]),
                        storeSuburb = Convert.ToString(i["storeSuburb"]),
                        unit = Convert.ToString(i["unit"])

                    };

                    if (check)
                    {
                        listData.Add(ob.store);

                        ListStores listStore = new ListStores()
                        {
                            store = Convert.ToString(i["store"]),
                            storeSuburb = Convert.ToString(i["storeSuburb"])
                        };
                        listStores.Add(listStore);
                        check = false;
                    }
                    else
                    {
                        if (listData.Contains(ob.store)) { }
                        else
                        {
                            listData.Add(ob.store);

                            ListStores listStore = new ListStores()
                            {
                                store = Convert.ToString(i["store"]),
                                storeSuburb = Convert.ToString(i["storeSuburb"])
                            };
                            listStores.Add(listStore);
                        }
                    }

                    dataItems.Add(ob);

                }

                List<ListStores> listViewStore = new List<ListStores>();

                if (listStores != null)
                {
                    foreach (ListStores listStore in listStores)
                    {
                        int counter = 0;
                        foreach (ListProduct listData in dataItems)
                        {
                            if (listData.store == listStore.store)
                            {
                                counter++;
                            }
                        }
                        ListStores listStorez = new ListStores()
                        {
                            store = listStore.store,
                            storeSuburb = listStore.storeSuburb,
                            storeDetail = listStore.store + " (" + counter + " Items)"
                        };

                        listViewStore.Add(listStorez);
                    }

                    //  SharedUserData.listStore = listStores;



                }
                listView.ItemsSource = listViewStore;

            }
            catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }


        }
        public void BackEvent(Object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        public void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem == null) return;
                ListStores trollyDetail = e.SelectedItem as ListStores;
                Navigation.PushAsync(new MyJobItems(items, trollyDetail.store, itemsCount));

                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }

        }
    }
}