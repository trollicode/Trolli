using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using OneSignalAPI.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using OneSignalAPI;


[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace OneSignalAPI.iOS
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                // do whatever you want to the UITextField here!
                Control.BackgroundColor = UIColor.FromRGB(45, 60, 84);
                Control.BorderStyle = UITextBorderStyle.RoundedRect;
                
               
            }
        }

      

    }
}