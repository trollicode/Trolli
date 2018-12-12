
using Com.OneSignal;
using OneSignalAPI;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Plugin.DeviceInfo;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using RealTrolli.ApiClass;
using RealTrolli.BeanClass;
using RealTrolli.ValidationClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealTrolli
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompleteRegistration : ContentPage
	{


        static HttpClient _client = new HttpClient();
        Dictionary<string, object> reloadData = new Dictionary<string, object>();
        static string phoneNumbers = "";
        static string name = "";
        static string email = "";
        static string referred = "";

        public CompleteRegistration (string phone, string nameUser, string emailUser, string referredBy)
		{
			InitializeComponent ();
            phoneNumbers = phone;
            name = nameUser;
            email = emailUser;
            referred = referredBy;
        }

        protected override bool OnBackButtonPressed()
        {
            
            return false;
        }

        public async void SignUpButton(object sender, EventArgs e)
        {
          

            String UUIDs = Guid.NewGuid().ToString();


            if (CheckTextField())
            {
                activIndicator.IsRunning = true;
                var deviceIds = CrossDeviceInfo.Current.Id; //Can't find Actual Object of CrossDeviceInfo
                // string simNumber = "";
                // string deviceIdJson = "";
                // var content = await _client.GetStringAsync(url);

                // List<Dictionary<string, object>> posts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);

                // foreach (Dictionary<string, object> i in posts)
                // {

                //     Dictionary<string, object> post = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(i["properties"]));
                //     deviceIdJson = Convert.ToString(post["deviceId"]);
                //      if (deviceIdJson == deviceId)
                //      {
                //       simNumber = Convert.ToString(post["simNumber"]);
                //     }
                // }

                //    await Navigation.PushModalAsync(new TermAndService());


                try
                {
                    string oneSignalId = "";
                    OneSignal.Current.IdsAvailable((playerID, pushToken) =>
                    {
                        oneSignalId = playerID;
                        //  App.Current.MainPage.DisplayAlert("playerId", id.ToString(), "OK");
                    });

                 

                    string names = name;
                    string emailId = email;
                    string subrubs = subrub.Text;
                    var statesPickers = statesPicker.Items[statesPicker.SelectedIndex]; //Can't find actual return type
                    string statess = statesPickers;
                    string postCodee = postCode.Text;
                    string countrys = "Australia";
                    string userTypes = "C";
                    string latitude = "45";
                    string longitude = "45";

                    var locator = CrossGeolocator.Current;

                        if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                         {
                            await DisplayAlert("", "Trolli app requires access to location services, in order to function properly. If you do not allow location access, some features may not work as expected.", "Ok");
                        }
                        else
                         {
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

                        string folderID = GoogleDriveAPI.CreateUserFolder("User_" + UUIDs);

                        SignupBean bean = new SignupBean
                        {
                            name = names,
                            email = emailId,
                            subrub = subrubs,
                            states = statess,
                            postCodes = postCodee,
                            country = countrys,
                            userType = userTypes,
                            phoneNumber = phoneNumbers,
                            deviceId = deviceIds,
                            latitude = latitude,
                            longitude = longitude,
                            UUID = UUIDs,
                            folderId = folderID,
                            imageId = "",
                            rewardCard1 = "",
                            rewardCard2 = "",
                            oneSignalId = oneSignalId
                        };
                        IsJobOnlineSeeker bean2 = new IsJobOnlineSeeker
                        {
                            fullName = name,
                            simNumber = phoneNumbers,
                            isJobSeeker = "true"
                        };

                        Application.Current.Properties["phoneNumber"] = phoneNumbers;
                        Application.Current.Properties["isOnlineJobSeeker"] = true;

                        Application.Current.Properties["checkTrolli"] = "showAlert";
                        reloadData.Add("country", countrys);
                        reloadData.Add("UniqueID", UUIDs);
                        reloadData.Add("fullName", names);
                        reloadData.Add("deviceId", deviceIds);
                        reloadData.Add("suburb", subrubs);
                        reloadData.Add("postCode", postCodee);
                        reloadData.Add("state", statess);
                        reloadData.Add("userType", userTypes);
                        reloadData.Add("simNumber", phoneNumbers);
                        reloadData.Add("gdFolderId", folderID);
                        reloadData.Add("gdRewardCard1", ""); 
                        reloadData.Add("gdRewardCard2", "");
                        reloadData.Add("email", emailId);
                        reloadData.Add("gdProfileImageId", "");
                        reloadData.Add("oneSignalId", oneSignalId);
                        SharedUserData.getUserData = reloadData;
                        ApiCalling callApi = new ApiCalling();
                        //  GlobalVaribles golbalVarible = new GlobalVaribles();
                        //  golbalVarible.setData(bean);
                        callApi.SignupPost(bean);
                        callApi.IsJobOnlineSeeker(bean2);
                      //  animationView.Play();
                        //                  
                        //   string folderID = GoogleDriveAPI.createUserFolder("User_"+UUIDs);
                        userReferral referralObject = new userReferral
                        {
                            userId = phoneNumbers,
                            availReward = true,
                            referredByCode = referred,
                            rewardBalance = 0,
                            referralCode = "Furqan_xw7e"

                        };
                        callApi.CreateReferralCode(referralObject);

                        await Navigation.PushAsync(new MenuPage());
                    }
                }
                catch (Exception ex)
                {
                   await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                }
            }
        }

        public string userReferralCodeGenerate()
        {

            return "";
        }

        public bool IsLocationAvailable()
        {
            if (CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        public void TermsAndConditions(Object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new TermsAndConditions());
        }

        public bool CheckTextField()
        {
            bool check = true;
            string validationText = "All fields are required";

            starStates.Text = " ";
            starPostcode.Text = " ";
            starSubrub.Text = " ";

            try
            {
                if (statesPicker.SelectedIndex == -1)
                {
                    starStates.Text = "*";
                    check = false;
                }
                if (subrub.Text == "")
                {
                    starSubrub.Text = "*";
                    check = false;
                }
                if (postCode.Text == "")
                {
                    starPostcode.Text = "*";
                    check = false;
                }
                if (!check)
                {
                    validationError.Text = validationText;
                    //DisplayAlert("Some Think Missing", "Text Field Missing \n" + text, "OK");
                }

                return check;
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                return false;
            }
        }
        
    }
}
