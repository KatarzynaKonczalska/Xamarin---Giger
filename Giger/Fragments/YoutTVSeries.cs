using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Giger.Adapters;
using Giger.Models;
using Giger.WebParser;
using System.Collections.Generic;

namespace Giger.Fragments
{
    public class YoutTVSeries : Fragment
    {
        OverviewGridAdapter gridAdapter;
        GridView gridView;
        string[] yourTVSeries = { };
        string[] yourImages = { };
        List<string> names = new List<string>();
        List<string> images = new List<string>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.YourTVSeries, container, false);
            //showGrid(yourTVSeries, yourImages);
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {

            showGrid();


            var fab = View.FindViewById(Resource.Id.fab);
            //onclick guzika (+)
            fab.Click += delegate
            {
                showInputDialog();
            };

            base.OnViewCreated(view, savedInstanceState);
        }

        public override void OnResume()
        {
            showGrid();
            base.OnResume();
        }

        public void showInputDialog()
        {
            LayoutInflater inflater = LayoutInflater.From(this.Activity);
            View promptView = inflater.Inflate(Resource.Layout.PopWindow, null);
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this.Activity);
            alertDialogBuilder.SetView(promptView);

            EditText imageUrl = promptView.FindViewById<EditText>(Resource.Id.url);
            EditText editText = promptView.FindViewById<EditText>(Resource.Id.name);
            alertDialogBuilder.SetCancelable(true)
                .SetPositiveButton("Add Series", (s, e) =>
                {
                    if(editText.Text!="")
                    {
                        Activities.aContentUser.database.InsertSerieToDatabase(editText.Text, imageUrl.Text);
                        Toast.MakeText(this.Activity, "Created", ToastLength.Short).Show();
                        showGrid();
                    }
                    else
                    {
                        Toast.MakeText(this.Activity, "No Title - try again", ToastLength.Short).Show();
                    }
                    
                });
            AlertDialog alert = alertDialogBuilder.Create();
            alert.Show();
        }

        public void showGrid()
        {         
            yourTVSeries = Activities.aContentUser.database.ReadSerieToString();
            yourImages = Activities.aContentUser.database.ReadImagesToString();
            gridAdapter = new OverviewGridAdapter(this.Activity);
            gridView = View.FindViewById<GridView>(Resource.Id.grid_view_image_text);
            gridView.Adapter = gridAdapter;
            gridView.ItemClick += (s, e) =>
            {
                var ft = FragmentManager.BeginTransaction();
                var sp = new Fragments.Seasons(yourTVSeries[e.Position]);
                ft.Replace(Resource.Id.HomeFrameLayout, sp, "seasons");
                ft.AddToBackStack(null);
                ft.Commit();
            };
            gridView.ItemLongClick += (s, e) =>
            {
                LayoutInflater inflater = LayoutInflater.From(this.Activity);
                View promptView = inflater.Inflate(Resource.Layout.PopupDelete, null);
                AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this.Activity);
                alertDialogBuilder.SetView(promptView);

                alertDialogBuilder.SetCancelable(true)
                    .SetPositiveButton("Remove", (se, ee) =>
                    {
                        //delete
                        Activities.aContentUser.database.deleteSereFromDatabase(yourTVSeries[e.Position]);
                        Toast.MakeText(this.Activity, "Removed", ToastLength.Short).Show();
                        showGrid();
                    })
                    .SetNegativeButton("Cancel", (si, ei) =>
                    {
                        showGrid();
                    });
                AlertDialog alert = alertDialogBuilder.Create();
                alert.Show();
            };
        }
    }


}