using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OneSignalAPI.ValidationClass
{
    class ExceptionManagement : ContentPage
    {
        public ExceptionManagement()
        {
        }

        public static string LogException(Exception ex)
        {
            return ex.Message;
        }
    }
}
