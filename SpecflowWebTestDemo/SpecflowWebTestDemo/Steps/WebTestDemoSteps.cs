using FluentAssertions;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace SpecflowWebTestDemo.Steps
{
    [Binding]
    public class WebTestDemoSteps
    {
        private ChromeDriver chromeDriver = new ChromeDriver();

        [Given(@"I visit JD website")]
        public void GivenIVisitJDWebsite()
        {
            chromeDriver.Navigate().GoToUrl("https://www.jd.com/");
        }

        [Given(@"I have entered ""(.*)"" into the search box")]
        public void GivenIHaveEnteredIntoTheSearchBox(string p0)
        {
            chromeDriver.FindElementById("key").SendKeys(p0);
        }

        [When(@"I press search")]
        public void WhenIPressSearch()
        {
            chromeDriver.FindElementByXPath("//button[@aria-label=\"搜索\"]").Click();
        }

        [Then(@"the result should be ""(.*)"" on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(string p0)
        {
            chromeDriver.FindElementByXPath("//a[text()=\"戴尔京东自营官方旗舰店\"]").Text.Should().Be("戴尔京东自营官方旗舰店");
            Quit();
        }

        public void Quit()
        {
            chromeDriver.Quit();
        }
    }
}
