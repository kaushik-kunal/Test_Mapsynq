using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using POC.Pages.Home;
using POC.Utility;
using Protractor;
using System;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;

namespace POC.Steps
{
    [Binding]
    public class LoginVerification
    {
        MapSynqHomePage MapSynqHomePage;
        public List<StepImageContext> _StepImageContext { get; private set; }


        [Given(@"I am on MapSynq Home screen")]
        public void GivenIAmOnMapSynqHomeScreen()
        {
            try
            {
                _StepImageContext = new List<StepImageContext>();
                SeleniumHelper.launchBrowser(Properties.getProperty("browser"));
                SeleniumHelper.navigateToUrl(Properties.getProperty("url"));
                SeleniumHelper.WaitTillPageLoadComplete();
                MapSynqHomePage = new MapSynqHomePage(SeleniumHelper.driverManager.getIWebDriver());

                Boolean HomePageLoadedCheck = MapSynqHomePage.LaunchmapSynq();
                if (HomePageLoadedCheck)
                {

                    _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, @"..\Reports\HomeScreen" + SeleniumHelper.GenerateRandomNumber() + ".png", "Home Screen loaded successfully");
                    ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
                }
                else
                {
                    _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, @"..\Reports\HomeScreen" + SeleniumHelper.GenerateRandomNumber() + ".png", "Home Screen not loaded successfully");
                    ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
                    Assert.Fail("Home Screen Logo not found");
                }
            }
            catch (Exception e)
            {
                _StepImageContext = new List<StepImageContext>();
                _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, @"..\Reports\ErrorHomeScreen" + SeleniumHelper.GenerateRandomNumber() + ".png", "Issue in loading Home Screen");
                ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
                Assert.Fail(e.Message);
            }

        }

        [When(@"I input ""(.*)"" in Incident Location text field")]
        public void WhenIInputInIncidentLocationTextField(string AreaName)
        {
            try
            {
                MapSynqHomePage = new MapSynqHomePage(SeleniumHelper.driverManager.getIWebDriver());
                _StepImageContext = new List<StepImageContext>();
                MapSynqHomePage.EnterValueInIncidentLocationTextField(AreaName);
                _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, @"..\Reports\InputValueInIncidentLocationField" + SeleniumHelper.GenerateRandomNumber() + ".png", "Input Value In Incident Location Field successfully");
                ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
            }
            catch (Exception e)
            {
                _StepImageContext = new List<StepImageContext>();
                _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, @"..\Reports\ErrorInputValueInIncidentLocationField" + SeleniumHelper.GenerateRandomNumber() + ".png", "Error while inputing Value In Incident Location Field");
                ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
                Assert.Fail(e.Message);
            }
        }

        [When(@"I Select Date as ""(.*)"" from the dropdown")]
        public void WhenISelectDateAsFromTheDropdown(string Date)
        {
            try
            {
                MapSynqHomePage = new MapSynqHomePage(SeleniumHelper.driverManager.getIWebDriver());
                _StepImageContext = new List<StepImageContext>();
                MapSynqHomePage.SelectIncidentDate(Date);
                _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, @"..\Reports\DateSelection" + SeleniumHelper.GenerateRandomNumber() + ".png", "Date Selection successful");
                ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
            }
            catch (Exception e)
            {
                _StepImageContext = new List<StepImageContext>();
                _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, @"..\Reports\ErrorDateSelection" + SeleniumHelper.GenerateRandomNumber() + ".png", "Error in Date Selection");
                ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
                Assert.Fail(e.Message);
            }
        }

        [Then(@"I see entries of the incidents based on the Area name provided along with Time in HH:MM format")]
        public void ThenISeeEntriesOfTheIncidentsBasedOnTheAreaNameProvidedAlongWithTimeInHHMMFormat()
        {
            try
            {
                MapSynqHomePage = new MapSynqHomePage(SeleniumHelper.driverManager.getIWebDriver());
                _StepImageContext = new List<StepImageContext>();
                Boolean SearchCheck = MapSynqHomePage.IncidentSearchValidation();
                if (SearchCheck)
                {
                    _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, @"..\Reports\IncidentSearchValidation" + SeleniumHelper.GenerateRandomNumber() + ".png", "Incident Search Validation successful");
                    ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
                }
                else
                {
                    _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, @"..\Reports\IncidentSearchValidation" + SeleniumHelper.GenerateRandomNumber() + ".png", "Incident Search Validation not successful");
                    ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
                    Assert.Fail("Home Screen Logo not found");
                }
            }
            catch (Exception e)
            {
                _StepImageContext = new List<StepImageContext>();
                _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, @"..\Reports\ErrorIndividualEntrepreneur" + SeleniumHelper.GenerateRandomNumber() + ".png", "Issue in Incident Search, please refer screenshot");
                ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
                Assert.Fail(e.Message);
            }
        }





    }
}
