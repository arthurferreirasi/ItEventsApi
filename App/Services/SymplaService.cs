using System.Collections.ObjectModel;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V129.Debugger;

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
            var listElements = _util.GetWebElementByCssSelector("body > main > div > div.zbqpbg3 > div > div._1g71xxu0._1g71xxu1");
            if (listElements.Count > 0)
            {
                events = this.AddEventsToList(events, listElements);
            }
            return events;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private List<Events> AddEventsToList(List<Events> events, ReadOnlyCollection<IWebElement> listElements)
    {
        try
        {
            foreach (var element in listElements)
            {
                var linkElement = _util.GetElementLinkByCssSelector(element, "a.sympla-card pn67h10 pn67h11");
                var urlItem = linkElement.GetDomAttribute("href");
                var htmlItem = _util.GetHtmlDocumentFromUrl(urlItem);

                events.Add(new Events()
                {
                    Name = _util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/div[1]/section/div/div/h1"),
                    Description = _util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/section[3]/div/div/div[1]"),
                    Location = _util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/div[1]/section/div/div/div[2]/div/span"),
                    StartDate = this.FormatDate( _util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/div[1]/section/div/div/div[1]/div/p"), true),
                    EndDate = this.FormatDate(_util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/div[1]/section/div/div/div[1]/div/p"), false),
                    IsOnline = _util.GetDataFromHtmlDoc(htmlItem, "//*[@id='__next']/div[1]/section/div/div/div[2]/div/span").Contains("Evento Online")? true : false,
                });
            }
            return events;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private DateTime FormatDate(string inputDate, bool isStart)
    {
        string[] parts = inputDate.Split('>');
        string dateFormat = "dd MMM - yyyy • HH:mm";
        if (isStart)
        {
            return DateTime.ParseExact(parts[0].Trim(), dateFormat, null);
        }
        else
        {
            return DateTime.ParseExact(parts[1].Trim(), dateFormat, null);
        }
    }

}