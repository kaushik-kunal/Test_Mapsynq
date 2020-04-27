using NUnit.Framework;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;

namespace POC.Report
{
    public class BaseReport
    {
        public static ExtentHtmlReporter htmlReporter;
        public ExtentReports extent;
        public ExtentTest test;

       // [OneTimeSetUp]
        public void StartReport()
        {
          
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;

            string reportPath = projectPath + "Reports\\MyOwnReport.html";

            htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Joe Loyzaga");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("User Name", "Joe Loyzaga");
            htmlReporter.LoadConfig(projectPath + "extent-config.xml");
         }

        //[Test]
        //public void DemoReportPass()
        //{
        //    test = extent.CreateTest("DemoReportPass");
        //    Assert.IsTrue(true);
        //    test.Pass("Test passed");
        //}

        //[Test]
        //public void DemoReportFail()
        //{
        //   // test = extent.CreateTest("DemoReportFail");
        //   // Assert.IsTrue(false);
        //   // test.Fail("Test failed");

        //}

      //  [TearDown]
        public void GetResult()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)

            {
                test.Fail(stackTrace + errorMessage);
               
            }
            //extent.EndTest(test);
        }

        //[OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();
           // extent.Close();
        }
       

    }


    //}
}
