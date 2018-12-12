using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using RealTrolli;
using Stripe;
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
	public partial class EnterGroceryAmount : ContentPage
	{
        TrollyCreation obj = null;
        int itemCount = 0;

       // ApiCalling api = new ApiCalling();
		public EnterGroceryAmount (TrollyCreation ob, int count)
		{
			InitializeComponent ();
            obj = ob;
            itemCount = count;
            LoadData();
		}

        public void LoadData() {
            trolliTitle.Text = obj.trollyTitle;
            assigneeId.Text = obj.trolliId;
            totalItem.Text = ""+itemCount;
        }

        public void BackEvent(Object sender, EventArgs e) {
            Navigation.PopAsync();
        }

      

        public async void UpdateFinancialAmount(object sender, EventArgs e){

            try
            {
                if (groceryAmount.Text != "")
                {
                    bool check = await DisplayAlert("Job Compilation", "Are you sure to complete this Job, Once you press 'Yes' then record will be updated into our datadase ", "Yes", "No");

                    if (check)
                    {
                        ApiCalling api = new ApiCalling();
                         Dictionary<string, object> userData = SharedUserData.getUserData; 
                        
                        TrollyFinancialInfo ob = new TrollyFinancialInfo()
                        {
                            trolliId = obj.trolliId,
                            baseGroceryAmount = Convert.ToDouble(groceryAmount.Text)
                        };
                        api.UpdateTrolliFee(ob);
                        api.SendNotificationWhenSBEnterAmount(Convert.ToString(userData["fullName"]), obj.clientNotificationId, obj.trolliId, obj.assigneeId);

                        await Navigation.PushAsync(new RatingAndReviewForClient(ob.trolliId, obj.clientId));
                    }
                }
                else
                {
                   await DisplayAlert("", "Enter Grocery Amount", "ok");
                }
            }catch(Exception ex)
            {
               await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }

        }

    }
}