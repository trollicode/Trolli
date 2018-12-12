using OneSignalAPI.ValidationClass;
using RealTrolli;
using RealTrolli.ValidationClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FirstStepRegistration : ContentPage
	{


        static string phoneNumbers = "";
        string referralCodeVar = "";
        public FirstStepRegistration (string phone)
		{
			InitializeComponent ();
            phoneNumbers = phone;
		}


        public void NextButton(Object sender, EventArgs e) {
            string name = "";
            string emailUser ="";
            try
            {
                if (CheckTextField())
                {
                    name = fullName.Text;
                    emailUser = email.Text;

                    Navigation.PushAsync(new CompleteRegistration(phoneNumbers, name, emailUser,referralCodeVar));
                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }


    

        public bool CheckTextField()
        {
            bool check = true;
            string validationText = "All fields are required";
            
            try
            {
                starName.Text = " ";

                starEmail.Text = " ";

                if (fullName.Text == "")
                {
                    starName.Text = "*";
                    check = false;
                }
                RegistrationValidation.IsValidEmail(email.Text);
                if (email.Text == "" || RegistrationValidation.validation)
                {
                    if (email.Text == "") { }
                    else
                    {
                        validationText = "Please enter a valid email";
                    }
                    starEmail.Text = "*";
                    check = false;
                }

                if(referralCode.Text == "")
                {
                    referralCodeVar = "none";
                }
                else
                {
                    referralCodeVar = referralCode.Text;
                }


                if (!check)
                {
                    validationError.Text = validationText;
                    //DisplayAlert("Some Think Missing", "Text Field Missing \n" + text, "OK");
                }

                return check;
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                return false;
            }
        }
    }
}