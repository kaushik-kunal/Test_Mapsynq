using NUnit.Framework;
using AventStack.ExtentReports;
using System;
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
            extent.AddSystemInfo("Host Name", "Kunal");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("User Name", "Kunal");
            htmlReporter.LoadConfig(projectPath + "extent-config.xml");
         }


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
