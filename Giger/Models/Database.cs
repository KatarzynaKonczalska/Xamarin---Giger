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
using Android.Util;

namespace Giger.Models
{
    public class Database
    {
        //SQLiteConnection connection;
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public Database()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    connection.CreateTable<Serie>();
                    connection.CreateTable<Season>();
                    connection.CreateTable<Episode>();
                }
            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }

        public void InsertSerieToDatabase(string name, string url)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    connection.Insert(new Serie()
                    {
                        Title = name,
                        Url = url,
                        seasons = new List<Season>()
                    });
                }
            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }

        public void InsertSeasonToDatabase(Season season)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    connection.Insert(season);
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }

        public Serie ReadSerieFromDatabase(string name)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    
                    return connection.Table<Serie>().Where(x => x.Title.Contains(name)).ToList().SingleOrDefault();
       
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }
        public List<Serie> ReadSerieFromDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    return connection.Table<Serie>().ToList();
                }
            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }


        public List<Serie> ReadSeriesFromDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    return connection.Table<Serie>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }

        public List<Season> ReadSeasonsFromDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    return connection.Table<Season>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }

        public bool insertIntoTableSerie(Serie serie)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    connection.Insert(serie);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public string[] ReadSerieToString()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    List<string> li = new List<string>();
                    foreach ( var item in connection.Table<Serie>().ToList())
                    {
                        li.Add(item.Title);
                    }
                    return li.ToArray();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }


        public List<Season> ReadSeasons(string serieName)
        {
            List<Season> li = new List<Season>();
            foreach (var it in Activities.aContentUser.database.ReadSeasonsFromDatabase())
            {
                if (it.ParentTitle == serieName)
                {
                    li.Add(it);
                }
            }

            return li;
        }

        public string[] ReadImagesToString()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {

                    List<string> li = new List<string>();
                    foreach (var item in connection.Table<Serie>().ToList())
                    {
                        li.Add(item.Url);
                    }
                    return li.ToArray();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return null;
            }
        }
        public int CountSeries()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    return connection.Table<Serie>().ToList().Count;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEX", ex.Message);
                return 0;
            }

        }

        public void deleteSereFromDatabase(string name)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    var x = ReadSerieFromDatabase().Where(m => m.Title.Contains(name)).SingleOrDefault();
                    connection.Delete(x);
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }

        public void deleteAllSeasonsFromDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    var x = ReadSerieFromDatabase();
                    connection.Delete(x);
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }


        public bool updateTablePerson(Serie serie)
        {
            try
            {

                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Series.db")))
                {
                    foreach(var a in serie.seasons)
                    {
                        Activities.aContentUser.database.InsertSeasonToDatabase(a);
                    }
                    connection.Query<Serie>("UPDATE Serie set Title=?,Url=?,seasons=? Where Id=?", serie.Title, serie.Url, serie.seasons, serie.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

    }
}