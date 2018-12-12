using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Foundation;
using UIKit;

namespace OneSignalAPI.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
                OneSignal.Current.StartInit("6be47d95-7a8a-4f0b-932a-6bffa9ee04ca").HandleNotificationOpened(HandleNotificationOpened).EndInit();
                OneSignal.Current.PromptLocation();
            //new Syncfusion.SfAutoComplete.XForms.iOS.SfAutoCompleteRenderer();

            new Syncfusion.SfAutoComplete.XForms.iOS.SfAutoCompleteRenderer();
            global::Xamarin.Forms.Forms.Init();
                LoadApplication(new App());


                return base.FinishedLaunching(app, options);
          
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


        public override void OnResignActivation(UIApplication uiApplication)
        {
            OneSignal.Current.PromptLocation();
            base.OnResignActivation(uiApplication);
        }
        public override async void DidEnterBackground(UIApplication uiApplication)
        {
           
            nint taskID = UIApplication.SharedApplication.BeginBackgroundTask(() => {
                OneSignal.Current.PromptLocation();

                Console.WriteLine("BackGround Event!!");
            });
            new Task(() => {
                //    OneSignal.Current.PromptLocation();

                Console.WriteLine("BackGround Event!! Task");
                UIApplication.SharedApplication.EndBackgroundTask(taskID);
            }).Start();
        }

        public override void WillTerminate(UIApplication uiApplication)
        {

            OneSignal.Current.PromptLocation();

            base.WillTerminate(uiApplication);
        }

    }
}
