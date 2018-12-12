using ListViewWithSubListView.ViewModels;
using ListViewWithSubListView.Views;
using OneSignalAPI.BeanClass;
using OneSignalAPI.ValidationClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneSignalAPI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewAddProduct : ContentPage
    {

        
		public NewAddProduct ()
		{
			InitializeComponent ();
            this.ViewModel = new HotelsGroupViewModel();

            // ListOfStore();

        }
        int quantityCounter = 1;
        public void IncrementQuantity(Object sender, EventArgs e) {
            quantityCounter++;
            quantity.Text = "" + quantityCounter;
        }

        public void DecrementQuantity(Object sender, EventArgs e)
        {
            quantityCounter--;
            quantity.Text = "" + quantityCounter;
        }

        bool rightSide = false;
        public void HandlerEvent(Object sender, EventArgs e)
        {
            ListOfStore();
            if (!rightSide)
            {
                rightSlide.Width = 320;
                rightSide = true;
            }
            else
            {
                rightSlide.Width = 40;
                rightSide = false;

            }
           // DisplayAlert("", "Hello World", "Ok");
        }
        private HotelsGroupViewModel ViewModel
        {
            get { return (HotelsGroupViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        private List<Hotels> ListHotel = new List<Hotels>();

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (ViewModel.Items.Count == -1)
                {
                    ViewModel.LoadHotelsCommand.Execute(null);
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.Message);
            }
        }
        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {

                try
                {
                    var product = e.Item as Product;
                    var vm = BindingContext as MainViewModel;
                    vm.ShowOrHidePoducts(product);
                }catch(Exception ex) {
 }
                /*
                b = (ListView)sender;
                StackLayout relative = (StackLayout)b.ParentView;
                StackLayout layout2 = (StackLayout)relative.Children[1];
                ListView list = (ListView)layout2.Children[0];

                list.ItemsSource = new ObservableCollection<ListProduct> {
                     new ListProduct() { items = "Baby products" },
                     new ListProduct() { items = "Bakery" },
                     new ListProduct() { items = "Bathroom" },
                     new ListProduct() { items = "Chocolate" },
                     new ListProduct() { items = "Dairy, Egs, Fridge" },
                     new ListProduct() { items = "Deli" },
                     new ListProduct() { items = "Drinks" },
                     new ListProduct() { items = "Fruits & Veg" },
                     new ListProduct() { items = "Health & Beauty" },
                     new ListProduct() { items = "Kitchen" },
                     new ListProduct() { items = "Laundry" },
                     new ListProduct() { items = "Meat, Seafood" },
                     new ListProduct() { items = "Pantry" },
                     new ListProduct() { items = "Skin Care" },
                    };*/
            }
            catch(Exception ex)
            {
                DisplayAlert("", "" + ex, "Ok");
            }
        }

      
        //   ObservableCollection<ListProduct> listz = null;

        public ObservableCollection<Product> Products { get; set; }
        public async void ListOfStore() //List of Stores
        {
            try
            {

            Products = new ObservableCollection<Product>
            {
                new Product
                {
                    Name = "Surface Book",
                    IsVisible = false,
                    list = new ObservableCollection<ListProduct> {
                     new ListProduct() { items = "Baby products" },
                     new ListProduct() { items = "Bakery" },
                     new ListProduct() { items = "Bathroom" },
                     new ListProduct() { items = "Chocolate" },
                     new ListProduct() { items = "Dairy, Egs, Fridge" },
                     new ListProduct() { items = "Deli" },
                     new ListProduct() { items = "Drinks" },
                     new ListProduct() { items = "Fruits & Veg" },
                     new ListProduct() { items = "Health & Beauty" },
                     new ListProduct() { items = "Kitchen" },
                     new ListProduct() { items = "Laundry" },
                     new ListProduct() { items = "Meat, Seafood" },
                     new ListProduct() { items = "Pantry" },
                     new ListProduct() { items = "Skin Care" },
                    }
                 },
                new Product
                {
                    Name = "Mac Book Pro",
                    IsVisible = false,
                    list = new ObservableCollection<ListProduct> {
                     new ListProduct() { items = "Baby products" },
                     new ListProduct() { items = "Bakery" },
                     new ListProduct() { items = "Bathroom" },
                     new ListProduct() { items = "Chocolate" },
                     new ListProduct() { items = "Dairy, Egs, Fridge" },
                     new ListProduct() { items = "Deli" },
                     new ListProduct() { items = "Drinks" },
                     new ListProduct() { items = "Fruits & Veg" },
                     new ListProduct() { items = "Health & Beauty" },
                     new ListProduct() { items = "Kitchen" },
                     new ListProduct() { items = "Laundry" },
                     new ListProduct() { items = "Meat, Seafood" },
                     new ListProduct() { items = "Pantry" },
                     new ListProduct() { items = "Skin Care" },
                    }
                },
                new Product
                {
                    Name = "Surface Laptop",
                    IsVisible = false,
                    list = new ObservableCollection<ListProduct> {
                     new ListProduct() { items = "Baby products" },
                     new ListProduct() { items = "Bakery" },
                     new ListProduct() { items = "Bathroom" },
                     new ListProduct() { items = "Chocolate" },
                     new ListProduct() { items = "Dairy, Egs, Fridge" },
                     new ListProduct() { items = "Deli" },
                     new ListProduct() { items = "Drinks" },
                     new ListProduct() { items = "Fruits & Veg" },
                     new ListProduct() { items = "Health & Beauty" },
                     new ListProduct() { items = "Kitchen" },
                     new ListProduct() { items = "Laundry" },
                     new ListProduct() { items = "Meat, Seafood" },
                     new ListProduct() { items = "Pantry" },
                     new ListProduct() { items = "Skin Care" },
                    }
                },
                new Product
                {
                    Name = "X1 Carbon",
                    IsVisible = false,
                    list = new ObservableCollection<ListProduct>{
                     new ListProduct() { items = "Baby products" },
                     new ListProduct() { items = "Bakery" },
                     new ListProduct() { items = "Bathroom" },
                     new ListProduct() { items = "Chocolate" },
                     new ListProduct() { items = "Dairy, Egs, Fridge" },
                     new ListProduct() { items = "Deli" },
                     new ListProduct() { items = "Drinks" },
                     new ListProduct() { items = "Fruits & Veg" },
                     new ListProduct() { items = "Health & Beauty" },
                     new ListProduct() { items = "Kitchen" },
                     new ListProduct() { items = "Laundry" },
                     new ListProduct() { items = "Meat, Seafood" },
                     new ListProduct() { items = "Pantry" },
                     new ListProduct() { items = "Skin Care" },
                    }
                },
            };
              //   listView.ItemsSource = null;
              //   listView.ItemsSource = Products;
            }
            catch (Exception ex)
            {
                await DisplayAlert("", ExceptionManagement.LogException(ex), "Ok");
            }
        }
    }
}