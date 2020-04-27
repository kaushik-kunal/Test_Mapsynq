using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;

namespace POC.Utility
{
    class RemoteDriverManager : DriverManager
    {
        internal override void createDriver()
        {

            ChromeOptions chromeOptions;

            chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--disable-notifications");
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            chromeOptions.AddArgument("--no-sandbox");
            //chromeOptions.AddArgument("--disable-dev-shm-usage");


             this.driver = new RemoteWebDriver(new Uri("http://localhost:4545/wd/hub/"), chromeOptions.ToCapabilities());


        }
    }
}
