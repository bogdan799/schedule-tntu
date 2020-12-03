using System.Collections.Generic;
using System.Runtime.Serialization;
using Schedule.Data.Models;

namespace Schedule.DataParser
{
    public sealed class IdGenerator
    {
        private ObjectIDGenerator _generator;

        public void FillGeneratedIds(Data.Models.Schedule schedule)
        {
            _generator = new ObjectIDGenerator();

            FillFacultiesIds(schedule.Faculties);
        }

        private void FillFacultiesIds(IEnumerable<Faculty> faculties)
        {
            foreach (var faculty in faculties)
            {
                faculty.Id = (int)_generator.GetId(faculty, out _);

                FillGroupIds(faculty.Groups);
            }
        }

        private void FillGroupIds(IEnumerable<Group> groups)
        {
            foreach (var group in groups)
            {
                group.Id = (int)_generator.GetId(group, out _);

                FillLessonIds(group.Lessons);
            }
        }

        private void FillLessonIds(IEnumerable<Lesson> lessons)
        {
            foreach (var lesson in lessons)
            {
                lesson.Id = (int)_generator.GetId(lesson, out _);
            }
        }
    }
}
