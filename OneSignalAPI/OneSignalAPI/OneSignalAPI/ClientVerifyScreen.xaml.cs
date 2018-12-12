using Newtonsoft.Json;
using OneSignalAPI.ApiClass;
using OneSignalAPI.ValidationClass;
using RealTrolli;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientVerifyScreen : ContentPage
	{

        static HttpClient _client = new HttpClient();
        ApiCalling api = new ApiCalling();
        string trolliId;
        string smartBuyerId;
        string jobChargeId;
        string totalAmounts;
        public ClientVerifyScreen (string trolliId, string smartBuyerId, string chargeId)
		{
			InitializeComponent ();
            loadData(trolliId);
            this.trolliId = trolliId;
            this.smartBuyerId = smartBuyerId;
            this.jobChargeId = chargeId;
        }


        public async void loadData(string trolliId) {
            try
            {
                indicator.IsRunning = true;
                string url = ApiEndPoints.trolliFinancialInfoAPI + "?TrolliId=" + trolliId;
                string content = await _client.GetStringAsync(url);

                List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);
                foreach (Dictionary<string, object> i in posts)
                {

                    Dictionary<string, object> post = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));

                    string trolliIds = Convert.ToString(post["trolliID"]);
                    string principalAmounts = Convert.ToString(post["baseGroceryAmount"]);
                    string serviceProviderFee = Convert.ToString(post["serviceProviderFee"]);
                    totalAmounts = Convert.ToString(post["totalAmount"]);




                    totalAmount.Text = "$" + totalAmounts+"/-";
                    serviceProviderAmount.Text = "$" + serviceProviderFee+"/-";
                    principalAmount.Text = "$"+principalAmounts+"/-";

                    if (principalAmounts == "0")
                    {
                        verify.IsVisible = false;
                    }
                }

                indicator.IsRunning = false;
            }catch(Exception ex)
            {

               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void BackEvent(Object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        public async void chargeGroceryAmount()
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_Q5wSnyXL03yN0KpPaAMYttOb");
                var options = new RefundCreateOptions
                {
                    ChargeId = jobChargeId
                };
                var service = new RefundService();
                Refund refund = service.Create(options);

                double amount = Convert.ToDouble(totalAmounts);
             
 
                var charge = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(amount * 100), // In cents, not dollars, times by 100 to convert
                    Currency = "aud", // or the currency you are dealing with
                    CustomerId = "cus_Dxa4I8cQgAL7D7"//Convert.ToString(userData["stripeCustomerId"]),

                };
                var services = new ChargeService();
                var response = services.Create(charge);

                await DisplayAlert("", "Successfully Charge Amount: " + amount, "Ok");
            }
            catch (Exception ex)
            {

                await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void VerifyEvent(Object sender, EventArgs e) {

            try
            {
                chargeGroceryAmount();
                api.JobCompilation(trolliId);
                api.TrolliVerifyFinancialInfo(trolliId);
                Navigation.PushAsync(new RatingAndReviewForSmartBuyer(trolliId, smartBuyerId));
            }
            catch (Exception ex) {

                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");

            }


        }

    }
}