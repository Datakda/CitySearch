using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearch
{
    [Serializable]
    public class City
    {
       
        public double id { get; set; }

        
        public string name { get; set; }

       
        public string state { get; set; }

       
        public string country { get; set; }

       
        public Coord coord { get; set; }
    }
}
