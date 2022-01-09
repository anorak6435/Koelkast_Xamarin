using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koelkast_App.Services
{
    public static class ConsumptionService
    {
        // User wants to take a drink from the fridge
        public static (bool, string) TakeDrink(string takenDrinkName, Model.User usr_taking, int amount_taken)
        {
            // Check if there is still a drink with that name inside the fridge
            Model.Drink FridgeDrink = Services.DrinkService.DrinkByName(takenDrinkName);

            // if there are not enough drinks inside the fridge 
            if (FridgeDrink.InStock < amount_taken)
            {
                return (false, "Niet genoeg drankjes in de koelkast!");
            }
            // Insert consumption into the database
            Model.Consumption consumed = Insert(FridgeDrink.ID, usr_taking.Id, DateTime.Now, amount_taken);
            if (consumed == null)
            {
                return (false, "Fout bij de database toevoegen consumptie!");
            }
            // Remove N drinks from the Drink Table
            return Services.DrinkService.UpdateDrink(FridgeDrink, consumed);
        }

        // insert a consumption into the Table
        public static Model.Consumption Insert(int BeerID, int UserID, DateTime TakenOn, int Amount)
        {
            Model.Consumption consume = new Model.Consumption();
            consume.BeerId = BeerID;
            consume.UserId = UserID;
            consume.TakenOn = TakenOn;
            consume.Amount = Amount;

            int ins_rows = -1;
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<Model.Consumption>();
                ins_rows = con.Insert(consume);
            }

            if (ins_rows == 1)
            {
                return consume;
            } else
            {
                return null;
            }
        }
    }
}
