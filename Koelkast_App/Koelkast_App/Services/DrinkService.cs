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
        /// <param name="drinkName"></param>
        /// <param name="drinkImagePath"></param>
        /// <param name="cost"></param>
        /// <param name="stock"></param>
        /// <returns>Number of rows inserted into the db</returns>
        public static int Insert(string drinkName, string drinkImagePath, int cost, int stock)
        {
            Model.Drink drink = new Model.Drink();
            drink.Name = drinkName;
            drink.ImagePath = drinkImagePath;
            drink.Cost = cost;
            drink.InStock = stock;

            int ins_rows = -1;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.Drink>();
                ins_rows = con.Insert(drink);
            }

            return ins_rows;
        }

        // update the amount of drinks of kind X inside the database / fridge
        public static (bool, string) UpdateDrink(Model.Drink drink, Model.Consumption consumed)
        {
            // remove the consumed amount of the Instock amount on the drink
            drink.InStock = drink.InStock - consumed.Amount;

            int updates = -1;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.Drink>();
                con.BeginTransaction();
                updates = con.Update(drink);
                con.Commit();
            }

            if (updates == 1)
            {
                return (true, "");
            } else
            {
                return (false, "Database fout bij Updaten aantal drankjes!");
            }
        }


        // Make sure the user can find his drink by searching for the name
        public static Model.Drink DrinkByName(string drinkName)
        {
            List<Model.Drink> drinks;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.Drink>();
                drinks = con.Table<Model.Drink>().Where(x => x.Name == drinkName).ToList();
            }

            if (drinks.Count == 1)
            {
                return drinks[0];
            } else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns a list of all the drinks stored in the database.
        /// </summary>
        /// <returns></returns>
        public static List<Model.Drink> GetAllDrinks()
        {
            List<Model.Drink> drinks;

            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.Drink>();
                drinks = con.Table<Model.Drink>().ToList();
            }
            return drinks;
        }

        public static void DeleteDrink(int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.Drink>();
                con.Table<Model.Drink>().Where(d => d.ID == id).Delete();
            }

        }

        /// <summary>
        /// How many drinks are there inside the fridge
        /// </summary>
        /// <returns></returns>
        public static int BeersInFridge()
        {
            int drinks = 0;
            List<Model.Drink> DblistDrink;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.Drink>();
                DblistDrink = con.Table<Model.Drink>().ToList();
                // sum the amount in stock for every element inside the database.
                foreach (Model.Drink drinkItem in DblistDrink)
                {
                    drinks += drinkItem.InStock;
                }
            }
            return drinks;
        }
    }
}
