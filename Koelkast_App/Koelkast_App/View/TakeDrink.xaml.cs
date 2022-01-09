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
    public partial class TakeDrink : ContentPage
    {
        public TakeDrink()
        {
            InitializeComponent();

            // setup the picker with all the kinds
            // get all the kinds
            List<Model.Drink> drinks = Services.DrinkService.GetAllDrinks();
            // put all te kinds in the picker
            foreach (Model.Drink d in drinks)
            {
                pickKind.Items.Add(d.Name);
            }

        }

        private void OutFridge_Clicked(object sender, EventArgs e)
        {
            // get the kind of drink taken
            string DrinkTaken = (string)pickKind.SelectedItem;
            // get the number of drinks taken
            int numberOfDrinks = (int)Taken.Value;

            // if there were 0 drinks selected return warning message
            if (numberOfDrinks == 0)
            {
                takeDrinkMsg.Text = "Geef aub aan hoeveel je wilt drinken!";
            }

            // if no user is logged in make sure you log in
            if (RootPage.loggedInUser == null)
            {   // return warning that someone has to be logged in to take a drink from the fridge
                takeDrinkMsg.Text = "Login of registreer om een drankje de nemen!";
            }

            // add the number of drinks to the users balance
            (bool Passed, string msgErr) = Services.ConsumptionService.TakeDrink(DrinkTaken, RootPage.loggedInUser, numberOfDrinks);

            if (!Passed)
            {
                takeDrinkMsg.TextColor = Color.Yellow;
                takeDrinkMsg.Text = msgErr;
            } else
            {
                // return to the overview page
                Navigation.PopModalAsync();
            }
        }
    }
}