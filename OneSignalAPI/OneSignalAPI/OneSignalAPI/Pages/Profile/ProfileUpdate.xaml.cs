using OneSignalAPI.ApiClass;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.Geolocator;
using Plugin.Media;
using RealTrolli.ApiClass;
using RealTrolli.BeanClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealTrolli
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfileUpdate : MasterDetailPage
	{

        GoogleDriveAPI googleAPI = new GoogleDriveAPI();
       public Dictionary<string, object> reloadData = new Dictionary<string, object>();

       public string imageId = "";
       public int updateImageCheck = 0;
       public string gdFolderID = "";

        public ProfileUpdate ()
		{
			InitializeComponent ();
            
            LoadProfile();
		}
        public void MenuNavigate(Object sender, EventArgs e)
        {

           this.IsPresented = true;
        }



        public int GetStates(string states)
        {
            try
            {

                if (states == "ACT")
                {
                    return 0;
                }
                else if (states == "NSW")
                {
                    return 1;
                }
                else if (states == "NT")
                {
                    return 2;
                }
                else if (states == "QLD")
                {
                    return 3;
                }
                else if (states == "SA")
                {
                    return 4;
                }
                else if (states == "TAS")
                {
                    return 5;
                }
                else if (states == "VIC")
                {
                    return 6;
                }
                else if (states == "WA")
                {
                    return 7;
                }

                return -1;
            }catch(Exception ex)
            {
                //Exception Class
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                return -1;
            }
        }

        public void LoadProfile()
        {
            try
            {
                Dictionary<string, object> userData = SharedUserData.getUserData;


                txtName.Text = Convert.ToString(userData["fullName"]);
                txtEmail.Text = Convert.ToString(userData["email"]);
                txtStates.SelectedIndex = GetStates(Convert.ToString(userData["state"]));
                txtSuburb.Text = Convert.ToString(userData["suburb"]);
                txtPostCode.Text = Convert.ToString(userData["postCode"]);
                gdFolderID = Convert.ToString(userData["gdFolderId"]);

                imageId = Convert.ToString(userData["gdProfileImageId"]);
                if (imageId == "")
                {
                    profileImage.Source = "logo2.png";
                }
                else
                {
                    profileImage.Source = ApiEndPoints.loadImageFromGoogleDrive + imageId;

                }
                /*     GlobalVaribles bal = new GlobalVaribles();
                     SignupBean bean2 = bal.getData();


                     txtName.Text = bean2.name;
                     txtEmail.Text = bean2.email;
                     txtStates.Text = bean2.states;
                     txtSuburb.Text = bean2.subrub;
                     txtPostCode.Text = bean2.postCodes;
                     */
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
        }

        public async void ButtonUpdate(Object sender, EventArgs e)
        {

            // ActivityIndicator AI = new ActivityIndicator();
            // AI.IsRunning = true;
            //GlobalVaribles bal = new GlobalVaribles();
            //SignupBean bean2 = bal.getData();
            try
            {
                loaderAI.IsRunning = true;
                Dictionary<string, object> userData = SharedUserData.getUserData;
                
                string name = txtName.Text;
                string email = txtEmail.Text;
                string states = txtStates.Items[txtStates.SelectedIndex];
                string suburb = txtSuburb.Text;
                string postCode = txtPostCode.Text;
                string UDID = Convert.ToString(userData["UniqueID"]);
                string simNumber = Convert.ToString(userData["simNumber"]);
                string userType = Convert.ToString(userData["userType"]);

                string latitude = "0";
                string longitude = "0";
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var position = await locator.GetPositionAsync(); //Can't find actual return type
                    latitude = "" + position.Latitude;
                    longitude = "" + position.Longitude;
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    var position = await locator.GetPositionAsync(); //Can't find actual return type

                    latitude = "" + position.Latitude;
                    longitude = "" + position.Longitude;
                }

                string country = Convert.ToString(userData["country"]);
                string deviceId = Convert.ToString(userData["deviceId"]);
                string folderId = Convert.ToString(userData["gdFolderId"]);
                string oneSignalId = Convert.ToString(userData["oneSignalId"]);
                if (updateImageCheck == 0)
                {
                    imageId = Convert.ToString(userData["gdProfileImageId"]);
                }


                SignupBean bean = new SignupBean
                {
                    name = name,
                    email = email,
                    subrub = suburb,
                    states = states,
                    postCodes = postCode,
                    country = country,
                    userType = userType,
                    phoneNumber = simNumber,
                    deviceId = deviceId,
                    latitude = latitude,
                    longitude = longitude,
                    UUID = UDID,
                    imageId = imageId,
                    rewardCard1 = "",
                    rewardCard2 = "",
                    folderId = folderId,
                    oneSignalId = oneSignalId

                };


                ApiCalling callApi = new ApiCalling();
                callApi.SignupPost(bean);
                reloadData.Add("country", country);
                reloadData.Add("UniqueID", UDID);
                reloadData.Add("fullName", name);
                reloadData.Add("deviceId", deviceId);
                reloadData.Add("suburb", suburb);
                reloadData.Add("postCode", postCode);
                reloadData.Add("state", states);
                reloadData.Add("userType", userType);
                reloadData.Add("simNumber", simNumber);
                reloadData.Add("gdFolderId", folderId);
                reloadData.Add("gdRewardCard1", "");
                reloadData.Add("gdRewardCard2", "");
                reloadData.Add("email", email);
                //reloadData.Add("gdProfileImageId", imageId);

                reloadData.Add("oneSignalId", oneSignalId);
                SharedUserData.getUserData = reloadData;

                // Application.Current.Properties["id"] = deviceId;
                //Application.Current.Properties["phoneNumber"] = simNumber;

                //  NavigationPage page = new NavigationPage(new ClientMainPage(name));
                //await Navigation.PopToRootAsync();
                //await Navigation.PushModalAsync(new ClientMainPage(name));
                //  this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count]);

                //  await Navigation.PushAsync(new ClientMainPage(name));
                // App app = Application.Current as App;
                // ClientMainPage md = (ClientMainPage)app.MainPage;
                // AI.IsRunning = false;
                loaderAI.IsRunning = false;
                await Navigation.PopModalAsync();
            }catch(Exception ex)
            {
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }


        }


   /*     public async void image_gallery(Object sender, EventArgs e) {
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
            });

            if (file == null)
                return;
           imageId = googleAPI.uploadImage(file.Path, gdFolderID);

         //   reloadData.Add("gdProfileImageId", imageId);
            profileImage.Source = "https://drive.google.com/uc?export=view&id="+imageId;
            updateImageCheck = 1;
            //  await DisplayAlert("File Location", file.Path, "OK");
        }
        */
        public async void ImageCamera(Object sender, EventArgs e) {

            try
            {

                loaderAI.IsRunning = true;
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small

                });
                if (file == null)
                    return;

                imageId = googleAPI.UploadImage(file.Path, gdFolderID); 
                profileImage.Source = ApiEndPoints.loadImageFromGoogleDrive + imageId;

                reloadData.Add("gdProfileImageId", imageId);
                updateImageCheck = 1;
                loaderAI.IsRunning = false;
                //await DisplayAlert("File Location", file.Path, "OK");
            }catch(Exception ex)
            {
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
        }

    }
}