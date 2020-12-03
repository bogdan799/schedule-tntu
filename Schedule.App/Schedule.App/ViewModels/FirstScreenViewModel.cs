using Schedule.App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Schedule.App.ViewModels
{
    public class FirstScreenViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public FirstScreenViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            await App.Current.NavigationProxy.PushModalAsync(new ScheduleConfigurationPage(), true);
        }
    }
}
