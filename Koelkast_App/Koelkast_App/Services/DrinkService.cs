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
