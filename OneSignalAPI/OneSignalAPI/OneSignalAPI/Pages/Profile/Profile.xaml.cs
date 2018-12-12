using Newtonsoft.Json;
using OneSignalAPI;
using OneSignalAPI.ApiClass;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using RealTrolli.ApiClass;
using RealTrolli.BeanClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealTrolli
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Profile : MasterDetailPage
	{

        string imageId = "";

        public Profile ()
		{
			InitializeComponent ();
            LoadProfile();

            if (Device.RuntimePlatform == Device.Android)
            {
                NavigationPage.SetHasBackButton(this, false);
                NavigationPage.SetHasNavigationBar(this, false);
            }
        }

        public void MenuNavigate(Object sender, EventArgs e)
        {

            this.IsPresented = true;
        }


        public void LoadProfile() {
            try
            {
                Dictionary<string, object> userData = SharedUserData.getUserData;
                //  SignupBean bean2 = bal.getData();

                nameTxt.Text = Convert.ToString(userData["fullName"]);
                emailTxt.Text = Convert.ToString(userData["email"]);
                stateTxt.Text = Convert.ToString(userData["state"]);
                subrubTxt.Text = Convert.ToString(userData["suburb"]);
                postalTxt.Text = Convert.ToString(userData["postCode"]);
                imageId = Convert.ToString(userData["gdProfileImageId"]);
                // profileImage.Source = "https://drive.google.com/uc?export=view&id=" + imageId;

                if (imageId == "")
                {
                    profileImage.Source = "logo2.png";
                }
                else
                {
                    profileImage.Source = ApiEndPoints.loadImageFromGoogleDrive + imageId;

                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
    
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadProfile();
        }

        public void EditProfile(Object sender, EventArgs e) {
            try
            {
                Navigation.PushModalAsync(new ProfileUpdate());
            }catch(Exception ex)
            {

                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

        }


      
    }
}