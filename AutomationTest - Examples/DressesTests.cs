using NUnit.Framework;
using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Interactions;
using Shouldly;
using System.Linq;

namespace AutomationTest___Examples
{
    class DressesTests
    {
        [TestFixture]
        public class SubNavigation
        {
            [TestFixture]
            public class WomenSubNav : SeleniumTest
            {
                [TestCase("Women", "Summer Dresses")]
                public void DressesOptionOnlyShowsDressesOfThatType(string category, string dress)
                {
                    HighlightAndSelect(category, dress);
                    GetCategoryOnPage().ShouldBe($"{dress.ToUpper()} ");
                }

                private string GetCategoryOnPage()
                {
                    return driver.FindElement(By.CssSelector("#center_column > h1 > span.cat-name")).Text;
                }

                private void HighlightAndSelect(string highlight, string select)
                {
                    //WomenNavigator

                    var navBar = driver.FindElement(By.Id("block_top_menu"));
                    var navBarWomenOption = navBar.FindElements(By.TagName("a")).Single(c => c.Text == highlight);

                    Actions a = new Actions(driver);
                    a.MoveToElement(navBarWomenOption)
                        .Click(LinkInSelection(highlight, select))
                        .Perform();
                }

                private IWebElement LinkInSelection(string highlight, string select)
                {
                    switch (highlight)
                    {
                        case "Women":
                            return driver.FindElements(By.TagName("a")).Single(c => c.Text == select);
                        default:
                            return null;
                    }
                }
            }
        }
    }
}
