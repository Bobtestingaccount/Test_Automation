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
    //SCENARIO 1 : Shopping Basket
    [TestFixture]
    public class ShoppingBaskets : SeleniumTest
    {
        //TODO Setup CreateDriver method that can pass in certain things such as screen size etc etc.
        

        [Test]
        public void DeletingOnlyItemInBasketLeavesEmptyBasket()
        {
            AddProductToBasket("Blouse");
            RemoveProductFromBasket();
            IsBasketEmpty().ShouldBe(true);
        }

        //Refactor these into a seperate class POM?
        private bool IsBasketEmpty()
        {
            //Again not sure about these Ask?
            Thread.Sleep(1000);

            var cartEmptyStatus = driver.FindElement(By.CssSelector("#center_column > p"));
            if (cartEmptyStatus.Text == "Your shopping cart is empty.")
            {
                return true;
            }
            return false;
        }

        private void RemoveProductFromBasket()
        {
            var basket = driver.FindElement(By.ClassName("shopping_cart"));
            var basketA = basket.FindElement(By.TagName("a"));
            basketA.Click();

            var removeItem = driver.FindElement(By.ClassName("icon-trash"));
            removeItem.Click();
        }

        private void AddProductToBasket(string itemName)
        {
            driver.FindElements(By.TagName("img")).First(c => c.GetAttribute("title").Contains(itemName)).Click();

            var fancyBox = driver.FindElement(By.ClassName("fancybox-iframe"));

            driver.SwitchTo().Frame(fancyBox);

            //Im curious about this why isn't implicit timeout not working here?
            Thread.Sleep(1000);

            var cartSelector = driver.FindElement(By.Id("add_to_cart"));
            cartSelector.FindElement(By.Name("Submit")).Click();

            driver.SwitchTo().ParentFrame();

            //Replace Me
            var cartLayer = driver.FindElement(By.Id("layer_cart"));
            cartLayer.FindElements(By.TagName("span")).First(c => c.GetAttribute("title").Contains("Close window")).Click();
        }
    }
}