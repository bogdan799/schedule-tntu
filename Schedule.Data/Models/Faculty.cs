using System;
using System.Collections.Generic;

namespace Schedule.Data.Models
{
    public sealed class Faculty : ScheduleItem
    {
        #region Properties

        public List<Group> Groups { get; set; }

        public string ShortName
        {
            get
            {
                var startIdx = Name.IndexOf('(');
                var endIdx = Name.IndexOf(')');

                if (startIdx != -1 && endIdx != -1 && startIdx < endIdx && endIdx < Name.Length)
                {
                    return Name.Substring(startIdx + 1, endIdx - startIdx - 1);
                }

                return String.Empty;
            }
        }

        #endregion
    }
}