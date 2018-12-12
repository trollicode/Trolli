using Com.OneSignal;
using Newtonsoft.Json;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.Connectivity;
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
	public partial class ScheduledTrolly : ContentPage
	{


        public static string UUIDs = "";
        string trolliTitlex = "";
        public ScheduledTrolly (string trolliTitle)
		{
			InitializeComponent ();
            
            currentDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            currentTime.Text = DateTime.Now.ToString("HH:mm:ss");
            statesPicker.SelectedItem = "Now";
            trolliTitlex = trolliTitle;
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
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                return "";
            }
        }

        public void BackEvent(Object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }


        public bool CheckField() {

            try
            {
                if (trolliTitlex == "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
                return false;
            }
        }


        public void SaveTrolly(Object sender, EventArgs e)
        {
            try
            {
                bool connect = CrossConnectivity.Current.IsConnected;
                TrollyCreation trolliDraftData = SharedUserData.draftRecord;
                if (connect)
                {
                    if (trolliDraftData == null)
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
                            if (datePicker.IsVisible == true && timePicker.IsVisible == true)
                            {
                                DateTime dateOnly = datePicker.Date;
                                //DateTime dateOnly = timePicker.Time;
                                deliveryDateTime = dateOnly.ToString("dd/MM/yyyy") + " " + timePicker.Time.ToString();
                            }
                            else
                            {
                                deliveryDateTime = "Immediate";//currentDate.Text + " " + currentTime.Text;
                            }
                            TrollyCreation trolly = new TrollyCreation
                            {
                                createdDate = DateTime.Now.ToString("dd/MM/yyyy"),
                                lastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy"),
                                trollyTitle = trolliTitlex,
                                trollyDetail = GetItemsInJson(),
                                trolliId = UUIDs,
                                clientId = Convert.ToString(userData["UniqueID"]),
                                status = "Open",
                                deliveryDateTime = deliveryDateTime,
                                assigneeId = " ",
                                clientNotificationId = clientNotificationId,
                                SBNotificationId = " "


                            };

                          //  apiCall.TrollyCreation(trolly);

                            //    SharedUserData.listStore = new ObservableCollection<ListStores>();
                            //    SharedUserData.addProduct = new ObservableCollection<ListProduct>();

                            Navigation.PushAsync(new FeesCalculation(trolly));
                        }


                        else { DisplayAlert("", "Enter the name Trolly Title", "Ok"); }

                    }else
                    {

                        ApiCalling apiCall = new ApiCalling();
                        string deliveryDateTime = "";
                        if (datePicker.IsVisible == true && timePicker.IsVisible == true)
                        {
                            DateTime dateOnly = datePicker.Date;
                            //DateTime dateOnly = timePicker.Time;
                            deliveryDateTime = dateOnly.ToString("dd/MM/yyyy") + " " + timePicker.Time.ToString();
                        }
                        else
                        {
                            deliveryDateTime = "Immediate";//currentDate.Text + " " + currentTime.Text;
                        }
                        TrollyCreation trolly = new TrollyCreation
                        {
                            createdDate = trolliDraftData.createdDate,
                            lastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy"),
                            trollyTitle = trolliTitlex,
                            trollyDetail = GetItemsInJson(),
                            trolliId = trolliDraftData.trolliId,
                            clientId = trolliDraftData.clientId,
                            status = "Open",
                            deliveryDateTime = deliveryDateTime,
                            assigneeId = " ",
                            clientNotificationId = trolliDraftData.clientNotificationId,
                            SBNotificationId = " "


                        };

                       // apiCall.TrollyCreation(trolly);

                        //    SharedUserData.listStore = new ObservableCollection<ListStores>();
                        //    SharedUserData.addProduct = new ObservableCollection<ListProduct>();
                        SharedUserData.draftRecord = null;
                        Navigation.PushAsync(new FeesCalculation(trolly));

                    }
                }
                else
                {
                    DisplayAlert("Uh Oh!", "It seems like you are not connected to internet. Retry once your connections is back", "Exit");
                    // internetCheck.IsVisible = true;
                    //   RetryVariable.IsVisible = true;
                }
            }
            catch (Exception ex)
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
                  //  internetCheck.IsVisible = false;
                 //   RetryVariable.IsVisible = false;

                }
                else
                {
                    DisplayAlert("Uh Oh!", "It seems like you are not connected to internet. Retry once your connections is back", "Exit");
                  //  internetCheck.IsVisible = true;
                 //   RetryVariable.IsVisible = true;
                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void TrolliSchuduled(Object sender, EventArgs e)
        {
            try
            {
                string statesPickers = statesPicker.Items[statesPicker.SelectedIndex];
                string states = statesPickers;
                if (statesPickers == "Later")
                {
                    datePicker.IsVisible = true;
                    currentDate.IsVisible = false;

                    timePicker.IsVisible = true;
                    currentTime.IsVisible = false;
                    datePicker.Date = DateTime.Now.Date;

                    dateText.IsVisible = true;
                    timeText.IsVisible = true;


                }
                else if(statesPickers == "Now")
                {
                    datePicker.IsVisible = false;
                   // currentDate.IsVisible = true;

                    timePicker.IsVisible = false;
                   // currentTime.IsVisible = true;
                    dateText.IsVisible = false;

                    timeText.IsVisible = false;
                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }
    }
}