using System;
using System.Globalization;
using MvvmCross.Forms.Converters;
using Schedule.Data.Models;

namespace Schedule.App.UI.Converters
{
    public sealed class SubGroupConverter : MvxFormsValueConverter<GroupMode, string>
    {
        protected override string Convert(GroupMode value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case GroupMode.FirstGroup:
                    return "перша підгрупа";
                case GroupMode.SecondGroup:
                    return "друга підгрупа";
                case GroupMode.ThirdGroup:
                    return "третя підгрупа";
                case GroupMode.FourthGroup:
                    return "четверта підгрупа";
                case GroupMode.AllGroups:
                    return "всі підгрупи";
            }

            return string.Empty;
        }
    }
}
