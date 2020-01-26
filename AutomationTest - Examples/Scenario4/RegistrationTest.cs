using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using AutomationTest___Examples.Scenario4.Pages;
using OpenQA.Selenium;
using Shouldly;

namespace AutomationTest___Examples
{
    class RegistrationTest
    {
        [TestFixture]
        public class InvalidFormSubmission : SeleniumTest
        {
            public void EmptyForm()
            {
                driver.Url = "http://automationpractice.com/index.php?controller=authentication&back=my-account#account-creation";
                //Random Email
                var randomEmail = $"{Guid.NewGuid().ToString().Substring(0, 5)}@test.com";
                driver.FindElement(By.Id("email_create")).SendKeys(randomEmail);
                driver.FindElement(By.Id("SubmitCreate")).Click();

                RegistrationForm form = new RegistrationForm(driver);

                form.Register();

                //I'd actually write a better test for this, but I actually can't not register now!
                driver.FindElement(By.Id("center_column")).Text.ShouldNotContain("MY ACCOUNT");
            }
        }

        [TestFixture]
        public class ValidFormSubmission : SeleniumTest
        {
            [Test]
            public void ValidRegistration()
            {
                driver.Url = "http://automationpractice.com/index.php?controller=authentication&back=my-account#account-creation";
                //Random Email
                var randomEmail = $"{Guid.NewGuid().ToString().Substring(0, 5)}@test.com";
                driver.FindElement(By.Id("email_create")).SendKeys(randomEmail);
                driver.FindElement(By.Id("SubmitCreate")).Click();

                RegistrationForm form = new RegistrationForm(driver);

                form.PersonalInformation.SetForename("Test");
                form.PersonalInformation.SetSurname("McTest");
                form.PersonalInformation.SetPassword("password");

                form.YourAddress.SetAddress("123 Test Street");
                form.YourAddress.SetCity("Test City");
                form.YourAddress.SetState("Alabama");
                form.YourAddress.SetPostcode("99524");
                form.YourAddress.SetPhone("0113123123");

                form.Register();

                driver.FindElement(By.Id("center_column")).Text.ShouldContain("MY ACCOUNT");
            }
        }
    }
}
