using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Utility
{
    class ChromeDriverManager : DriverManager
    {
        internal override void createDriver()
        {

            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--disable-notifications");
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            chromeOptions.AddArgument("-no-sandbox");
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            userName = userName.Replace("APAC\\", "");
            //chromeOptions.AddArgument("user-data-dir=C:/Users/kakunal/AppData/Local/Google/Chrome/User Data/Default");
            chromeOptions.AddUserProfilePreference("download.default_directory", "C:\\Users\\" + userName + "\\Downloads\\");
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
            this.driver = new ChromeDriver(@"..\packages\Selenium.Chrome.WebDriver.77.0.0\driver", chromeOptions);
                // ngDriver = new NgWebDriver(this.driver);


        }
    }
}
