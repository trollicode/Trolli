using OneSignalAPI.ValidationClass;
using RealTrolli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RatingAndReviewForClient : ContentPage
	{
        string trolliId = "";
        string userId = "";

        ApiCalling api = new ApiCalling();
        bool check = true;
        public RatingAndReviewForClient (string trolliId, string userId)
		{
			InitializeComponent ();
            this.trolliId = trolliId;
            this.userId = userId;
		}

        int rating = 0;


        protected override bool OnBackButtonPressed()
        {

            return true;
        }

        public void Rate1Handler(object sender, EventArgs e)
        {
            try
            {
                rate1.IsVisible = false;


                fillRate1.IsVisible = true;

                rate2.IsVisible = true;
                rate3.IsVisible = true;
                rate4.IsVisible = true;
                rate5.IsVisible = true;


                fillRate2.IsVisible = false;
                fillRate3.IsVisible = false;
                fillRate4.IsVisible = false;
                fillRate5.IsVisible = false;

                ratingContain.Text = "Poor";
                rating = 1;
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }


        public void Rate2Handler(object sender, EventArgs e)
        {
            try
            {
                rate2.IsVisible = false;

                fillRate1.IsVisible = true;
                fillRate2.IsVisible = true;

                rate3.IsVisible = true;
                rate4.IsVisible = true;
                rate5.IsVisible = true;


                fillRate3.IsVisible = false;
                fillRate4.IsVisible = false;
                fillRate5.IsVisible = false;

                ratingContain.Text = "Fair";
                rating = 2;
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void Rate3Handler(object sender, EventArgs e)
        {
            try
            {
                rate3.IsVisible = false;

                fillRate1.IsVisible = true;
                fillRate2.IsVisible = true;
                fillRate3.IsVisible = true;

                rate4.IsVisible = true;
                rate5.IsVisible = true;


                fillRate4.IsVisible = false;
                fillRate5.IsVisible = false;

                ratingContain.Text = "Average";
                rating = 3;
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void Rate4Handler(object sender, EventArgs e)
        {
            try
            {
                rate4.IsVisible = false;

                fillRate1.IsVisible = true;
                fillRate2.IsVisible = true;
                fillRate3.IsVisible = true;
                fillRate4.IsVisible = true;

                rate5.IsVisible = true;
                fillRate5.IsVisible = false;

                ratingContain.Text = "Good";
                rating = 4;
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void Rate5Handler(object sender, EventArgs e)
        {

            try
            {
                rate5.IsVisible = false;

                fillRate1.IsVisible = true;
                fillRate2.IsVisible = true;
                fillRate3.IsVisible = true;
                fillRate4.IsVisible = true;
                fillRate5.IsVisible = true;

                ratingContain.Text = "Excellent";
                rating = 5;
            }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");

            }
        }

        public void SaveEvent(object sender, EventArgs e)
        {
            try
            {
                if (check)
                {
                    string review = reviewText.Text;

                    api.ClientRating(trolliId, userId, "" + rating, review);

                    Navigation.PushAsync(new MenuPage());
                    check = false;
                }
                }catch(Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public void MainMenuEvent(object sender, EventArgs e)
        {
            try
            {
                if (check)
                {
                    api.ClientRating(trolliId, userId, "" + rating, "");
                }
                    Navigation.PushAsync(new MenuPage());

            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }

        }

    }
}