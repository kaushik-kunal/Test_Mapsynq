using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using POC.Utility;
using Protractor;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Pages.Home
{
    class MapSynqHomePage
    {

        public MapSynqHomePage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@id='txtSearchIncidentsingapore']")]
        protected IWebElement TextBox_IncidentLocation { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@id='selIncidentDays']")]
        protected IWebElement Dropdown_IncidentDate { get; set; }

        [FindsBy(How = How.XPath, Using = "//ul[@id='search_incident_singapore']/li[not(contains(@style,'none')) or contains(@style,'list-item')][1]")]
        protected IWebElement Text_IncidentResultListFirstItem { get; set; }


        [FindsBy(How = How.XPath, Using = "//ul[@id='search_incident_singapore']/li[not(contains(@style,'none')) or contains(@style,'list-item')][1]//div[@class='item_time']")]
        protected IWebElement Text_IncidentResultListFirstItemTime { get; set; }


        public void EnterValueInIncidentLocationTextField(string IncidentLocationText)
        {
            try
            {
                SeleniumHelper.WaitForElementClickable(SeleniumHelper.driverManager.getIWebDriver(), TextBox_IncidentLocation);
                TextBox_IncidentLocation.SendKeys(IncidentLocationText);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Boolean LaunchmapSynq()
        {
            try
            {
                IWebElement HomePageCheck = SeleniumHelper.driverManager.getIWebDriver().FindElement(By.XPath("//a[@class='header_logo sprite']"));
                if (HomePageCheck.Displayed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void SelectIncidentDate(String Date)
        {
            try
            {
                SelectElement sel = new SelectElement(Dropdown_IncidentDate);
                sel.SelectByText(Date);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Boolean IncidentSearchValidation()
        {
            try
            {
                if (Text_IncidentResultListFirstItem.Displayed)
                {
                    DateTime ignored;
                    try
                    {
                        DateTime.TryParseExact(Text_IncidentResultListFirstItemTime.Text.Trim(), "HH:mm",
                                                  CultureInfo.InvariantCulture,
                                                  DateTimeStyles.None,
                                                  out ignored);
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
