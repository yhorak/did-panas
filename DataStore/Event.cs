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
}
