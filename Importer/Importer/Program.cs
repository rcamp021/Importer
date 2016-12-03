using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Spatial;
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
                    Latitude = x.ElementAtOrDefault(9)?.Trim(),
                    Longitude = x.ElementAtOrDefault(10)?.Trim()
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
            using(var conn = new SqlBulkCopy(Properties.Settings.Default.ConnString))
            {
                conn.BulkCopyTimeout = int.MaxValue;
                var table = new DataTable("Ems");
                table.Columns.Add(new DataColumn
                {
                    AllowDBNull = false,
                    AutoIncrement = true,
                    DataType = typeof(int),
                    ColumnName = "Id"
                });
                table.Columns.Add("EmsCallNumber", typeof(int));
                table.Columns.Add("CallPriority", typeof(int));
                table.Columns.Add( GetCol("RescueSquadNumber", typeof(string), true));
                table.Columns.Add( GetCol("CallDateTime", typeof(DateTime), true));
                table.Columns.Add( GetCol("EntryDateTime", typeof(DateTime), true));
                table.Columns.Add( GetCol("DispatchDateTime", typeof(DateTime), true));
                table.Columns.Add( GetCol("EnRouteDateTime", typeof(DateTime), true));
                table.Columns.Add( GetCol("OnSceneDateTime", typeof(DateTime), true));
                table.Columns.Add( GetCol("CloseDateTime", typeof(DateTime), true));
                table.Columns.Add(GetCol("Location", typeof(DbGeography), true));
                table.Columns.Add( GetCol("Latitude", typeof(string), true));
                table.Columns.Add(GetCol("Longitude", typeof(string), true));

                conn.DestinationTableName = "Ems";
                foreach (var item in emsData)
                    table.Rows.Add(null, item.EmsCallNumber, item.CallPriority, item.RescueSquadNumber, item.CallDateTime,
                        item.EntryDateTime, item.DispatchDateTime, item.EnRouteDateTime, item.OnSceneDateTime,
                        item.CloseDateTime,null,item.Latitude, item.Longitude
                        );

                conn.WriteToServer(table);
            }
        }

        private static DataColumn GetCol(string name, Type type, bool AllowNull)
        {
            return  new DataColumn()
            {
                AllowDBNull = AllowNull,
                ColumnName = name,
                DataType = type

            };

            var col = new DataColumn
            {
                AllowDBNull = true,
                ColumnName = "",
                DataType = typeof(DateTime)
            };
        }
    }
}
