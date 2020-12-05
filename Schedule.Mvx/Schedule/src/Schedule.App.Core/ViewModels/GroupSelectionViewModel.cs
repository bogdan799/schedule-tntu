using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Schedule.App.Core.Models;
using Schedule.App.Core.Services;
using Schedule.App.Core.ViewModels.Home;
using Schedule.App.Core.ViewModels.Main;
using Schedule.Data.Models;

namespace Schedule.App.Core.ViewModels
{
    public sealed class GroupSelectionViewModel : BaseViewModelResult<int>
    {
        #region Declarations

        private readonly IMvxNavigationService _navigationService;
        private readonly IScheduleService _scheduleService;
        private readonly ISettingsService _settingsService;

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

                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public List<Group> Groups { get; set; }

        public MvxAsyncCommand SaveCommand { get; }

        #endregion

        #region Constructors

        public GroupSelectionViewModel(IMvxNavigationService navigationService,
            IScheduleService scheduleService,
            ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _scheduleService = scheduleService;
            _settingsService = settingsService;

            SaveCommand = new MvxAsyncCommand(OnSaveCommandAsync, CanSave);
        }

        #endregion

        #region Public Methods

        public override async Task Initialize()
        {
            Faculties = await _scheduleService.GetFacultiesAsync();

            await base.Initialize();
        }

        public override async Task RaisePropertyChanged(string whichProperty = "")
        {
            await base.RaisePropertyChanged(whichProperty);

            if (whichProperty == nameof(SelectedFaculty))
            {
                await UpdateGroupsAsync();
            }
            else if (whichProperty == nameof(SelectedCourse))
            {
                UpdateFilteredGroups();
            }
        }

        #endregion

        #region Private Methods

        private bool CanSave()
        {
            return SelectedGroup != null;
        }

        private Task OnSaveCommandAsync()
        {
            _settingsService.SelectedGroupId = SelectedGroup.Id;
            return _navigationService.Navigate<HomeViewModel>();
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

        private async Task UpdateGroupsAsync()
        {
            if (SelectedFaculty != null)
            {
                Groups = await _scheduleService.GetGroupsByFacultyAsync(SelectedFaculty.Id);
                Courses = Groups.GroupBy(g => g.Course)
                    .Select(g => g.Key)
                    .Select(course => new Course {CourseName = $"{course} курс", CourseNumber = course}).ToList();
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
