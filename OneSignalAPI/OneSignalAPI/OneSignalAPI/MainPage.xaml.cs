using Com.OneSignal;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Plugin.Media;
using RealTrolli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace OneSignalAPI
{
	public partial class MainPage : ContentPage
	{
       

        public MainPage()
		{
			InitializeComponent();
          //  getAccessTokenAsync();
            //getData(getAccessToken());
           // createFolder(getAccessToken());  
        }
        HttpClient _client = new HttpClient();

        public async void btn_profile(Object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProfileUpdate());

        }


        public async void btn_gallery(Object sender, EventArgs e) {

            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
                });

                if (file == null)
                    return;

                //     imageStream = file.GetStream();
                //    BinaryReader br = new BinaryReader(imageStream);
                //  imageByte = br.ReadBytes((int)imageStream.Length);
                //  UploadImage(imageByte, getAccessToken());
                uploadImage(getAccessToken(), file.Path);
                await DisplayAlert("File Location", file.Path, "OK");
                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERROR!", " " + ex, "ok");
            }



        }



        public async void btn_camera(Object sender, EventArgs e) {
            //  Stream imageStream = null;
            //   byte[] imageByte;
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Custom,
                    CustomPhotoSize = 15

                });

                if (file == null)
                    return;

       //     imageStream = file.GetStream();
        //    BinaryReader br = new BinaryReader(imageStream);
          //  imageByte = br.ReadBytes((int)imageStream.Length);
            //  UploadImage(imageByte, getAccessToken());
            uploadImage(getAccessToken(), file.Path);
            await DisplayAlert("File Location", file.Path, "OK");
            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            }
            catch (Exception ex) {
                await DisplayAlert("ERROR!", " "+ex, "ok");
            }


        }

     public void createFolder(string accessToken) {

            //  _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string folderName = "TestingTrolliFolder";
            var request = WebRequest.Create("https://www.googleapis.com/drive/v3/files") as HttpWebRequest;
            request.KeepAlive = false;
            request.Method = "POST";

            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Bearer "+ accessToken);

            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                            + "\"name\": \""+folderName+"\","
                                            + "\"mimeType\": \"application/vnd.google-apps.folder\"}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }
        }

      




        public void uploadImage(string accessToken, string pFilename) {
            FileInfo info = new FileInfo(pFilename);
            //Createing the MetaData to send
            List<string> _postData = new List<string>();
            _postData.Add("{");
            _postData.Add("\"title\": \"" + info.Name + "\",");
            _postData.Add("\"description\": \"Uploaded with SendToGoogleDrive\",");
            _postData.Add("\"parents\": [{\"id\":\"1_KUtsjLwZdzSYaYnhSkhMrVbxwEVPM6E\"}],");
            _postData.Add("\"mimeType\": \"image/jpeg\"");
            _postData.Add("}");
            string postData = string.Join(" ", _postData.ToArray());
            byte[] MetaDataByteArray = Encoding.UTF8.GetBytes(postData);

            // creating the Data For the file
            byte[] FileByteArray = System.IO.File.ReadAllBytes(pFilename);

            string boundry = "foo_bar_baz";
            string url = "https://www.googleapis.com/upload/drive/v2/files?uploadType=multipart" + "&access_token=" + accessToken;

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "multipart/related; boundary=\"" + boundry + "\"";

            // Wrighting Meta Data
            string headerJson = string.Format("--{0}\r\nContent-Type: {1}\r\n\r\n",
                            boundry,
                            "application/json; charset=UTF-8");
            string headerFile = string.Format("\r\n--{0}\r\nContent-Type: {1}\r\n\r\n",
                            boundry,
                            "image/jpeg");

            string footer = "\r\n--" + boundry + "--\r\n";

            int headerLenght = headerJson.Length + headerFile.Length + footer.Length;
            request.ContentLength = MetaDataByteArray.Length + FileByteArray.Length + headerLenght;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(Encoding.UTF8.GetBytes(headerJson), 0, Encoding.UTF8.GetByteCount(headerJson));   // write the MetaData ContentType
            dataStream.Write(MetaDataByteArray, 0, MetaDataByteArray.Length);                                          // write the MetaData


            dataStream.Write(Encoding.UTF8.GetBytes(headerFile), 0, Encoding.UTF8.GetByteCount(headerFile));   // write the File ContentType
            dataStream.Write(FileByteArray, 0, FileByteArray.Length);                                  // write the file

            // Add the end of the request.  Start with a newline

            dataStream.Write(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));
            dataStream.Close();

            try
            {
                WebResponse response = request.GetResponse();
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

                DisplayAlert("ERROR!", "Image can't upload "+ex, "ok");
                //return "Exception uploading file: uploading file." + ex.Message;

            }


        }





        private async Task<string> UploadImage(byte[] photoBytes, string accessToken)
        {

            string url = "https://www.googleapis.com/upload/drive/v3/files";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);



            var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(photoBytes);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                
                FileName = "record.jpeg",
                Name = "NewRecord"
            };
            content.Add(fileContent);
            
            var response =  await _client.PostAsync(url, content);

            return response.Content.ReadAsStringAsync().Result;
         

        }





        public async void getData(string accessToken) {

            string url = "https://www.googleapis.com/drive/v3/files";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await _client.GetStringAsync(url);
            Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
            string deviceIdJson = Convert.ToString(values["nextPageToken"]);


            Console.Write(deviceIdJson);
        }

        string LOGIN_ENDPOINT = "https://accounts.google.com/o/oauth2/token";

        public string getAccessToken() {
            String jsonResponse;


            using (var client = new HttpClient())
            {
                var request = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"client_id", "152414496982-3ihvmp245245tdk6vjuek1vfientm7d3.apps.googleusercontent.com"},
                {"client_secret", "yRX7XOvyDXmg7KKJ4x43HSPz"},
                    {"redirect_uri","https://developers.google.com/oauthplayground" },
                    {"scope","https://www.googleapis.com/auth/drive" },
                    {"refresh_token","1/Sv1WaojrndaEH2m0DYKek-eWijWQdz7HQWH9v9UW9wE"},


                }
                );
                request.Headers.Add("X-PrettyPrint", "1");
                var response = client.PostAsync(LOGIN_ENDPOINT, request).Result;
                jsonResponse = response.Content.ReadAsStringAsync().Result;
            }
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
            string AuthToken = values["access_token"];
            //var InstanceUrl = values["instance_url"];

            return AuthToken;
        }




        /*   void SomeMethod()
           {
               OneSignal.Current.IdsAvailable(IdsAvailable);
           }

           private void IdsAvailable(string userID, string pushToken)
           {
               Console.Write("UserID:" + userID);
               Console.Write("pushToken:" + pushToken);
           }
           */
    }
}
