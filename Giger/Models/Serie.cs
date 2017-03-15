using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Giger.Models
{
    [Table("Serie")]
    public class Serie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        public string Url { get; set; }
        [Ignore]
        public List<Season> seasons { get; set; }

    }

    [Table("Season")]
    public class Season
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public int ParentId { get; set; }
        public string ParentTitle { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        [Ignore]
        public List<Episode> episodes { get; set; }

    }

    [Table("Episode")]
    public class Episode
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}