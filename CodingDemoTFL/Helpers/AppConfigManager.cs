using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDemoTFL.Helpers
{
    public class AppConfigManager
    {
        public static TimeSpan GetImplicitWait => TimeSpan.FromSeconds(double.Parse(TestContext.Parameters["timeoutInSeconds"]));

        public static string GetWebBaseUrl => TestContext.Parameters["webUrl"];

        public static string GetBrowser => TestContext.Parameters["browser"];

        public static bool IsRunHeadless => Convert.ToBoolean(TestContext.Parameters["runHeadless"]);
    }
}
