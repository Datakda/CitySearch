using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text.Json;

namespace CitySearch
{

    class Program
    {




        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Введите город:");

            string city = Console.ReadLine();

           
            var resultJson = SearchCityNewtonsoft.ReadCitiesLazy("city.list.json");

            var result1 = resultJson.Where(x => x.name == city).FirstOrDefault();

            if (result1 != null)
            {
                Console.WriteLine("Город найден");

            }
            else
            {
                Console.WriteLine("Город не найден");
            }
           
            


            var result = await SearchCity.SearchAsync(city);

            if (result == true)
            {
                Console.WriteLine("Город найден");

            }
            else
            {
                Console.WriteLine("Город не найден");
            }


            result = await SearchCity.BinarySearchAsync(city);

            if (result == true)
            {
                Console.WriteLine("Город найден в бинарной версии");

            }
            else
            {
                Console.WriteLine("Город не найден в бинарной версии");
            }
            Console.WriteLine("Стоп");
        }
    }
}
