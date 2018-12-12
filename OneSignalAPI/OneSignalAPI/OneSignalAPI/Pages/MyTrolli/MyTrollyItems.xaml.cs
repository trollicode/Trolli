using Newtonsoft.Json;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.Connectivity;
using RealTrolli;
using Stripe;
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
    public partial class MyTrollyItems : MasterDetailPage
    {
        TrollyCreation items = null;
        string store = "";
        public MyTrollyItems(TrollyCreation ob, string storeName)
        {
            InitializeComponent();
            items = ob;
            store = storeName;
            trolliName.Text = storeName;
            LoadData();


        }

        public void MenuNavigate(Object sender, EventArgs e)
        {

            this.IsPresented = true;
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

            }else if (status == "Cancelled by Client")
            {
                statusCancel.IsVisible = true;
                statusCancel.Text = "Cancelled by Client";
            }
        }


        protected override void OnAppearing()
        {
            try
            {
                if (items.status == "Open")
                {
                    statusButton.Text = "Cancel Job";
                }
                else if (items.status == "Cancelled by Client" || items.status == "Cancelled by Smart Buyer")
                {
                    statusButton.Text = "Re-open Job";
                }
                else if(items.status == "Assign")
                {
                    statusButton.Text = "Cancel Job";
                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
            base.OnAppearing();
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
                            //  status.Text = "Open";
                            StatusUpdate("Open");
                            statusButton.Text = "Cancel Job";
                        items.status = "Open";
                        apiCall.TrollyCreation(trolly);
                        OnAppearing();
                    }
                }else if(items.status == "Assign")
                {
                    bool check = await DisplayAlert("", "You are about to cancel this Trolli, while it is already assigned to someone. The $5 base fee and plus $5 penalty will still be charged. Are you sure you want to cancel?", "Yes", "No");
                    if (check)
                    {
                        ApiCalling apiCall = new ApiCalling();
                            chargePenalty(items.stripeChargeId);
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
            catch(Exception ex)
            {
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
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
             }
        }


        public void RetryEvent(Object sender, EventArgs e)
        {
            try
            {
                bool connect = CrossConnectivity.Current.IsConnected;
                if (connect)
                {
                 //   internetCheck.IsVisible = false;
                 //   RetryVariable.IsVisible = false;

                }
                else
                {
                    DisplayAlert("Uh Oh!", "It seems like you are not connected to internet. Retry once your connections is back", "Exit");
                  //  internetCheck.IsVisible = true;
                  //  RetryVariable.IsVisible = true;
                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void VerifyTrolli(Object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new ClientVerifyScreen(items.trolliId, items.clientId, items.stripeChargeId));
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }
        public void SBProfile(Object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new SmartBuyerProfile(items.assigneeId));
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

                StatusUpdate(items.status);
                string trollyData = "[" + items.trollyDetail + "]";
                List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(trollyData);
                listView.ItemsSource = posts;


                foreach (Dictionary<string, object> i in posts)
                {
                    if (store == Convert.ToString(i["store"])) {

                        ListProduct ob = new ListProduct()
                        {
                            detailItem = Convert.ToString(i["detailItem"])

                        };

                        dataItems.Add(ob);
                    }
                }

                listView.ItemsSource = dataItems;

            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }


        }


        public void BackEvent(Object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}