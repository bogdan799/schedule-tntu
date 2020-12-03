using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io;

namespace Schedule.DataParser.Parsers
{
    public class ParserBase
    {
        #region Declarations

        public const string Colspan = "colspan";
        public const string Rowspan = "rowspan";

        public const string BaseUrl = "http://tntu.edu.ua/";

        #endregion

        #region Private Methods

        protected Task<IDocument> GetDocumentAsync(string url)
        {
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            return context.OpenAsync(new DocumentRequest(new Url(Url(url))));
        }

        protected string Url(string url)
        {
            if (!url.StartsWith("http"))
            {
                return BaseUrl + url;
            }

            return url;
        }

        #endregion
    }
}