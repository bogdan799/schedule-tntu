using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Schedule.App.Core.Services;
using Schedule.App.Core.Tools;
using Schedule.App.Core.ViewModels.Main;
using Schedule.Data.Models;

namespace Schedule.App.Core.ViewModels.Home
{
    public class LessonDay
    {
        public DayOfWeek Day { get; set; }

        public List<LessonViewModel> Lessons { get; set; }

        public WeekMode WeekMode { get; set; }
    }

    public class HomeViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ISettingsService _settingsService;
        private readonly IScheduleService _scheduleService;

        #region Properties

        public MvxObservableCollection<LessonViewModel> Lessons { get; } =
            new MvxObservableCollection<LessonViewModel>();

        public MvxObservableCollection<LessonDay> LessonDays { get; } =
            new MvxObservableCollection<LessonDay>();

        public Group Group { get; set; }

        #endregion

        public HomeViewModel(IMvxNavigationService navigationService, ISettingsService settingsService, IScheduleService scheduleService)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;
            _scheduleService = scheduleService;
        }

        public override async Task Initialize()
        {
            Group = await _scheduleService.GetGroupByIdAsync(_settingsService.SelectedGroupId);
            List<Lesson> lessons = await _scheduleService.GetLessonsByGroupIdAsync(_settingsService.SelectedGroupId);
            Lessons.AddRange(lessons.Select(l => new LessonViewModel(l)));

            if (Lessons.Any(l => l.Lesson.WeekMode != WeekMode.AllWeeks))
            {
                LessonDays.AddRange(Lessons.GetWeekDays(WeekMode.FirstWeek));
                LessonDays.AddRange(Lessons.GetWeekDays(WeekMode.SecondWeek));
            }
            else
            {
                LessonDays.AddRange(Lessons.GetWeekDays(WeekMode.AllWeeks));
            }
        }
    }
}
