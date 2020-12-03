using System;
using System.Collections.Generic;
using Schedule.App.ViewModels;
using Schedule.App.Views;
using Xamarin.Forms;

namespace Schedule.App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
