using Com.OneSignal;
using Newtonsoft.Json;
using OneSignalAPI.ApiClass;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using RealTrolli;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPageDetail : MasterDetailPage
    {
        bool globalValue = true;//Convert.ToBoolean(Application.Current.Properties["isOnlineJobSeeker"]);

        bool notif = true;

        public MenuPageDetail()
        {
            InitializeComponent();
            // showMessage();
            // GetRecord();
            this.IsPresentedChanged += OnPresentedChanged;
            //   bool globalValue = Convert.ToBoolean(Application.Current.Properties["isOnlineJobSeeker"]);
            //   isToggle.IsToggled = globalValue;
        }

       

        protected override void OnAppearing()
        {
            showMessage();
            GetRecord();
        }
        protected override bool OnBackButtonPressed()
        {

            return true;
        }

        public void MenuNavigate(Object sender, EventArgs e)
        {

            this.IsPresented = true;
        }

        private void OnPresentedChanged(object sender, EventArgs e) {
            showMessage();

        }


        public void showMessage()
        {
            mainGridJobs.IsVisible = true;
            firstTrolliGrid.IsVisible = false;
            mainGrid.IsVisible = false;
            serviceSelecter.IsVisible = false;

              string check = Convert.ToString(Application.Current.Properties["checkTrolli"]);

              if (check == "firstTrolli") {
                  firstTrolliGrid.IsVisible = true;
                  mainGrid.IsVisible = false;
                  serviceSelecter.IsVisible = false;
                  mainGridJobs.IsVisible = false;
                firstJobGrid.IsVisible = false;

              }else if(check == "showAlert")
              {
                  firstTrolliGrid.IsVisible = false;
                  mainGrid.IsVisible = false;
                  serviceSelecter.IsVisible = true;
                mainGridJobs.IsVisible = false;
                firstJobGrid.IsVisible = false;
            }
            else if(check == "myTrolli")
              {
                  firstTrolliGrid.IsVisible = false;
                  mainGrid.IsVisible = true;
                  serviceSelecter.IsVisible = false;
                mainGridJobs.IsVisible = false;
                firstJobGrid.IsVisible = false;
            }else if(check == "firstJob")
            {
                firstTrolliGrid.IsVisible = false;
                mainGrid.IsVisible = false;
                serviceSelecter.IsVisible = false;
                mainGridJobs.IsVisible = false;
                firstJobGrid.IsVisible = true;
            }else if(check == "myJobs")
            {
                firstTrolliGrid.IsVisible = false;
                mainGrid.IsVisible = false;
                serviceSelecter.IsVisible = false;
                mainGridJobs.IsVisible = true;
                firstJobGrid.IsVisible = false;
            }
              
        }

        public void SmartBuyerSelect(Object sender, EventArgs e)
        {
            firstJobGrid.IsVisible = true;
            firstTrolliGrid.IsVisible = false;
            mainGrid.IsVisible = false;
            mainGridJobs.IsVisible = false;
            serviceSelecter.IsVisible = false;

            Application.Current.Properties["checkTrolli"] = "firstJob";
            Application.Current.Properties["service"] = "Smart Buyer";


           
        }

        public void ClientSelect(Object sender, EventArgs e)
        {
            firstTrolliGrid.IsVisible = true;
            firstJobGrid.IsVisible = false;
            mainGrid.IsVisible = false;
            mainGridJobs.IsVisible = false;
            
            serviceSelecter.IsVisible = false;

            Application.Current.Properties["checkTrolli"] = "firstTrolli";
            Application.Current.Properties["service"] = "Client";
        }


        public void ToggledHandler(object sender, ToggledEventArgs e)
        {
            try
            {
                Dictionary<string, object> userData = SharedUserData.getUserData;
                bool isOnlineSeeker = e.Value;
               
                if (globalValue)
                {
                    string fullName = Convert.ToString(userData["fullName"]);
                    string simNumber = Convert.ToString(userData["simNumber"]);

                    IsJobOnlineSeeker bean = new IsJobOnlineSeeker
                    {
                        fullName = fullName,
                        simNumber = simNumber,
                        isJobSeeker = "false"
                    };
                    ApiCalling callApi = new ApiCalling();
                    callApi.IsJobOnlineSeeker(bean);
                    Application.Current.Properties["isOnlineJobSeeker"] = false;
                    globalValue = false;


                   // OneSignal.Current.SetSubscription(false);
                    DisplayAlert("Notification Msg", "Notification Off", "Ok");
                }
                else
                {

                    string fullName = Convert.ToString(userData["fullName"]);
                    string simNumber = Convert.ToString(userData["simNumber"]);

                    IsJobOnlineSeeker bean = new IsJobOnlineSeeker
                    {
                        fullName = fullName,
                        simNumber = simNumber,
                        isJobSeeker = "true"
                    };
                    ApiCalling callApi = new ApiCalling();
                    callApi.IsJobOnlineSeeker(bean);
                    Application.Current.Properties["isOnlineJobSeeker"] = true;
                    globalValue = true;

                   // OneSignal.Current.SetSubscription(true);
                    DisplayAlert("Notification Msg", "Notification On", "Ok");

                }
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        static HttpClient _client = new HttpClient();
        List<TrollyCreation> list = null;


        List<string> listData = new List<string>();
        public void CloneHandle(Object sender, EventArgs e)
        {
            try
            {
                //   if (e.SelectedItem == null) return;
                TrollyCreation trollyDetail = (sender as Button).CommandParameter as TrollyCreation;
                ObservableCollection<ListProduct> dataItems = new ObservableCollection<ListProduct>();

                ObservableCollection<ListStores> listStores = new ObservableCollection<ListStores>();
                SharedUserData.addProduct = null;
                string trollyData = "[" + trollyDetail.trollyDetail + "]";
                List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(trollyData);
                //   listView.ItemsSource = posts;

                bool check = true;
                foreach (Dictionary<string, object> i in posts)
                {
                    ListProduct ob = new ListProduct()
                    {
                        detailItem = Convert.ToString(i["detailItem"]),
                        description = Convert.ToString(i["description"]),
                        items = Convert.ToString(i["items"]),
                        quantity = Convert.ToInt32(i["quantity"]),
                        size = Convert.ToString(i["size"]),
                        store = Convert.ToString(i["store"]),
                        storeSuburb = Convert.ToString(i["storeSuburb"]),
                        unit = Convert.ToString(i["unit"])

                    };

                    if (check)
                    {
                        listData.Add(ob.store);

                        ListStores listStore = new ListStores()
                        {
                            store = Convert.ToString(i["store"]),
                            storeSuburb = Convert.ToString(i["storeSuburb"])
                        };
                        listStores.Add(listStore);
                        check = false;
                    }
                    else
                    {
                        if (listData.Contains(ob.store)) { }
                        else
                        {
                            listData.Add(ob.store);

                            ListStores listStore = new ListStores()
                            {
                                store = Convert.ToString(i["store"]),
                                storeSuburb = Convert.ToString(i["storeSuburb"])
                            };
                            listStores.Add(listStore);
                        }
                    }

                    dataItems.Add(ob);

                }

                //  SharedUserData.listStore = listStores;


                SharedUserData.addProduct = dataItems;
                UpdateStoreList();
                Navigation.PushAsync(new CreateTrolli());

            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void FirstTrolli(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateTrolli());
        }


        ObservableCollection<ListProduct> listx = new ObservableCollection<ListProduct>();
        public void UpdateStoreList()
        {
            try
            {
                string store = "";
                string storeItemCount = "";
                int counter = 0;

                int length = listData.Count;
                listx = SharedUserData.addProduct;
                ObservableCollection<ListStores> lists = new ObservableCollection<ListStores>();

                if (listx != null)
                {
                    for (int i = 0; i < length; i++)
                    {
                        foreach (ListProduct listDatas in listx)
                        {
                            if (listData.ElementAt(i) == listDatas.store)
                            {
                                store = listDatas.store;
                                counter++;
                            }
                        }
                        storeItemCount = store + " (" + counter + " Items)";
                        counter = 0;
                        ListStores update = new ListStores()
                        {
                            store = listData.ElementAt(i),
                            storeSuburb = "(No Suburb)",
                            storeDetail = storeItemCount
                        };
                        lists.Add(update);
                    }
                }
                listData = new List<string>();
                SharedUserData.listStore = null;

                SharedUserData.listStore = lists;
            }
            catch (Exception ex)
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
                Navigation.PushAsync(new MyTrolliDetail(trollyDetail));

                ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
        }



        public async void GetRecord()
        {
            try
            {

                string checkStatus = Convert.ToString(Application.Current.Properties["checkTrolli"]);
               
                if (checkStatus == "myTrolli")
                {
                    int check = 0;
                    activIndicator.IsRunning = true;
                    list = new List<TrollyCreation>();
                    Dictionary<string, object> userData = SharedUserData.getUserData;
                    string globalClientId = Convert.ToString(userData["UniqueID"]);  //Application.Current.Properties["phoneNumber"];
                    string url = ApiEndPoints.myTrolliesFind + globalClientId;
                    string content = await _client.GetStringAsync(url);
                    List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);

                    foreach (Dictionary<string, object> i in posts)
                    {
                        Dictionary<string, object> post = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));

                        Dictionary<string, object> trollyData = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(post["trollyDetail"]));
                        string clientId = Convert.ToString(post["clientId"]);
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
                            check++;
                        }

                    }

                    if (check == 0)
                    {
                        mainGrid.IsVisible = false;
                        firstTrolliGrid.IsVisible = true;
                    }

                    listView.ItemsSource = null;

                    listView.ItemsSource = list;
                    activIndicator.IsRunning = false;
                }
                else if(checkStatus == "myJobs")
                {
                    int check = 0;

                    activIndicatorJobs.IsRunning = true;
                    listViewJobs.ItemsSource = null;
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

                            check++;

                        }
                    }
                    if (check == 0)
                    {
                        mainGridJobs.IsVisible = false;
                        firstJobGrid.IsVisible = true;
                    }
                    listViewJobs.ItemsSource = null;

                    listViewJobs.ItemsSource = list;
                    activIndicator.IsRunning = false;
                }
                    
            }
            catch (Exception ex)
            {
                activIndicator.IsRunning = false;

                await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

    }
}