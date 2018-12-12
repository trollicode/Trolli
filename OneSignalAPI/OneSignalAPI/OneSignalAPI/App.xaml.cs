using Com.OneSignal;
using Com.OneSignal.Abstractions;
using ListViewWithSubListView.ViewModels;
using ListViewWithSubListView.Views;
using OneSignalAPI.BeanClass;
using OneSignalAPI.Interface;
using Plugin.Connectivity;
using RealTrolli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace OneSignalAPI
{
	public partial class App : Application
	{
		public App ()
		{

            
                InitializeComponent();
                //  OneSignal.Current.StartInit("6be47d95-7a8a-4f0b-932a-6bffa9ee04ca").EndInit();
                Dictionary<string, object> notificationData = DependencyService.Get<IOneSignalNotification>().getData();
                OneSignal.Current.StartInit("6be47d95-7a8a-4f0b-932a-6bffa9ee04ca").HandleNotificationOpened(HandleNotificationOpened).EndInit();
                OneSignal.Current.PromptLocation();

                // TrollyCreation ob = null;
                if (notificationData == null)
                {
                //  MainPage = new NavigationPage(new Hotels(new HotelsGroupViewModel())); 
                  MainPage = new NavigationPage(new FirstStepRegistration("234"));
                }
                else
                {// 
                    if (notificationData.ContainsKey("asigneeId"))
                    {
                        string extraMessage = Convert.ToString(notificationData["asigneeId"]);
                        MainPage = new NavigationPage(new JobDetails(extraMessage));
                        // extraMessage = (string)additionalData["discount"];
                        // Take user to your store.
                    }
                    else if (notificationData.ContainsKey("SBProfileId"))
                    {
                        string extraMessage = Convert.ToString(notificationData["SBProfileId"]);
                        MainPage = new NavigationPage(new SmartBuyerProfile(extraMessage));

                    }
                    else if (notificationData.ContainsKey("transactionId"))
                    {
                        string transactionId = Convert.ToString(notificationData["transactionId"]);
                        string smartBuyerId = Convert.ToString(notificationData["smartBuyerId"]);
                        MainPage = new NavigationPage(new ClientVerifyScreen(transactionId, smartBuyerId, ""));

                    }
                    else
                    {
                        MainPage = new NavigationPage(new WelcomeTrolliPage());
                    }
                }
            }

        private async void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            //result.notification.isAppInFocus = true;
            try
            {
                OSNotificationPayload payload = result.notification.payload;
                Dictionary<string, object> additionalData = payload.additionalData;


                string message = payload.body;
                string actionID = result.action.actionID;

                // await MainPage.Navigation.PushAsync(new LatLongPushNotification());
                // print("GameControllerExample:HandleNotificationOpened: " + message);
                // extraMessage = "Notification opened with text: " + message;

                //    string extraMessage = (string)additionalData["location"];
                string data = message + " " + actionID;


                if (additionalData != null)
                {
                    if (additionalData.ContainsKey("asigneeId"))
                    {
                        string extraMessage = Convert.ToString(additionalData["asigneeId"]);
                        await MainPage.Navigation.PushAsync(new JobDetails(extraMessage));


                        // extraMessage = (string)additionalData["discount"];
                        // Take user to your store.
                    }
                    else if (additionalData.ContainsKey("SBProfileId"))
                    {
                        string extraMessage = Convert.ToString(additionalData["SBProfileId"]);
                        await MainPage.Navigation.PushAsync(new SmartBuyerProfile(extraMessage));
                    }
                   
                    //      else if (additionalData.ContainsKey("location"))
                    //     {
                    //     string extraMessage = (string)additionalData["location"];

                    //    await MainPage.Navigation.PushAsync(new LatLongPushNotification(extraMessage));

                    //  }
                }
                else {
                    MainPage = new NavigationPage(new WelcomeTrolliPage());

                }


                if (actionID != null)
                {
                    // actionSelected equals the id on the button the user pressed.
                    // actionSelected will equal "__DEFAULT__" when the notification itself was tapped when buttons were present.
                    //extraMessage = "Pressed ButtonId: " + actionID;
                }
            }catch(Exception ex)
            {
                //Exception Class
            }
        }
        protected override void OnStart ()
		{
            OneSignal.Current.PromptLocation();

        //    OneSignal.Current.StartInit("6be47d95-7a8a-4f0b-932a-6bffa9ee04ca").HandleNotificationOpened(HandleNotificationOpened).EndInit();
            // Handle when your app starts
        }

        protected override  async void OnSleep ()
		{
            // Handle when your app sleeps
            OneSignal.Current.PromptLocation();

         //   OneSignal.Current.StartInit("6be47d95-7a8a-4f0b-932a-6bffa9ee04ca").HandleNotificationOpened(HandleNotificationOpened).EndInit();
           
        }

        protected override void OnResume ()
		{
            OneSignal.Current.PromptLocation();

      //      OneSignal.Current.StartInit("6be47d95-7a8a-4f0b-932a-6bffa9ee04ca").HandleNotificationOpened(HandleNotificationOpened).EndInit();
            // Handle when your app resumes
        }

    }
}