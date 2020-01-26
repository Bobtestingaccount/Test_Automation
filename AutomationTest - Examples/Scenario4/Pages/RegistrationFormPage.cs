using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomationTest___Examples.Scenario4.Pages
{
    class RegistrationForm
    {
        public IWebDriver driver;
        public PersonalInformation PersonalInformation;
        public YourAddress YourAddress;

        public RegistrationForm(IWebDriver driver)
        {
            this.driver = driver;
            PersonalInformation = new PersonalInformation(driver);
            YourAddress = new YourAddress(driver);
        }
        public void Register() => driver.FindElement(By.Id("submitAccount")).Click();
    }

    public class YourAddress
    {
        private IWebDriver driver;

        public YourAddress(IWebDriver driver)
        {
            this.driver = driver;
        }


        public void SetAddress(string address) => driver.FindElement(By.Id("address1")).SendKeys(address);
        public void SetCity(string city) => driver.FindElement(By.Id("city")).SendKeys(city);
        public void SetState(string state) => driver.FindElement(By.Id("id_state")).SendKeys(state);

        public void SetPostcode(string postcode) => driver.FindElement(By.Id("postcode")).SendKeys(postcode);

        public void SetPhone(string phone) => driver.FindElement(By.Id("phone")).SendKeys(phone);
    }

    public class PersonalInformation
    {
        IWebDriver driver;
        public PersonalInformation(IWebDriver driver)
        {
            this.driver = driver;
        }
        public enum Gender
        {
            Male,
            Female
        }

        public void SelectGender(Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    driver.FindElement(By.Id("id_gender1")).Click();
                    break;
                case Gender.Female:
                    driver.FindElement(By.Id("id_gender2")).Click();
                    break;
                default:
                    break;
            }
        }

        public void SetForename(string forename) => driver.FindElement(By.Id("customer_firstname")).SendKeys(forename);

        public void SetSurname(string forename) => driver.FindElement(By.Id("customer_lastname")).SendKeys(forename);

        public void SetEmail(string forename) => driver.FindElement(By.Id("email")).SendKeys(forename);

        public void SetPassword(string forename) => driver.FindElement(By.Id("passwd")).SendKeys(forename);


    }
}
