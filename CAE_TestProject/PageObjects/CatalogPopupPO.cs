using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace CAE_TestProject.PageObjects
{
    public class CatalogPopupPO :PageBase
    {
        public CatalogPopupPO(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        private IWebElement CatalogPopupContainer => _driver.FindElement(By.XPath(".//*[contains(@data-testid, 'catalogPopup')]"));

        private IWebElement CatalogPopupLeft => CatalogPopupContainer.FindElement(By.XPath(".//*[contains(@class, 'styles_leftContainer')]"));

        private IWebElement CatalogPopupRight => CatalogPopupContainer.FindElement(By.XPath(".//*[contains(@class, 'styles_rightContainer')]"));



        public void LeftCatalogItemHover(string itemName)
        {
            Actions action = new Actions(_driver);
            var itemToHover = CatalogPopupLeft.FindElement(By.XPath($".//*[contains(text(),'{itemName}')]"));
            action.MoveToElement(itemToHover).Build().Perform();
        }

        public void RightCatalogItemClick(string itemName) => CatalogPopupRight.FindElement(By.XPath($".//*[text()='{itemName}']")).Click();
        
    }
}
