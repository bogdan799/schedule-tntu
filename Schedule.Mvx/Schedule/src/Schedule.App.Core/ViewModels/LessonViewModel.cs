using Schedule.App.Core.Tools;
using Schedule.Data.Models;
using Xamarin.Forms;

namespace Schedule.App.Core.ViewModels
{
    public class LessonViewModel : BaseViewModel
    {
        #region Declarations

        private Color _accentColor;
        private Color _backgroundColor;

        #endregion

        #region Properties

        public Color AccentColor
        {
            get => _accentColor;
            set
            {
                _accentColor = value;
                OnPropertyChanged();
            }
        }

        public Lesson Lesson { get; }

        public string TimeSlotFrom { get; set; }
        public string TimeSlotTo { get; set; }

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public LessonViewModel(Lesson lesson)
        {
            Lesson = lesson;

            SetColors();
            SetTimeSlot();
        }

        #endregion

        #region Private Methods

        private void SetColors()
        {
            var lessonName = Lesson.Name;
            var seed = lessonName.GetHashCode();

            var colorGenerator = new RandomColorGenerator(seed);
            AccentColor = colorGenerator.Generate(1);
            BackgroundColor = colorGenerator.Generate(0.05);
        }

        private void SetTimeSlot()
        {
            var timeSlotFrom = string.Empty;
            var timeSlotTo = string.Empty;

            switch (Lesson.Number)
            {
                case 1:
                    timeSlotFrom = "08:00";
                    timeSlotTo = "9:20";
                    break;
                case 2:
                    timeSlotFrom = "09:30";
                    timeSlotTo = "10:50";
                    break;
                case 3:
                    timeSlotFrom = "11:10";
                    timeSlotTo = "12:30";
                    break;
                case 4:
                    timeSlotFrom = "13:00";
                    timeSlotTo = "14:20";
                    break;
                case 5:
                    timeSlotFrom = "14:40";
                    timeSlotTo = "16:00";
                    break;
                case 6:
                    timeSlotFrom = "16:10";
                    timeSlotTo = "17:30";
                    break;
                case 7:
                    timeSlotFrom = "17:40";
                    timeSlotTo = "19:00";
                    break;
            }

            TimeSlotFrom = timeSlotFrom;
            TimeSlotTo = timeSlotTo;
        }

        #endregion
    }
}
