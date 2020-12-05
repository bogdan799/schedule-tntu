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
using MvvmCross.Forms.Platforms.Android.Views;
using Schedule.App.Core.ViewModels.Main;
using Xamarin.Forms;

namespace Schedule.App.Droid
{
    [Activity(
        Theme = "@style/AppTheme")]
    public class MainActivity : MvxFormsAppCompatActivity<MainViewModel>
    {

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
        }
    }
}
