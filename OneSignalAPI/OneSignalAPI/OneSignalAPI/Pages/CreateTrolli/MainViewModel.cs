using OneSignalAPI.BeanClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace OneSignalAPI
{
    public class MainViewModel
    {
        private Product _oldProduct;

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<ListProduct> list { get; set; }

        public MainViewModel()
        {
            list = new ObservableCollection<ListProduct>
            {
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
            };

            Products = new ObservableCollection<Product>
            {
                new Product {Name = "Baby products",IsVisible = false},
                new Product {Name = "Bakery",IsVisible = false},

                new Product
                {
                    Name = "Health & Beauty",
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
        }

        public void ShowOrHidePoducts(Product product)
        {
            if (_oldProduct == product)
            {
                // click twice on the same item will hide it
                product.IsVisible = !product.IsVisible;
                UpdateProducts(product);
            }
            else
            {
                if (_oldProduct != null)
                {
                    // hide previous selected item
                    _oldProduct.IsVisible = false;
                    UpdateProducts(_oldProduct);
                }
                // show selected item
                product.IsVisible = true;
                UpdateProducts(product);
            }

            _oldProduct = product;
        }

        private void UpdateProducts(Product product)
        {
            var index = Products.IndexOf(product);
            Products.Remove(product);
            Products.Insert(index, product);
        }
    }
}
