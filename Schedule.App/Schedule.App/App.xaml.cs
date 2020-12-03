using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Schedule.App.Services;
using Schedule.App.Views;
using Xamarin.Essentials;

namespace Schedule.App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            Preferences.Clear();

            if (Preferences.ContainsKey("group_id"))
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new FirstPage();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
