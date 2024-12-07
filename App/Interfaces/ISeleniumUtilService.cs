
using System.Collections.ObjectModel;
using HtmlAgilityPack;
using OpenQA.Selenium;

public interface ISeleniumUtilService
{
    void OpenBrowser(string path);
    bool CheckExistsByXpath(string xpath);
    ReadOnlyCollection<IWebElement> GetWebElementByCssSelector(string cssSelector);
    HtmlDocument GetHtmlDocumentFromUrl(string url);
    string GetDataFromHtmlDoc(HtmlDocument htmlDoc, string xPath);
    IWebElement GetElementLinkByCssSelector(IWebElement element,string cssSelector);
}