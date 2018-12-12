﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OneSignalAPI
{
    public class ShoppingCart
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }

    public class Section
    {
        public string Title { get; set; }
        public IEnumerable<ShoppingCart> List { get; set; }
    }

    public class ViewModel
    {
        public IEnumerable<Section> List { get; set; }
    }

    public class Accordion : ContentPage
    {
        public Accordion()
        {
            this.Title = "Accordion";

            var template = new DataTemplate(typeof(DefaultTemplate));

            var view = new AccordionView(template);
            view.SetBinding(AccordionView.ItemsSourceProperty, "List");
            view.Template.SetBinding(AccordionSectionView.TitleProperty, "Title");
            view.Template.SetBinding(AccordionSectionView.ItemsSourceProperty, "List");

            view.BindingContext =
                new ViewModel
                {
                    List = new List<Section> {
                        new Section
                        {
                            Title = "December",
                            List = new List<ShoppingCart> {
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 }
                            }
                        },
                        new Section
                        {
                            Title = "November",
                            List = new List<ShoppingCart> {
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 }
                            }
                        },
                        new Section
                        {
                            Title = "October",
                            List = new List<ShoppingCart> {
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 }
                            }
                        },
                        new Section
                        {
                            Title = "September",
                            List = new List<ShoppingCart> {
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 }
                            }
                        },
                        new Section
                        {
                            Title = "August",
                            List = new List<ShoppingCart> {
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 }
                            }
                        },
                        new Section
                        {
                            Title = "July",
                            List = new List<ShoppingCart> {
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 },
                                new ShoppingCart { Date = DateTime.UtcNow, Amount = 10.05 }
                            }
                        },
                    }
                };
            this.Content = view;
        }
    }
}
