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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnLoggingIn_Clicked(object sender, EventArgs e)
        {
            if (this.IsValidUserData(userNameEntry.Text, passwordEntry.Text))
            {
                List<Model.User> DBUserList;
                using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
                {
                    con.CreateTable<Model.User>();
                    DBUserList = con.Table<Model.User>().Where(x => x.Name == userNameEntry.Text && x.Password == passwordEntry.Text).ToList();
                }
                await Navigation.PushAsync(new HomePage(DBUserList[0]));
            }
        }

        // make sure the user information given is right
        private bool IsValidUserData(string Username, string Password)
        {
            List<Model.User> DBUserList;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.User>();
                DBUserList = con.Table<Model.User>().Where(x => x.Name == Username && x.Password == Password).ToList();
            }

            // check if the username passwordcombo was found in the database
            if (DBUserList.Count < 1)
            {
                _ = DisplayAlert("Credentials not matching!", "We vonden deze Gebruikersnaam Wachtwoord combinatie niet!", "Check info");
                return false;
            }

            return true;
        }
    }
}