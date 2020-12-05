using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Schedule.Data.Models;

namespace Schedule.DataParser.Parsers
{
    public sealed class LessonsParser : ParserBase
    {
        #region Declarations

        private Dictionary<int, DayOfWeek> _columnDictionary;

        private int _columns;
        private Dictionary<int, GroupMode> _groupModeDictionary;
        private bool[,] _matrix;
        private Dictionary<int, int> _rowDictionary;
        private int _rows;
        private Dictionary<int, WeekMode> _weeksDictionary;

        #endregion

        #region Public Methods

        public async Task<List<Lesson>> ParseLessonsAsync(string url)
        {
            var document = await GetDocumentAsync(url).ConfigureAwait(false);
            var list = new List<Lesson>();

            ParseColumnsDictionary(document);
            ParseRowsDictionary(document);
            CreateMatrix();

            var currentRow = 0;
            foreach (var row in document.QuerySelectorAll("#ScheduleWeek > tbody > tr"))
            {
                var cells = row.QuerySelectorAll("td").Where(td => td.ClassName == null || !td.ClassName.StartsWith("LessonNumber")).ToArray();
                var currentCol = 0;
                foreach (var cell in cells)
                {
                    (currentRow, currentCol) = FindNextPosition(currentRow, currentCol);

                    var colspan = GetColSpan(cell);
                    var rowspan = GetRowSpan(cell);

                    MarkMatrix(currentRow, currentCol, rowspan, colspan);

                    if (cell.ChildNodes.Length > 1)
                    {
                        var lesson = ParseLesson(cell);

                        lesson.Day = _columnDictionary[currentCol];
                        lesson.GroupMode = _groupModeDictionary[currentCol];

                        lesson.WeekMode = _weeksDictionary[currentRow];
                        lesson.Number = _rowDictionary[currentRow];

                        if (lesson.WeekMode == WeekMode.FirstWeek && rowspan > 1)
                        {
                            lesson.WeekMode = WeekMode.AllWeeks;
                        }

                        if (lesson.GroupMode == GroupMode.FirstGroup && colspan == _columnDictionary.Values.Count(v => v == lesson.Day))
                        {
                            lesson.GroupMode = GroupMode.AllGroups;
                        }

                        list.Add(lesson);
                    }

                    currentCol++;
                }

                currentRow++;
            }

            return list;
        }

        #endregion

        #region Private Methods

        private void CreateMatrix()
        {
            _matrix = new bool[_rows, _columns];
        }

        private (int, int) FindNextPosition(int currentRow, int currentCol)
        {
            for (var i = currentCol; i < _columns; i++)
            {
                if (!_matrix[currentRow, i])
                {
                    return (currentRow, i);
                }
            }

            throw new Exception("Schedule table has unpredictable data. Failed to parse");
        }

        private int GetAttribute(IElement element, string attributeName, int defaultValue = 1)
        {
            if (element.HasAttribute(attributeName))
            {
                return Int32.Parse(element.GetAttribute(attributeName));
            }

            return defaultValue;
        }

        private int GetColSpan(IElement element)
        {
            return GetAttribute(element, Colspan);
        }

        private int GetRowSpan(IElement element)
        {
            return GetAttribute(element, Rowspan);
        }

        private void MarkMatrix(int currentRow, int currentCol, int rowspan, int colspan)
        {
            for (var i = currentRow; i < currentRow + rowspan; i++)
            {
                for (var j = currentCol; j < currentCol + colspan; j++)
                {
                    _matrix[i, j] = true;
                }
            }
        }

        private void ParseColumnsDictionary(IDocument document)
        {
            var headers = document.QuerySelectorAll("#ScheduleWeek th")
                .Where(h => !String.IsNullOrEmpty(h.InnerHtml.Trim()));
            _columnDictionary = new Dictionary<int, DayOfWeek>();
            _groupModeDictionary = new Dictionary<int, GroupMode>();

            var currentColumn = 0;
            var day = DayOfWeek.Monday;
            foreach (var element in headers)
            {
                if (!element.HasAttribute(Colspan))
                {
                    _columnDictionary[currentColumn] = day;
                    _groupModeDictionary[currentColumn] = GroupMode.AllGroups;
                    currentColumn++;
                }
                else
                {
                    var columns = Int32.Parse(element.GetAttribute(Colspan));
                    var groupMode = GroupMode.FirstGroup;
                    for (var i = 0; i < columns; i++, currentColumn++)
                    {
                        _columnDictionary[currentColumn] = day;
                        _groupModeDictionary[currentColumn] = groupMode;
                        groupMode++;
                    }
                }

                day++;
            }

            _columns = _columnDictionary.Count;
        }

        private Lesson ParseLesson(IElement element)
        {
            var divs = element.QuerySelectorAll("div");

            if (divs.Length < 2)
            {
                Debugger.Break();
            }
            var firstDiv = divs[0];
            var secondDiv = divs[1];

            var reference = firstDiv.ChildNodes.Any(n => n.NodeType == NodeType.Element)
                ? firstDiv.QuerySelector("a")
                : firstDiv;
            var texts = secondDiv.ChildNodes.Where(n => n.NodeType == NodeType.Text).ToArray();
            return new Lesson
            {
                Name = reference.InnerHtml.Trim(),
                SelfUrl = reference.HasAttribute("href") ? reference.GetAttribute("href") : null,
                Type = texts.First().TextContent.Trim(),
                Location = texts.Last().TextContent.Trim()
            };
        }

        private void ParseRowsDictionary(IDocument document)
        {
            var rows = document.QuerySelectorAll("#ScheduleWeek > tbody > tr").ToArray();
            _rowDictionary = new Dictionary<int, int>();
            _weeksDictionary = new Dictionary<int, WeekMode>();

            var lectureNumber = 1;
            for (var i = 0; i < rows.Length; i++)
            {
                var row = rows[i];
                _rowDictionary[i] = lectureNumber;
                if (row.QuerySelectorAll("td").Any(td => td.HasAttribute(Rowspan)))
                {
                    _weeksDictionary[i] = WeekMode.FirstWeek;
                    _rowDictionary[++i] = lectureNumber;
                    _weeksDictionary[i] = WeekMode.SecondWeek;
                }
                else
                {
                    _weeksDictionary[i] = WeekMode.AllWeeks;
                }

                lectureNumber++;
            }

            _rows = _rowDictionary.Count;
        }

        #endregion
    }
}