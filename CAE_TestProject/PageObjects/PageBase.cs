using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAE_TestProject.PageObjects
{
    public class PageBase
    {
        public IWebDriver _driver;
        public WebDriverWait _wait;

        public PageBase(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }
    }
}
