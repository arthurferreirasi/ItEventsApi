using System.Collections.ObjectModel;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RpaAlura.Infrastructure;

public class SeleniumUtilService : ISeleniumUtilService
{
    private readonly WebDriverManager _driverManager;
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public SeleniumUtilService(WebDriverManager driverManager)
    {
        _driverManager = driverManager;
        _driver = _driverManager.GetDriver();
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    public void OpenBrowser(string path)
    {
        try
        {
            _driver.Navigate().GoToUrl(path);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while navigating to url: {path}.", ex);
        }
    }

    public bool CheckExistsByXpath(string xpath)
    {
        try
        {
            return _wait.Until(driver => driver.FindElement(By.XPath(xpath)).Enabled && driver.FindElement(By.XPath(xpath)).Displayed);
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to identify or verify element: {xpath}.", ex);
        }
    }

    public bool CheckExistsXpathOnHtml(HtmlDocument htmlDoc, string xpath)
    {
        try
        {
            var data = htmlDoc.DocumentNode.SelectSingleNode(xpath);
            return (data != null) ? true : false;
        }
        catch 
        {
            return false;
        }
    }

    public void ClickByXpath(string xpath)
    {
        try
        {
            _driver.FindElement(By.XPath(xpath)).Click();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while clicking on element: {xpath}.", ex);
        }
    }

    public IWebElement GetElementLinkByCssSelector(IWebElement element, string cssSelector)
    {
        try
        {
            var link = element.FindElement(By.CssSelector(cssSelector));
            return link;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting link by selector.", ex);
        }
    }

    public ReadOnlyCollection<IWebElement> GetWebElementByCssSelector(string cssSelector)
    {
        try
        {
            return _driver.FindElements(By.CssSelector(cssSelector));
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while identifying and getting element: {cssSelector}", ex);
        }
    }

    public HtmlDocument GetHtmlDocumentFromUrl(string url)
    {
        try
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            return htmlDoc;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Html page.", ex);
        }
    }

    public HtmlNode GetDataFromHtmlDoc(HtmlDocument htmlDoc, string xPath)
    {
        try
        {
            var data = htmlDoc.DocumentNode.SelectSingleNode(xPath);
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting element from Html", ex);
        }
    }

}
