using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Text.RegularExpressions;
using OneSignalAPI;
using OneSignalAPI.ApiClass;
using OneSignalAPI.ValidationClass;

namespace RealTrolli
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registrations : ContentPage
	{
		public Registrations ()
		{
			InitializeComponent ();
            TimeZone zone = TimeZone.CurrentTimeZone;

            DateTime universal = zone.ToUniversalTime(DateTime.Now);

            long code = Convert.ToInt64(String.Format("{0:yyyyMMddHHmmss}", universal));
            
            webView.Source = ApiEndPoints.registrationUrl + code;
           
                webView.Navigated += OnNavigatedHandler;
        }


        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    
        public void OnNavigatedHandler(object sender, WebNavigatedEventArgs args)
        {
            try
            {
                // Console.WriteLine("Navigated to: " + args.Url);
                string str = args.Url;
                if (str.Length > 54)
                {
                    string substr = str.Substring(54);

                    if (args.Url == ApiEndPoints.completePhoneVerified + substr)
                    {
                        Navigation.PushAsync(new FirstStepRegistration(substr));
                    }

                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
        }
    }
}