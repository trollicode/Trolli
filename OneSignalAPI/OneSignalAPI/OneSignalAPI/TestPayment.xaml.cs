using Stripe;
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
	public partial class TestPayment : ContentPage
	{
		public TestPayment ()
		{
			InitializeComponent ();
		}


        public void TestEvent(Object sender, EventArgs e)
        {
            TestStripe();
        }
        public void TestStripe()
        {
            try
            {
                activIndicator.IsRunning = true;
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
              //  var customerService = new CustomerService();
              //  Customer customer = customerService.Create(customerOptions);

             //   DisplayAlert("", "customer Id: " + customer.Id, "Ok");
                var charge = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(50 * 100), // In cents, not dollars, times by 100 to convert
                    Currency = "aud", // or the currency you are dealing with
                    SourceId = stripeToken.Id,
                    Capture = false
                };
                var services = new ChargeService();
                var response = services.Create(charge);

                DisplayAlert("", "Transaction Id: " + response.Id, "Ok");

                activIndicator.IsRunning = false;
            }
            catch (Exception ex)
            {
                DisplayAlert("", " " + ex, "Ok");
            }
        }

    }
}