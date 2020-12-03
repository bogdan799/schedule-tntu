using System;
using System.IO;
using Newtonsoft.Json;
using Schedule.DataParser.Parsers;

namespace Schedule.DataParser
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            var parser = new FacultiesParser();

            // ReSharper disable once AsyncConverter.AsyncWait
            var faculties = parser.ParseFacultiesAsync().Result;

            var serializer = new JsonSerializer {Formatting = Formatting.Indented};

            Console.WriteLine(JsonConvert.SerializeObject(new Data.Models.Schedule {Faculties = faculties}));
            Console.ReadLine();
            using (var textWriter = File.OpenWrite("D:/schedule.json"))
            using (var stringWriter = new StreamWriter(textWriter))
            {
                serializer.Serialize(stringWriter, new Data.Models.Schedule {Faculties = faculties});
            }
        }

        #endregion
    }
}