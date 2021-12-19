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
    public partial class HomeFunctionsOverview : ContentPage
    {
        public HomeFunctionsOverview()
        {
            InitializeComponent();
        }

        private async void ChooseDrink_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDrinkPage());
        }

        private async void Personalinfo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new profilePage());
        }

        private async void Stock_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StockOverviewPage());
        }
    }
}