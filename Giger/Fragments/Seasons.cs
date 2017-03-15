
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Views;
using Android.Widget;
using Giger.Adapters;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace Giger.Fragments
{
    public class Seasons : Fragment
    {
        string serieName="";
        GridView gridSeasons;
        GridView gridEpisodes;
        EpisodesGridAdapter gridAdapterSeasons, gridAdapterEpisodes;

        List<string> li = new List<string>();
        public string[] seasons = {  };
        private string[] episodes = { "episode 1", "episode 2", "episode 3",
            "episode 4", "episode 5", "episode 6", "episode 7",
            "episode 8", "episode 9", "episode 10", "episode 11" };

        public Seasons(string serieNameShow)
        {
            this.serieName = serieNameShow;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.Season, container, false);
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            TextView t1 = View.FindViewById<TextView>(Resource.Id.textView1);
            t1.Text = serieName;

            var seasonsItems = Activities.aContentUser.database.ReadSeasons(serieName);
            foreach ( var i in seasonsItems)
            {
                li.Add(i.Name);
            }
            seasons = li.ToArray();

            //lista sezonów
            gridAdapterSeasons = new EpisodesGridAdapter(this.Activity, seasons);
            gridSeasons = View.FindViewById<GridView>(Resource.Id.gridView1);
            gridSeasons.Adapter = gridAdapterSeasons;
            gridSeasons.ItemClick += (s, e) =>
            {
                foreach( var i in seasonsItems)
                {
                    if(i.Name==seasons[e.Position])
                    {
                        var uri = Android.Net.Uri.Parse(i.Url);
                        var intent = new Intent(Intent.ActionView, uri);
                        StartActivity(intent);
                    }
                }
                //Toast.MakeText(this.Activity, "Season", ToastLength.Short).Show();
                
            };

            ////lista odcinków
            //gridAdapterEpisodes = new EpisodesGridAdapter(this.Activity, episodes);
            //gridEpisodes = View.FindViewById<GridView>(Resource.Id.gridView2);
            //gridEpisodes.Adapter = gridAdapterEpisodes;
            //gridEpisodes.ItemClick += (s, e) =>
            //{
            //    Toast.MakeText(this.Activity, "Episode", ToastLength.Short).Show();
            //};

            base.OnViewCreated(view, savedInstanceState);
        }

        public void f()
        {

        }
    }
}