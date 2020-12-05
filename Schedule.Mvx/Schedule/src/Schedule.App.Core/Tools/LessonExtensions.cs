using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schedule.App.Core.ViewModels;
using Schedule.App.Core.ViewModels.Home;
using Schedule.Data.Models;

namespace Schedule.App.Core.Tools
{
    public static class LessonWeekExtensions
    {

        public static bool IsWeekMode(this Lesson lesson, WeekMode weekMode)
        {
            return lesson.WeekMode == weekMode || lesson.WeekMode == WeekMode.AllWeeks;
        }

        public static bool IsFirstWeek(this Lesson lesson)
        {
            return lesson.IsWeekMode(WeekMode.FirstWeek);
        }

        public static bool IsFirstWeek(this LessonViewModel lesson)
        {
            return lesson.Lesson.IsFirstWeek();
        }

        public static bool IsSecondWeek(this Lesson lesson)
        {
            return lesson.IsWeekMode(WeekMode.SecondWeek);
        }

        public static bool IsSecondWeek(this LessonViewModel lesson)
        {
            return lesson.Lesson.IsSecondWeek();
        }

        public static List<LessonDay> GetWeekDays(this IEnumerable<LessonViewModel> lessons, WeekMode weekMode)
        {
            return lessons.Where(l => l.Lesson.IsWeekMode(weekMode))
                .GroupBy(l => l.Lesson.Day)
                .Select(d => new LessonDay
                {
                    Day = d.Key,
                    Lessons = d.ToList(),
                    WeekMode = weekMode
                })
                .OrderBy(d => d.Day)
                .ToList();
        }
    }
}
