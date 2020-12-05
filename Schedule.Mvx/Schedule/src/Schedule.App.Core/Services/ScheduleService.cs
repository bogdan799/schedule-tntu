using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Schedule.Data.Models;

namespace Schedule.App.Core.Services
{
    public sealed class ScheduleService : IScheduleService
    {
        #region Declarations

        private const string JsonFileName = "schedule.json";
        private List<Faculty> _faculties;
        private Dictionary<int, List<Group>> _groups;
        private Dictionary<int, List<Lesson>> _lessons;
        private Data.Models.Schedule _schedule;

        #endregion

        #region Constructors

        public ScheduleService()
        {
            ReadSchedule();
        }

        #endregion

        #region Public Methods

        public Task<List<Faculty>> GetFacultiesAsync()
        {
            return Task.FromResult(_faculties);
        }

        public Task<List<Group>> GetGroupsByFacultyAsync(int facultyId)
        {
            return Task.FromResult(_groups[facultyId]);
        }

        public Task<List<Lesson>> GetLessonsByGroupIdAsync(int groupId)
        {
            return Task.FromResult(_lessons[groupId]);
        }

        public Task<Group> GetGroupByIdAsync(int groupId)
        {
            return Task.FromResult(_groups.SelectMany(g => g.Value).FirstOrDefault(g => g.Id == groupId));
        }

        #endregion

        #region Private Methods

        private void FillCollections(Data.Models.Schedule schedule)
        {
            _faculties = schedule.Faculties.Select(f => new Faculty {Name = f.Name, SelfUrl = f.SelfUrl, Id = f.Id})
                .ToList();

            _groups = schedule.Faculties.SelectMany(f => f.Groups.Select(g =>
                    new Group {Name = g.Name, SelfUrl = g.SelfUrl, Id = g.Id, FacultyId = f.Id}))
                .GroupBy(g => g.FacultyId)
                .ToDictionary(g => g.Key, g => g.ToList());

            _lessons = schedule.Faculties.SelectMany(f => f.Groups).SelectMany(g =>
            {
                g.Lessons.ForEach(l => l.GroupId = g.Id);
                return g.Lessons;
            }).GroupBy(l => l.GroupId).ToDictionary(l => l.Key, l => l.ToList());
        }

        private void ReadSchedule()
        {
            Assembly assembly = GetType().Assembly;
            using Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{JsonFileName}");
            if (stream == null)
            {
                return;
            }

            using var reader = new StreamReader(stream);
            var jsonString = reader.ReadToEnd();
            _schedule = JsonConvert.DeserializeObject<Data.Models.Schedule>(jsonString);
            FillCollections(_schedule);
        }

        #endregion
    }
}
