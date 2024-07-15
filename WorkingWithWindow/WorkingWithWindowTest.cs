using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace WorkingWithWindow
{
    public class WorkingWithWindowTests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
        }
        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
            driver.Quit();
        }

        [Test, Order(1)]
        public void HandlingMultipleWindows()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
            Assert.That(windowHandles.Count, Is.EqualTo(2), "There should be two windows open");

            driver.SwitchTo().Window(windowHandles[1]);

            string newWindowContent = driver.PageSource;

            Assert.IsTrue(newWindowContent.Contains("New Window"), "The content of the new window is not as expected.");

            string path = Path.Combine(Directory.GetCurrentDirectory() + "window.txt");

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.AppendAllText(path, "Window handle for new window: " + driver.CurrentWindowHandle + "\n\n");
            File.AppendAllText(path, "The page content:\n\n" + newWindowContent + "\n\n");

            driver.Close();

            driver.SwitchTo().Window(windowHandles[0]);

            string originalWindowContent = driver.PageSource;

            Assert.IsTrue(originalWindowContent.Contains("Opening a new window"), "The content of the original page is not as expected");

            File.AppendAllText(path, "Window handle for original window: " + driver.CurrentWindowHandle + "\n\n");
            File.AppendAllText(path, "The page content:\n\n" + originalWindowContent + "\n\n");

        }

        [Test, Order(2)]

        public void NoSuchWindowException()
        {
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            driver.SwitchTo().Window(windowHandles[1]);

            driver.Close();

            try
            {
                driver.SwitchTo().Window(windowHandles[1]);
            }
            catch(NoSuchWindowException ex)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "window.txt");
                File.AppendAllText(path, "NoSuchWindowException cought: " + ex.Message + "\n\n");
                Assert.Pass("NoSuchWindowException was correctly handled");
            }

            catch (Exception ex)
            {
                Assert.Fail("An unexpected exception was thrown" + ex.Message);
            }
            finally
            {
                driver.SwitchTo().Window(windowHandles[0]);
            }
        }
    }
}