// <auto-generated> This file has been auto generated. </auto-generated>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
#if!GRAPHQL_GENERATOR_DISABLE_NEWTONSOFT_JSON
using Newtonsoft.Json;
#endif

namespace MTGViewer.Maui.Data.MtgJson
{
    public partial class SetList
    {
        public string? Id { get; set; }
        public decimal? BaseSetSize { get; set; }
        public string? Code { get; set; }
        public bool? IsFoilOnly { get; set; }
        public bool? IsOnlineOnly { get; set; }
        public bool? IsPaperOnly { get; set; }
        public string? Name { get; set; }
        public string? ReleaseDate { get; set; }
        public decimal? TotalSetSize { get; set; }
        public string? Type { get; set; }
    }
}
