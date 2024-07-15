using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Security.Cryptography.X509Certificates;

namespace ExplicitWait
{
    public class ExplicitWaitTests
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

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                var buyNowBtn = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                buyNowBtn.Click();

                Assert.IsTrue(driver.PageSource.Contains("keyboard"), "The product 'keyboard' was not found in cart page.");
                Console.WriteLine("Scenarion completed");

            }

            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception " + ex.Message);
            }

            
        }
        [Test]

        public void SearchProduct_Junk_ShouldThrowNoSuchElementException()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("junk");
            driver.FindElement(By.XPath("//input[@type='image']")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                WebDriverWait wait = new WebDriverWait (driver, TimeSpan.FromSeconds(10));
                IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

                buyNowLink.Click();
                Assert.Fail("The 'Buy Now' link was found for non-existing product.");
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Pass("WebDriverTimeoutException was thrown.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exceprion " + ex.Message);
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }
    }
}