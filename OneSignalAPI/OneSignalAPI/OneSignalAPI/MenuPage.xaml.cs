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
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage()
        {
            InitializeComponent();
//            IsPresented = true;
           
            //  MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }


        public void MenuShow(Object sender, EventArgs e)
        {
            IsPresented = true;

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
         //   var item = e.SelectedItem as MenuPageMenuItem;
           
          //  MasterPage.ListView.SelectedItem = null;
        }
    }
}