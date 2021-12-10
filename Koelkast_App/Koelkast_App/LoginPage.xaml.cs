using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koelkast_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnLoggingIn_Clicked(object sender, EventArgs e)
        {
            // TODO: check the user entered valid credentials
            string username = userNameEntry.Text;
            string password = passwordEntry.Text;
            if (username == "prins" && password == "pils")
            {
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                DisplayAlert("Login clicked", "Don't you dare take my password!", "try again", "Cancel");
            }
        }
    }
}