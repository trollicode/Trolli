using ListViewWithSubListView.ViewModels;
using ListViewWithSubListView.Views;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using Syncfusion.SfAutoComplete.XForms;
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
    public partial class NewCreateTrolli : ContentPage
    {


        ObservableCollection<ListStores> lists = new ObservableCollection<ListStores>();
        int index = -1;

        public NewCreateTrolli()
        {
            InitializeComponent();
            getStroies();
            ListOfStore();
           // autoComplete.DataSource = data;
        }
        Button b = null;
        public void EditHandler(Object sender, SelectedItemChangedEventArgs e)
        {
            try
            {


                b = (Button)sender; //Change var into Button

                AbsoluteLayout relative = (AbsoluteLayout)b.ParentView; //Change var into AbsoluteLayout
               // SfAutoComplete viewEntry1 = (SfAutoComplete)relative.Children[4]; //Change var into SfAutoComplete
                              // viewEntry1.IsVisible = true;
                //  Entry viewEntry2 = (Entry)relative.Children[3]; //Change var into Entry
                //  viewEntry2.IsVisible = true;
                //  Button updateStoreBtn = (Button)relative.Children[4]; //Change var into Button
                //  updateStoreBtn.IsVisible = true;

                Entry entry1 = (Entry)relative.Children[3];
                entry1.IsVisible = true;

                Label labelStore1 = (Label)relative.Children[0]; //Change var into Label

                Label labelStore2 = (Label)relative.Children[1]; ////Change var into Label
                                                                 //      labelStore2.IsVisible = false;

                Label labelStore3 = (Label)relative.Children[2];
                labelStore1.IsVisible = false;
                labelStore2.IsVisible = false;
                labelStore3.IsVisible = false;
                //DisplayAlert("", "" + labelStore1.Text, "Ok");
                //DisplayAlert("", "" + labelStore2.Text, "Ok");
                //  updateStore = labelStore1.Text;
                //   updateStoreSuburb = labelStore2.Text;

                //  autoComplete.Text = labelStore1.Text;
                //   autoComplete.Focus();
                //   updateStoreName = labelStore1.Text;
                //  storesSuburb.Text = labelStore2.Text;
                //    check = 1;
                //    addStore.Text = "Update Store";

                //    mainGrid.Height = 1;

                //    listSection.TranslateTo(0, 0, 100);

                //    listSection1.TranslateTo(0, 0, 100);
                //    isListOpen = true;
                // viewEntry1.DataSource = data;
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
            //listStore.IsVisible = true;
            // listStoreSuburb.IsVisible = true;
        }

        public void ItemTappedEvent(Object sender, ItemTappedEventArgs e)
        {
            ListStores list = e.Item as ListStores;
            Navigation.PushModalAsync(new Hotels(new HotelsGroupViewModel(), list));
           // DisplayAlert("", "" + list.store, "Ok");
        }

       //Button b = null;
        public void SaveHandler(Object sender, SelectedItemChangedEventArgs e)
        {

            b = (Button)sender;
            AbsoluteLayout relative = (AbsoluteLayout) b.ParentView;
            Entry entry1 = (Entry)relative.Children[2];
            ListStores listData = new ListStores()
            {
                store = entry1.Text,
                storeSuburb = "(No Suburb)",
                storeDetail = "(0 Items)"
            };
            int findLastIndex = lists.Count - 1;
            lists.RemoveAt(findLastIndex);
            lists.Insert(findLastIndex, listData);
            entry1.IsVisible = false;

            listView.ItemsSource = null;
            listView.ItemsSource = lists;

            Label labelStore1 = (Label)relative.Children[0];
            Label labelStore2 = (Label)relative.Children[1];

            labelStore1.IsVisible = true;
            labelStore2.IsVisible = true;
           Button updateStoreBtn = (Button)relative.Children[5];
            updateStoreBtn.IsVisible = false;
        }

        ObservableCollection<string> data = new ObservableCollection<string>();
        public async void ListOfStore() //List of Stores
        {
            try
            {
                data.Add("Australia Post");
                data.Add("Big W");
                data.Add("Chemist Warehouse");
                data.Add("My Chemist");
                data.Add("Amcal Pharmacy");
                data.Add("Coles");
                data.Add("ALDI");
                data.Add("Woolworths");
                data.Add("IGA");
                data.Add("The Reject Shop");
                data.Add("Priceline");
                data.Add("Kmart");
                data.Add("Bunnings Warehouse");

                //autoComplete.DataSource = data;
            }

            catch (Exception ex)
            {
                await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }

        public bool CheckField()
        {

            try
            {
                if (trolliTitle.Text == "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
                //Exception Class
                return false;
            }
        }

        public void CheckOutEvent(Object sender, EventArgs e)
        {
            if (CheckField())
            {
                string trolliTitleVar = trolliTitle.Text;
                Navigation.PushAsync(new ScheduledTrolly(trolliTitleVar), true);
            }
            else
            {
                DisplayAlert("", "Please Enter Trolli Title", "OK");
            }
        }

        public void UnFocusedEvent(Object sender, FocusEventArgs e)
        {

          
           /*  b = (Button)sender;
             AbsoluteLayout relative = (AbsoluteLayout)b.ParentView;
             Entry entry1 = (Entry)relative.Children[2];
             ListStores listData = new ListStores()
             {
                 store = entry1.Text,
                 storeSuburb = "(No Suburb)",
                 storeDetail = "(0 Items)"
             };
             int findLastIndex = lists.Count - 1;
             lists.RemoveAt(findLastIndex);
             lists.Insert(findLastIndex, listData);
             entry1.IsVisible = false;

             listView.ItemsSource = null;
             listView.ItemsSource = lists;

             Label labelStore1 = (Label)relative.Children[0];
             Label labelStore2 = (Label)relative.Children[1];

             labelStore1.IsVisible = true;
             labelStore2.IsVisible = true;*/
        }

        public void AddStore(Object sender, EventArgs e)
        {
            ListStores listData = new ListStores()
            {
                store = "Any Store",
                storeSuburb = "(No Suburb)",
                storeDetail = "(0 Items)"
            };
            lists.Add(listData);

            listView.ItemsSource = null;
            listView.ItemsSource = lists;
        }

        public void getStroies()
        {
            try
            {
                //  listView.ItemsSource = null;
                //  listView.ItemsSource = SharedUserData.listStore;
                //   scheduleButtonEnable();
                if (listView.ItemsSource == null)
                {
                    // listValidationText.Text = "There is no Selected Stroes";
                 /*   ListStores listData = new ListStores()
                    {
                        store = "Any Store",
                        storeSuburb = "(No Suburb)",
                        storeDetail = "(0 Items)"
                    };

                    lists.Add(listData);

                    //  SharedUserData.listStore = lists;
                    listView.ItemsSource = null;
                    listView.ItemsSource = lists; //SharedUserData.listStore;
                    */
                }
                else
                {
                    //  listValidationText.Text = "";

                    //   listView.ItemsSource = null;
                    //    listView.ItemsSource = SharedUserData.listStore;
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}