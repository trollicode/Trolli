using Newtonsoft.Json;
using OneSignalAPI.ApiClass;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.Media;
using RealTrolli;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaymentCardInfo : ContentPage
	{


        Dictionary<string, object> userData = SharedUserData.getUserData;
        bool IsCardAvailable = false;
        public TrollyCreation trollyCreation = new TrollyCreation();
        public PaymentCardInfo(TrollyCreation ob)
        {
            InitializeComponent();
            trollyCreation = ob;
           
            checkCardInfo();

            
        }

        public void TestEvent(Object sender, EventArgs e)
        {
            try
            {
                  var chargeOptions = new ChargeCaptureOptions
                  {
                      Amount = 3000,
                  };
                  var chargeService = new ChargeService();
                 Charge charge = chargeService.Capture("ch_1DZFGDBXLNpDm6rJAZgwGBuz", chargeOptions, null); 
            }
            catch (Exception ex)
            {

            }
        }



            public void checkCardInfo()
            {

            try
            {
                /*if (Xamarin.Forms.Application.Current.Properties.ContainsKey("paymentCardInfo"))
                 {
                   string cardInfo = Convert.ToString(Xamarin.Forms.Application.Current.Properties["paymentCardInfo"]);

                   List<Dictionary<string, object>> post = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>("["+cardInfo+"]");

                   foreach (Dictionary<string, object> posts in post)
                   {

                       cardHolderName.Text = Convert.ToString(posts["cardHolderName"]);
                       cardNumber.Text = Convert.ToString(posts["cardNumber"]);
                       experiationDate.Text = Convert.ToString(posts["experationDate"]);
                       cvcNumber.Text = Convert.ToString(posts["cvvNumber"]);
                   }
                   */

                StripeConfiguration.SetApiKey("sk_test_Q5wSnyXL03yN0KpPaAMYttOb");

                    string customerId = Convert.ToString(userData["stripeCustomerId"]);
                    var service = new CustomerService();
                    Customer customer = service.Get(customerId);
                    List<IPaymentSource> list = customer.Sources.Data;

                    if (list.Count != 0)
                    {
                        foreach (Card ob in list)
                        {
                            cardHolderName.Text = ob.Name;
                            cardNumber.Text = "****" + ob.Last4;
                            experiationDate.Text = ob.ExpMonth + "/" + ob.ExpYear;
                            cvcNumber.Text = ob.CvcCheck;
                            IsCardAvailable = true;
                        }
                    }
                
            }
            catch(Exception ex)
            {

            }
        }



        private static readonly DateTime Jan1st1970 = new DateTime
        (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        ApiCalling api = new ApiCalling();
        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalSeconds;
        }


        public async void FindSmartBuyer(Object sender, EventArgs e)
        {
            try
            {
                // searchText.IsVisible = true;
                StripeConfiguration.SetApiKey("sk_test_Q5wSnyXL03yN0KpPaAMYttOb");
                if (IsCardAvailable)
                {

                  /*  ChargeDestinationCreateOptions chargeDestination = new ChargeDestinationCreateOptions {
                        Account = "acct_1DWkSgB4PaSzuOZk",
                        Amount = 1000
                    };
                    */
                    var charge = new ChargeCreateOptions
                      {
                         Amount = Convert.ToInt32(50 * 100), // In cents, not dollars, times by 100 to convert
                         Currency = "aud", // or the currency you are dealing with
                         CustomerId = Convert.ToString(userData["stripeCustomerId"]),
                         Capture = false
                      };
                      var services = new ChargeService();
                      var response = services.Create(charge);

                    TrollyCreation trolly = new TrollyCreation()
                    {

                        createdDate = trollyCreation.createdDate,
                        lastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy"),
                        trollyTitle = trollyCreation.trollyTitle,
                        trollyDetail = trollyCreation.trollyDetail,
                        trolliId = trollyCreation.trolliId,
                        clientId = trollyCreation.clientId,
                        status = "Open",
                        deliveryDateTime = trollyCreation.deliveryDateTime,
                        assigneeId = " ",
                        clientNotificationId = trollyCreation.clientNotificationId,
                        SBNotificationId = " ",
                        stripeChargeId = response.Id



                    };
                    api.TrollyCreation(trolly);
                    searchText.IsVisible = true;
                    await Navigation.PushAsync(new FindSmartBuyer());
                    await DisplayAlert("", "Transaction Id: " + response.Id, "Ok");
                }
                else
                {

                   /* var options = new TransferCreateOptions
                    {
                        Amount = 400,
                        Currency = "aud",
                        Destination = "acct_1DWlL0LUUlibc6UD"
                    };

                    var service = new TransferService();
                    Transfer Transfer = service.Create(options);
                      
                      //-------Get IP Address---------//
                      var MyIp = "";
                      string uploadId = "";
                      foreach (IPAddress adress in Dns.GetHostAddresses(Dns.GetHostName()))
                      {
                          MyIp = adress.ToString();
                          break;
                      }

                      //------Camera will Open User take the Picture Of Idenity Card-----------//
                       var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
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

                      //------Set File.id-----///
                      AccountVerificationOptions verifiy = new AccountVerificationOptions { DocumentId = uploadId, };



                      //--------Create User Account Legal Entity-----------//  
                      AccountLegalEntityOptions legalEntity = new AccountLegalEntityOptions
                      {
                          FirstName = "Saeed",
                          LastName = "Ali",
                          Address = new AddressOptions { City = "Sydney", Country = "AU", PostalCode = "5106", Line1 = "Home", State = "Victoria" },
                          Dob = new AccountDobOptions { Day = 12, Month = 09, Year = 1990 },
                          Type = "individual",
                          Verification = verifiy,
                          PhoneNumber = "+6112345678"
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
                              AccountNumber = "000123456",
                              RoutingNumber = "110000",
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

                  




                */

                    /*
                                        AccountTosAcceptanceOptions tos = new AccountTosAcceptanceOptions()
                                        {
                                            Ip = MyIp,
                                            Date = DateTime.Now.AddMilliseconds(1234)
                                         };
                                        DisplayAlert("", "" + DATE, "Ok");
                                        TokenCreateOptions optionss = new TokenCreateOptions
                                        {
                                            BankAccount = new BankAccountOptions
                                            {
                                                Country = "AU",
                                                AccountNumber = "000123456",
                                                RoutingNumber = "110000",
                                                Currency = "aud"

                                            }
                                        };
                                        TokenService service = new TokenService();
                                        Token stripeToken = service.Create(optionss);

                                        var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                                        {
                                            Directory = "Sample",
                                            Name = "test.jpg",
                                            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small

                                        });

                                        AccountVerificationOptions verifiy = new AccountVerificationOptions { DocumentId = file.Path,  };

                                        AccountLegalEntityOptions legalEntity = new AccountLegalEntityOptions
                                        {
                                            FirstName = "Usman",
                                            LastName = "Kamran",
                                            Address = new AddressOptions { City = "Sydney", Country = "AU", PostalCode = "5106", Line1 = "Home", State = "Victoria"},
                                            Dob = new AccountDobOptions { Day = 12, Month = 09, Year = 1990},
                                            Verification = verifiy,
                                            Type = "individual",

                                        };
                                        var options = new AccountCreateOptions
                                        {
                                            Email = "Farhan@example.com",
                                            Type = AccountType.Custom,
                                            Country = "AU",
                                            LegalEntity  =legalEntity
                                        };

                                        var services = new AccountService();
                                        Account account = services.Create(options);
                                        DisplayAlert("", "" + account.TosAcceptance.Ip, "Ok");

                */
                                       // DisplayAlert("", "" + tos.Date, "Ok");
                                           string cardHolderNameVar = cardHolderName.Text;
                                           string cardNumberVar = cardNumber.Text;
                                           string experiationDateVar = experiationDate.Text;
                                           string cvvVar = cvcNumber.Text;
                                           TokenCreateOptions options = new TokenCreateOptions
                                           {
                                               Card = new CreditCardOptions
                                               {
                                                   Number = cardNumberVar,
                                                   ExpYear = 2019,
                                                   ExpMonth = 11,
                                                   Cvc = cvvVar,
                                                   Name = cardHolderNameVar

                                               }
                                           };
                                           TokenService service = new TokenService();
                                           Token stripeToken = service.Create(options);

                                           var customerOptions = new CustomerCreateOptions
                                           {
                                               Email = Convert.ToString(userData["email"]),
                                               SourceToken = stripeToken.Id,
                                           };
                                           var customerService = new CustomerService();
                                           Customer customer = customerService.Create(customerOptions);

                                           var charge = new ChargeCreateOptions
                                           {
                                               Amount = Convert.ToInt32(50 * 100), // In cents, not dollars, times by 100 to convert
                                               Currency = "aud", // or the currency you are dealing with
                                               CustomerId = customer.Id,
                                               Capture = false
                                           };
                                           var services = new ChargeService();
                                           var response = services.Create(charge);

                                           await DisplayAlert("", "Transaction Id: " + response.Id, "Ok");


                                           ApiCalling api = new ApiCalling();
                                           api.CreateStripeCustomerId(Convert.ToString(userData["simNumber"]), customer.Id);//
                                           TrollyCreation trolly = new TrollyCreation()
                                           {
                                               createdDate = trollyCreation.createdDate,
                                                lastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy"),
                                                trollyTitle = trollyCreation.trollyTitle,
                                                trollyDetail = trollyCreation.trollyDetail,
                                                trolliId = trollyCreation.trolliId,
                                                clientId = trollyCreation.clientId,
                                                status = "Open",
                                                deliveryDateTime = trollyCreation.deliveryDateTime,
                                                assigneeId = " ",
                                                clientNotificationId = trollyCreation.clientNotificationId,
                                                SBNotificationId = " ",
                                                stripeChargeId = response.Id

                                            };

                                            api.TrollyCreation(trolly);


                    //   DisplayAlert("", output, "OK");


                    // Application.Current.Properties["paymentCardInfo"] = output;

                                          searchText.IsVisible = true;
                                          await Navigation.PushAsync(new FindSmartBuyer());
                }
            }
            catch (Exception ex){
                 await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                }

                      

                // Navigation.PushAsync(new FindSmartBuyer());
               

                 
              
              //  DisplayAlert("", "" + name, "Ok");
                // Charge the Customer instead of the card:




                /*  var chargeOptions = new ChargeCaptureOptions
                   {
                       Amount = 50,
                   };
                   var chargeService = new ChargeService();
                  Charge charge = chargeService.Capture("ch_1DUTlGBXLNpDm6rJjHQFSzhL", chargeOptions, null); */
                //   var service = new ChargeService();
                //    Charge charge = service.Capture("ch_1DUCTtBXLNpDm6rJzTe5o7Nl", null,null);

                // YOUR CODE: Save the customer ID and other info in a database for later.

                // When it's time to charge the customer again, retrieve the customer ID.


                /*    TokenCreateOptions options = new TokenCreateOptions
                       {
                           Card = new CreditCardOptions
                           {
                               Number = info.cardNumber,
                               ExpYear = Convert.ToInt32(info.experationDate),
                               ExpMonth = 11,
                               Cvc = info.cvvNumber,

                           }
                       };

                       TokenService service = new TokenService();
                       Token stripeToken = service.Create(options);
                       */
                /*var charge = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(43.65 * 100), // In cents, not dollars, times by 100 to convert
                    Currency = "aud", // or the currency you are dealing with
                    Description = "2018110901",
                    CustomerId = "cus_DvIGCEtsC9BZxL",
                    Capture = false
                };
                var services = new ChargeService();
                var response = services.Create(charge);*/

                // Record or do something with the charge information
                // Navigation.PushAsync(new FindSmartBuyer(), true);

            }
           
            

    }
}