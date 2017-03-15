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
using Java.Lang;
using Android.Graphics;
using System.Net;
using Giger.Models;

namespace Giger.Adapters
{
    class OverviewGridAdapter : BaseAdapter
    {
        private Context context;

        public OverviewGridAdapter(Context context)
        {
            this.context = context;
        }
        public override int Count
        {
            get
            {
                return Activities.aContentUser.database.CountSeries();
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view;
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            if (convertView == null)
            {
                view = new View(context);
                view = inflater.Inflate(Resource.Layout.yrSerie, null);
                ImageView img = view.FindViewById<ImageView>(Resource.Id.imageButton1);
                TextView txt = view.FindViewById<TextView>(Resource.Id.textView1);

                var y = Activities.aContentUser.database.ReadSerieFromDatabase();
                var x = y.ToArray();

                //var imageBitmap = GetImageBitmapFromUrl(TVSeriesImages[position]);
                //img.SetImageBitmap(imageBitmap);
                //txt.Text = TVSeriesNames[position];
                if (x[position].Url != null)
                {
                    try
                    {
                        var imageBitmap = GetImageBitmapFromUrl(x[position].Url);
                        img.SetImageBitmap(imageBitmap);
                    }
                    catch(System.Exception es)
                    {

                    }
                }

                txt.Text = x[position].Title;

            }
            else
            {
                view = (View)convertView;
            }
            return view;
        }

        public Bitmap GetImageBitmapFromUrl(string url)
        {

            Bitmap imageBitmap = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                    return imageBitmap;
                }
            }
            catch (System.Exception ext)
            {
                return null;
            }
        }
    }
}