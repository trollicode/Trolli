using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Newtonsoft.Json;
using OneSignalAPI.ApiClass;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.DeviceInfo;
using RealTrolli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JobDetails : ContentPage
	{

        
        static HttpClient _client = new HttpClient();
        Dictionary<string, object> values = null;

        string notificationAssignIds = "";
        string clientNotificationId = "";
        string assignId = "";

        public JobDetails (string notificationAssignId)
		{
			InitializeComponent ();
            notificationAssignIds = notificationAssignId;
            DataLoad();
		}

        public async void DataLoad() {
            try
            {
                activIndicator.IsRunning = true;
                string url = ApiEndPoints.assigneeJobTransationAPI+"?trolliID="+notificationAssignIds;
                string content = await _client.GetStringAsync(url);

                List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);
                foreach (Dictionary<string, object> i in posts)
                {

                    Dictionary<string, object> post = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));

                    assignId = Convert.ToString(post["trolliId"]);
                    if (assignId == notificationAssignIds)
                    {
                        values = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));
                        deliveryTime.Text = Convert.ToString(values["deliveryDateTime"]);
                        deliveryDate.Text = Convert.ToString(values["createdDate"]);
                        assignText.Text = Convert.ToString(values["trolliId"]);
                        statusText.Text = Convert.ToString(values["status"]);
                        clientNotificationId = Convert.ToString(values["clientNotificationId"]);
                    }

                }
               activIndicator.IsRunning = false;
            }catch(Exception ex) {
                //Exception Class

                activIndicator.IsRunning = false;
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
         }
       
        public async void AssignHandler(Object sender, EventArgs e){
            try
            {

                if (statusText.Text != "Assign")
                {

                    activIndicator.IsRunning = true;
                    string SBNotificationid = "";
                    OneSignal.Current.IdsAvailable((playerID, pushToken) =>
                    {
                        SBNotificationid = playerID;
                    //  App.Current.MainPage.DisplayAlert("playerId", id.ToString(), "OK");
                    });
                    ApiCalling apiCall = new ApiCalling();

                    var deviceId = CrossDeviceInfo.Current.Id;

                    if (SharedUserData.getUserData == null) {

                        string userId = Convert.ToString(Application.Current.Properties["phoneNumber"]);
                        string userUrl = ApiEndPoints.userDetail + userId;

                        string content = await _client.GetStringAsync(userUrl);

                        List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);
                        foreach (Dictionary<string, object> i in posts)
                        {
                            Dictionary<string, object> post = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));
                           string deviceIdJson = Convert.ToString(post["deviceId"]);

                            if (deviceIdJson == deviceId)
                            {
                              
                                Dictionary<string, object> userDataRecord = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));
                                SharedUserData.getUserData = userDataRecord;

                            }
                        }
                    }
                    Dictionary<string, object> userData = SharedUserData.getUserData;


                    TrollyCreation ob = new TrollyCreation()
                    {
                        assigneeId = Convert.ToString(userData["UniqueID"]), //Smart Buyer Assigne ID 
                        trolliId = notificationAssignIds, // Trolli ID
                        SBNotificationId = SBNotificationid // Smart Buyer OneSignal Notification UDID.
                    };


                    string data = apiCall.TrollyTestTransaction(ob);
                    //   data = "[" + data + "]";
                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
                    string ErrorMessage = "";
                    if (values != null)
                    {
                        ErrorMessage = values["Error Message"];
                    }

                    if (ErrorMessage == "Invalid Record")
                    {
                      await  DisplayAlert("", "This Trolli has been already assign", "Ok");
                    }
                    else
                    {
                       await DisplayAlert("", "The Trolli has been assigned to you", "Ok");
                        apiCall.SendSBNotification(ob.assigneeId, clientNotificationId);

                        statusText.Text = "Assign";
                    }
                }
                else
                {
                   await DisplayAlert("", "This Trolli has been already assign", "Ok");
                }
                activIndicator.IsRunning = false;
             
            }
            catch (Exception ex)
            {
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }

        }

        public async void CancelHandler(Object sender, EventArgs e)
        {
            try
            {

                var deviceId = CrossDeviceInfo.Current.Id;

                if (SharedUserData.getUserData == null)
                {

                    string userId = Convert.ToString(Application.Current.Properties["phoneNumber"]);
                    string userUrl = ApiEndPoints.userDetail + userId;

                    string content = await _client.GetStringAsync(userUrl);

                    List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);
                    foreach (Dictionary<string, object> i in posts)
                    {
                        Dictionary<string, object> post = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));
                        string deviceIdJson = Convert.ToString(post["deviceId"]);

                        if (deviceIdJson == deviceId)
                        {

                            Dictionary<string, object> userDataRecord = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));
                            SharedUserData.getUserData = userDataRecord;

                           await Navigation.PushAsync(new MenuPage());
                        }
                    }
                }
                else
                {
                   await Navigation.PushAsync(new MenuPage());
                }
            }catch(Exception ex)
            {
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }

          
        }

        public void BackEvent(Object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}