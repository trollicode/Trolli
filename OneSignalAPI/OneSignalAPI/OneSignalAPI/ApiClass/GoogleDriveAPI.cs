using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace RealTrolli.ApiClass
{
    class GoogleDriveAPI
    {
        static string LOGIN_ENDPOINT = "https://accounts.google.com/o/oauth2/token";

        public static string GetAccessToken()
        {
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

        public static string CreateUserFolder(string folderName)
        {
            string accessToken = GetAccessToken();
            //  _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //string folderName = "TestingTrolliFolder";
            var request = WebRequest.Create("https://www.googleapis.com/drive/v2/files") as HttpWebRequest;
            request.KeepAlive = false;
            request.Method = "POST";

            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Bearer " + accessToken);

            byte[] byteArray = Encoding.UTF8.GetBytes("{\"title\": \""+folderName+"\", " +
                                                        "\"mimeType\": \"application/vnd.google-apps.folder\", 	" +
                                                        "\"parents\": [{\"id\":\"1k0dGHSe3OJ7ruLcz9qcHvhK-upq-1YG1\"}]}");

            string responseContent = null;
            string folderId = "folderID";
            
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
                        Dictionary<string, object> posts = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);
                        folderId = Convert.ToString(posts["id"]);
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }
        return folderId;
        }

        public string UploadImage(string pFilename, string pFolder) {
            List<string> _postData = new List<string>();
            _postData.Add("{");
            _postData.Add("\"title\": \"ProfileImage\",");
            _postData.Add("\"description\": \"Uploaded with Trolli API Google Drive\",");
            _postData.Add("\"parents\": [{\"id\":\"" + pFolder + "\"}],");
            _postData.Add("\"mimeType\": \"image/jpeg\"");
            _postData.Add("}");
            string postData = string.Join(" ", _postData.ToArray());
            byte[] MetaDataByteArray = Encoding.UTF8.GetBytes(postData);

            // creating the Data For the file
            byte[] FileByteArray = System.IO.File.ReadAllBytes(pFilename);

            string boundry = "foo_bar_baz";
            string url = "https://www.googleapis.com/upload/drive/v2/files?uploadType=multipart" + "&access_token=" + GetAccessToken();

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "multipart/related; boundary=\"" + boundry + "\"";

            // Wrighting Meta Data
            string headerJson = string.Format("--{0}\r\nContent-Type: {1}\r\n\r\n",
                            boundry,
                            "application/json; charset=UTF-8");
            string headerFile = string.Format("\r\n--{0}\r\nContent-Type: {1}\r\n\r\n",
                            boundry, "image/jpeg");

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

              

                Dictionary<string, object> posts = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseFromServer);
                string imageId = Convert.ToString(posts["id"]);
                // Display the content.
                //Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
                return imageId;
            }
            catch (Exception ex)
            {
                return "Exception uploading file: uploading file." + ex.Message;

            }

        }

    }
}
