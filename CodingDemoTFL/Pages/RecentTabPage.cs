using CodingDemoTFL.Extensions;
using CodingDemoTFL.Setup;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V115.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDemoTFL.Pages
{
    public class RecentTabPage : WebDriverSupport
    {
        #region Elements
        
        By RecentjourneyList = By.XPath("//div[@id='jp-recent-content-home-']/a");
       
        #endregion

        #region Page Actions

        public List<string> GetRecentJourney()
        {
            var elements = RecentjourneyList.GetAllElements();
            var list = new List<string>();
            foreach (var element in elements)
            {
                list.Add(element.Text);
            }

            return list;
        }

        #endregion
    }
}
