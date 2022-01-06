using Koelkast_App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koelkast_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinkInventoryListView : ContentPage
    {
        public List<Model.Drink> Items { get; set; }

        public DrinkInventoryListView()
        {
            InitializeComponent();

            Items = DrinkService.GetAllDrinks();

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // if there is no item given return at once
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", string.Format("{0} was tapped", ((Model.Drink)e.Item).Name), "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
