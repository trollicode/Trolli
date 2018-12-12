using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Newtonsoft.Json;
using OneSignalAPI.BeanClass;
using OneSignalAPI.Interface;

using RealTrolli;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestWebView : ContentPage
    {

        

        public TestWebView ()
		{
			InitializeComponent ();
            // dataValue.Text = NotificationFire;
            //webView.Source = "http://www.y2mate.com";

            //  Application.Current.Properties["isOnlineJobSeeker"] = false;
            //    witchToggled.IsToggled = true;
        


        }

        public void TestStripe() {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_Q5wSnyXL03yN0KpPaAMYttOb");

                TokenCreateOptions options = new TokenCreateOptions
                {
                    Card = new CreditCardOptions
                    {
                        Number = "4000000360000006",
                        ExpYear = 2019,
                        ExpMonth = 11,
                        Cvc = "234",
                        Name = "Waleed"

                    }
                };
                TokenService service = new TokenService();
                Token stripeToken = service.Create(options);

                DisplayAlert("", "stripeToken Id: " + stripeToken.Id, "Ok");
                var customerOptions = new CustomerCreateOptions
                {
                    Email = "Waleed.rafiquex79@gmail.com",//Convert.ToString(userData["email"]),
                    SourceToken = stripeToken.Id,
                };
                var customerService = new CustomerService();
                Customer customer = customerService.Create(customerOptions);

                DisplayAlert("", "customer Id: " + customer.Id, "Ok");
                var charge = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(50 * 100), // In cents, not dollars, times by 100 to convert
                    Currency = "aud", // or the currency you are dealing with
                    CustomerId = customer.Id,
                    Capture = false
                };
                var services = new ChargeService();
                var response = services.Create(charge);

                DisplayAlert("", "Transaction Id: " + response.Id, "Ok");

            }
            catch (Exception ex)
            {
                DisplayAlert("", " "+ex, "Ok");
            }
        }



        public async void InviteUser(Object sender, EventArgs e)
        {

            TestStripe();
            /*
            try
            {

                var gateway = new BraintreeGateway
                {
                    Environment = Braintree.Environment.SANDBOX,
                    MerchantId = "pxqrsfx34zhd89q6",
                    PublicKey = "tvfkkdcw3rjgcyq8",
                    PrivateKey = "8008c03d11211c3fc2c8a1952777509d"
                };

                var request = new CustomerRequest
                {
                    FirstName = "Fadsdfiz",
                    LastName = "Ahesdfsdfmd",
                    Company = "Decsdfsdfsdojent",
                    Email = "mark.jsdf34ones@example.com",
                    Fax = "419-555-12234234",
                    Phone = "614-555-123423234",
                    Website = "http://decojent.com",
                    CreditCard = new CreditCardRequest
                    {
                        CardholderName = "Faiz Asdfwerwerwerhmed",
                        ExpirationMonth = "12",
                        Number = "2223000048400111",
                        ExpirationYear = "2020",
                        CVV = "903",
                        Token = "faizAliiili"
                    }

                };

                Result<Customer> result = gateway.Customer.Create(request);

                string success = result.Message;
                // true
                Console.WriteLine("" + success);
                string customerId = result.Target.Id;
                // e.g. 594019

                Console.WriteLine("" + customerId);
                Result<PaymentMethodNonce> results = gateway.PaymentMethodNonce.Create("faizAliiili");
                String nonce = results.Target.Nonce;

                var requests = new TransactionRequest
                {
                    Amount = 10.00M,
                    PaymentMethodNonce = nonce,
                    Options = new TransactionOptionsRequest
                    {
                        SubmitForSettlement = true
                    }
                };

                Result<Transaction> result2 = gateway.Transaction.Sale(requests);
                if (result2.IsSuccess())
                {
                    Console.WriteLine("Transaction SuccessFull");
                    // See result.Target for details
                }
                else
                {
                    // Handle errors
                }

                Customer customer = gateway.Customer.Find("252802696");

                Console.WriteLine(customer.FirstName);

            }
            catch (Exception ex)
            {
             //   Console.WriteLine("" + ex);
                 await DisplayAlert("", "" + ex, "ok");
            }
            */
        }


    }

    }

