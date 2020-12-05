using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Schedule.App.Core.Services
{
    public sealed class SettingsService : ISettingsService
    {
        public int SelectedGroupId
        {
            get => Preferences.Get("group_id", 0);
            set => Preferences.Set("group_id", value);
        }
    }
}
