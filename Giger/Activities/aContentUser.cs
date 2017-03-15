using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Widget;
using Android.Content;
using Java.IO;
using Android.Util;
using System.Text;
using System.Collections.Generic;
using System;
using Giger.Models;

namespace Giger.Activities
{
    [Activity(Label = "aContentUser", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true, Theme = "@style/MyTheme")]
    public class aContentUser : AppCompatActivity
    {
        #region Fields
        DrawerLayout drawerLayout;
        int backCount = 0;
        #endregion

        #region Database
        public static Database database = new Database();
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //SetContentView(Resource.Layout.aContentUser);
            SetContentView(Resource.Layout.aContentUser);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            //Init toolbar
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            //load default home screen
            var ft = FragmentManager.BeginTransaction();
            var QM = new Fragments.First();
            ft.Add(Resource.Id.HomeFrameLayout, QM, "quickmenu");
            ft.AddToBackStack(null);
            ft.Commit();
        }

        #region MENU METHODS
        //  akcje na klikniecie w element menu
        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var ft = FragmentManager.BeginTransaction();
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    var sp = new Fragments.YoutTVSeries();
                    ft.Replace(Resource.Id.HomeFrameLayout, sp, "your_TV_series");
                    ft.AddToBackStack(null);
                    ft.Commit();
                    break;
                case (Resource.Id.nav_messages):
                    var sp2 = new Fragments.Update();
                    ft.Replace(Resource.Id.HomeFrameLayout, sp2, "update");
                    ft.AddToBackStack(null);
                    ft.Commit();
                    break;
                case (Resource.Id.nav_friends):
                    break;
                case (Resource.Id.nav_SearchApartment):
                    break;
            }
            // Close drawer
            drawerLayout.CloseDrawers();
        }

        //add custom icon to tolbar
        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            if (menu != null)
            {
                menu.FindItem(Resource.Id.action_refresh).SetVisible(true);
                menu.FindItem(Resource.Id.action_attach).SetVisible(false);
            }
            return base.OnCreateOptionsMenu(menu);
        }
        //define action for tolbar icon press
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    return true;
                case Resource.Id.action_attach:
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        //to avoid direct app exit on backpreesed and to show fragment from stack
        public override void OnBackPressed()
        {
            if (drawerLayout.IsDrawerOpen(GravityCompat.Start)) drawerLayout.CloseDrawers();    // zamknij menu jesli otwarte
            else
            {
                if (FragmentManager.BackStackEntryCount > 0)
                {
                    if (!(FragmentManager.FindFragmentByTag("quickmenu").IsVisible))
                    {
                        FragmentManager.PopBackStack();
                        backCount = (backCount > 0) ? backCount-- : backCount;
                    }
                    else
                    {
                        backCount++;
                        if (backCount > 1)
                        {
                            Finish();
                        }
                        Toast m = Toast.MakeText(this, "Aby wyjsc, ponownie nacisnij wstecz", ToastLength.Short);
                        m.Show();
                    }
                }
            }
        }

        public override void OnSaveInstanceState(Bundle outState, PersistableBundle outPersistentState)
        {
            base.OnSaveInstanceState(outState, outPersistentState);
        }


        #endregion
    }
}