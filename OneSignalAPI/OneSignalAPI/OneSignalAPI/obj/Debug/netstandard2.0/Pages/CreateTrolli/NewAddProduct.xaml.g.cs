//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("OneSignalAPI.Pages.CreateTrolli.NewAddProduct.xaml", "Pages/CreateTrolli/NewAddProduct.xaml", typeof(global::OneSignalAPI.NewAddProduct))]

namespace OneSignalAPI {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Pages\\CreateTrolli\\NewAddProduct.xaml")]
    public partial class NewAddProduct : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ColumnDefinition rightSlide;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.AbsoluteLayout hotelStack;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView HotelsList;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry itemName;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label quantity;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::OneSignalAPI.CustomPickerRenderer statesPicker;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry note;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(NewAddProduct));
            rightSlide = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ColumnDefinition>(this, "rightSlide");
            hotelStack = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.AbsoluteLayout>(this, "hotelStack");
            HotelsList = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "HotelsList");
            itemName = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "itemName");
            quantity = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "quantity");
            statesPicker = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::OneSignalAPI.CustomPickerRenderer>(this, "statesPicker");
            note = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "note");
        }
    }
}