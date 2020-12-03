using System;
using System.IO;
using System.Runtime.Serialization;
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
            var idGenerator = new IdGenerator();

            // ReSharper disable once AsyncConverter.AsyncWait
            var faculties = parser.ParseFacultiesAsync().Result;
            var schedule = new Data.Models.Schedule {Faculties = faculties};

            idGenerator.FillGeneratedIds(schedule);

            var serializer = new JsonSerializer {Formatting = Formatting.Indented};

            Console.WriteLine(JsonConvert.SerializeObject(new Data.Models.Schedule {Faculties = faculties}));
            Console.ReadLine();
            using (var textWriter = File.OpenWrite("D:/schedule2.json"))
            using (var stringWriter = new StreamWriter(textWriter))
            {
                serializer.Serialize(stringWriter, schedule);
            }
        }

        #endregion
    }
}