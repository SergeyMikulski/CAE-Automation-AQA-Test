using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAE_TestProject.Helpers;
using OpenQA.Selenium.Support.UI;

namespace CAE_TestProject
{
    public class TestsBase
    {
        public IWebDriver _driver;
        public WebDriverWait _wait;

        public TestsBase()
        {

        }


        [SetUp]
        public void Setup()
        {
            var driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            _driver = driver;
            _wait = wait;

            _driver.Navigate().GoToUrl("https://www.21vek.by/");
            AlertsCloseHelper.CookiesAlertClose(_driver, _wait);
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
        }
    }
}
