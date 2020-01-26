using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AutomationTest___Examples
{
    public class SliderTests
    {
        [TestFixture]
        public class ShoppingPageTest : SeleniumTest
        {
            [TestCase(16, 20)]
            public void PriceSelectionSliderShowsItems(int min, int max)
            {
                LaunchProductPage();
                SetPriceSlider(min, max);
                ProductsInBandAreSelected(min, max);
            }

            private void ProductsInBandAreSelected(int min, int max)
            {
                //Disabled due to a bug where the loading button will never dissapear
                //And the filterning never happens

                //var loading = driver.FindElements(By.TagName("p"));
                //defaultWait.Until(c => c.FindElements(By.TagName("p"))?
                //.Single(c => c.Text.Contains("Loading")).Displayed == false);

                var products = driver.FindElements(By.ClassName("product-container"));
                var prices = new List<int>();

                foreach (var product in products)
                {
                    var priceOfProduct = product.FindElement(By.ClassName("price"));
                    var individualPrice = GetPriceAsInt(priceOfProduct.GetAttribute("innerText"));
                    prices.Add(individualPrice);
                }

                foreach(var price in prices)
                {
                    price.ShouldBeGreaterThan(min);
                    price.ShouldBeLessThan(max);
                }
            }

            private int GetPriceAsInt(string text) => Convert.ToInt32(Regex.Match(text, @"(?<=\$)\d+?(?=\.)").Value);

            private void LaunchProductPage()
            {
                driver.Url = "http://automationpractice.com/index.php?id_category=11&controller=category";
            }

            private void SetPriceSlider(int min, int max)
            {
                //Change these if the value on the slider changes.
                int minValueOnSlider = 16;
                int maxValueOnSlider = 32;

                if (min < minValueOnSlider)
                    throw new ArgumentException($"Min value on slider is {minValueOnSlider}");
                if (max > maxValueOnSlider)
                    throw new ArgumentException($"Max value on slider is {maxValueOnSlider}");

                int rangeOnSlider = maxValueOnSlider - minValueOnSlider;

                double percentMin = Math.Abs((double)(min - rangeOnSlider) / rangeOnSlider);
                double percentMax = (1 - (Math.Abs((double)(rangeOnSlider - max) / rangeOnSlider)));

                //Calculate the distance to move the slider based on it's size.
                var slider = driver.FindElement(By.Id("layered_price_slider"));
                var control = slider.FindElements(By.TagName("a"));
                var minSliderButton = control[0];
                var maxSliderButton = control[1];
                var minMoveBy = Math.Round(slider.Size.Width * percentMin);
                var maxMoveBy = -Math.Round((slider.Size.Width * percentMax));

                Actions minSliderAction = new Actions(driver);
                Actions maxSliderAction = new Actions(driver);

                //The numbers aren't exact here.
                minSliderAction.ClickAndHold(minSliderButton)
                    .MoveByOffset((int)(minMoveBy), 0)
                    .Release(minSliderButton)
                    .Perform();

                maxSliderAction.ClickAndHold(maxSliderButton)
                    .MoveByOffset((int)maxMoveBy, 0)
                    .Release(maxSliderButton)
                    .Perform();
            }
        }
    }
}
