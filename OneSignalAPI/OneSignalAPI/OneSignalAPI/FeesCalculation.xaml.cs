using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using RealTrolli;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FeesCalculation : ContentPage
	{


        ObservableCollection<ListStores> list = new ObservableCollection<ListStores>();
        double totalFees = 0;
        public TrollyCreation ob = new TrollyCreation();
        public FeesCalculation (TrollyCreation trolly)
		{
			InitializeComponent ();
           
          //  Calculations();
            ob = trolly;
            additionalAmount.Text = "Your Grocery Amount is          $100\n\nBase Fees is                                $15\n\nTotal Amount you pay              $115";
        }

        public void Calculations()
        {
            try
            {

                ObservableCollection<ListStores> listStore = SharedUserData.listStore;
                bool baseStores = true;
                
                foreach (ListStores list in listStore)
                {
                    if (baseStores)
                    {
                      //  baseStore.Text = "• " + list.store;
                      //  baseAmount.Text = "$ " + TrolliFeeCalculation.baseFees + "/-";
                        baseStores = false;
                        totalFees += TrolliFeeCalculation.baseFees;
                    }
                    else
                    {
                        additonalStore.Text += "• " + list.store + "\n";
                        additionalAmount.Text += "$ " + TrolliFeeCalculation.addtionalFees + "/- \n";
                        totalFees += TrolliFeeCalculation.addtionalFees;
                    }
                }

           //     serviceFees.Text = "$ " + totalFees + "/-";
            }catch(Exception ex)
            {

                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
        }

        public void saveTrollyFees() {

            try
            {
                TrollyFinancialInfo feeInfo = new TrollyFinancialInfo()
                {
                    isConfirmed = "false",
                    baseGroceryAmount = 0,
                    serviceProviderFee = totalFees,
                    trolliFeeAmount = 0,
                    totalAmount = totalFees,
                    trolliId = ScheduledTrolly.UUIDs
                };
                ApiCalling api = new ApiCalling();
                api.SaveTrolliFee(feeInfo);
            }catch(Exception ex)
            {

                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }
        public void BackEvent(Object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        public void NextButton(object sender, EventArgs e)
        {
            try
            {
                SharedUserData.listStore = new ObservableCollection<ListStores>();
                SharedUserData.addProduct = new ObservableCollection<ListProduct>();

                ListStores listData = new ListStores()
                {
                    store = "Any Store",
                    storeSuburb = "(No Suburb)",
                    storeDetail = "Any Store (0 Items)"
                };

                list.Add(listData);
                SharedUserData.listStore = list;

                saveTrollyFees();
                Navigation.PushAsync(new PaymentCardInfo(ob));
            }
            catch(Exception ex)
            {

                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
            }
        }
    }
}