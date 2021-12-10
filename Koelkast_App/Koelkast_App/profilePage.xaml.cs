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
    public partial class profilePage : ContentPage
    {
        public int DBNumberOfDrinks = 5;
        public string DBFavoriteDrink = "Aflighem";
        public profilePage()
        {
            InitializeComponent();
            // TODO: load the users data from the database
            LoadDatabaseData();

            // wanneer waardes aangepast zijn een melding sturen dat de waardes
            // nog niet in de database zijn opgeslagen.

            UserNameEntry.Text = "Antoine";
            NumberOfDrinksEntry.Text = DBNumberOfDrinks.ToString();
            FavoriteDrinkEnry.Text = DBFavoriteDrink;
            
        }

        public void LoadDatabaseData()
        {

        }
    }
}