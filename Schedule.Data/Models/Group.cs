using System.Collections.Generic;

namespace Schedule.Data.Models
{
    public sealed class Group : ScheduleItem
    {
        #region Properties

        public List<Lesson> Lessons { get; set; }

        #endregion
    }
}