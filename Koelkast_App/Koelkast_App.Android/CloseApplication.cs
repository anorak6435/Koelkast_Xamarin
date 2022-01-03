using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Koelkast_App.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace Koelkast_App.Droid
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}