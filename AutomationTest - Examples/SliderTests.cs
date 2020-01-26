using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomationTest___Examples
{
    public class SliderTests
    {
        [TestFixture]
        public class ShoppingPageTest : SeleniumTest
        {
            [TestCase(20, 30)]
            public void PriceSelectionSliderShowsItems(int min, int max)
            {
                LaunchProductPage();
                SetPriceSlider(min, max);
                ProductsInBandAreSelected(min, max);
            }

            private void ProductsInBandAreSelected(int min, int max)
            {
                defaultWait.Until(c => c.FindElements(By.TagName("br"))
                .Single(c => c.Text.Contains("Loading")).Displayed == false);
                driver.FindElements(By.Id("product-container"));
            }

            private void LaunchProductPage()
            {
                driver.Url = "http://automationpractice.com/index.php?id_category=11&controller=category";
            }

            private void SetPriceSlider(int min, int max)
            {
                int minValueOnSlider = 16;
                int maxValueOnSlider = 32;

                if (min < minValueOnSlider)
                    throw new ArgumentException($"Min value on slider is {minValueOnSlider}");
                if (max > maxValueOnSlider)
                    throw new ArgumentException($"Max value on slider is {maxValueOnSlider}");

                int rangeOnSlider = maxValueOnSlider - minValueOnSlider;

                double percentMin = Math.Abs((double)(min - rangeOnSlider) / rangeOnSlider);
                double percentMax = (1 - (Math.Abs((double)(rangeOnSlider - max) / rangeOnSlider)));


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
