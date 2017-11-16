using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore
{
    public class Event
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public string CardImageUrl { get; set; }
    }
    public class Profile
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public int Vacation { get; set; }
        public string SickLeave { get; set; }
        public string Url { get; set; }
        public double Ensurance { get; set; }
        public string CardImageUrl { get; set; }
    }
}
