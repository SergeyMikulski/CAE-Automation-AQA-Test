using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V124.Network;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAE_TestProject.Helpers
{
    internal static class AlertsCloseHelper
    {
        public static void CookiesAlertClose(IWebDriver driver, WebDriverWait wait)
        {
            if (cookieAlertContainer(driver) != null)
            {
                var cookieAlertAcceptButton = cookieAlertContainer(driver).FindElement(By.XPath(".//*[contains(@class, 'Button-module__blue-primary')]"));
                cookieAlertAcceptButton.Click();
                wait.Until(x => cookieAlertContainer(driver) == null);
            }
        }

        private static IWebElement cookieAlertContainer(IWebDriver driver) => driver.FindElements(By.XPath(".//*[contains(@class, 'AgreementCookie_buttons')]")).FirstOrDefault();
    }
}
