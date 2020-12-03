using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Schedule.Data.Models;

namespace Schedule.DataParser.Parsers
{
    public sealed class FacultiesParser : ParserBase
    {
        #region Declarations

        private const string ScheduleUrl = "http://tntu.edu.ua/?p=uk/schedule";

        #endregion

        #region Public Methods

        public async Task<List<Faculty>> ParseFacultiesAsync()
        {
            var document = await GetDocumentAsync(ScheduleUrl).ConfigureAwait(false);

            var array = await Task.WhenAll(
                    document.QuerySelectorAll("#FacultiesFullList a")
                        .Select(ParseFacultyAsync))
                .ConfigureAwait(false);
            return array.ToList();
        }

        #endregion

        #region Private Methods

        private async Task<Faculty> ParseFacultyAsync(IElement element)
        {
            var groupsParser = new GroupsParser();
            var selfUrl = element.GetAttribute("href");
            var faculty = new Faculty
            {
                Name = element.FirstElementChild.InnerHtml.Trim(),
                SelfUrl = Url(selfUrl),
                Groups = await groupsParser.ParseGroupsAsync(selfUrl).ConfigureAwait(false)
            };

            return faculty;
        }

        #endregion
    }
}