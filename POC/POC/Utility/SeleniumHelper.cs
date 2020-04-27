using AutoIt;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using POC.Pages;
using Protractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POC.Utility
{
   static  class SeleniumHelper
    {
        public static DriverManager driverManager;

        public static void launchBrowser(string browserType)
        {
           
            driverManager = DriverManagerFactory.getManager(browserType);
            driverManager.getWebDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(2);
            
        }
        public static void navigateToUrl(string url)
        {
            NgWebDriver MyWebDriver = SeleniumHelper.driverManager.ngDriver;
            IWebDriver driver = MyWebDriver.WrappedDriver;
            //driver.Url = url;
            driverManager.getWebDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(2);
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }
        public static IWebElement getWebElementByXpath(string xpath)
        {
          return SeleniumHelper.driverManager.getWebDriver().FindElement(By.XPath(xpath));
        }
        public static void TakeScreenShotAndSave(string location)
        {
            NgWebDriver MyWebDriver = SeleniumHelper.driverManager.ngDriver;
            IWebDriver driver = MyWebDriver.WrappedDriver;
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(location, ScreenshotImageFormat.Png);

       
        }



        public static void JavaScriptClick(IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)SeleniumHelper.driverManager.getIWebDriver();
            executor.ExecuteScript("arguments[0].click();", element);
        }


        public static Boolean WaitForElementVisible(IWebDriver driver, IWebElement element)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                wait.Until(d => (bool)(element as IWebElement).Displayed);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static Boolean WaitForElementClickable(IWebDriver driver, IWebElement element)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                wait.Until(d => (bool)(element as IWebElement).Enabled && (element as IWebElement).Displayed);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public static int GenerateRandomNumber()
        {
            Random rnd = new Random();
           return rnd.Next(1, 5000000);
        }
        public static int GenerateRandomNumbers()
        {
            Random rnd = new Random();
            return rnd.Next(1, 1000000000);
        }

        public static List<StepImageContext> ScreenShotLogInCOntext(List<StepImageContext> _StepImageContext, String ScreenShotPathWithName, String StepInformation)
        {
            //changes
            if (!ScreenShotPathWithName.Equals(""))
            {
                SeleniumHelper.TakeScreenShotAndSave(ScreenShotPathWithName);
            }

            StepImageContext step = new StepImageContext();
            step.ScreenShotPathWithName = ScreenShotPathWithName;
            step.StepInformation = StepInformation;
            _StepImageContext.Add(step);
            return _StepImageContext;

        }

        public static void WaitTillPageLoadComplete()
        {
            var wait = new WebDriverWait(SeleniumHelper.driverManager.getIWebDriver(), TimeSpan.FromSeconds(30));
            wait.Until(d => (IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete");


        }


        public static IWebElement ElementPresence(IWebDriver driver, IWebElement Elem, int time_InSec)
        {


            IWebElement ele = null;

            for (int i = 0; i < time_InSec; i++)
            {
                try
                {
                    ele = Elem;
                    break;
                }
                catch (Exception e)
                {
                    try
                    {
                        Thread.Sleep(1000);
                    }
                    catch (Exception e1)
                    {
                        Console.Out.WriteLine("Waiting for element to appear on DOM");
                    }
                }


            }
            return ele;

        }

    }
}
