using CAE_TestProject.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CAE_TestProject.TestData;

namespace CAE_TestProject
{
    public class Tests : TestsBase
    {
        [Test]
        public void Test1()
        {
            var searchText = "Настольные лампы National";
            var goodToCheck = "Настольная лампа National NL-94LED (бронзовый)";
            var name = "User";

            LandingPagePO landingPage = new LandingPagePO(_driver, _wait);
            landingPage.DoSearch(searchText);

            SearchResultPagePO searchResultPage = new SearchResultPagePO(_driver, _wait);
            searchResultPage.ArriveNotificationButtonClick(goodToCheck);

            ArriveNotifDialogPO arriveNotifDialog = new ArriveNotifDialogPO(_driver, _wait);
            arriveNotifDialog.SendCloseButtonClick();
            arriveNotifDialog.CheckBothFieldsRequired();
            arriveNotifDialog.EnterName(name);
            arriveNotifDialog.EnterEmail(SetRandomEmail());
            arriveNotifDialog.SendCloseButtonClick();
            arriveNotifDialog.CheckOrderMessageShown();
            arriveNotifDialog.SendCloseButtonClick();

            searchResultPage.CheckGoodIsInWaitList(goodToCheck);

        }

        [Test]
        [TestCase(Test2Variants.Task2)]
        public void Test2(Test2Variants testVariant)
        {
            var testData = ChooseTest2Data(testVariant);
            ChooseCatalogGoodType(testData);
            
            CatalogFiltersPO catalogFilters = new CatalogFiltersPO(_driver, _wait);
            catalogFilters.SetFilters(testData);

            ChooseComparison(testVariant);
        }




        private string SetRandomEmail()
        {
            var emailStart = "user";
            var emailEnd = "@user.com";
            Random random = new Random();
            var rnd = random.Next(1, 10000).ToString();
            var emailResult = $"{emailStart}{rnd}{emailEnd}";
            return emailResult;
        }

        private Dictionary<string, List<string>> ChooseTest2Data(Test2Variants testVariant)
        {
            Dictionary<string, List<string>> values = new Dictionary<string, List<string>>();

            switch (testVariant)
            {
                case Test2Variants.Task2:
                {
                    values = TestData.TestData.Test2FirstData();
                    break;
                }

            }
            return values;
        }

        private void ChooseCatalogGoodType(Dictionary<string, List<string>> testData)
        {
            LandingPagePO landingPage = new LandingPagePO(_driver, _wait);
            landingPage.CatalogButtonClick();

            CatalogPopupPO catalogPopup = new CatalogPopupPO(_driver, _wait);
            catalogPopup.LeftCatalogItemHover(testData["catalogItemToHover"][0]);
            catalogPopup.RightCatalogItemClick(testData["good"][0]);
        }

        private void ChooseComparison(Test2Variants testVariant)
        {
            switch (testVariant)
            {
                case Test2Variants.Task2:
                    {
                        SetComparisonTask2();
                        break;
                    }
                case Test2Variants.Task3option1:
                    break;
                case Test2Variants.Task3option2:
                    break;
            }
        }

        private void SetComparisonTask2()
        {
            CatalogGridPO catalogGrid = new CatalogGridPO(_driver, _wait);
            var closestToAveragePrice = catalogGrid.GetClosestToAveragePrice();
            var maxPrice = catalogGrid.GetMostExpensivePrice();
            catalogGrid.AddToComparisonGoodByPrice(closestToAveragePrice);
            catalogGrid.WaitCompareGoodsNotice("1");
            catalogGrid.AddToComparisonGoodByPrice(maxPrice);
            catalogGrid.WaitCompareGoodsNotice("2");
            catalogGrid.CompareGoodsButtonClick();
        }

    }
}

public enum Test2Variants
{
    Task2,
    Task3option1,
    Task3option2
}