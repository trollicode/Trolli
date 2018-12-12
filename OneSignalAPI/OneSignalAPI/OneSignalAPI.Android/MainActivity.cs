using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.OneSignal;
using Google.Apis.Drive.v3;
using System.Collections.Generic;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Google.Apis.Logging;
using Com.OneSignal.Abstractions;
using Android.Content;

namespace OneSignalAPI.Droid
{
    [Activity(Label = "Trolli", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

      
        protected override void OnCreate(Bundle bundle)
        {
            
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;


                base.OnCreate(bundle);
                //OneSignal.Current.StartInit("6be47d95-7a8a-4f0b-932a-6bffa9ee04ca").EndInit();
                OneSignal.Current.StartInit("6be47d95-7a8a-4f0b-932a-6bffa9ee04ca").HandleNotificationOpened(HandleNotificationOpened).EndInit();
                OneSignal.Current.PromptLocation();

                //new Syncfusion.SfAutoComplete.XForms.Droid.SfAutoCompleteRenderer();
                Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;
                global::Xamarin.Forms.Forms.Init(this, bundle);
                //  FormsControls.Droid.Main.Init(this);
                LoadApplication(new App());
           
        }
       



        public static Dictionary<string, object> dataDependency = null;



        public static Dictionary<string, object> getNotificationData()
        {

            return dataDependency;
        }
        private void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            OSNotificationPayload payload = result.notification.payload;
            Dictionary<string, object> additionalData = payload.additionalData;


            string message = payload.body;
            string actionID = result.action.actionID;

            dataDependency = additionalData;
            // await MainPage.Navigation.PushAsync(new LatLongPushNotification());
            // print("GameControllerExample:HandleNotificationOpened: " + message);
            // extraMessage = "Notification opened with text: " + message;


            //    string extraMessage = (string)additionalData["location"];
            /*string data = message + " " + actionID;


            if (additionalData != null)
            {
                if (additionalData.ContainsKey("asigneeId"))
                {
                    string extraMessage = Convert.ToString(additionalData["asigneeId"]);
                    dataDependency = extraMessage;
                    //await MainPage.Navigation.PushAsync(new JobDetails(extraMessage));


                    // extraMessage = (string)additionalData["discount"];
                    // Take user to your store.
                }
                //      else if (additionalData.ContainsKey("location"))
                //     {
                //     string extraMessage = (string)additionalData["location"];

                //    await MainPage.Navigation.PushAsync(new LatLongPushNotification(extraMessage));

                //  }
            }
            if (actionID != null)
            {
                // actionSelected equals the id on the button the user pressed.
                // actionSelected will equal "__DEFAULT__" when the notification itself was tapped when buttons were present.
                //extraMessage = "Pressed ButtonId: " + actionID;
            }*/
        }
        


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

