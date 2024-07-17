using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAE_TestProject.PageObjects
{
    public class SearchResultPagePO : PageBase
    {
        public SearchResultPagePO(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        string goodIsInWaitListMessage = "В листе ожидания";

        private List<IWebElement> GoodsList => _driver.FindElements(By.ClassName("result__item")).ToList();

        private IWebElement goodExpected(string goodToCheck) => GoodsList.Where(x => x.Text.Contains(goodToCheck)).FirstOrDefault();

        private void WaitArriveNotification(string goodToCheck) => _wait.Until(x => goodExpected(goodToCheck).FindElement(By.Id("arive_notif")).Displayed);
        public void ArriveNotificationButtonClick(string goodToCheck)
        {
            WaitArriveNotification(goodToCheck);
            goodExpected(goodToCheck).FindElement(By.Id("arive_notif")).Click();
        }

        public void CheckGoodIsInWaitList(string goodToCheck) => goodExpected(goodToCheck).FindElement(By.XPath(".//*[contains(@class, 'item__notification')]/span")).Text
            .Should().Contain(goodIsInWaitListMessage);
    }
}
