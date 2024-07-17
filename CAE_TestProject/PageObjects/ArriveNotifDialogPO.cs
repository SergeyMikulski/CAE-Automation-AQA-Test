using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CAE_TestProject.PageObjects
{
    public class ArriveNotifDialogPO : PageBase
    {
        public ArriveNotifDialogPO(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        string requiredMessageText = "Это поле обязательно для заполнения";
        string orderAlertText = "Если товар появится на складе, вам придет сообщение на почту.";

        private IWebElement ArriveNotifDialogContainer => _driver.FindElement(By.XPath("(.//div[contains(@class,'ui-dialog')]//*[contains(text(),'Узнать о поступлении')])[last()]/../.."));

        private IWebElement SendCloseButton => ArriveNotifDialogContainer.FindElement(By.TagName("button"));

        private IWebElement NameFieldInput => ArriveNotifDialogContainer.FindElement(By.XPath(".//input[@type='text']"));
        private IWebElement EmailFieldInput => ArriveNotifDialogContainer.FindElement(By.XPath(".//input[@type='email']"));

        public void SendCloseButtonClick() => SendCloseButton.Click();

        public void CheckBothFieldsRequired() => ArriveNotifDialogContainer.FindElements(By.XPath($"//*[contains(text(), '{requiredMessageText}')]")).Count().Should().Be(2);

        public void EnterName(string name) => NameFieldInput.SendKeys(name);
        public void EnterEmail(string email) => EmailFieldInput.SendKeys(email);

        public void CheckOrderMessageShown() => ArriveNotifDialogContainer.FindElement(By.XPath($".//*[contains(text(),'{orderAlertText}')]"));
    }
}
