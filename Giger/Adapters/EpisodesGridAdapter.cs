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

namespace Giger.Adapters
{
    class EpisodesGridAdapter : BaseAdapter
    {
        private Context context;
        private string[] listOfItems;

        public EpisodesGridAdapter(Context context, string[] list)
        {
            this.context = context;
            listOfItems = list;
        }

        public override int Count
        {
            get
            {
                return listOfItems.Length;
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
            if(convertView == null)
            {
                view = new View(context);
                view = inflater.Inflate(Resource.Layout.yrRaw, null);
                TextView item = view.FindViewById<TextView>(Resource.Id.list_item);

                item.Text = listOfItems[position];
            }
            else
            {
                view = (View)convertView;
            }
            return view;
        }
    }
}