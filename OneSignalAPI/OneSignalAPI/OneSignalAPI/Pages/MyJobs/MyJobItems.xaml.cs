using Newtonsoft.Json;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.Connectivity;
using Plugin.Geolocator;
using RealTrolli;
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
	public partial class MyJobItems : MasterDetailPage
    {
        TrollyCreation items = null;
        string store = "";

        public MyJobItems (TrollyCreation ob, string storeName, int itemCount)
		{
			InitializeComponent ();
            items = ob;
            store = storeName;
            trolliName.Text = storeName;
            itemsCount = itemCount;
            StatusUpdate(ob.status);
            LoadData();
		}

        public void BackEvent(Object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        public void MenuNavigate(Object sender, EventArgs e)
        {
            this.IsPresented = true;
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

        public void StatusUpdate(string status)
        {
            if (status == "Assign")
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
            
                statusAssignBox.IsVisible = false;
                statusAssign.IsVisible = false;
                assignSBProfile.IsVisible = false;

                statusCompleteBox.IsVisible = false;
                statusComplete.IsVisible = false;


                statusCancel.IsVisible = true;
                statusCancel.Text = "Cancelled by Shopper";
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
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

        }


        public void LoadData()
        {
            try
            {
                List<ListProduct> dataItems = new List<ListProduct>();
                //  title.Text = items.trollyTitle;
               
                string trollyData = "[" + items.trollyDetail + "]";
                List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(trollyData);
                listView.ItemsSource = posts;


                foreach (Dictionary<string, object> i in posts)
                {
                    if (store == Convert.ToString(i["store"]))
                    {

                        ListProduct ob = new ListProduct()
                        {
                            detailItem = Convert.ToString(i["detailItem"])

                        };

                        dataItems.Add(ob);
                    }
                }

                listView.ItemsSource = dataItems;

            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
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
            }
            catch (Exception ex)
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
                                    clientNotificationId = items.clientNotificationId
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





        public void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

        }

    }
}