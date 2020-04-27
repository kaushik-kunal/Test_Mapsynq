using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using POC.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace POC.Report
{
    [Binding]
    public class Hooks
    {
        public static ExtentV3HtmlReporter htmlReporter;
        public static ExtentLoggerReporter loggerReporter;
        public static ExtentReports extent;
        public static ExtentReports extentLog;
        public static ExtentTest testlog;
        public static ExtentTest feature;
        public static ExtentTest scenario;
        public static ExtentTest steps;
        public static String dt = DateTime.Now.ToString("yyyyMMddhmmsstt");
        public static String reportName = "";
        public static String ScenarioStatus = "";
        static String ReplacedValue = "";
        static String ReportHolder = "";
        static int count1 = 1;
        static Boolean count2 = false;
        static string tempKeyTimeSt = "";


        [BeforeTestRun]
        [Obsolete]
        public static void StartReport()
        {

            Properties.loadProperties(@"..\POC\Config\Data.properties");


            Copy(@"..\Reports", @"..\ReportBackup\Backup" + dt);
            string reportPath = @"..\Reports\MyOwnReport.html";
            string logPath = @"..\Reports\";

            loggerReporter = new ExtentLoggerReporter(logPath);
            extentLog = new ExtentReports();
            extentLog.AttachReporter(loggerReporter);

            htmlReporter = new ExtentV3HtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            extent.CreateTest("IGNORE");

            extent.AddSystemInfo("Host Name", "Automation");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("User Name", "Kunal");
            htmlReporter.LoadConfig(@"..\POC\extent-config.xml");

        }


        [AfterTestRun]
        public static void TearDown()
        {
            System.IO.StreamReader objReader;
            objReader = new System.IO.StreamReader(Properties.getProperty("screenshotpath") + @"\\MyOwnReport.html");
            String Content = objReader.ReadToEnd();
            objReader.Close();
            StreamWriter writer = new StreamWriter(Properties.getProperty("screenshotpath") + @"\\MyOwnReport.html");
            writer.Write(Content);
            writer.Close();

            SeleniumHelper.driverManager.getIWebDriver().Quit();
            HTMLUpdate();
   



        }


        public static void HTMLUpdate()
        {
            System.IO.StreamReader objReader;
            objReader = new System.IO.StreamReader(Properties.getProperty("screenshotpath") + @"\\MyOwnReport.html");
            String Content = objReader.ReadToEnd();
            objReader.Close();

            String temp = @"..\Reports\";

            String temp1 = Content.Replace(temp, "");
            StreamWriter writer = new StreamWriter(Properties.getProperty("screenshotpath") + @"\\MyOwnReport.html");
            writer.Write(temp1);
            writer.Close();

        }



        [AfterScenario]
        public void ExecutionStatus()
        {

        }

        [BeforeFeature]
        [Obsolete]
        public static void BeforeFeature()
        {
            feature = extent.CreateTest(FeatureContext.Current.FeatureInfo.Title, FeatureContext.Current.FeatureInfo.Description);
            Hooks.testlog = extentLog.CreateTest(Properties.getProperty("projectname"));
            feature.AssignCategory("Regression");
        }

        [BeforeScenario]
        [Obsolete]
        public static void BeforeScenario()
        {

            scenario = feature.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title, ScenarioContext.Current.ScenarioInfo.Description);
        }

        [AfterStep]
        [Obsolete]
        public static void InsertReportingSteps()
        {
            if (ScenarioContext.Current.Count != 0)
            {
                List<StepImageContext> StepImageContext = (List<StepImageContext>)ScenarioContext.Current["StepImageContext"];
                var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
                PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
                MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
                object TestResult = getter.Invoke(ScenarioContext.Current, null);

                if (ScenarioContext.Current.TestError == null)
                {
                    if (stepType == "Given")
                    {
                        steps = scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text);
                        foreach (StepImageContext model in StepImageContext)
                        {
                            LogInfo(model);
                        }
                    }
                    else if (stepType == "When")
                    {
                        steps = scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text);
                        foreach (StepImageContext model in StepImageContext)
                        {
                            LogInfo(model);
                        }

                    }
                    else if (stepType == "Then")
                    {
                        steps = scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text);
                        foreach (StepImageContext model in StepImageContext)
                        {
                            LogInfo(model);
                        }
                    }
                }
                else if (ScenarioContext.Current.TestError != null)
                {
                    if (stepType == "Given")
                    {
                        steps = scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                        foreach (StepImageContext model in StepImageContext)
                        {
                            LogInfo(model);
                        }

                    }
                    else if (stepType == "When")
                    {
                        steps = scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                        foreach (StepImageContext model in StepImageContext)
                        {
                            LogInfo(model);
                        }
                    }
                    else if (stepType == "Then")
                    {
                        steps = scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                        foreach (StepImageContext model in StepImageContext)
                        {
                            LogInfo(model);
                        }
                    }
                }

                if (TestResult.ToString() == "StepDefinitionPending")
                {
                    if (stepType == "Given")
                    {
                        steps = scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        steps.Log(Status.Skip, "STEP DEFINITION PENDING");
                    }
                    else if (stepType == "When")
                    {
                        steps = scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        steps.Log(Status.Skip, "STEP DEFINITION PENDING");
                    }
                    else if (stepType == "Then")
                    {
                        steps = scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString() + " " + ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                        steps.Log(Status.Skip, "STEP DEFINITION PENDING");
                    }
                }
            }
            Properties.SceenShotLocation = Properties.getProperty("screenshotpath");
            ScenarioContext.Current.Clear();

            ScenarioStatus = ScenarioContext.Current.ScenarioExecutionStatus.ToString();

        }

        [AfterFeature]
        public static void EndReport()
        {

            Hooks.extent.Flush();
            Hooks.extentLog.Flush();
            // SeleniumHelper.driverManager.quitDriver();
        }



        public static List<MediaEntityModelProvider> GenerateMediaEntityModelProvider(List<StepImageContext> list)
        {
            List<MediaEntityModelProvider> mediaModelList = new List<MediaEntityModelProvider>();
            foreach (StepImageContext contextStepImage in list)
            {
                MediaEntityModelProvider mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath(contextStepImage.ScreenShotPathWithName).Build();
                mediaModelList.Add(mediaModel);
            }
            return mediaModelList;
        }

        public static void LogInfo(StepImageContext model)
        {
            if (!model.ScreenShotPathWithName.Equals(""))
            {
                MediaEntityModelProvider mediaModel1 = MediaEntityBuilder.CreateScreenCaptureFromPath(model.ScreenShotPathWithName).Build();
                steps.Log(Status.Pass, model.StepInformation, mediaModel1);

            }
            else
            {
                steps.Log(Status.Pass, model.StepInformation);
            }
        }

        public static void Copy(string sourcePath, string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            string newPath;
            foreach (string srcPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                newPath = srcPath.Replace(sourcePath, targetPath);
                File.Copy(srcPath, newPath, true);
            }
        }

        public static void HandleError(Exception exception, String ScreenShotFullPath, String StepDescription)
        {
            Hooks.testlog.Log(Status.Fail, exception.StackTrace + "\n" + exception.InnerException);
            List<StepImageContext> _StepImageContext = new List<StepImageContext>();
            _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, ScreenShotFullPath, StepDescription);
            ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
            Assert.Fail(exception.Message);
        }

        public static void HandleError(Exception exception, List<StepImageContext> _StepImageContext)
        {
            Hooks.testlog.Log(Status.Fail, exception.StackTrace + "\n" + exception.InnerException);
            _StepImageContext = SeleniumHelper.ScreenShotLogInCOntext(_StepImageContext, @"..\Reports\Error" + SeleniumHelper.GenerateRandomNumber() + ".png", "ERROR");
            ScenarioContext.Current.Add("StepImageContext", _StepImageContext);
            Assert.Fail(exception.Message);
        }
    }
}
