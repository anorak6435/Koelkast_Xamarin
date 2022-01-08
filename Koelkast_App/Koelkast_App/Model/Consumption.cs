using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koelkast_App.Model
{
    // model to track how many beers are taken by person X
    public class Consumption
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int BeerId { get; set; }

        public int UserId { get; set; }

        public DateTime TakenOn { get; set; } // To Track when the drink was consumed

        public int Amount { get; set; }
    }
}
