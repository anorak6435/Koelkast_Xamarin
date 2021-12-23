using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koelkast_App.Services
{
    // Handling the crud operations to the Drink Table from the database
    public static class DrinkService
    {
        /// <summary>
        /// Inserts a drink model into the Drink table in the local database.
        /// </summary>
        /// <param name="drink"></param>
        /// <returns>Number of rows inserted into the db</returns>
        public static int Insert(string drinkName, string drinkImagePath)
        {
            Model.Drink drink = new Model.Drink();
            drink.Name = drinkName;
            drink.ImagePath = drinkImagePath;
            int ins_rows = -1;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.Drink>();
                ins_rows = con.Insert(drink);
            }
            return ins_rows;
        }
    }
}
