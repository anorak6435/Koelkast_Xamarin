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
    public partial class UserBalancePage : ContentPage
    {
        public UserBalancePage()
        {
            InitializeComponent();
        }

        private async void Settings_Btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GeneralSettingsPage());
        }

        private async void Logout_Btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}