using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koelkast_App.Model
{
    // drink contains values/rows that can be selected when the user wants to add a drink into the fridge.
    public class Drink
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }
    }
}
