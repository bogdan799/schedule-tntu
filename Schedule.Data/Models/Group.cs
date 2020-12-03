using System;
using System.Collections.Generic;

namespace Schedule.Data.Models
{
    public sealed class Group : ScheduleItem
    {
        #region Properties

        public int FacultyId { get; set; }

        public List<Lesson> Lessons { get; set; }

        public int Course
        {
            get
            {
                if (!String.IsNullOrEmpty(Name) && Name.Contains("-"))
                {
                    var idx = Name.IndexOf('-');
                    if (idx < Name.Length - 1)
                    {
                        var course = Name[idx+1].ToString();
                        return Int32.Parse(course);
                    }
                }

                return 0;
            }
        }

        #endregion
    }
}