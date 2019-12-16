using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Management;

namespace SpecflowWebTestDemo.Drivers
{
    public class WebDrivers
    {
        private string OS = Distinguish64Or32System();
        private IWebDriver _currentWebDriver;
        private string _browserDriver;

        public static WebDrivers webDrivers = new WebDrivers();

        public IWebDriver Current
        {
            get
            {
                if (_currentWebDriver == null)
                {
                    _currentWebDriver = GetWebDriver();
                    _currentWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(30);
                }

                return _currentWebDriver;
            }
        }

        private IWebDriver GetWebDriver()
        {
            switch (Environment.GetEnvironmentVariable("Test_Browser"))
            {
                case "IE":
                    if (OS == "64")
                    {
                        _browserDriver = AppDomain.CurrentDomain.BaseDirectory + @"\BrowserDrivers\IE\64";
                    }
                    else if (OS == "32")
                    {
                        _browserDriver = AppDomain.CurrentDomain.BaseDirectory + @"\BrowserDrivers\IE\32";
                    }
                    else
                    {
                        throw new NotSupportedException("browser is not a supported browser");
                    }

                    InternetExplorerOptions internetExplorerOptions = new InternetExplorerOptions();
                    internetExplorerOptions.IgnoreZoomLevel = true;
                    InternetExplorerDriver internetExplorerDriver = new InternetExplorerDriver(_browserDriver, internetExplorerOptions, TimeSpan.FromMinutes(30));

                    return internetExplorerDriver;
                case "Edge":
                    _browserDriver = AppDomain.CurrentDomain.BaseDirectory + @"\BrowserDrivers\Edge";
                    EdgeOptions edgeOptions = new EdgeOptions();
                    EdgeDriver edgeDriver = new EdgeDriver(_browserDriver, edgeOptions, TimeSpan.FromMinutes(30));

                    return edgeDriver;
                case "Chrome":
                    _browserDriver = AppDomain.CurrentDomain.BaseDirectory + @"\BrowserDrivers\Chrome";
                    ChromeOptions chromeOptions = new ChromeOptions();
                    ChromeDriver chromeDriver = new ChromeDriver(_browserDriver, chromeOptions, TimeSpan.FromMinutes(30));

                    return chromeDriver;
                case "Firefox":
                    _browserDriver = AppDomain.CurrentDomain.BaseDirectory + @"\BrowserDrivers\Firefox\";
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    FirefoxDriver firefoxDriver = new FirefoxDriver(_browserDriver, firefoxOptions, TimeSpan.FromMinutes(30));

                    return firefoxDriver;
                case string browser:
                    throw new NotSupportedException($"{browser} is not a supported browser");
                default: throw new NotSupportedException("not supported browser: <null>");
            }
        }
        /// <summary>
        /// Get System is 64 or 32
        /// </summary>
        /// <returns>64/32</returns>
        public static string Distinguish64Or32System()
        {
            try
            {
                string addressWidth = String.Empty;
                ConnectionOptions mConnOption = new ConnectionOptions();
                ManagementScope mMs = new ManagementScope(@"\\localhost", mConnOption);
                ObjectQuery mQuery = new ObjectQuery("select AddressWidth from Win32_Processor");
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(mMs, mQuery);
                ManagementObjectCollection mObjectCollection = mSearcher.Get();
                foreach (ManagementObject mObject in mObjectCollection)
                {
                    addressWidth = mObject["AddressWidth"].ToString();
                }
                return addressWidth;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return String.Empty;
            }

        }
    }
}
