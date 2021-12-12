using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koelkast_App.Model
{
    public class Beer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public int Cost { get; set; }
    }
}
