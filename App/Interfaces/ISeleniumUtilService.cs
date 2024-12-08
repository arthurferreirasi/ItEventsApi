
using System.Collections.ObjectModel;
using HtmlAgilityPack;
using OpenQA.Selenium;

public interface ISeleniumUtilService
{
    void OpenBrowser(string path);
    bool CheckExistsByXpath(string xpath);
    bool CheckExistsXpathOnHtml(HtmlDocument htmlDoc, string xpath);
    void ClickByXpath(string xpath);
    ReadOnlyCollection<IWebElement> GetWebElementByCssSelector(string cssSelector);
    HtmlDocument GetHtmlDocumentFromUrl(string url);
    HtmlNode GetDataFromHtmlDoc(HtmlDocument htmlDoc, string xPath);
    IWebElement GetElementLinkByCssSelector(IWebElement element,string cssSelector);
    void SaveToCsv(List<Events> data, string filePath);
}