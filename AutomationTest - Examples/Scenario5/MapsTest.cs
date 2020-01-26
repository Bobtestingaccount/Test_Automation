using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutomationTest___Examples.Scenario5
{
    class MapsTest : SeleniumTest
    {
        [Test]
        public void WestPalmBeachInView()
        {
            driver.Url = "http://automationpractice.com/index.php?controller=stores";
            driver.FindElement(By.ClassName("dismissButton")).Click();

            var map = driver.FindElement(By.Id("map"));
            var xStart = map.Location.X;
            var yStart = map.Location.Y;

            var xMid = xStart + map.Size.Width / 2;

            Actions actions = new Actions(driver);
            actions.MoveToElement(map)
                .ClickAndHold()
                .MoveByOffset(0, 500)
                .Release()
                .Perform();

            //Have to do it twice not sure why..
            actions.MoveToElement(map)
                .ClickAndHold()
                .MoveByOffset(0, 500)
                .Release()
                .Perform();

            ITakesScreenshot screenshot = (ITakesScreenshot)driver;
            var info = new DirectoryInfo(Environment.CurrentDirectory);

            screenshot.GetScreenshot().SaveAsFile($"{info.Parent.Parent.Parent.FullName}/Scenario5/Output/test.jpg");
        }
    }
}
