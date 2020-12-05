using System;
using System.Globalization;
using MvvmCross.Converters;
using MvvmCross.Forms.Converters;
using Schedule.Data.Models;

namespace Schedule.App.UI.Converters
{
    public sealed class WeekModeConverter : MvxFormsValueConverter<WeekMode, string>
    {
        protected override string Convert(WeekMode value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case WeekMode.FirstWeek:
                    return "перший тиждень";
                case WeekMode.SecondWeek:
                    return "другий тиждень";
                case WeekMode.AllWeeks:
                    return "всі тижні";
            }

            return string.Empty;
        }
    }
}
