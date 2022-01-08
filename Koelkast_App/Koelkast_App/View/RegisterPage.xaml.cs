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

        // a message to inform user about valid user data.
        private void WarningMessage(string msg)
        {
            lblRegisterMsg.TextColor = Color.Yellow;
            lblRegisterMsg.Text = msg;
        }

        private void RegistreerBtn_Clicked(object sender, EventArgs e)
        {
            //check that the data entered in the register form is correct.
            if (this.CheckNewUserData())
            {
                // make a new user from the input in the forms
                // register this user inside the database
                Model.User usr = this.BuildUser();
                // log de nieuwe gebruiker in

                (Model.User temp_usr, string msg) = Services.UserService.Login(usr.Name, usr.Password);
                // if null. The user did something wrong
                if (temp_usr == null)
                {
                    // give the message to the user
                    WarningMessage(msg);
                } else
                {   // everything seems to have gone well.
                    // return the Navigation.Pop
                    RootPage.loggedInUser = temp_usr;
                    Navigation.PopModalAsync();
                }
                // changes the pages title to reflect I am logged in
                // this.Parent.Parent TabbedHomePage
                // var tab = this.Parent.Parent as TabbedPage;
                // tab.Children[2].Title = "ingelogd";

            }
        }

        private Model.User BuildUser()
        {
            // the new user is allowed by the input rules on the register form

            // before inserting the user into the user table check if the user wants to be remembered
            if (rememberMe.IsChecked)
            {
                // if so remove all remember references in the User Table
                Services.UserService.ForgetEveryone();
            }
            // if not, just continue with inserting the new user into the database

            (Model.User usr, string errMsg) = Services.UserService.Insert(RegisterUserName.Text, RegisterEmail.Text, RegisterPassword.Text, rememberMe.IsChecked);
            

            return usr;
        }

        private bool CheckNewUserData()
        {
            // Check all fields of the form are filled
            if (string.IsNullOrEmpty(RegisterEmail.Text))
            {
                WarningMessage("Vul een email adres in!");
                return false;
            } else if (string.IsNullOrEmpty(RegisterUserName.Text))
            {
                WarningMessage("Vul een gebruikersnaam in!");
                return false;
            } else if (string.IsNullOrEmpty(RegisterPassword.Text) || string.IsNullOrEmpty(RegisterPasswordRepeat.Text))
            {
                WarningMessage("Vul je gekozen wachtwoord 2 keer in!");
                return false;
            }
            // Check that both passwords are the same
            string first_pass = RegisterPassword.Text;
            string second_pass = RegisterPasswordRepeat.Text;
            if (first_pass != second_pass)
            {
                WarningMessage("Het wachtwoord en het herhaalde wachtwoord verschillen!");
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
                lblRegisterMsg.Text = "Dit email adres is gebruikt voor een ander account!";
                return false;
            }

            // If none of the checks failed new user data is valid
            return true;
        }
    }
}