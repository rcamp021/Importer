//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Importer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ems
    {
        public int Id { get; set; }
        public Nullable<int> EmsCallNumber { get; set; }
        public Nullable<int> CallPriority { get; set; }
        public string RescueSquadNumber { get; set; }
        public Nullable<System.DateTime> CallDateTime { get; set; }
        public Nullable<System.DateTime> EntryDateTime { get; set; }
        public Nullable<System.DateTime> DispatchDateTime { get; set; }
        public Nullable<System.DateTime> EnRouteDateTime { get; set; }
        public Nullable<System.DateTime> OnSceneDateTime { get; set; }
        public Nullable<System.DateTime> CloseDateTime { get; set; }
        public System.Data.Entity.Spatial.DbGeography Location { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
    }
}
