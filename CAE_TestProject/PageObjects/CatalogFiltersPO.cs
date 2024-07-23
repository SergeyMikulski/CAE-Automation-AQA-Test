using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.DevTools.V124.DOM;

namespace CAE_TestProject.PageObjects
{
    public class CatalogFiltersPO : PageBase
    {
        public CatalogFiltersPO(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        private IWebElement FiltersContainer => _driver.FindElement(By.XPath(".//*[@data-testid='filters-block']"));

        private IWebElement PriceMinField => FiltersContainer.FindElement(By.Id("minPrice"));

        private IWebElement PriceMaxField => FiltersContainer.FindElement(By.Id("maxPrice"));

        private IWebElement DeliveryDate(string deliveryDate) => FiltersContainer.FindElement(By.XPath($".//*[@data-testid='short-delivery-terms']//span[contains(text(),'{deliveryDate}')]"));

        private IWebElement ProducersContainer => FiltersContainer.FindElement(By.XPath(".//*[@data-testid='producerBlock']"));

        private List<IWebElement> ProducersShort => ProducersContainer.FindElements(By.TagName("button")).ToList();

        private IWebElement ProducerPopupButton => ProducersContainer.FindElement(By.XPath(".//*[@data-testid='listing-dropdown-extendedProducers']"));

        private IWebElement ProducersExtendedDialog => _driver.FindElements(By.Id("extendedProducers")).FirstOrDefault();

        private IWebElement ProducerExtendedName(string producerToFind) => ProducersExtendedDialog.FindElement(By.XPath($".//*[contains(@class,'Text-module__text') and text()='{producerToFind}']"));

        private IWebElement SortingHeader => FiltersContainer.FindElement(By.XPath(".//*[contains(@class, 'ListingFilters_filterTitle') and text()='Сортировка']"));



        private IWebElement DinamicFiltersContainer => FiltersContainer.FindElement(By.XPath(".//*[@data-testid='dynamic-filters']"));

        private List<IWebElement> DinamicFiltersCheckbox()
        {
            try
            {
                return DinamicFiltersContainer.FindElements(By.TagName("button")).ToList();
            }
            catch (StaleElementReferenceException)
            {
                return DinamicFiltersContainer.FindElements(By.TagName("button")).ToList();
            }
        }

        private IWebElement DinamicFiltersExtendedValuesButton(string headerName) => DinamicFiltersContainer.FindElement(By.XPath(
            $".//*[contains(@class,'ListingFilters_filterTitle') and contains(text(), '{headerName}')]/..//*[contains(text(),'Посмотреть все')]/.."));

        private IWebElement DinamicFiltersExtendedValuesContainer => _driver.FindElement(By.XPath(".//*[contains(@id, 'extended-attribute')]//*[@aria-label='grid']"));

        private IWebElement DinamicFiltersExtendedValuesCheckbox(string valueNeeded) => DinamicFiltersExtendedValuesContainer.FindElement(By.XPath($".//*[text()='{valueNeeded}']/../../.."));

        private IWebElement DinamicFiltersDropdownsArrow(string headerName, string textFromOrTo) => DinamicFiltersContainer.FindElement(By.XPath(
            $".//*[contains(@class,'ListingFilters_filterTitle') and contains(text(), '{headerName}')]/..//*[contains(text(),'{textFromOrTo}')]/..//*[contains(@class,'select__indicators')]"));

        private IWebElement DinamicFiltersDropdownContainer(string headerName, string textFromOrTo) => DinamicFiltersDropdownsArrow(headerName, textFromOrTo).FindElement(By.XPath(
            "./../../../..//*[contains(@class,'select__menu-list')]"));

        private IWebElement DinamicFiltersDropdownValue(string valueToChoose, string headerName, string textFromOrTo) => DinamicFiltersDropdownContainer(headerName, textFromOrTo)
            .FindElement(By.XPath($".//*[contains(text(),'{valueToChoose}')]"));

        private IWebElement DinamicFiltersTextField(string headerName, string textFromOrTo) => DinamicFiltersContainer.FindElement(By.XPath(
            $".//*[contains(@class,'ListingFilters_filterTitle') and contains(text(), '{headerName}')]/..//*[contains(text(),'{textFromOrTo}')]/..//input"));

        private IWebElement? FilterResultAlert => _driver.FindElements(By.Id("snackbar-container")).FirstOrDefault();






        private void PriceMinType(string value) => PriceMinField.SendKeys(value);

        private void PriceMaxType(string value) => PriceMaxField.SendKeys(value);

        private void DeliveryDateChoose(string deliveryDate) => DeliveryDate(deliveryDate).Click();

        private void ChooseProducer(List<string> producerNames)
        {
            //var yyy = ProducersContainer.Text;
            //var catalogSearch = _driver.FindElement(By.Id("catalogSearch"));

            //var ttt = ProducersShort.Select(x => x.Text);
            //foreach(var t in ttt)
            //    catalogSearch.SendKeys(t);
            //var elementWithProducer = ProducersShort.SingleOrDefault(x => x.FindElement(By.XPath(".//*[text()='producerName']")).Displayed);
            foreach (var producerName in producerNames)
            {
                var elementWithProducer = ProducersShort.SingleOrDefault(x => x.Text == producerName);
                //catalogSearch.SendKeys(elementWithProducer.Text);
                if (elementWithProducer != null)
                {
                    ScrollToElement(elementWithProducer);
                    elementWithProducer.Click();
                }
                else
                {
                    ProducerFindInPopup(producerName);
                }
            }
        }

        private void ProducerFindInPopup(string producerName)
        {
            ScrollToElement(ProducerPopupButton);
            ProducerPopupButton.Click();
            _wait.Until(x => ProducersExtendedDialog.Displayed);

            ProducerExtendedName(producerName).Click();
            SortingHeaderClickToClosePopups();
            _wait.Until(x => !ProducersExtendedDialog.Displayed || ProducersExtendedDialog == null);
        }

        private void SortingHeaderClickToClosePopups() => SortingHeader.Click();

        private void ScrollToElement(IWebElement element)
        {
            Actions actions = new Actions(_driver);
            actions.ScrollToElement(element).ScrollByAmount(0, 150).Build().Perform();
        }

        private void ChooseDinamicCheckboxValue(List<string> checkboxNames, string fieldHeader = null)
        {
            foreach (var checkboxName in checkboxNames)
            {
                //DinamicFiltersCheckbox.Clear();
                //CatalogFiltersPO catalogFilters = new CatalogFiltersPO(_driver, _wait);
                IWebElement? checkboxElement;
                try
                {
                    checkboxElement = DinamicFiltersCheckbox().SingleOrDefault(x => x.Text.Trim() == checkboxName);
                }
                catch(StaleElementReferenceException)
                {
                    checkboxElement = DinamicFiltersCheckbox().SingleOrDefault(x => x.Text.Trim() == checkboxName);
                }

                if (checkboxElement != null)
                {
                    ScrollToElement(checkboxElement);
                    checkboxElement.Click();
                }
                else
                {
                    var extendedValuesButton = DinamicFiltersExtendedValuesButton(fieldHeader);
                    ScrollToElement(extendedValuesButton);
                    extendedValuesButton.Click();
                    _wait.Until(x => DinamicFiltersExtendedValuesContainer.Displayed);

                    DinamicFiltersExtendedValuesCheckbox(checkboxName).Click();
                    SortingHeaderClickToClosePopups();
                    _wait.Until(x => !DinamicFiltersExtendedValuesContainer.Displayed || DinamicFiltersExtendedValuesContainer == null);

                }
            }
        }

        private string ChooseFromOrTo(bool isFromValueToBeChosen)
        {
            var textFrom = "От";
            var textTo = "До";
            string textFromOrTo;
            if (isFromValueToBeChosen)
            {
                textFromOrTo = textFrom;
            }
            else
            {
                textFromOrTo = textTo;
            }
            return textFromOrTo;
        }

        private void ChooseDinamicDropdownValue(string valueToChoose, string dropdownHeaderName, bool isFromValueToBeChosen)
        {
            CatalogFiltersPO catalogFilters = new CatalogFiltersPO(_driver, _wait); //I know that's incorrect, but it helps here to avoid Stale Element Exception

            var textFromOrTo = ChooseFromOrTo(isFromValueToBeChosen);
            var arrowToClick = DinamicFiltersDropdownsArrow(dropdownHeaderName, textFromOrTo);
            ScrollToElement(arrowToClick);
            _wait.Until(x => arrowToClick.Enabled && arrowToClick.Displayed);
            arrowToClick.Click();
            _wait.Until(x => DinamicFiltersDropdownContainer(dropdownHeaderName, textFromOrTo).Displayed);

            DinamicFiltersDropdownValue(valueToChoose, dropdownHeaderName, textFromOrTo).Click();
        }

        private void ChooseDinamicFiltersTextField(string valueToChoose, string dropdownHeaderName, bool isFromValueToBeChosen)
        {
            CatalogFiltersPO catalogFilters = new CatalogFiltersPO(_driver, _wait);//I know that's incorrect, but it helps here to avoid Stale Element Exception

            var textFromOrTo = ChooseFromOrTo(isFromValueToBeChosen);
            var textField = DinamicFiltersTextField(dropdownHeaderName, textFromOrTo);
            ScrollToElement(textField);
            textField.SendKeys(valueToChoose);
        }

        public void WaitFilterResultAlertDisappear()
        {
            try
            {
                _wait.Until(x => FilterResultAlert.Displayed);
                _wait.Until(x => FilterResultAlert == null);
            }
            catch { }
        }






        public void SetFilters(Dictionary<string, List<string>> testData)
        {
            PriceMinType(testData["priceMin"][0]);
            PriceMaxType(testData["priceMax"][0]);
            DeliveryDateChoose(testData["deliveryDate"][0]);
            ChooseProducer(testData["producer"]);
            ChooseDinamicCheckboxValue(testData["type"]);//, GetDinamicValueHeader("type"));

            ChooseDinamicFiltersTextField(testData["screenDiagonalMin"][0], GetDinamicValueHeader("screenDiagonalMin"), true);
            ChooseDinamicDropdownValue(testData["screenResolutionMin"][0], GetDinamicValueHeader("screenResolutionMin"), true);

            ChooseDinamicCheckboxValue(testData["popularParameters"]);//, GetDinamicValueHeader("type"));
            ChooseDinamicDropdownValue(testData["ramMin"][0], GetDinamicValueHeader("ramMin"), true);
            ChooseDinamicDropdownValue(testData["ramMax"][0], GetDinamicValueHeader("ramMax"), false);
            ChooseDinamicCheckboxValue(testData["ramType"]);//, GetDinamicValueHeader("type"));
            ChooseDinamicCheckboxValue(testData["cpu"]);//, GetDinamicValueHeader("type"));
            ChooseDinamicDropdownValue(testData["coresMin"][0], GetDinamicValueHeader("coresMin"), true);

            WaitFilterResultAlertDisappear();
        }

        private string GetDinamicValueHeader(string filterName) => TestData.TestData.FilterHeaderNames()[filterName];
    }
}
