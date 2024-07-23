using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAE_TestProject.PageObjects
{
    public class LandingPagePO : PageBase
    {
 
        public LandingPagePO(IWebDriver driver, WebDriverWait wait): base(driver, wait)
        {
        }

        private IWebElement SearchContainer => _driver.FindElement(By.XPath(".//div[contains(@class, 'Search_searchInputContainer')]"));

        private IWebElement GoodsSearchField => SearchContainer.FindElement(By.Id("catalogSearch"));

        private IWebElement CatalogueButton => _driver.FindElement(By.XPath(".//*[contains(@class, 'styles_catalogButton')]"));

        public void DoSearch(string searchText)
        {
            GoodsSearchField.SendKeys(searchText);
            GoodsSearchField.SendKeys(Keys.Enter);
        }

        public void CatalogButtonClick()
        {
            CatalogueButton.Click();
        }
    }
}
