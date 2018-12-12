using Newtonsoft.Json;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.Connectivity;
using RealTrolli;
using Stripe;
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
    public partial class MyTrolliDetail : MasterDetailPage
    {
        TrollyCreation items = null;

        public MyTrolliDetail(TrollyCreation trollyCreation)
        {
            InitializeComponent();
            LoadData(trollyCreation);
            items = trollyCreation;
            trolliName.Text = trollyCreation.trollyTitle;
            StatusUpdate(trollyCreation.status);

        }

        public void MenuNavigate(Object sender, EventArgs e)
        {

            this.IsPresented = true;
        }


        public void StatusUpdate(string status)
        {
            if(status == "Open")
            {
                statusOpen.IsVisible = true;
                statusOpenBox.IsVisible = true;

                statusOpen.Text = "OPEN";
            }else if(status == "Assign")
            {
                statusAssignBox.IsVisible = true;

                statusAssign.IsVisible = true;
                assignSBProfile.IsVisible = true;
                statusAssign.Text = "ASSIGN";
            }else if(status == "Completed")
            {
                statusCompleteBox.IsVisible = true;
                statusComplete.IsVisible = true;
                statusComplete.Text = "COMPLETED";

            }
            else if (status == "Cancelled by Client")
            {
                statusCancel.IsVisible = true;
                statusCancel.Text = "Cancelled by Client";
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

            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

        }

        public void SBProfile(Object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new SmartBuyerProfile(items.assigneeId));
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }


        public void BackEvent(Object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        public void VerifyTrolli(Object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new ClientVerifyScreen(items.trolliId, items.assigneeId, items.stripeChargeId));
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public async void JobStatusHandler(object sender, EventArgs e)
        {
            try
            {

                bool connect = CrossConnectivity.Current.IsConnected;
                if (connect)
                {

                    if (items.status == "Open")
                    {
                        bool check = await DisplayAlert("", "Are you sure to Cancel the Job", "Yes", "No");
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
                                status = "Cancelled by Client",
                                deliveryDateTime = items.deliveryDateTime,
                                assigneeId = " ",
                                SBNotificationId = " ",
                                clientNotificationId = items.clientNotificationId

                            };
                            //   status.Text = "";
                            StatusUpdate("Cancelled by Client");
                            statusButton.Text = "Re-Open Job";
                            items.status = "Cancelled by Client";
                            apiCall.TrollyCreation(trolly);
                            OnAppearing();
                        }
                    }
                    else if (items.status == "Cancelled by Client" || items.status == "Cancelled by Smart Buyer")
                    {
                        bool check = await DisplayAlert("", "Are you sure to re-open the Job", "Yes", "Cancel");
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
                            //    status.Text = "Open";
                            StatusUpdate("Open");
                            statusButton.Text = "Cancel Job";
                            items.status = "Open";
                            apiCall.TrollyCreation(trolly);
                            OnAppearing();
                        }
                    }
                    else if (items.status == "Assign")
                    {
                        bool check = await DisplayAlert("", "You are about to cancel this Trolli, while it is already assigned to someone. The $5 base fee and $5 as a penalty will still be charged. Are you sure you want to cancel?", "Yes", "No");
                        if (check)
                        {
                            chargePenalty(items.stripeChargeId);
                            ApiCalling apiCall = new ApiCalling();
                            TrollyCreation trolly = new TrollyCreation
                            {
                                createdDate = items.createdDate,
                                lastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy"),
                                trollyTitle = items.trollyTitle,
                                trollyDetail = items.trollyDetail,

                                trolliId = items.trolliId,
                                clientId = items.clientId,//Convert.ToString(userData["simNumber"]),
                                status = "Cancelled by Client",
                                deliveryDateTime = items.deliveryDateTime,
                                assigneeId = " ",
                                SBNotificationId = " ",
                                clientNotificationId = items.clientNotificationId,
                                stripeChargeId = " "


                            };
                            //    status.Text = "Cancelled by Client";

                            StatusUpdate("Cancelled by Client");
                            statusButton.Text = "Re-Open Job";
                            items.status = "Cancelled by Client";
                            apiCall.TrollyCreation(trolly);
                            apiCall.SendNotificationToSB(items.SBNotificationId);
                            await DisplayAlert("", "Your Trolli has been cancelled", "Ok");
                        }


                    }
                }
                else
                {
                    await DisplayAlert("Uh Oh!", "It seems like you are not connected to internet. Retry once your connections is back", "Exit");
                    //  internetCheck.IsVisible = true;
                    //  RetryVariable.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
        }

        public void chargePenalty(string chargeId)
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_Q5wSnyXL03yN0KpPaAMYttOb");
                var chargeOptions = new ChargeCaptureOptions
                {
                    Amount = 1000,
                };
                var chargeService = new ChargeService();
                Charge charge = chargeService.Capture(chargeId, chargeOptions, null);

                DisplayAlert("", "$10 will be charge on your account as penalty", "Ok");
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }


        public void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem == null) return;
                ListStores trollyDetail = e.SelectedItem as ListStores;
                Navigation.PushAsync(new MyTrollyItems(items, trollyDetail.store));

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