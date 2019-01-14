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

namespace TextMe_Final
{
    public class OnSignInEventArgs1 : EventArgs
    {
        private string mEmail;
        private string mPassword;


        public string Email
        {
            get { return mEmail; }
            set
            {
                mEmail = value;
            }
        }
        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
        public OnSignInEventArgs1(string email, string password) : base()
        {
            Email = email;
            Password = password;
        }
    }

    class SignIn_Dialog : DialogFragment
    {

        private EditText mTxtEmail;
        private EditText mTxtPassword;
        private Button mBtnSignIn;

        public event EventHandler<OnSignInEventArgs1> mOnSignInComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.SignIn_layout, container, false);

            mTxtEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            mTxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            mBtnSignIn = view.FindViewById<Button>(Resource.Id.btnDialogEmail);

            mBtnSignIn.Click += mBtnSignIn_Click;
            return view;
        }

        void mBtnSignIn_Click(object sender, EventArgs e)
        {
            ApiConnector conector = ApiConnector.GetInstance();

            conector.Authenticate(mTxtEmail.Text, mTxtPassword.Text);
            conector.LoggedUser = conector.GetUserByID(conector.LoggedUserID);

          

            mOnSignInComplete.Invoke(this, new OnSignInEventArgs1(mTxtEmail.Text, mTxtPassword.Text));

            this.Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }
    }
}