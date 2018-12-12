using OneSignalAPI;
using OneSignalAPI.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(CustomPickerRenderer), typeof(CustomPicker))]
namespace OneSignalAPI.iOS
{

    class CustomPicker : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
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
