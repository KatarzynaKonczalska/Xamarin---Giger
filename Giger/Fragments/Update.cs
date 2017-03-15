using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Giger.Fragments
{
    public class Update : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.Update, container, false);
            
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            Button updateButton = View.FindViewById<Button>(Resource.Id.update_now);
            updateButton.Click += async (s, e) =>
            {
               
                
                WebParser.WebParser w = new WebParser.WebParser();
                await w.whereToWatch();
                var x = 12;

            };
            base.OnViewCreated(view, savedInstanceState);
        }
    }
}