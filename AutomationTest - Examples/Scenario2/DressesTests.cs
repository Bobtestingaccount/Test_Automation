using NUnit.Framework;
using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Interactions;
using Shouldly;
using System.Linq;

namespace AutomationTest___Examples.Scenario2
{
    class DressesTests
    {
        [TestFixture]
        public class SubNavigation
        {
            [TestFixture]
            public class WomenSubNav : SeleniumTest
            {
                [TestCase("WOMEN", "Summer Dresses")]
                public void DressesOptionOnlyShowsDressesOfThatType(string category, string dress)
                {
                    LaunchHomePage();
                    HighlightAndSelect(category, dress);
                    GetCategoryOnPage().ShouldBe($"{dress.ToUpper()} ");
                }

                private void LaunchHomePage()
                {
                    driver.Url = "http://automationpractice.com/index.php";
                }

                private string GetCategoryOnPage()
                {
                    return driver.FindElement(By.ClassName("cat-name")).Text;
                }

                private void HighlightAndSelect(string highlight, string select)
                {
                    var navBar = driver.FindElement(By.Id("block_top_menu"));
                    var navBarWomenOption = navBar.FindElements(By.TagName("a"))
                        .Single(c => c.Text.ToUpper() == highlight);

                    Actions a = new Actions(driver);
                    a.MoveToElement(navBarWomenOption)
                        .Perform();
                    LinkInSelection(select).Click();
                }

                private IWebElement LinkInSelection(string select)
                {
                    return driver.FindElements(By.TagName("a")).Single(c => c.Text == select);
                }
            }
        }
    }
}
