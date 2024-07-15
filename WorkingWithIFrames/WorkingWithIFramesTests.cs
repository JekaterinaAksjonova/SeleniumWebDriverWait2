using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WorkingWithIFrames
{
    public class WorkingWithIFramesTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void TestFrameByIndex()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn"))).Click();

            var dropMenu = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var option in dropMenu)
            {
                Console.WriteLine(option.Text);
                Assert.IsTrue(option.Displayed, "Link inside the dropdown is not visible as expected");
            }
            driver.SwitchTo().DefaultContent();
        }

        [Test, Order(2)]

        public void TestFrameById()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id("result")));

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn"))).Click();

            var dropMenu = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var option in dropMenu)
            {
                Console.WriteLine(option.Text);
                Assert.IsTrue(option.Displayed, "Link inside the dropdown is not visible as expected");
            }

            driver.SwitchTo().DefaultContent();
        }

        [Test, Order(3)]
        public void TestFrameByWebelement()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));

            driver.SwitchTo().Frame(frameElement);

            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("dropbtn"))).Click();

            var dropDownMenu = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var option in dropDownMenu)
            {
                Console.WriteLine(option.Text);
                Assert.IsTrue(option.Displayed, "Link inside the dropdown is not visible as expected");
            }
            driver.SwitchTo().DefaultContent();
        }
    }
}