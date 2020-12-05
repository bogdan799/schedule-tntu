using System.Collections.Generic;
using System.Threading.Tasks;
using Schedule.Data.Models;

namespace Schedule.App.Core.Services
{
    public interface IScheduleService
    {
        Task<List<Faculty>> GetFacultiesAsync();

        Task<List<Group>> GetGroupsByFacultyAsync(int facultyId);

        Task<List<Lesson>> GetLessonsByGroupIdAsync(int groupId);

        Task<Group> GetGroupByIdAsync(int groupId);
    }
}
