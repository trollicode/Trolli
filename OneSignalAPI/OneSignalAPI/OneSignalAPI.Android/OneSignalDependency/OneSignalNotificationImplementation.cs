using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OneSignalAPI.Droid.OneSignalDependency;
using OneSignalAPI.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(OneSignalNotificationImplementation))]
namespace OneSignalAPI.Droid.OneSignalDependency
{
    public class OneSignalNotificationImplementation : IOneSignalNotification
    {
        public Dictionary<string, object> getData()
        {
            return MainActivity.getNotificationData();
        }
    }
}

