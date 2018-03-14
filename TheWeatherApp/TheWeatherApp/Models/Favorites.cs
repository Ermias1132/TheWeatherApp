using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TheWeatherApp.Models
{
    public class Favorites
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ZipCode { get; set; }
    }
}
