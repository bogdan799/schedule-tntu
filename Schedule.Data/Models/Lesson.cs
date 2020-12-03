using System;

namespace Schedule.Data.Models
{
    public sealed class Lesson : ScheduleItem
    {
        #region Properties

        public int GroupId { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }

        public DayOfWeek Day { get; set; }

        public int Number { get; set; }

        public WeekMode WeekMode { get; set; }

        public GroupMode GroupMode { get; set; }

        #endregion
    }
}