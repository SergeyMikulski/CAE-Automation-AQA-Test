using CAE_TestProject.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

        private string SetRandomEmail()
        {
            var emailStart = "user";
            var emailEnd = "@user.com";
            Random random = new Random();
            var rnd = random.Next(1, 10000).ToString();
            var emailResult = $"{emailStart}{rnd}{emailEnd}";
            return emailResult;
        }
    }
}