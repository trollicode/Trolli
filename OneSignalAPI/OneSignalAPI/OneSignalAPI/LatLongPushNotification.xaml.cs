using RealTrolli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LatLongPushNotification : ContentPage
	{
        public LatLongPushNotification (string data)
		{
            
            InitializeComponent();
            
            lbl.Text = data;
        }

        public void noti_btn(object sender, EventArgs e) {

            string lati = lat.Text;
            string longi = longitude.Text;
            double km = Convert.ToDouble(radius.Text);
            double radiConvert = (km * 1000)*2;

            string radi = "" + radiConvert;
            lbl.Text = lati + "," + longi +","+radi;

            postNotification(lati,longi, radi);
        }

        public async void postNotification(string latAPI, string longAPI, string range)
        {

            string msgPush = msg.Text; 
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
            request.KeepAlive = false;
            request.Method = "POST";

            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic MGFhOTZjM2UtZjc3Yy00ZTk1LThlZjUtYTE2NzJhOTI5ZDMz");

            byte[] byteArray = Encoding.UTF8.GetBytes("{ \"app_id\": \"6be47d95-7a8a-4f0b-932a-6bffa9ee04ca\", " +
                " \"included_segments\": [\"All\"],  \"data\": {\"foo\": \"bar\"}, " +
                "  \"contents\": {\"en\": \""+msgPush+"\"},  " +
                "\"filters\": [{ \"field\": \"location\", 	" +
                "\"radius\": \""+range+"\", " +
                "\"lat\": \""+latAPI+"\",	" +
                "\"long\": \""+longAPI+"\" }]}");

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

        public string data() {
            return "";
        }
     /*   public async void view_profile(Object sender, EventArgs e)
        {
          //  await DisplayAlert("Testing", "On iOS", "ok");
            await Navigation.PushAsync(new ClientMainPage("Testing"));

        }
    */
    }
}