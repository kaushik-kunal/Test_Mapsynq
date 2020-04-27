using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Protractor;

namespace POC.Utility
{
    public abstract class DriverManager
    {

        protected IWebDriver driver;
        public NgWebDriver ngDriver;
        public void quitDriver()
        {
            if (null != ngDriver)
            {
                ngDriver.Close();
                ngDriver = null;
            }

        }
        public IWebDriver getIWebDriver()
        {
            
            return driver;
        }

        public NgWebDriver getWebDriver()
        {
            if (null == ngDriver)
            {
                createDriver();
                ngDriver = new NgWebDriver(driver);
                //ngDriver.waitForAngularRequestsToFinish();
            }
            return ngDriver;
        }

        internal abstract void createDriver();
    }
}
