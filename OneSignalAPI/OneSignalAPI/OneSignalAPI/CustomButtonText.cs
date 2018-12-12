using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OneSignalAPI
{
  public  class CustomButtonText : Button
    {
        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
        public StackOrientation Orientation { get; set; }
        public static implicit operator CustomButtonText(StackLayout v)
        {
            throw new NotImplementedException();
        }
    }
}
