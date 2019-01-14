using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Views;
using System;
using Android.Content;
using Android.Content.PM;
using Android;
using Java.Lang;

namespace TextMe_Final
{
    [Activity(MainLauncher = true, Icon = "@drawable/imgupd", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Activity
    {
        private Button mbtnSignUp;
        private Button mbtnSignIn;
        private ProgressBar mProgressBar;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            
            mbtnSignUp = FindViewById<Button>(Resource.Id.btnSignUpWithEmail);
            mbtnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            mbtnSignUp.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Dialog_SignUp signUpDialog = new Dialog_SignUp();
                signUpDialog.Show(transaction, "dialog fragment");
                signUpDialog.mOnSignUpComplete += signUpDialog_mOnSignUpComplete;

            };
            mbtnSignIn.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction1 = FragmentManager.BeginTransaction();
                SignIn_Dialog signInDialog1 = new SignIn_Dialog();
                signInDialog1.Show(transaction1, "dialog fragment");
                signInDialog1.mOnSignInComplete += signInDialog1_mOnSignInComplete;
            };

        }

        private void signInDialog1_mOnSignInComplete(object sender, OnSignInEventArgs1 e)
        {
            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeRequest);
            thread.Start();
            string userPassword = e.Password;
            var intent = new Intent(this, typeof(Activity_NextScreen));
            StartActivity(intent);
        }

        private void signUpDialog_mOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {
            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeRequest);
            thread.Start();
            string userPassword = e.Password;

        }

        private void ActLikeRequest()
        {
            Thread.Sleep(3000);
            RunOnUiThread(() => { mProgressBar.Visibility = ViewStates.Invisible; });
        }
    }
}

