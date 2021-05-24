using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySearch
{
    public static class SearchCityNewtonsoft
    {

        public static IEnumerable<City> ReadCitiesLazy(string path)
        {
            var serializer = new JsonSerializer();
            using var tr = File.OpenText(path); // открываем файл
            using var jr = new JsonTextReader(tr); // натравливаем на него токенизатор
            jr.Read(); // жёстко проверяем формат: убеждаемся, что у нас тут идёт начало массива
            if (jr.TokenType != JsonToken.StartArray)
                throw new FormatException("Array start expected");
            jr.Read(); // переходим к следующему токену
            while (jr.TokenType == JsonToken.StartObject) // это или начало объекта
            {                                             // если так, читаем объект одним махом
                yield return serializer.Deserialize<City>(jr);
                if (jr.TokenType != JsonToken.EndObject)  // тут должен быть конец объекта
                    throw new FormatException("Object end expected");
                jr.Read();                                // переходим к следующему токену в цикле
            }
            if (jr.TokenType != JsonToken.EndArray)       // не начало объекта => конец массива
                throw new FormatException("Array end expected");


        }

    }
}
