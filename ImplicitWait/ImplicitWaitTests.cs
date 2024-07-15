using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWaitsExc
{
    public class ImplicitWaitTests
    {
        IWebDriver driver;
         
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void SearchProduct_Keyboard_SouldAddToCard()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("keyboard");
            driver.FindElement(By.XPath("//input[@type='image']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();
                Assert.IsTrue(driver.PageSource.Contains("keyboard"));
                Console.WriteLine("Scenario completed");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception" + ex.Message);
            } 
        }

        [Test]
        public void SearchProduct_Junk_ShouldThrowNoSuchElementException()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("junk");
            driver.FindElement(By.XPath("//input[@type='image']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();
            }
            catch (NoSuchElementException ex) 
            {
                Assert.Pass("Expected NoSuchElementException was thrown.");
                Console.WriteLine("Timeout - " + ex.Message);
            }
            catch(Exception ex)
            {
                Assert.Fail("Unexpected exception:" + ex.Message);
            } 
        }
    }
}