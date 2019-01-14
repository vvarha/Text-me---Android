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
    public class User
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }


        public User(string name, string email, string pw)
        {
            this.name = name;
            this.email = email;
            password = pw;
        }

        public User()
        {

        }
    }
}
