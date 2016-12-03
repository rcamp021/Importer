using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Importer.Models;
using Dapper;
using Dapper.Contrib;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

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

            //using (var ctx = new cod3Entities())
            using(var conn = new SqlConnection(Properties.Settings.Default.ConnString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                string sql = @"insert Ems(EmsCallNumber, CallPriority, RescueSquadNumber, CallDateTime, EntryDateTime,
DispatchDateTime, EnRouteDateTime, OnSceneDateTime, CloseDateTime, Latitude, Longitude)
values(@EmsCallNumber, @CallPriority, @RescueSquadNumber, @CallDateTime, @EntryDateTime,
@DispatchDateTime, @EnRouteDateTime, @OnSceneDateTime, @CloseDateTime, @Latitude, @Longitude)";
                //ctx.Ems.Add(item);
                conn.Execute(sql,lines, trans);
                trans.Commit();
                //                var trans = conn.BeginTransaction();
                //              //  ctx.Ems.AddRange(itemsToInsert);
                //                foreach (var item in itemsToInsert)
                //                {
                //                   // conn.Insert(item);
                //                    string sql = @"bulk insert into Ems values(@EmsCallNumber, @CallPriority, @RescueSquadNumber, @CallDateTime, @EntryDateTime,
                //@DispatchDateTime, @EnRouteDateTime, @OnSceneDateTime, @CloseDateTime, @Latitude, @Longitude)";
                //                    //ctx.Ems.Add(item);
                //                    conn.Execute(sql, new
                //                    {
                //                        item.EmsCallNumber,
                //                        item.CallPriority,
                //                        item.RescueSquadNumber,
                //                        item.CallDateTime,
                //                        item.EntryDateTime,
                //                        item.DispatchDateTime,
                //                        item.EnRouteDateTime,
                //                        item.OnSceneDateTime,
                //                        item.CloseDateTime,
                //                        item.Latitude,
                //                        item.Longitude
                //                    ,trans});
                //                }
                //             //   ctx.SaveChanges();
                //                for (var i = 0; i < lines.Count() / taking; i++)
                //                {
                //                   // ctx.Ems.AddRange(lines.Skip(counter).Take(taking));
                //                    counter += taking;
                //                   // ctx.SaveChanges();
                //                }
            }
        }
    }
}
