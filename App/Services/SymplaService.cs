using HtmlAgilityPack;
using Microsoft.Extensions.Options;

public class SymplaService : ISymplaService
{
    private readonly ISeleniumUtilService _util;
    private readonly SymplaSettings _settings;

    public SymplaService(ISeleniumUtilService util, IOptions<SymplaSettings> options)
    {
        _util = util;
        _settings = options.Value;
    }

    public List<Events> GetItEvents(List<Events> events)
    {
        try
        {
            _util.OpenBrowser(_settings.TechnologyAddress);
            if (!_util.CheckExistsByXpath("/html/body/main/div/div[2]/div/div[2]"))
            {
                throw new Exception("Unable to identify events grid");
            };
            var html = _util.GetHtmlDocumentFromUrl(_settings.TechnologyAddress);
            var divNode = _util.GetDataFromHtmlDoc(html, "//div[contains(@class, '_1g71xxu0')]");

            if (divNode != null)
            {
                var linkNodes = divNode.SelectNodes(".//a");

                var linksItem = this.BuildLinksPack(linkNodes);
                events = this.AddEventsToList(events, linksItem);
            }
            else
            {
                throw new Exception("Unable to find grid with all events.");
            }
            return events;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private List<string> BuildLinksPack(HtmlNodeCollection linkNodes)
    {
        List<string> links = new List<string>();
        foreach (var linkNode in linkNodes)
        {
            var href = linkNode.GetAttributeValue("href", string.Empty);
            if (!string.IsNullOrEmpty(href))
            {
                links.Add(href);
            }
        }
        return links;
    }

    private List<Events> AddEventsToList(List<Events> events, List<string> links)
    {
        try
        {
            foreach (var urlItem in links)
            {
                var htmlItem = _util.GetHtmlDocumentFromUrl(urlItem);
                if (!_util.CheckExistsXpathOnHtml(htmlItem, "//div[contains(@class, 'sc-cc6dd638-0 sc-d4d5091a-6 TmdRk')]"))
                {
                    events.Add(new Events()
                    {
                        Name = _util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/div[1]/section/div/div/h1").InnerText,
                        Description = _util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/section[3]/div/div/div[1]").InnerText,
                        Location = _util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/div[1]/section/div/div/div[2]/div/span").InnerText,
                        StartDate = this.FormatDate(_util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/div[1]/section/div/div/div[1]/div/p").InnerText, true),
                        EndDate = this.FormatDate(_util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/div[1]/section/div/div/div[1]/div/p").InnerText, false),
                        IsOnline = _util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/div[1]/section/div/div/div[2]/div/span").InnerText.Contains("Evento Online") ? true : false,
                    });
                }

            }
            return events;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    private bool IsOneDay(string inputDate)
    {
        return !inputDate.Contains(">") && !inputDate.Contains("&gt;");
    }

    private DateTime FormatDate(string inputDate, bool isStart)
    {
        if (IsOneDay(inputDate))
        {
            return DateTime.ParseExact(inputDate, "dd MMM - yyyy • HH:mm", null);
        }
        else
        {
            string separator = inputDate.Contains(">") ? ">" : "&gt;";
            string[] parts = inputDate.Split(separator);
            string date = isStart ? parts[0].Trim() : parts[1].Trim();

            var dicMonth = new Dictionary<string, string>
            {
                { "jan", "01" }, { "fev", "02" }, { "mar", "03" },
                { "abr", "04" }, { "mai", "05" }, { "jun", "06" },
                { "jul", "07" }, { "ago", "08" }, { "set", "09" },
                { "out", "10" }, { "nov", "11" }, { "dez", "12" }
            };

            foreach (var month in dicMonth)
            {
                date = date.Replace(month.Key, month.Value);
            }

            return DateTime.ParseExact(date, "dd MM - yyyy • HH:mm", null);
        }
    }

}