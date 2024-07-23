using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace CAE_TestProject.PageObjects
{
    internal class CatalogGridPO : PageBase
    {
        public CatalogGridPO(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        private IWebElement GoodsContainer => _driver.FindElement(By.XPath(".//*[@data-testid='product-list']"));

        private List<IWebElement> GoodsPrices => GoodsContainer.FindElements(By.XPath(".//*[@data-testid='card-current-price']")).ToList();

        private IWebElement GoodContainerByPriceFind(string price) => GoodsContainer.FindElement(By.XPath(
            $".//*[@data-testid='card-current-price' and contains(text(),'{price}')]/../../../.."));

        private IWebElement AddToComparisonGoodByPriceElement(string price) => GoodContainerByPriceFind(price).FindElements(By.XPath(".//*[@data-testid='card-comparison']")).Last();

        private IWebElement CompareGoodsButton => _driver.FindElement(By.XPath(".//*[@id='modal-listing-comparison']//a"));

        private IWebElement CompareGoodsNotice => _driver.FindElement(By.XPath(".//*[@id='modal-listing-comparison']//p"));


        public string GetClosestToAveragePrice()
        {
            List<float> prices = new List<float>();
            var goods = GoodsPrices;

            foreach (var good in goods)
            {
                var price = float.Parse(good.Text.Replace(" р.", ""));
                prices.Add(price);
            }
            var avg = prices.Average();

            var closest = prices.OrderBy(price => Math.Abs(avg - price)).First();
            var priceRuCulture = closest.ToString("C", new System.Globalization.CultureInfo("ru-RU"));
            var resultPrice = priceRuCulture.Remove(priceRuCulture.Length - 2).Replace("\u00A0", " ");
            
            return resultPrice;
        }

        public string GetMostExpensivePrice()
        {
            List<float> prices = new List<float>();
            var goods = GoodsPrices;

            foreach (var good in goods)
            {
                var price = float.Parse(good.Text.Replace(" р.", ""));
                prices.Add(price);
            }

            var max = prices.Max();
            var priceRuCulture = max.ToString("C", new System.Globalization.CultureInfo("ru-RU"));
            var resultPrice = priceRuCulture.Remove(priceRuCulture.Length - 2).Replace("\u00A0", " ");

            return resultPrice;
        }

        public void AddToComparisonGoodByPrice(string price) => AddToComparisonGoodByPriceElement(price).Click();

        public void CompareGoodsButtonClick() => CompareGoodsButton.Click();

        public void WaitCompareGoodsNotice(string numberOfGoods) => _wait.Until(x => CompareGoodsNotice.Text.Contains(numberOfGoods));
    }
}
