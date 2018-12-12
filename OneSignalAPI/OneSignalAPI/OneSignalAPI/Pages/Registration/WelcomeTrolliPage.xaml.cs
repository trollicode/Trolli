using Com.OneSignal;
using Newtonsoft.Json;
using OneSignalAPI;
using OneSignalAPI.ApiClass;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.Connectivity;
using Plugin.DeviceInfo;
using RealTrolli;
using RealTrolli.BeanClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealTrolli
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeTrolliPage : ContentPage
    {


        ExceptionManagement exceptionObject = new ExceptionManagement();
        static HttpClient _client = new HttpClient();
        static Dictionary<string, object> values = null;
        bool check = true;


        public WelcomeTrolliPage()
        {
            InitializeComponent();
            //initial progress is 20%
            getConnection();
           // CheckDeviceId();
        }

        
        private void getConnection()
        {
            
            bool connect = CrossConnectivity.Current.IsConnected;
            if (connect)
            {
                CheckDeviceId();
            }
            else
            {
                DisplayAlert("Uh Oh!", "It seems like you are not connected to internet. Retry once your connections is back", "Exit");
                internetCheck.IsVisible = true;
                RetryVariable.IsVisible = true;
            }
           
        }

        public void RetryEvent(Object sender, EventArgs e) {
            internetCheck.IsVisible = false;
            RetryVariable.IsVisible = false;

            getConnection();
        }


        public async void CheckDeviceId()
        {
           // animate the progression to 80%, in 250ms
            //await progressBar.ProgressTo
            string deviceIdJson = "";
         try
            {
               // getConnection();
                var deviceId = CrossDeviceInfo.Current.Id;
                OneSignal.Current.SendTag("deviceId", deviceId);
                OneSignal.Current.SendTag("userType", "C");
                if (Application.Current.Properties.ContainsKey("phoneNumber"))
                    {

                    string userId = Convert.ToString(Application.Current.Properties["phoneNumber"]);
                     string url = ApiEndPoints.userDetail + userId;
                     string content = await _client.GetStringAsync(url);
                    await progressBar.ProgressTo(1, 2000, Easing.CubicIn);

                    List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);
                        bool userFound = true;
                        foreach (Dictionary<string, object> i in posts)
                        {
                            Dictionary<string, object> post = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));
                            deviceIdJson = Convert.ToString(post["deviceId"]);

                            if (deviceIdJson == deviceId)
                            {
                                userFound = false;
                                Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));
                                SharedUserData.getUserData = values;

                                await Navigation.PushAsync(new MenuPage());
                            }
                        }
                        if (userFound)
                        {
                          //  await DisplayAlert("", "Can't find your Device ID. Register now", "Ok");
                            await Navigation.PushAsync(new Registrations());
                        }
                    }
                    else
                    {
                        await progressBar.ProgressTo(1, 2000, Easing.CubicIn);
                        await Navigation.PushAsync(new Registrations());
                    }
                
            }
            catch (Exception ex)
            {
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }


        }

      

    }
}