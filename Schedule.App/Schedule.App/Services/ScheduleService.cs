using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Schedule.Data.Models;

namespace Schedule.App.Services
{
    public sealed class ScheduleService
    {
        private Data.Models.Schedule _schedule;
        private List<Faculty> _faculties;
        private Dictionary<int, List<Group>> _groups;
        private Dictionary<int, List<Lesson>> _lessons;
        private const string JsonFileName = "schedule.json";

        public ScheduleService()
        {
            ReadSchedule();
        }

        private void ReadSchedule()
        {
            var assembly = GetType().Assembly;
            using (var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{JsonFileName}"))
            {
                if (stream == null)
                {
                    return;
                }

                using (var reader = new System.IO.StreamReader(stream))
                {
                    var jsonString = reader.ReadToEnd();
                    _schedule = JsonConvert.DeserializeObject<Data.Models.Schedule>(jsonString);
                    FillCollections(_schedule);
                }
            }
        }

        private void FillCollections(Data.Models.Schedule schedule)
        {
            _faculties = schedule.Faculties.Select(f => new Faculty
            {
                Name = f.Name,
                SelfUrl = f.SelfUrl,
                Id = f.Id
            }).ToList();

            _groups = schedule.Faculties.SelectMany(f => f.Groups.Select(g =>
                new Group
                {
                    Name = g.Name,
                    SelfUrl = g.SelfUrl,
                    Id = g.Id,
                    FacultyId = f.Id
                })).GroupBy(g => g.FacultyId).ToDictionary(g => g.Key, g => g.ToList());

            _lessons = schedule.Faculties.SelectMany(f => f.Groups).SelectMany(g =>
            {
                g.Lessons.ForEach(l => l.GroupId = g.Id);
                return g.Lessons;
            }).GroupBy(l => l.GroupId).ToDictionary(l => l.Key, l => l.ToList());
        }

        public IEnumerable<Faculty> GetFaculties()
        {
            return _faculties;
        }

        public IEnumerable<Group> GetGroupsByFaculty(int facultyId)
        {
            return _groups[facultyId];
        }

        public IEnumerable<Lesson> GetLessonsByGroupId(int groupId)
        {
            return _lessons[groupId];
        }
    }
}
