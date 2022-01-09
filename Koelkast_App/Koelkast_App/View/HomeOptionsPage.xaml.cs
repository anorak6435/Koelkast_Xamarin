using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koelkast_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeOptionsPage : ContentPage
    {
        public HomeOptionsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            lblMessage.Text = RootPage.msg;
        }

        private async void AddKindOfDrink_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddKindOfDrinkPage());
        }
        private void BtnInventaris_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DrinkInventoryListView());
        }

        private void TakeDrink_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new TakeDrink(), true);
        }
    }
}