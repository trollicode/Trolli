//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("OneSignalAPI.Pages.Profile.ProfileUpdate.xaml", "Pages/Profile/ProfileUpdate.xaml", typeof(global::RealTrolli.ProfileUpdate))]

namespace RealTrolli {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Pages\\Profile\\ProfileUpdate.xaml")]
    public partial class ProfileUpdate : global::Xamarin.Forms.MasterDetailPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ActivityIndicator loaderAI;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::ImageCircle.Forms.Plugin.Abstractions.CircleImage profileImage;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::OneSignalAPI.MyEntry txtName;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::OneSignalAPI.MyEntry txtEmail;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::OneSignalAPI.MyEntry txtSuburb;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::OneSignalAPI.CustomPickerRenderer txtStates;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::OneSignalAPI.MyEntry txtPostCode;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::OneSignalAPI.CustomButtonText btnSignUp;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(ProfileUpdate));
            loaderAI = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ActivityIndicator>(this, "loaderAI");
            profileImage = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::ImageCircle.Forms.Plugin.Abstractions.CircleImage>(this, "profileImage");
            txtName = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::OneSignalAPI.MyEntry>(this, "txtName");
            txtEmail = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::OneSignalAPI.MyEntry>(this, "txtEmail");
            txtSuburb = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::OneSignalAPI.MyEntry>(this, "txtSuburb");
            txtStates = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::OneSignalAPI.CustomPickerRenderer>(this, "txtStates");
            txtPostCode = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::OneSignalAPI.MyEntry>(this, "txtPostCode");
            btnSignUp = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::OneSignalAPI.CustomButtonText>(this, "btnSignUp");
        }
    }
}