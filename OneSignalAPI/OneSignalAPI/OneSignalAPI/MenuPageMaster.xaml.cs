using Com.OneSignal;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using RealTrolli;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPageMaster : ContentPage
    {
        public ListView ListView;
        ObservableCollection<MenuName> data = new ObservableCollection<MenuName>();

        public MenuPageMaster()
        {
            InitializeComponent();
            //  btn.ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left,10);
            LoadProfile();
            ListOfStore();
            ServiceSelect();

           
        }

       

            public void ServiceSelect()
            {

            if (Application.Current.Properties.ContainsKey("service"))
            {

                if (Convert.ToString(Application.Current.Properties["service"]).Equals("Smart Buyer"))
                {
                    // statesPicker.SelectedItem = "Use as Smart Buyer";
                    SmartBuyerMenuData();
                }
                else if (Convert.ToString(Application.Current.Properties["service"]).Equals("Client"))
                {
                    //  statesPicker.SelectedItem = "Use as Client";
                    ClientMenuData();
                }
                else
                {
                 //   statesPicker.SelectedItem = "Use as Client";
                    ClientMenuData();
                }
            }
            else
            {
               // statesPicker.SelectedItem = "Use as Client";
                ClientMenuData();
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ListOfStore() //List of Stores
        {

            try
            {
                data.Add(new MenuName() { menuName = "Home", imageUrl = "homeIcon.png" });
                data.Add(new MenuName() { menuName = "Create Trolli", imageUrl = "myTrolliIcon.png" });
                data.Add(new MenuName() { menuName = "My Trollies", imageUrl = "conversationsIcon.png" });

                data.Add(new MenuName() { menuName = "Referral Counter", imageUrl = "myTrolliIcon.png" });
                data.Add(new MenuName() { menuName = "Terms And Condition", imageUrl = "myTrolliIcon.png" });

                data.Add(new MenuName() { menuName = "Contact Us", imageUrl = "myTrolliIcon.png" });
                listView.ItemsSource = data;
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void SmartBuyerMenuData() {
            smartBuyerBtn.BackgroundColor = Color.FromHex("#00629c");
            clientBtn.BackgroundColor = Color.Transparent;

            data = new ObservableCollection<MenuName>();

            data.Add(new MenuName() { menuName = "Home", imageUrl = "homeIcon.png" });
            data.Add(new MenuName() { menuName = "My Jobs", imageUrl = "myTrolliIcon.png" });

            data.Add(new MenuName() { menuName = "Bank Account Info", imageUrl = "myTrolliIcon.png" });
            data.Add(new MenuName() { menuName = "Terms And Condition", imageUrl = "myTrolliIcon.png" });

            data.Add(new MenuName() { menuName = "Contact Us", imageUrl = "myTrolliIcon.png" });

            listView.ItemsSource = null;
            listView.ItemsSource = data;

        }

        public void ClientMenuData() {
            clientBtn.BackgroundColor = Color.FromHex("#00629c");
            smartBuyerBtn.BackgroundColor = Color.Transparent;

            data = new ObservableCollection<MenuName>();

            data.Add(new MenuName() { menuName = "Home", imageUrl = "homeIcon.png" });
            data.Add(new MenuName() { menuName = "Create Trolli", imageUrl = "myTrolliIcon.png" });
            data.Add(new MenuName() { menuName = "My Trollies", imageUrl = "conversationsIcon.png" });

            data.Add(new MenuName() { menuName = "Referral Counter", imageUrl = "myTrolliIcon.png" });
            data.Add(new MenuName() { menuName = "Terms And Condition", imageUrl = "myTrolliIcon.png" });

            data.Add(new MenuName() { menuName = "Contact Us", imageUrl = "myTrolliIcon.png" });
            // OneSignal.Current.SendTag("userType", "C");

            listView.ItemsSource = null;
            listView.ItemsSource = data;

        }

        public void SmartBuyerHandler(Object sender, EventArgs e)
        {
            SmartBuyerMenuData();
            Application.Current.Properties["service"] = "Smart Buyer";

            Application.Current.Properties["checkTrolli"] = "myJobs";
           
        }

        public void ClientHandler(Object sender, EventArgs e)
        {
            ClientMenuData();
            Application.Current.Properties["service"] = "Client";

            Application.Current.Properties["checkTrolli"] = "myTrolli";
           

        }

        public void ItemSelect(Object sender, SelectedItemChangedEventArgs e)
        {
            // var store = e.SelectedItem as ListStores;
            try
            {

                if (e.SelectedItem == null) return;
                MenuName menuName = e.SelectedItem as MenuName;

                if (menuName.menuName == "Home")
                {
                    Navigation.PushAsync(new MenuPage());
                }
                else if (menuName.menuName == "Create Trolli")
                {
                    Navigation.PushAsync(new NewCreateTrolli());
                }
                else if (menuName.menuName == "My Trollies")
                {
                    Navigation.PushAsync(new MyTrolly());

                }
                else if (menuName.menuName == "My Jobs")
                {
                    Navigation.PushAsync(new MyJobs());

                }else if (menuName.menuName == "Bank Account Info")
                {
                    Navigation.PushAsync(new BankAccountScreen());

                }



            ((ListView)sender).SelectedItem = null;
            }
            catch (Exception ex) {

                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
            /*   if (_oldData == store)
               {
                   store.IsVisible = !store.IsVisible;
               }
               else {
                   if(_oldData != null) {
                       _oldData.IsVisible = false;
                   }
                   store.IsVisible = true;

               }
               _oldData = store;
               //        var product = e.SelectedItem as ListStores;
               listView.ItemsSource = null;
               listView.ItemsSource = ShareUserData.listStore;
             */
        }




        public void CreateTrollyEvent(object sender, EventArgs e) {
            try
            {
                SharedUserData.addProduct = null;
                SharedUserData.listStore = null;
                Navigation.PushAsync(new CreateTrolli());
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
            }

        public void MyTrollyEvent(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new MyTrolly());
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }


      


      

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                LoadProfile();
                ServiceSelect();
            }
            catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

    /*    public void IsJobSeeker(Object sender, EventArgs e) {

            if (nameJobRole.Text == "Use as Smart Buyer")
            {
                nameJobRole.Text = "Use as Client";
            }else if(nameJobRole.Text == "Use as Client")
            {
                nameJobRole.Text = "Use as Smart Buyer";

            }
        }

    */
        public void LoadProfile()
        {

            try
            {
                Dictionary<string, object> userData = SharedUserData.getUserData;

                //  SignupBean bean2 = bal.getData();
                string imageId = "";

                name.Text = Convert.ToString(userData["fullName"]);
                email.Text = Convert.ToString(userData["email"]);

               imageId = Convert.ToString(userData["gdProfileImageId"]);
                if (imageId == "")
                {
                    imageSource.Source = "img.png";
                }
                else
                {
                    imageSource.Source = "https://drive.google.com/uc?export=view&id=" + imageId;

                }

            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        
     


        public void ViewProfile(Object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new Profile());
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void MyJobsEvent(Object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new MyJobs());
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void TrolliHome(Object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new ClientMainPage("asda"));
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
            }

    }
}