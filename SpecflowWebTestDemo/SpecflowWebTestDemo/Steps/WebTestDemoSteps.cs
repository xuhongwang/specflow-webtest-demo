using FluentAssertions;
using OpenQA.Selenium;
using SpecflowWebTestDemo.Drivers;
using TechTalk.SpecFlow;

namespace SpecflowWebTestDemo.Steps
{
    [Binding]
    public class WebTestDemoSteps
    {
        private readonly WebDrivers _webDrivers;
        public WebTestDemoSteps(WebDrivers webDrivers)
        {
            _webDrivers = webDrivers;
        }

        [Given(@"I visit JD website")]
        public void GivenIVisitJDWebsite()
        {
            _webDrivers.Current.Navigate().GoToUrl("https://www.jd.com/");
        }

        [Given(@"I have entered ""(.*)"" into the search box")]
        public void GivenIHaveEnteredIntoTheSearchBox(string p0)
        {
            _webDrivers.Current.FindElement(By.Id("key")).SendKeys(p0);
        }

        [When(@"I press search")]
        public void WhenIPressSearch()
        {
            _webDrivers.Current.FindElement(By.XPath("//button[@aria-label=\"搜索\"]")).Click();
            System.Threading.Thread.Sleep(5000);
        }

        [Then(@"the result should be ""(.*)"" on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(string p0)
        {
            _webDrivers.Current.FindElement(By.XPath("//a[text()=\"戴尔京东自营官方旗舰店\"]")).Text.Should().Be("戴尔京东自营官方旗舰店");
            _webDrivers.Current.Quit();
        }
    }
}
