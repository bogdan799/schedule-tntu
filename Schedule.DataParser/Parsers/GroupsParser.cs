using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Schedule.Data.Models;

namespace Schedule.DataParser.Parsers
{
    public sealed class GroupsParser : ParserBase
    {
        #region Public Methods

        public async Task<List<Group>> ParseGroupsAsync(string url)
        {
            var document = await GetDocumentAsync(url).ConfigureAwait(false);
            var groups = new List<Group>();

            foreach (var batches in document.QuerySelectorAll("#GroupsList a").Batch(10))
            {
                var result = await Task.WhenAll(batches.Select(ParseGroupAsync)).ConfigureAwait(false);
                groups.AddRange(result);
            }

            return groups;
        }

        #endregion

        #region Private Methods

        private async Task<Group> ParseGroupAsync(IElement element)
        {
            var url = element.GetAttribute("href");
            var lessonParser = new LessonsParser();
            var group = new Group
            {
                Name = element.InnerHtml.Trim(),
                SelfUrl = Url(url),
                Lessons = await lessonParser.ParseLessonsAsync(url).ConfigureAwait(false)
            };

            return group;
        }

        #endregion
    }
}