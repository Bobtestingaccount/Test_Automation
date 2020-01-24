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
        public WebDriverWait defaultWait;
        
        [SetUp]
        public void Setup()
        {
            var browser = "Chrome";

            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver(new ChromeOptions { Proxy = null });
                    driver.Manage().Window.Maximize();
                    break;
                default:
                    throw new NotSupportedException($"Browser {browser} is not supported");
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            defaultWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void Dispose()
        {
            driver.Quit();
        }
    }
}