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
    [TestFixture]
    public class ShoppingBasketTests : SeleniumTest
    {
        [Test]
        public void DeletingOnlyItemInBasketLeavesEmptyBasket()
        {
            AddProductToBasket();
            RemoveProductFromBasket();
            BasketPageErrorStatusMatches("Your shopping cart is empty.").ShouldBe(true);
        }

        private bool BasketPageErrorStatusMatches(string expectedStatus)
        {
            //This is the best identifier for the element I could find - might clash with future warnings.
            return defaultWait.Until(c => c.FindElement(By.ClassName("alert-warning")).Text == expectedStatus);
        }

        private void RemoveProductFromBasket()
        {
            var basket = driver.FindElement(By.ClassName("shopping_cart"));
            var basketLink = basket.FindElement(By.TagName("a"));

            basketLink.Click();

            var removeItemButton = driver.FindElement(By.ClassName("icon-trash"));
            removeItemButton.Click();
        }

        private void AddProductToBasket()
        {
            driver.Url = "http://automationpractice.com/index.php";
            driver.Url = "http://automationpractice.com/index.php?controller=cart&add=1&id_product=1&token=e817bb0705dd58da8db074c69f729fd8";
        }
    }
}