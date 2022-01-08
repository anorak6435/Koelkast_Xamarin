using SQLite;
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
    public partial class LoginPage : ContentPage
    {
        TabbedPage tabParent;
        public LoginPage(TabbedPage parent)
        {
            InitializeComponent();
            tabParent = parent;
        }

        //private async void BtnLoggingIn_Clicked(object sender, EventArgs e)
        //{
        //    string Username = userNameEntry.Text;
        //    string Password = passwordEntry.Text;
        //    if (this.IsValidUserData(Username, Password))
        //    {
        //        List<Model.User> DBUserList;
        //        using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
        //        {
        //            con.CreateTable<Model.User>();
        //            DBUserList = con.Table<Model.User>().Where(x => x.Name == Username && x.Password == Password).ToList();
        //        }
        //        await Navigation.PushAsync(new HomePage(DBUserList[0]));
        //    }
        //}

        // a message to inform user about valid user data.
        private void WarningMessage(string msg)
        {
            lblLoginMsg.TextColor = Color.Yellow;
            lblLoginMsg.Text = msg;
        }

        private bool IsValidUserData(string Username, string Password)
        {
            Model.User searchUser = new Model.User();
            searchUser.Name = Username;
            searchUser.Password = Password;
            List<Model.User> DBUserList;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.User>();
                DBUserList = con.Table<Model.User>()
                                .Where(tableUser => tableUser.Name == Username && tableUser.Password == Password)
                                .ToList();
            }

            // Display an alert if the username password were found in less than 1 user in the database
            if (DBUserList.Count < 1)
            {
                _ = DisplayAlert("Credentials not matching!", "We vonden deze Gebruikersnaam Wachtwoord combinatie niet!", "Check info");
                return false;
            } else
            {
                return true;
            }
        }

        private bool IsValidFormData()
        {
            if (string.IsNullOrEmpty(userNameEntry.Text))
            {
                lblLoginMsg.Text = "Vul je gebruikersNaam in AUB!";
                return false;
            }
            else if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                lblLoginMsg.Text = "Vul je wachtwoord in AUB!";
                return false;
            }
            return true;
        }
        
        async private void btnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage(), true);
        }

        private void BtnLoggingIn_Clicked(object sender, EventArgs e)
        {
            if (IsValidFormData())
            {
                // log the user in
                (Model.User usr, string msg) = Services.UserService.Login(userNameEntry.Text, passwordEntry.Text);
                if (usr == null)
                {
                    WarningMessage(msg);
                } else
                {
                    if (rememberMe.IsChecked)
                    {
                        // toggle the current users .Remembered to true
                        Services.UserService.RememberMe(usr);
                    }
                    // setup the app to update the main page.
                    RootPage.loggedInUser = usr;
                    RootPage.msg = msg;
                    Navigation.InsertPageBefore(new HomePage(), this.tabParent);
                    Navigation.PopAsync();
                }
            }
        }
    }
}