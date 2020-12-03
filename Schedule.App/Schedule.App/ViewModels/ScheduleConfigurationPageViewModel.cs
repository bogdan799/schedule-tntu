using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Schedule.App.Services;
using Schedule.Data.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Schedule.App.ViewModels
{
    public class Course
    {
        #region Properties

        public int CourseNumber { get; set; }

        public string CourseName { get; set; }

        #endregion
    }

    public sealed class ScheduleConfigurationPageViewModel : BaseViewModel
    {
        #region Declarations

        private readonly ScheduleService _scheduleService;
        private List<Course> _courses;
        private List<Faculty> _faculties;
        private List<Group> _filteredGroups;
        private Course _selectedCourse;
        private Faculty _selectedFaculty;
        private Group _selectedGroup;

        #endregion

        #region Properties

        public List<Faculty> Faculties
        {
            get => _faculties;
            set
            {
                _faculties = value;
                OnPropertyChanged();
            }
        }

        public Faculty SelectedFaculty
        {
            get => _selectedFaculty;
            set
            {
                _selectedFaculty = value;
                OnPropertyChanged();
            }
        }

        public List<Course> Courses
        {
            get => _courses;
            set
            {
                _courses = value;
                OnPropertyChanged();
            }
        }

        public Course SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
            }
        }

        public List<Group> FilteredGroups
        {
            get => _filteredGroups;
            set
            {
                _filteredGroups = value;
                OnPropertyChanged();
            }
        }

        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public List<Group> Groups { get; set; }

        public Command SaveCommand { get; set; }

        #endregion

        #region Constructors

        public ScheduleConfigurationPageViewModel()
        {
            _scheduleService = new ScheduleService();

            SaveCommand = new Command(SaveSchedule, o => SelectedGroup != null);
            Faculties = _scheduleService.GetFaculties().ToList();
        }

        private void SaveSchedule(object obj)
        {
            Preferences.Set("group_id", SelectedGroup.Id);
            Application.Current.MainPage = new AppShell();
        }

        #endregion

        #region Private Methods

        protected override void DoOnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(SelectedFaculty))
            {
                UpdateGroups();
            }

            if (propertyName == nameof(SelectedCourse))
            {
                UpdateFilteredGroups();
            }
        }

        private void UpdateFilteredGroups()
        {
            if (SelectedCourse != null)
            {
                FilteredGroups = Groups.Where(g => g.Course == SelectedCourse.CourseNumber).ToList();
            }
            else
            {
                FilteredGroups = null;
                SelectedGroup = null;
            }
        }

        private void UpdateGroups()
        {
            if (SelectedFaculty != null)
            {
                Groups = _scheduleService.GetGroupsByFaculty(SelectedFaculty.Id).ToList();
                Courses = Groups.GroupBy(g => g.Course)
                    .Select(g => g.Key)
                    .Select(course => new Course
                    {
                        CourseName = $"{course} курс",
                        CourseNumber = course
                    }).ToList();
            }
            else
            {
                Groups = null;
                Courses = null;
                SelectedCourse = null;
            }
        }

        #endregion
    }
}