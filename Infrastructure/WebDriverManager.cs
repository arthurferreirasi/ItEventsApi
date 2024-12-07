using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RpaAlura.Infrastructure
{
    public class WebDriverManager : IDisposable
    {
        private IWebDriver _driver;

        public IWebDriver GetDriver()
        {
            if (_driver == null)
            {
                var options = new ChromeOptions();
                options.AddArgument("--headless"); // Comment here if you want to see the magic happening
                options.AddArgument("--disable-gpu");
                _driver = new ChromeDriver(options);
            }
            return _driver;
        }

        public void Dispose()
        {
            _driver?.Quit();
            _driver?.Close();
        }
    }
}
