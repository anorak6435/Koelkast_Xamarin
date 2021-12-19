using SQLite;
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
    public partial class AddDrinkPage : ContentPage
    {
        public AddDrinkPage()
        {
            InitializeComponent();
            // adding items to the picker
            // TODO: Take the different types of beer from the database.
            BeerPicker.Items.Add("Selecteer Item");
            BeerPicker.Items.Add("Aflighem");
            BeerPicker.Items.Add("Brugse Zot");
            BeerPicker.SelectedIndex = 0;
        }

        private void TakeDrink_Clicked(object sender, EventArgs e)
        {
            // Take drink from fridge
            //Make a consumption model from the form on the page.


            int ret_rows = -1;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.Consumption>();
                //ret_rows = con.Insert(usr);

            }
            if (ret_rows <= 0)
            {
                _ = DisplayAlert("Error!", "Er is een fout opgetreden bij het toevoegen van het drankje aan mijn consumtie lijst.", "Okay");
            }
            Navigation.PopAsync();
        }
    }
}