using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Utility
{
    class FirefoxDriverManager : DriverManager
    {
        internal override void createDriver()
        {

            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(Properties.getProperty("gecko_path"));
            this.driver = new FirefoxDriver(service);


            // this.driver = new FirefoxDriver(service, options);

        }
    }
}
