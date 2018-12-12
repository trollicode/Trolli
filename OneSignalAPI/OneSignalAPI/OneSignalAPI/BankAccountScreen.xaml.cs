using Plugin.Media;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BankAccountScreen : MasterDetailPage
    {
		public BankAccountScreen ()
		{
			InitializeComponent ();
		}

        public void MenuNavigate(Object sender, EventArgs e)
        {

            this.IsPresented = true;
        }

        public async void RegisterUser(Object sender, EventArgs e)
        {

            try
            {
                string firstNameVar = firstName.Text;
                string lastNameVar = lastName.Text;
                int dobDayVar = Convert.ToInt32(dobDay.Text);
                int dobMonthVar = Convert.ToInt32(dobMonth.Text);
                int dobYearVar = Convert.ToInt32(dobYear.Text);
                string addressLine1Var = addressLine1.Text;
                string addressLine2Var = addressLine2.Text;
                string postalCodeVar = postalCode.Text;
                var statesPickerVar = statesPicker.Items[statesPicker.SelectedIndex];
                string routingNumberVar = routingNumber.Text;
                string accountNumberVar = accountNumber.Text;
                string cityVar = city.Text;


                StripeConfiguration.SetApiKey("sk_test_Q5wSnyXL03yN0KpPaAMYttOb");

                //-------Get IP Address---------//
                var MyIp = "";
                string uploadId = "";
                foreach (IPAddress adress in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    MyIp = adress.ToString();
                    break;
                }

                //------Camera will Open User take the Picture Of Idenity Card-----------//
                /*   var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                   {
                       Directory = "Sample",
                       Name = "test.jpg",
                       PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small

                   });

                   //-------Upload File in stripe and return the file Id -------------//
                   using (FileStream stream = System.IO.File.Open(file.Path, FileMode.Open))
                   {
                       var fileService = new FileService();
                       var fileCreateOptions = new FileCreateOptions
                       {
                           File = stream,
                           Purpose = "identity_document",
                       };
                       Stripe.File upload = fileService.Create(fileCreateOptions);
                       uploadId = upload.Id;
                   }
                   */
                //------Set File.id-----///
                AccountVerificationOptions verifiy = new AccountVerificationOptions { DocumentId = uploadId, };



                //--------Create User Account Legal Entity-----------//  
                AccountLegalEntityOptions legalEntity = new AccountLegalEntityOptions
                {
                    FirstName = firstNameVar,
                    LastName = lastNameVar,
                    Address = new AddressOptions { City = cityVar, Country = "AU", PostalCode = postalCodeVar, Line1 = addressLine1Var, Line2 = addressLine2Var, State = statesPickerVar },
                    Dob = new AccountDobOptions { Day = dobDayVar, Month = dobMonthVar, Year = dobYearVar },
                    Type = "individual",
                    Verification = verifiy,
                    PhoneNumber = "+611234567"//Convert.ToString(Xamarin.Forms.Application.Current.Properties["phoneNumber"])
                };

                //--------Create Payout Account-----------//                   
                var options = new AccountCreateOptions
                {
                    Email = "saeed@gmail.com",
                    Type = AccountType.Custom,
                    Country = "AU",
                    LegalEntity = legalEntity
                };
                var services = new AccountService();
                Account account = services.Create(options);


                //---------Create External Bank Account and return Token---------///
                TokenCreateOptions optionss = new TokenCreateOptions
                {
                    BankAccount = new BankAccountOptions
                    {
                        Country = "AU",
                        AccountNumber = accountNumberVar,
                        RoutingNumber = routingNumberVar,
                        Currency = "aud"

                    }
                };
                TokenService service = new TokenService();
                Token stripeToken = service.Create(optionss);


                var externalOption = new ExternalAccountCreateOptions
                {
                    ExternalAccountTokenId = stripeToken.Id, //pass the bank account creation token
                };
                var externalAccount = new ExternalAccountService();
                var bankAccount = externalAccount.Create(account.Id, externalOption); //Bank Account Created

                //  await DisplayAlert("", "" + account.Id, "Ok");
                //api.AcceptedStripeAgreement(account.Id,CurrentTimeMillis(), MyIp); //Accepted Stripe TOS 


                await DisplayAlert("", "" + account.Id, "Ok");
            }catch(Exception ex)
            {
                await DisplayAlert("", "" + ex, "Ok");
            }
        }
    }
}