using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WorkingWithAlerts
{
    public class WorkingWithAlertsTests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
            
        }

        [Test, Order(1)]
        public void HandleBasicAlert()
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Alert')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "alert text is not asexpected.");
            
            alert.Accept();

            IWebElement result = driver.FindElement(By.Id("result"));
            Assert.That(result.Text, Is.EqualTo("You successfully clicked an alert"), "Result message is not as expected");
        }
        [Test, Order(2)]

        public void HandleConfirmalert()
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected.");

            alert.Accept();

            IWebElement result = driver.FindElement(By.Id("result"));

            Assert.That(result.Text, Is.EqualTo("You clicked: Ok"), "Result message is not as expected after accepting the alert.");

            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Confirm')]")).Click();

            alert = driver.SwitchTo().Alert();

            alert.Dismiss();

            result = driver.FindElement(By.Id("result"));

            Assert.That(result.Text, Is.EqualTo("You clicked: Cancel"), "Result message is not as expected after dismissing the alert.");

        }

        [Test, Order(3)]

        public void HandlePromptalert()
        {
            driver.FindElement(By.XPath("//button[contains(text(), 'Click for JS Prompt')]")).Click();

            IAlert alert = driver.SwitchTo().Alert();

            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text is not as expected");

            string inputText = "Hello there!";

            alert.SendKeys(inputText);
            alert.Accept();

            IWebElement result = driver.FindElement(By.Id("result"));

            Assert.That(result.Text, Is.EqualTo("You entered: " + inputText), "Result message is not as expected after enetering text in the prompt.");

        }
    }
}