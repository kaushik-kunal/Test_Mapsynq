using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Utility
{
    class InternetExplorerDriverManager : DriverManager
    {
        internal override void createDriver()
        {
            var options = new InternetExplorerOptions();
           // options.AddAdditionalCapability("useAutomationExtension", false);
           // options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            options.IgnoreZoomLevel = true;
            options.PageLoadStrategy = PageLoadStrategy.Eager;
            // options.EnableNativeEvents = true;
            // options.EnsureCleanSession = true;
            options.EnsureCleanSession = true;
            options.EnableNativeEvents = false;
            options.AcceptInsecureCertificates = false;
            options.AddAdditionalCapability("disable-popup-blocking", true);

            
            options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
            options.EnablePersistentHover = true;
           
            
            options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            options.IgnoreZoomLevel = true;
            options.RequireWindowFocus = true;
           // options.ForceCreateProcessApi = true;
            options.EnablePersistentHover = false;
            options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;



            this.driver = new InternetExplorerDriver(@"C:\Users\SSHRADD\source\repos\POC\packages\Selenium.InternetExplorer.WebDriver.3.141\driver", options, TimeSpan.FromSeconds(30));

        }

      
        
    }
}
