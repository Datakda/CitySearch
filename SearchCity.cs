using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CitySearch
{
    public static class SearchCity
    {




        public static async Task<bool> SearchAsync(string city)
        {

            using (FileStream fs = new FileStream("city.list.json", FileMode.Open))
            {
                var citys = await JsonSerializer.DeserializeAsync<List<City>>(fs);

                var result = citys.Where(x => x.name == city).FirstOrDefault();

                if (result != null)
                {
                    return true;
                }
                else
                { 
                    return false;

                }
            }



        }



        public static async Task<bool> BinarySearchAsync(string city)
        {
            FileInfo fileInf = new FileInfo("version.txt");

            if (!fileInf.Exists)
            {
                using (StreamWriter version = new StreamWriter("version.txt", false, System.Text.Encoding.Default))
                {

                    version.WriteLine(GetMD5());

                }

                await CreateBinaryVersionAsync();

            }

            else
            {
                using (StreamReader ver = new StreamReader("version.txt"))
                {





                    if (ver.ReadLine() != GetMD5())
                    {
                        await CreateBinaryVersionAsync();
                    }



                }



            }

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("city.dat", FileMode.Open))
            {
                var searchCity = (List<City>)formatter.Deserialize(fs);


                var result = searchCity.Where(x => x.name == city).FirstOrDefault();

                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;

                }

            }


        }



        private static string GetMD5()
        {

            string hash;

            using (var md5 = MD5.Create())
            {
                using (var city = File.OpenRead("city.list.json"))
                {


                    hash = BitConverter.ToString(md5.ComputeHash(city)).Replace("-", "").ToLowerInvariant();
                }
            }

            return hash;




        }

        private static async Task CreateBinaryVersionAsync()
        {
            using (FileStream city = new FileStream("city.list.json", FileMode.Open))
            {
                var citys = await JsonSerializer.DeserializeAsync<List<City>>(city);

                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs1 = new FileStream("city.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs1, citys);


                }

            }


        }

    }
}
