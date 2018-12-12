using ListViewWithSubListView.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewWithSubListView.ViewModels
{
    public class HotelsGroupViewModel : BaseViewModel
    {
        private HotelViewModel _oldHotel;

        private ObservableCollection<HotelViewModel> items;
        public ObservableCollection<HotelViewModel> Items
        {
            get => items;

            set => SetProperty(ref items, value);
        }
      
        public Command LoadHotelsCommand { get; set; }
        public Command<HotelViewModel> RefreshItemsCommand { get; set; }

        public HotelsGroupViewModel()
        {
            items = new ObservableCollection<HotelViewModel>();
            Items = new ObservableCollection<HotelViewModel>();
            LoadHotelsCommand = new Command(async () => await ExecuteLoadItemsCommandAsync());
            RefreshItemsCommand = new Command<HotelViewModel>((item) => ExecuteRefreshItemsCommand(item));
        }
      
        public bool isExpanded = false;
        private void ExecuteRefreshItemsCommand(HotelViewModel item)
        {
            if (_oldHotel == item)
            {
                // click twice on the same item will hide it
                item.Expanded = !item.Expanded;
            }
            else
            {
                if (_oldHotel != null)
                {
                    // hide previous selected item
                    _oldHotel.Expanded = false;
                }
                // show selected item
                item.Expanded = true;
            }

            _oldHotel = item;
        }
        async System.Threading.Tasks.Task ExecuteLoadItemsCommandAsync()
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                Items.Clear();
                List<Room> Hotel1rooms = new List<Room>() { new Room("Humidifier", 1), new Room("Swaddle Blanket", 1), new Room("Moby Wrap", 1), new Room("Pacifier", 1), new Room("Co-Sleeper", 1)
                };
                List<Room> Hotel2rooms = new List<Room>()
                {
                    new Room("Plain Bread", 1), new Room("Milky Bread", 1), new Room("Bran Bread", 1), new Room("Sandwich Bread", 1), new Room("Bread Rolls", 1), new Room("Bread Loaf", 1)
                };
                List<Room> Hotel3rooms = new List<Room>()
                {
                    new Room("Soap", 1), new Room("Soap Dishes", 1), new Room("Soap Dispenser", 1),new Room("Towel", 1),new Room("Towel Holder", 1),new Room("Toothbrush", 1),new Room("Toothbrush holder", 1)
                };
                List<Hotel> items = new List<Hotel>() { new Hotel("Baby products", Hotel1rooms), new Hotel("Bakery", Hotel2rooms), new Hotel("Bathroom", Hotel3rooms) };

                if (items != null && items.Count > 0)
                {
                    foreach (var hotel in items)
                        Items.Add(new HotelViewModel(hotel));
                }
                else { IsEmpty = true; }

            }
            catch (Exception ex)
            {
                IsBusy = false;
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
