using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Utility
{
    class DriverManagerFactory
    {
        public static DriverManager getManager(string type)
        {

            DriverManager driverManager = null;

            switch (type.ToUpper())
            {
                case "CHROME":
                    driverManager = new ChromeDriverManager();
                    break;
                case "FIREFOX":
                    driverManager = new FirefoxDriverManager();
                    break;
                case "IE":
                    driverManager = new InternetExplorerDriverManager();
                    break;
                case "REMOTE":
                    driverManager = new RemoteDriverManager();
                    break;
                default:
                   // driverManager = new SafariDriverManager();
                    break;
            }
            return driverManager;

        }
    }
}
