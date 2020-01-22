using NUnit.Framework;
using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Interactions;
using Shouldly;

namespace AutomationTest___Examples
{
    class SummerDresses
    {
        [TestFixture]
        public class SubNavigation
        {
            [TestFixture]
            public class WomenSubNav : SeleniumTest
            {
                [TestCase("Summer Dresses")]
                public void DressesOptionOnlyShowsDressesOfThatType(string dress)
                {
                    ClickDressesOption(dress);

                    //How do we identify what constitutes as a summer dress?
                    //The word summer is not in Printed Chiffon Dress
                    GetCategoryOnPage().ShouldBe($"{dress.ToUpper()} ");
                    //The images are tagged? Perhaps this is a good way to identify
                }

                private string GetCategoryOnPage()
                {
                    return driver.FindElement(By.CssSelector("#center_column > h1 > span.cat-name")).Text;
                }

                private void ClickDressesOption(string v)
                {
                    var basket = driver.FindElement(By.CssSelector("#block_top_menu > ul > li:nth-child(1) > a"));
                    Actions a = new Actions(driver);
                    a.MoveToElement(basket)
                        .Click(driver.FindElement(By.CssSelector("#block_top_menu > ul > li:nth-child(1) > ul > li:nth-child(2) > ul > li:nth-child(3) > a")))
                        .Perform();
                }
            }
        }
    }
}
