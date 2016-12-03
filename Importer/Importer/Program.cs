using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Importer.Models;

namespace Importer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string Emslocation = @"../../csvs/EMS.csv";
            const string PoliceLocation = @"csvs/Police.csv";

            var emsLines = File.ReadLines(Emslocation);

            //var arrayLines = from line in emsLines select (line.Split(',')).t
            var arrayLines = emsLines.Select(x => x.Split(','))
                .Select(x => new Ems()
                {
                    EmsCallNumber = x.ElementAtOrDefault(0).SafeParseInt(),// Convert.ToInt32(x.ElementAtOrDefault(0)),
                    CallPriority = x.ElementAtOrDefault(1).SafeParseInt(),// Convert.ToInt32(x.ElementAtOrDefault(1)),
                    RescueSquadNumber = x.ElementAtOrDefault(2)?.Trim(),
                    CallDateTime = x.ElementAtOrDefault(3).SafeParseDate(),
                    EntryDateTime = x.ElementAtOrDefault(4).SafeParseDate(),
                    DispatchDateTime = x.ElementAtOrDefault(5).SafeParseDate(),
                    EnRouteDateTime = x.ElementAtOrDefault(6).SafeParseDate(),
                    OnSceneDateTime = x.ElementAtOrDefault(7).SafeParseDate(),
                    CloseDateTime = x.ElementAtOrDefault(8).SafeParseDate(),
                    Latitude = Convert.ToDouble(x.ElementAtOrDefault(9).SafeParseDouble()),
                    Longitude = Convert.ToDouble(x.ElementAtOrDefault(10).SafeParseDouble())
                });

            insertEms(arrayLines);

            var t = 0;
            // var lines = File.ReadLines(PoliceLocation).Select(x => x.Split(';'));

            //using (var ctx = new cod3Entities())
            //{

            //}

        }

        private static void insertEms(IEnumerable<Ems> emsData)
        {
            var lines = emsData.ToList();
            var counter = 0;
            var taking = 1000;
            var itemsToInsert = lines.Take(taking);

            using (var ctx = new cod3Entities())
            {
              //  ctx.Ems.AddRange(itemsToInsert);
                foreach (var item in itemsToInsert)
                {
                    ctx.Ems.Add(item);
                }
                ctx.SaveChanges();
                for (var i = 0; i < lines.Count() / taking; i++)
                {
                    ctx.Ems.AddRange(lines.Skip(counter).Take(taking));
                    counter += taking;
                    ctx.SaveChanges();
                }
            }
        }
    }
}
