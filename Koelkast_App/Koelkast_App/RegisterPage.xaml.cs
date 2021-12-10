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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegistreerBtn_Clicked(object sender, EventArgs e)
        {
            // Check that both passwords are the same
            string first_pass = RegisterPassword.Text;
            string second_pass = RegisterPasswordRepeat.Text;
            if (first_pass == second_pass)
            {
                // both passwords are equal.

            }
        }

        private void DeveloperLogin_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
    }
}