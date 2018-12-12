using Newtonsoft.Json;
using OneSignalAPI.ApiClass;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.Connectivity;
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
	public partial class MyTrolly : MasterDetailPage
    {
        static HttpClient _client = new HttpClient();
        List<TrollyCreation> list = null;

      //  private static string url = "https://trolli-194513.appspot.com/MyTrollies?clientId=";


        public MyTrolly ()
		{
			InitializeComponent ();
            GetConnection();
		}
        
        protected override void OnAppearing()
        {
         //   GetRecord();
            base.OnAppearing();
        }

        public void MenuNavigate(Object sender, EventArgs e)
        {

            this.IsPresented = true;
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
            catch (Exception ex){
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
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
                    internetCheck.IsVisible = true;
                    RetryVariable.IsVisible = true;
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
                internetCheck.IsVisible = false;
                RetryVariable.IsVisible = false;

                GetConnection();
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public async void GetRecord() {
            try
            {
                activIndicator.IsRunning = true;
                listView.ItemsSource = null;
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
                    if(globalClientId == clientId)
                    {
                        TrollyCreation ob = new TrollyCreation() {
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
                            SBNotificationId = Convert.ToString(post["SBNotificationId"]),
                            stripeChargeId = Convert.ToString(post["stripeChargeId"])
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
                        if (listData.Contains(ob.store)) { } else
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


                if(trollyDetail.status == "Draft")
                {
                    TrollyCreation trolly = new TrollyCreation
                    {
                        createdDate = trollyDetail.createdDate,
                        lastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy"),
                        trollyTitle = trollyDetail.trollyTitle,
                        trollyDetail = trollyDetail.trollyDetail,
                        trolliId = trollyDetail.trolliId,
                        clientId = trollyDetail.clientId,//Convert.ToString(userData["UniqueID"]),
                        status = "Draft",
                        deliveryDateTime = trollyDetail.deliveryDateTime,
                        assigneeId = " ",
                        clientNotificationId = trollyDetail.clientNotificationId,
                        SBNotificationId = " "
                      };

                    SharedUserData.draftRecord = trolly;
                }

                Navigation.PushAsync(new CreateTrolli());

            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void MenuHandler(Object sender, EventArgs e)
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = true;
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
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }


        }
    }
}