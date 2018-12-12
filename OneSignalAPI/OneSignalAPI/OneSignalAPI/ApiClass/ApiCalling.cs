using Newtonsoft.Json;
using OneSignalAPI.ApiClass;
using OneSignalAPI.BeanClass;
using Plugin.Connectivity;
using Plugin.Geolocator;
using RealTrolli.BeanClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RealTrolli
{
    class ApiCalling
    {
        static HttpClient _client = new HttpClient();
        static HttpClient _client2 = new HttpClient();


        public async void SignupPost(SignupBean ob) {
            try
            {
                await _client.PostAsync(ApiEndPoints.userRegistrations+"&fullName=" + ob.name + "&simNumber=" + ob.phoneNumber + "&suburb=" + ob.subrub + "&state=" + ob.states + "&postCode=" + ob.postCodes + "&country=" + ob.country + "&email=" + ob.email + "&userType=" + ob.userType + "&latitude=" + ob.latitude + "&longitude=" + ob.longitude + "&deviceId=" + ob.deviceId+"&UUID="+ob.UUID + "&rewardCard1=" + ob.rewardCard1+ "&rewardCard2=" + ob.rewardCard2+ "&gdFolderId="+ob.folderId+ "&imageId=" + ob.imageId +"&oneSignalId="+ob.oneSignalId, null);
            }
            catch (Exception ex) {
                Log.Warning("Error", ex.ToString());
            }
        }

        public async void IsJobOnlineSeeker(IsJobOnlineSeeker ob)
        {
            try
            {
                await _client.PostAsync("https://trolli-194513.appspot.com/isOnlineJobSeeker?fullName=" + ob.fullName + "&simNumber=" + ob.simNumber+ "&isJobSeeker=" + ob.isJobSeeker, null);
            }
            catch (Exception ex){
                Log.Warning("Error", ex.ToString());
            }
        }

        public async void TrollyCreation(TrollyCreation ob) 
        {
            try
            {
                await _client.PostAsync(ApiEndPoints.trolliCreation+"&trolliId="+ ob.trolliId +"&clientId="+ob.clientId+"&trollyTitle="+ob.trollyTitle+"&trollyDetail="+ob.trollyDetail+"&createdDate="+ob.createdDate+"&lastModifiedDate="+ob.lastModifiedDate+"&deliveryDateTime="+ob.deliveryDateTime+"&status="+ob.status + "&assigneeId=" + ob.assigneeId +"&SBNotificationId= "+ob.SBNotificationId + "&clientNotificationId="+ob.clientNotificationId+"&stripeChargeId="+ob.stripeChargeId, null);
            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.ToString());
            }

        }
        public async void CreateReferralCode(userReferral ob)
        {
            try
            {
                await _client.PostAsync(ApiEndPoints.userReferralData + "?userId=" + ob.userId + "&referralCode=" + ob.referralCode + "&referredByCode=" + ob.referredByCode + "&rewardBalance=" + ob.rewardBalance + "&availReward=" + ob.availReward, null);
            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.ToString());
            }

        }



        public string TrollyTestTransaction(TrollyCreation ob)
        {
            try
            {
               var response = _client.PutAsync(ApiEndPoints.assigneeJobTransationAPI+"?assigneeId=" + ob.assigneeId + "&trolliId=" + ob.trolliId +"&SBNotificationId=" +ob.SBNotificationId, null).Result;
               string jsonResponse = response.Content.ReadAsStringAsync().Result;
               return jsonResponse;
            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.ToString());
                return "";
            }

        }


        public async void SaveTrolliFee(TrollyFinancialInfo ob)
        {
            try
            {
                await _client.PostAsync(ApiEndPoints.trolliFinancialInfoAPI+"?principalAmount="+ ob.baseGroceryAmount +"&serviceProviderAmount="+ob.serviceProviderFee+"&TotalAmount="+ob.totalAmount+"&TrolliId="+ob.trolliId+"&clientVerified="+ob.isConfirmed, null);
            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.ToString());
            }
        }

        public async void UpdateTrolliFee(TrollyFinancialInfo ob)
        {
            try
            {
                await _client.PostAsync(ApiEndPoints.trolliFinancialInfoAPI+"?TrolliId="+ob.trolliId+ "&baseGroceryAmount=" + ob.baseGroceryAmount+ "&clientVerified=false", null);
            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.ToString());
            }
        }

        public async void ClientRating(string trolliId, string userId, string rating, string review)
        {
            try
            {
               await _client.PostAsync(ApiEndPoints.rating+"?trolliID=" + trolliId + "&ratingBySmartBuyer=0&reviewBySmartBuyer=&userId=" + userId + "&ratingByClient="+rating+"&reviewByClient="+review+"&userAsA=Client", null);
            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.ToString());
            }


        }

        public async void SmartBuyerRating(string trolliId, string userId, string rating, string review) {
            try
            {
                await _client.PostAsync(ApiEndPoints.rating+"?trolliID="+trolliId+"&ratingBySmartBuyer="+rating+ "&reviewBySmartBuyer=" + review +"&userId="+userId+ "&ratingByClient=0&reviewByClient=&userAsA=SmartBuyer", null);
            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.ToString());
            }

        }

        public async void JobCompilation(string trolliId)
        {
            try
            {
                await _client.PostAsync(ApiEndPoints.jobCompilation+"?trolliId=" + trolliId, null);
            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.ToString());
            }

        }

        public async void CreateStripeCustomerId(string trolliId, string customerId)
        {
            try
            {
                await _client.PostAsync(ApiEndPoints.createStripeCustomerId + "?TrolliId=" + trolliId + "&stripeCustomerId=" + customerId, null);

            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.ToString());
            }
        }

        public async void TrolliVerifyFinancialInfo(string trolliId)
        {
            try
            {
                await _client.PutAsync(ApiEndPoints.trolliVerifyFinancialInfo + "?trolliID=" + trolliId, null);
            }
            catch (Exception ex)
            {
                Log.Warning("Error", ex.ToString());
            }

        }

        public void SendNotificationWhenSBEnterAmount(string shopperName, string clientNotificationId, string transcationId, string smartBuyerId)
        {
            string msgPush = shopperName + " has entered grocery amount. Click to view Payment Screen";

            var request = WebRequest.Create(ApiEndPoints.oneSignalCallApi) as HttpWebRequest;
            request.KeepAlive = false;
            request.Method = "POST";

            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic MGFhOTZjM2UtZjc3Yy00ZTk1LThlZjUtYTE2NzJhOTI5ZDMz");

            byte[] byteArray = Encoding.UTF8.GetBytes("{ \"app_id\": \"6be47d95-7a8a-4f0b-932a-6bffa9ee04ca\", " +
                   " \"include_player_ids\": [\"" + clientNotificationId + "\"]," +
                   " \"image\": \"img.png\"," +
                   " \"headings\": {\"en\": \"Trolly Job Accepted\"}, " +
                   " \"data\": {\"location\": \"Location Testing\", \"transactionId\": \"" + transcationId + "\", \"smartBuyerId\": \""+smartBuyerId+"\"}, " +
                   " \"contents\": {\"en\": \"" + msgPush + "\"}}");

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

            System.Diagnostics.Debug.WriteLine(responseContent);


        }

        public async void AcceptedStripeAgreement(string accountId, long date, string ip)
        {

            string url = "https://api.stripe.com/v1/accounts/"+accountId+"?tos_acceptance[date]=" + date + "&tos_acceptance[ip]=" + ip;

            var request = WebRequest.Create(url) as HttpWebRequest;
            request.KeepAlive = false;
            request.Method = "POST";

            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Bearer sk_test_Q5wSnyXL03yN0KpPaAMYttOb");

            string responseContent = null;
            try
            {
               

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

        public async void SendNotificationWhenSBCancelled(string trolliId, string clientNotifcationId, string smartBuyerNotificationId)
        {
            try
            {
                string range = "10000";
                string latAPI = "";
                string longAPI = "";
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var position = await locator.GetPositionAsync(); //Can't find actual return type
                    latAPI = "" + position.Latitude;
                    longAPI = "" + position.Longitude;
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    var position = await locator.GetPositionAsync(); //Can't find actual return type

                    latAPI = "" + position.Latitude;
                   longAPI = "" + position.Longitude;
                }

                string msgPush = "Trolli Assign ID: " + trolliId;
                
                var request = WebRequest.Create(ApiEndPoints.oneSignalCallApi) as HttpWebRequest;
                request.KeepAlive = false;
                request.Method = "POST";

                request.ContentType = "application/json; charset=utf-8";

                request.Headers.Add("authorization", "Basic MGFhOTZjM2UtZjc3Yy00ZTk1LThlZjUtYTE2NzJhOTI5ZDMz");

                byte[] byteArray = Encoding.UTF8.GetBytes("{ \"app_id\": \"6be47d95-7a8a-4f0b-932a-6bffa9ee04ca\", " +
                    " \"included_segments\": [\"All\"]," +
                    " \"excluded_segments\": [\"f13d9b4e-47d3-459b-9e7f-61e720dccdb9\"]," +
                    " \"image\": \"img.png\"," +
                    " \"headings\": {\"en\": \"Trolli Job Invitation\"}, " +
                    " \"data\": {\"location\": \"Location Testing\", \"asigneeId\": \"" + trolliId + "\"}, " +
                    " \"contents\": {\"en\": \"" + msgPush + "\"},  " +
                    "\"filters\": [{\"field\": \"tag\", \"key\": \"deviceId\", \"relation\": \"!=\", \"value\": \"" + clientNotifcationId + "\"}," +
                    "{\"field\": \"tag\", \"key\": \"deviceId\", \"relation\": \"!=\", \"value\": \"" + smartBuyerNotificationId + "\"},"+
                    "{\"field\": \"location\", \"radius\": \"" + range + "\", " +
                    "\"lat\": \"" + latAPI + "\", " +
                    "\"long\": \"" + longAPI + "\"}]}");

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

                System.Diagnostics.Debug.WriteLine(responseContent);
                
                Plugin.Toast.CrossToastPopUp.Current.ShowToastMessage("Notification sent to Smart Buyer's");
            }
            catch (Exception ex)
            {
                
                //Exception Class
            }

        }

        public void SendindividualNotification(SignupBean ob, string clientNotificationId) {
            string msgPush = ob.name + " has accepted this job. Click to view Smart buyer's profile";

            var request = WebRequest.Create(ApiEndPoints.oneSignalCallApi) as HttpWebRequest;
            request.KeepAlive = false;
            request.Method = "POST";

            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic MGFhOTZjM2UtZjc3Yy00ZTk1LThlZjUtYTE2NzJhOTI5ZDMz");

            byte[] byteArray = Encoding.UTF8.GetBytes("{ \"app_id\": \"6be47d95-7a8a-4f0b-932a-6bffa9ee04ca\", " +
                   " \"include_player_ids\": [\""+clientNotificationId+"\"]," +
                   " \"image\": \"img.png\"," +
                   " \"headings\": {\"en\": \"Trolly Job Accepted\"}, " +
                   " \"data\": {\"location\": \"Location Testing\", \"SBProfileId\": \"" + ob.UUID + "\"}, " +
                   " \"contents\": {\"en\": \"" + msgPush + "\"}}");

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

            System.Diagnostics.Debug.WriteLine(responseContent);

        }
        

        public async void SendSBNotification(string userId, string clientNotificationId)
        {
            string url = "https://trolli-194513.appspot.com/Test?id=" + userId;
            SignupBean bean = null;
            try
            {
                string content = await _client.GetStringAsync(url);
                List<Dictionary<string, object>> post = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(Convert.ToString(content));

                foreach (Dictionary<string, object> ob in post)
                {

                    Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(Convert.ToString(ob["properties"]));
                    SignupBean obj = new SignupBean() {
                        name = Convert.ToString(values["fullName"]),
                        phoneNumber = Convert.ToString(values["simNumber"]),
                        UUID = Convert.ToString(values["UniqueID"])
                       
                    };

                    SendindividualNotification(obj, clientNotificationId);
                }

               
            }
            catch(Exception ex)
            {
                //Exception Class
                
            }
        }

        public void SendNotificationToClient(string clientNotificationId)
        {
            string msgPush = "The assigned smart buyer is unable to make it. Please wait while we find another smart buyer for you";

            var request = WebRequest.Create(ApiEndPoints.oneSignalCallApi) as HttpWebRequest;
            request.KeepAlive = false;
            request.Method = "POST";

            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic MGFhOTZjM2UtZjc3Yy00ZTk1LThlZjUtYTE2NzJhOTI5ZDMz");

            byte[] byteArray = Encoding.UTF8.GetBytes("{ \"app_id\": \"6be47d95-7a8a-4f0b-932a-6bffa9ee04ca\", " +
                   " \"include_player_ids\": [\"" + clientNotificationId + "\"]," +
                   " \"image\": \"img.png\"," +
                   " \"headings\": {\"en\": \"Smart Buyer Cancelled the Job.\"}, " +
                   " \"contents\": {\"en\": \"" + msgPush + "\"}}");

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

            System.Diagnostics.Debug.WriteLine(responseContent);


        }

        public void SendNotificationToSB(string SBNotificationId)
        {
            string msgPush = "The client has cancelled this Trolli. However you will still receive the $[Amount] base service fee.\n Please do not go ahead with shopping. If you have already purchased some items, you can return them to store.";

            var request = WebRequest.Create(ApiEndPoints.oneSignalCallApi) as HttpWebRequest;
            request.KeepAlive = false;
            request.Method = "POST";

            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic MGFhOTZjM2UtZjc3Yy00ZTk1LThlZjUtYTE2NzJhOTI5ZDMz");

            byte[] byteArray = Encoding.UTF8.GetBytes("{ \"app_id\": \"6be47d95-7a8a-4f0b-932a-6bffa9ee04ca\", " +
                   " \"include_player_ids\": [\"" + SBNotificationId + "\"]," +
                   " \"image\": \"img.png\"," +
                   " \"headings\": {\"en\": \"Client Cancelled the Job.\"}, " +
                   " \"contents\": {\"en\": \"" + msgPush + "\"}}");

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

            System.Diagnostics.Debug.WriteLine(responseContent);


        }


        private bool getConnection()
        {

            bool connect = CrossConnectivity.Current.IsConnected;
            if (connect)
            {
                //   CheckDeviceId();
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
