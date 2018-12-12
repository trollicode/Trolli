using OneSignalAPI.iOS.OneSignalDependency;
using System;
using System.Collections.Generic;
using System.Text;
using OneSignalAPI.Interface;
[assembly: Xamarin.Forms.Dependency(typeof(OneSignalIOSNotificationImplementation))]
namespace OneSignalAPI.iOS.OneSignalDependency
{
   public class OneSignalIOSNotificationImplementation : IOneSignalNotification
    {
        public Dictionary<string, object> getData()
        {
            return AppDelegate.getNotificationData();
        }
    }
}
