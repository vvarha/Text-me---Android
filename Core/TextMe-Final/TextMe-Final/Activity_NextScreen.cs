using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Animation;
using Android.Support.V7.App;

namespace TextMe_Final
{
    [Activity(Label = "Activity_NextScreen")]
    public class Activity_NextScreen : Activity
    {
        private TextView mLogin;
        private ListView mListViews;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            //SetTheme(Resource.Style.AppCompat);
            // Create your application here

            SetContentView(Resource.Layout.NextScreenLayout);
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            SlidingTabsFragment fragment = new SlidingTabsFragment();
            transaction.Replace(Resource.Id.sample_content_fragment, fragment);
            transaction.Commit();
            ComplexUser loggedUser = ApiConnector.GetInstance().LoggedUser;

            List<string> mItems = new List<string>();
            foreach (var item in loggedUser.Friends)
                mItems.Add(item.ToString());

            mLogin = FindViewById<TextView>(Resource.Id.txtLogin);
            mListViews = FindViewById<ListView>(Resource.Id.ListFriends);
            ArrayAdapter<String> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mItems);

            mListViews.Adapter = adapter;
            mLogin.Text = $"Logged as: {loggedUser.name}, {loggedUser.email}";
        }

        //    fabAccountPlus = FindViewById<FloatingActionButton>(Resource.Id.fab_accountPlus);
        //    fabGroupPlus = FindViewById<FloatingActionButton>(Resource.Id.fab_multiplePLus);
        //    mainPlus = FindViewById<FloatingActionButton>(Resource.Id.plus);
        //    bgFabMenu = FindViewById<View>(Resource.Id.bg_fab_menu);

        //    mainPlus.Click += (o, e) =>
        //    {
        //        if (!isFabOpen)
        //        {
        //            ShowFabMenu();

        //        }
        //        else
        //        {
        //            CloseFabMenu();

        //        }
        //    };

        //    fabAccountPlus.Click += (o, e) =>
        //    {
        //        CloseFabMenu();
        //        Toast.MakeText(this, "Cake!", ToastLength.Short);
        //    };

        //    fabGroupPlus.Click += (o, e) =>
        //    {
        //        CloseFabMenu();
        //        Toast.MakeText(this, "AirBaloon!", ToastLength.Short);
        //    };

        //    bgFabMenu.Click += (o, e) => CloseFabMenu();

        //}

        //private void CloseFabMenu()
        //{
        //    isFabOpen = false;

        //    mainPlus.Animate().Rotation(0f);
        //    bgFabMenu.Animate().Alpha(0f);
        //    fabGroupPlus.Animate().TranslationY(0f)
        //        .Rotation(90f);
        //    fabAccountPlus.Animate().TranslationY(0f)
        //        .Rotation(90f).SetListener(new FabAnimatorListener(bgFabMenu, fabAccountPlus,fabGroupPlus));
        //}
        //private class FabAnimatorListener : Java.Lang.Object, Animator.IAnimatorListener
        //{
        //    View[] viewsToHide;
        //    public FabAnimatorListener(params View[] viewsToHide)
        //    {
        //        this.viewsToHide = viewsToHide;
        //    }
        //    public void OnAnimationCancel(Animator animation)
        //    {
        //    }

        //    public void OnAnimationEnd(Animator animation)
        //    {
        //        if (!isFabOpen)
        //            foreach (var view in viewsToHide)
        //                view.Visibility = ViewStates.Gone;
        //    }

        //    public void OnAnimationRepeat(Animator animation)
        //    {
        //    }

        //    public void OnAnimationStart(Animator animation)
        //    {
        //    }
        //}

        //private void ShowFabMenu()
        //{
        //    isFabOpen = true;
        //    fabGroupPlus.Visibility = ViewStates.Visible;
        //    fabAccountPlus.Visibility = Android.Views.ViewStates.Visible;
        //    bgFabMenu.Visibility = Android.Views.ViewStates.Visible;
        //    mainPlus.Animate().Rotation(135f);
        //    bgFabMenu.Animate().Alpha(1f);
        //    fabGroupPlus.Animate().TranslationY(-Resources.GetDimension(Resource.Dimension.standard_100))
        //        .Rotation(0f);
        //    fabAccountPlus.Animate().TranslationY(-Resources.GetDimension(Resource.Dimension.standard_55))
        //        .Rotation(0f);
        //}

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_next, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
    
}