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

            var emsLines = File.ReadLines(Emslocation).Select(x => x.Split('\n'))
                .Select(x => new Ems()
                {
                    EmsCallNumber =null,// Convert.ToInt32(x.ElementAtOrDefault(0)),
                    CallPriority =null,// Convert.ToInt32(x.ElementAtOrDefault(1)),
                    RescueSquadNumber = x.ElementAtOrDefault(2),
                    CallDateTime = Convert.ToDateTime(x.ElementAtOrDefault(3)),
                    EntryDateTime = Convert.ToDateTime(x.ElementAtOrDefault(4)),
                    DispatchDateTime = Convert.ToDateTime(x.ElementAtOrDefault(5)),
                    EnRouteDateTime = Convert.ToDateTime(x.ElementAtOrDefault(6)),
                    OnSceneDateTime = Convert.ToDateTime(x.ElementAtOrDefault(7)),
                    CloseDateTime = Convert.ToDateTime(x.ElementAtOrDefault(8)),
                    Latitude = Convert.ToDouble(x.ElementAtOrDefault(9)),
                    Longitude = Convert.ToDouble(x.ElementAtOrDefault(10))
                });

            insertEms(emsLines);

            // var lines = File.ReadLines(PoliceLocation).Select(x => x.Split(';'));

            //using (var ctx = new cod3Entities())
            //{
                
            //}

        }

        private static void insertEms(IEnumerable<Ems> emsData)
        {
            //var counter = 0;
            //var taking = 1000;
            //var itemsToInsert = lines.Take(taking);

            //using (var ctx = new cod3Entities())
            //{
            //    for (var i = 0; i < lines.Count() / taking; i++)
            //    {
            //        ctx.Ems.AddRange(itemsToInsert);
            //    }
            //}
        }
    }
}
