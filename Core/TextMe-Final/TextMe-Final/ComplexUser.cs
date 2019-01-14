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
    public class ComplexUser : User
    {
        public int ID { get; set; }
        public List<ComplexUser> Friends { get; set; }

        public override string ToString()
        {
            return $"{name}";
        }
    }
}