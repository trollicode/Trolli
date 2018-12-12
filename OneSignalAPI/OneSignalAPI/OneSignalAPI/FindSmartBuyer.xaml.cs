using Com.OneSignal;
using Newtonsoft.Json;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.DeviceInfo;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FindSmartBuyer : ContentPage
    {

        private static string url = "https://trolli-194513.appspot.com/isOnlineJobSeeker";
        static HttpClient _client = new HttpClient();
        private List<IsJobOnlineSeeker> smartBuyer = new List<IsJobOnlineSeeker>();

        public FindSmartBuyer()
        {
            InitializeComponent();
            //  FindingSmartBuyer();
        }

        public async void FindingSmartBuyer()
        {
            try
            {
                string content = await _client.GetStringAsync(url);
                List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);
                bool isJobSeekerCheck;
                foreach (Dictionary<string, object> i in posts)
                {
                    Dictionary<string, object> post = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));
                    isJobSeekerCheck = Convert.ToBoolean(post["isJobSeeker"]);
                    if (isJobSeekerCheck == true)
                    {
                        bool isJob = Convert.ToBoolean(post["isJobSeeker"]);
                        string name = Convert.ToString(post["fullName"]);
                        string sim = Convert.ToString(post["simNumber"]);
                        IsJobOnlineSeeker ob = new IsJobOnlineSeeker
                        {
                            isJobSeeker = Convert.ToString(isJob),
                            fullName = name,
                            simNumber = sim
                        };
                        smartBuyer.Add(ob);
                    }
                }
            }
            catch (Exception ex)
            {
              await  DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

            //     listView.ItemsSource = null;
            //    listView.ItemsSource = smartBuyer;
        }

        public async void FindSmart(object sender, EventArgs e)
        {
           
            try
            {
                string range = "10000";
                string latAPI = "";
                string longAPI = "";
                var locator = CrossGeolocator.Current;

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    await DisplayAlert("", "Trolli app requires access to location services, in order to function properly. If you do not allow location access, some features may not work as expected.", "Ok");
                }
                else
                {
                    locator.DesiredAccuracy = 100;
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        var position = await locator.GetPositionAsync(); //Can't find actual return type
                        latAPI = "" + position.Latitude;
                        longAPI = "" + position.Longitude;
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        var position = await locator.GetPositionAsync(); //Can't find actual return type

                        latAPI = "" + position.Latitude;
                        longAPI = "" + position.Longitude;
                    }


                    Application.Current.Properties["checkTrolli"] = "myTrolli";
                    postNotification(latAPI, longAPI, range);
                }

            }
            catch (Exception ex) {
               await DisplayAlert("", "Unable to get location: " + ex, "Ok");
            }
        }

        public void postNotification(string latAPI, string longAPI, string range)
        {
            try
            {
                activIndicator.IsRunning = true;

                var deviceId = CrossDeviceInfo.Current.Id;


                string msgPush = "Trolli ID: " + ScheduledTrolly.UUIDs;
                var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
                request.KeepAlive = false;
                request.Method = "POST";

                request.ContentType = "application/json; charset=utf-8";

                request.Headers.Add("authorization", "Basic MGFhOTZjM2UtZjc3Yy00ZTk1LThlZjUtYTE2NzJhOTI5ZDMz");

                byte[] byteArray = Encoding.UTF8.GetBytes("{ \"app_id\": \"6be47d95-7a8a-4f0b-932a-6bffa9ee04ca\", " +
                    " \"included_segments\": [\"All\"]," +
                    " \"headings\": {\"en\": \"Trolli Job Invitation\"}, " +
                    "  \"data\": {\"location\": \"Location Testing\", \"asigneeId\": \"" + ScheduledTrolly.UUIDs + "\"}, " +
                    "  \"contents\": {\"en\": \"" + msgPush + "\"},  " +
                    "\"filters\": [{\"field\": \"tag\", \"key\": \"deviceId\", \"relation\": \"!=\", \"value\": \""+deviceId+"\"}," +
                    "{\"field\": \"location\", \"radius\": \""+range+"\", " +
                    "\"lat\": \""+latAPI+"\", " +
                    "\"long\": \""+longAPI+"\"}]}");

                string responseContent = null;

                try
                {
                    using (var writer = request.GetRequestStream())
                    {
                        writer.Write(byteArray, 0, byteArray.Length);
                    }

                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseContent = reader.ReadToEnd();
                        }
                    }
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                }

                System.Diagnostics.Debug.WriteLine(responseContent);
                activIndicator.IsRunning = false;
                Plugin.Toast.CrossToastPopUp.Current.ShowToastMessage("Notification sent to Smart Buyer's");
            }catch(Exception ex)
            {

                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
        }
    }

}