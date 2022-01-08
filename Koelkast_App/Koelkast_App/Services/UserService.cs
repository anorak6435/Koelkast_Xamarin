using Koelkast_App.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koelkast_App.Services
{
    public static class UserService
    {
        public static (Model.User, string) Insert(string userName, string email, string password, bool remembered)
        {
            Model.User usr = new Model.User();
            usr.Name = userName;
            usr.Email = email;
            usr.Password = password;
            usr.Remembered = remembered;

            // default values
            usr.Balance = 0; //nothing taken or added yet. new user starts at 0
            usr.Language = "NL"; // TODO: When handling multiple language requirements make it connected to the app variable like the DatabaseLocation string
            usr.ThemeColor = "ff0033";
            usr.FavoriteDrink = "NoDrink";

            int ins_rows = -1;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.User>();
                ins_rows = con.Insert(usr);
            }

            // check if inserting worked -- expect it to go well else update the message
            string msg = "User succesfully added to database!";

            if (ins_rows <= 0)
            {
                msg = "Fout in database connectie!";
            }


            return (usr, msg);
        }

        // return the user that wanted to be remembered (An automatic login)
        public static (Model.User, string) RememberMe()
        {
            List<Model.User> selection;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.User>();
                selection = con.Table<Model.User>()
                    .Where(usr => usr.Remembered == true).ToList();
            }
            if (selection.Count > 1)
            {
                // alert the user that this should not happen inside the app.
                return (null, "Meerdere gebruikers hebben geselecteerd om onthouden te worden.");
            } else if (selection.Count == 1)
            {
                // send the user a friendly message welcoming him back
                return (selection[0], "Welkom terug!");
            } // else nobody added a remember me so no nothing
            return (null, "");
        }

        // when a different user wants to be remembered other users .Remembered = false
        public static void ForgetEveryone()
        {
            // set all rememberme = true to false
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                var UpdateUserQuery = (from u in con.Table<Model.User>() where u.Remembered == true select u);

                List<Model.User> users = UpdateUserQuery.ToList();

                if (users != null)
                {
                    con.BeginTransaction();
                    foreach (Model.User usr in users)
                    {
                        usr.Remembered = false;
                        con.Update(usr);
                    }
                    con.Commit();
                }

                List<Model.User> tempurs = con.Table<Model.User>().ToList();
                //con.CreateTable<Model.User>();
                //con.Table<Model.User>().Where(u => u.Remembered)
                //    .ToList()
                //    .ForEach(x => x.Remembered = false);
                
            }
        }

        //logging in a user into the application
        public static (Model.User, string) Login(string username, string password)
        {
            List<Model.User> DBUserList;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.User>();
                DBUserList = con.Table<Model.User>().Where(x => x.Name == username && x.Password == password).ToList();
            }
            if (DBUserList.Count == 1)
            {
                return (DBUserList[0], "Welkom");
            }
            else 
            {
                return (null, "Gebruiker niet gevonden!");
            }
        }
    }
}
