using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koelkast_App.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public string Password { get; set; }

        public int Balance { get; set; }

        public string Language { get; set; }

        [MaxLength(6)] //colors will be the strings "ffffff" RRGGBB
        public string ThemeColor { get; set; }

        public string FavoriteDrink { get; set; }

        // is this the user that will be logged in automatically
        public bool Remembered { get; set; }
    }
}
