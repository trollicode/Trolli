using Newtonsoft.Json;
using OneSignalAPI.ApiClass;
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
    public partial class SmartBuyerProfile : ContentPage
    {


        static HttpClient _client = new HttpClient();

        public SmartBuyerProfile(string SBId)
        {
            InitializeComponent();
            loadData(SBId);
            //   sbId.Text = SBId;
        }
        /// <summary>
        /// 
        /// </summary>
        public async void loadData(string SBId)
        {

            try
            {
                string url = ApiEndPoints.userProfile + SBId;

                string content = await _client.GetStringAsync(url);

                List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);

                foreach (Dictionary<string, object> i in posts)
                {
                    Dictionary<string, object> post = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));

                    userName.Text = Convert.ToString(post["fullName"]);
                  //  smartBuyerImage.Source = ApiEndPoints.loadImageFromGoogleDrive + Convert.ToString(post["gdProfileImageId"]);



                }

                string ratingUrl = ApiEndPoints.userRating + "?userId=" + SBId + "&userAsA=SmartBuyer";
                string contents = "["+ await _client.GetStringAsync(ratingUrl) +"]";

                List<Dictionary<string, object>> postss = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(contents);
                foreach (Dictionary<string, object> i in postss)
                {
                    jobCount.Text = Convert.ToString(i["Count"]);
                    rating.Text = Convert.ToString(i["Avg"]);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}