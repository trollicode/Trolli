using OneSignalAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealTrolli
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientMainPage : ContentPage
	{
		public ClientMainPage (string name)
		{
			InitializeComponent ();
            value.Text += " " + name;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

     //   public async void btn_view(Object sender, EventArgs e) {

            //await Navigation.PushAsync(new Profile());
           // MessagingCenter.Send<TestWebView>(this, "RefreshMainPage");
       //     await Navigation.PopAsync();
      //  }

        public void toggled_event(object sender, ToggledEventArgs e) {
            label.IsVisible = e.Value;
        }

    }
}