using System;
using System.Diagnostics;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace WinAppDriverAndWebDriver
{
    class Program
    {
        // I have not tried firefox yet, but you can put "edge" or "chrome" here and it should work
        private static string _browser = "edge";

        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string WinAppDriver = @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";

        private static Process myProcess = null; 
        private static WindowsDriver<WindowsElement> browserSession;
        private static WindowsDriver<WindowsElement> desktopSession;
        private static RemoteWebDriver _driver;

        static void Main(string[] args)
        {
            // start winappdriver
            myProcess = Process.Start(WinAppDriver);

            // launch the browser via WebDriver
            CreateDriver(_browser);
            _driver.Navigate().GoToUrl("https://www.bing.com");

            //var wh1 = _driver.WindowHandles.Count;
            //Console.WriteLine(wh1);

            //_driver.Navigate().GoToUrl("http://ie-snap/scratchtests/nisunny/drt/bugs/validity/popup.html");
            //Thread.Sleep(2000);

            //_driver.FindElementByTagName("Button").Click();
            //Thread.Sleep(2000);

            //var wh2 = _driver.WindowHandles.Count;
            //Console.WriteLine(wh2);

            //Console.ReadLine();
            // Create a session for Desktop
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");

            desktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), desktopCapabilities);

            if (desktopSession == null)
            {
                Console.WriteLine("deskTopSession is null!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Pass! deskTopSession is not null.");
            }

            // Find the browser window from the Desktop Session
            var browser = new BrowserInformation(_browser);

            WindowsElement browserWindow = desktopSession.FindElementByName(browser.applicationIdentifier);
            string browserTopLevelWindowHandle = browserTopLevelWindowHandle = (int.Parse(browserWindow.GetAttribute("NativeWindowHandle"))).ToString("x");

            // Create session for already running browser
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("appTopLevelWindow", browserTopLevelWindowHandle);

            browserSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

            if (browserSession == null)
            {
                Console.WriteLine("browserSession is null!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Pass! browserSession is not null.");
            }

            // get the addressbar via WinAppDriver
            var addressBar = browserSession.FindElementByXPath("//Edit[@Name=\"" + browser.searchElementIdenifier + "\"]");
            addressBar.Clear();
            addressBar.SendKeys("http://www.google.com");
            addressBar.SendKeys(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(3000);

            // enter the username and password into the dialog
            //var userFields = browserSession.FindElementsByXPath(browser.basicAuthDialogEditFields); 
            //userFields[0].Clear();
            //userFields[0].SendKeys("userName");
            //Thread.Sleep(500);
            
            //userFields[1].Clear();
            //userFields[1].SendKeys("passWord");
            //Thread.Sleep(500);

            //userFields[2].Clear();
            //userFields[2].SendKeys("passWord");
            // hit enter
            //var okButton = browserSession.FindElementByXPath("//Window[@Name=\"Windows Security\"]/*[3]");
            //okButton.SendKeys(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(3000);
            Console.WriteLine("clicking to close the browser");
            browserSession.FindElementByName("Close Google - Microsoft Edge").Click();


            TearDown();
        }
        private static void CreateDriver(string browser)
        {
            switch (browser)
            {
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "chrome":
                    ChromeOptions option = new ChromeOptions();
                    //option.AddArgument(@"user-data-dir=C:\Users\JOHNJAN\AppData\Local\Google\Chrome\User Data");
                    _driver = new ChromeDriver(@"c:\drivers");
                    _driver.Navigate().GoToUrl("about:settings");
                    break;
                default:
                    _driver = new EdgeDriver(@"c:\drivers");
                    break;
            }

            Thread.Sleep(2000);
            _driver.Manage().Window.Maximize();
            Thread.Sleep(1000);
        }
        public static void TearDown()
        {
            if (browserSession != null)
            {
                browserSession.Quit();
                browserSession = null;
            }

            if (desktopSession != null)
            {
                desktopSession.Quit();
                desktopSession = null;
            }

            // stop winappdriver
            if (myProcess != null)
            {
                myProcess.CloseMainWindow();
            }

            // stop webdriver
            if (_driver != null)
            {
                _driver.Quit();
            }
        }
    }
}
