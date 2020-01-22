using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using Shouldly;

namespace AutomationTest___Examples
{
    public class SeleniumTest
    {

        public IWebDriver driver;

        [SetUp]
        public void Setup()
        {

            ////Annoyingly app.config doesn't work will look at later if time but this is typically how I would manage test variables / endpoints etc.
            //var browser = ConfigurationManager.AppSettings["Browser"];

            var browser = "Chrome";

            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();

                    //Browser size matters for the website maximize it just for ease of testing
                    driver.Manage().Window.Maximize();
                    break;
                default:
                    throw new NotSupportedException($"Browser {browser} is not supported");
            }

            driver.Url = "http://automationpractice.com/index.php";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    }
}