using Newtonsoft.Json;
using OneSignalAPI.ApiClass;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.Connectivity;

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
    public partial class MyJobs : MasterDetailPage
    {

        static HttpClient _client = new HttpClient();
        List<TrollyCreation> list = null;

        public MyJobs()
        {
            InitializeComponent();
              GetRecord();
            GetConnection();

           
        }

        protected override void OnAppearing()
        {
            GetRecord();
            base.OnAppearing();
        }

        private void GetConnection()
        {
            try
            {
                bool connect = CrossConnectivity.Current.IsConnected;
                if (connect)
                {
                    GetRecord();
                }
                else
                {
                    DisplayAlert("Uh Oh!", "It seems like you are not connected to internet. Retry once your connections is back", "Exit");
                 //   internetCheck.IsVisible = true;
                 //   RetryVariable.IsVisible = true;
                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void RetryEvent(Object sender, EventArgs e)
        {
            try
            {
              //  internetCheck.IsVisible = false;
               // RetryVariable.IsVisible = false;

                GetConnection();
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem == null) return;
                TrollyCreation trollyDetail = e.SelectedItem as TrollyCreation;
                Navigation.PushAsync(new MyJobDetail(trollyDetail));

                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public async void GetRecord()
        {
            try
            {
                activIndicator.IsRunning = true;
                listView.ItemsSource = null;
                list = new List<TrollyCreation>();
                Dictionary<string, object> userData = SharedUserData.getUserData;
                string globalClientId = Convert.ToString(userData["UniqueID"]);//"c9bdb5ec-7ef1-462c-adbb-076e8a4cddf4";//   //Application.Current.Properties["phoneNumber"];
                string url = ApiEndPoints.myJobsFind + globalClientId;
                string content = await _client.GetStringAsync(url);
                List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);

                foreach (Dictionary<string, object> i in posts)
                {
                    Dictionary<string, object> post = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));

                    Dictionary<string, object> trollyData = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(post["trollyDetail"]));
                    string clientId = Convert.ToString(post["assigneeId"]);
                    if (globalClientId == clientId)
                    {

                        TrollyCreation ob = new TrollyCreation()
                        {
                            clientId = Convert.ToString(post["clientId"]),
                            trolliId = Convert.ToString(post["trolliId"]),
                            createdDate = Convert.ToString(post["createdDate"]),
                            deliveryDateTime = Convert.ToString(post["deliveryDateTime"]),
                            lastModifiedDate = Convert.ToString(post["lastModifiedDate"]),
                            status = Convert.ToString(post["status"]),

                           trollyDetail = Convert.ToString(trollyData["value"]),

                            trollyTitle = Convert.ToString(post["trollyTitle"]),
                            assigneeId = Convert.ToString(post["assigneeId"]),
                            clientNotificationId = Convert.ToString(post["clientNotificationId"]),
                            SBNotificationId = Convert.ToString(post["SBNotificationId"])

                        };
                        list.Add(ob);

                    }

                }

                listView.ItemsSource = list;
                activIndicator.IsRunning = false;


            }
            catch (Exception ex)
            {
                activIndicator.IsRunning = false;

               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

        }
        public void MenuNavigate(Object sender, EventArgs e)
        {
          
            this.IsPresented = true;
        }
    }
}