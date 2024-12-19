using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

    public class WebDriverManager : IDisposable
    {
        private IWebDriver _driver;

        public IWebDriver GetDriver()
        {
            if (_driver == null)
            {
                var options = new ChromeOptions();
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                _driver = new ChromeDriver(options);
            }
            return _driver;
        }

        public void Dispose()
        {
            _driver?.Close();
        }
    }

