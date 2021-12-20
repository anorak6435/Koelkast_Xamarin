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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegistreerBtn_Clicked(object sender, EventArgs e)
        {
            //check that the data entered in the register form is correct.
            //if (this.CheckNewUserData())
            //{
            //    // make a new user from the input in the forms
            //    Model.User usr = this.BuildUser();
            //    Navigation.PushAsync(new HomePage(usr));
            //}
        }

        private Model.User BuildUser()
        {
            // the new user is allowed by the rules we have on the register form

            Model.User usr = new Model.User();
            usr.Language = "NL"; // TODO: When handling multiple language requirements make it connected to the app variable like the DatabaseLocation string
            usr.Balance = 0; // the balance of new users starts at 0
            usr.Name = RegisterUserName.Text;
            usr.Email = RegisterEmail.Text;
            usr.Password = RegisterPassword.Text;
            usr.ThemeColor = "#00ff00"; // TODO: Just like with the language property take the general setting the user has selected.


            // insert the user object into the database
            int ret_rows = -1;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.User>();
                ret_rows = con.Insert(usr);
            }

            if (ret_rows <= 0)
            {
                _ = DisplayAlert("Error!", "Er is een fout opgetreden bij het toevoegen van de nieuwe gebruiker. Controleer uw informatie", "Okay");
            }
            return usr;
        }

        private bool CheckNewUserData()
        {
            // Check that both passwords are the same
            string first_pass = RegisterPassword.Text;
            string second_pass = RegisterPasswordRepeat.Text;
            if (first_pass != second_pass)
            {
                _ = DisplayAlert("Wachtwoord", "Het wachtwoord en het herhaalde wachtwoord verschillen. Typ wachtwoorden opnieuw!", "Okay");
                return false;
            }

            //checking if the email exists in the list
            int ret_count = -1;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.User>();
                ret_count = con.Table<Model.User>().Where(x => x.Email == RegisterEmail.Text).ToList().Count();
            }
            if (ret_count > 0)
            {
                _ = DisplayAlert("Email", "Dit email adres is gebruikt voor een ander account!", "Verander email");
                return false;
            }

            return true;
        }

        private void DeveloperLogin_Clicked(object sender, EventArgs e)
        {
            Model.User usr = new Model.User();
            usr.Language = "NL"; // TODO: When handling multiple language requirements make it connected to the app variable like the DatabaseLocation string
            usr.Balance = 0; // the balance of new users starts at 0
            usr.Name = "Stefan";
            usr.Email = "stefan.beckers14@gmail.com";
            usr.Password = "passworddata";
            usr.ThemeColor = "#00ff00"; // TODO: Just like with the language property take the general setting the user has selected.

            // Navigation.PushAsync(new HomePage(usr));
        }
    }
}