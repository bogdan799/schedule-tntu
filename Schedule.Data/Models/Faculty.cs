using System.Collections.Generic;

namespace Schedule.Data.Models
{
    public sealed class Faculty : ScheduleItem
    {
        #region Properties

        public List<Group> Groups { get; set; }

        #endregion
    }
}