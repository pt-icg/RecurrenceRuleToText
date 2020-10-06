using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Ical.Net;
using Ical.Net.DataTypes;
using IcgSoftware.RecurrenceRuleToText;
using Microsoft.AspNetCore.Mvc;
using RecurrenceRuleWebAppVue.Models;

namespace RecurrenceRuleWebAppVue.Controllers
{
    public class HomeController : Controller
    {

        private readonly int maxOccurences = 50;
        private readonly int maxYears = 20;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        //VUE erstmal ohne [ValidateAntiForgeryToken]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRRule([FromBody] RRuleWrapper RRuleWrapper)
        {
            if (RRuleWrapper == null)
            {
                var rRuleResultE = new RRuleResult() { ErrorText = "invalid data" };
                return new JsonResult(rRuleResultE);
            };

            var recurrencePattern = new RecurrencePattern(RRuleWrapper.GetFrequencyType(), RRuleWrapper.Interval);
            switch (recurrencePattern.Frequency)
            {
                case FrequencyType.None:
                case FrequencyType.Secondly:
                case FrequencyType.Minutely:
                case FrequencyType.Hourly:
                case FrequencyType.Daily:
                    break;
                case FrequencyType.Weekly:
                    recurrencePattern.ByDay = RRuleWrapper.GetByDayList();
                    break;
                case FrequencyType.Monthly:
                    recurrencePattern.ByDay = RRuleWrapper.GetByDayList();
                    recurrencePattern.ByMonthDay = RRuleWrapper.ByMonthDay;
                    recurrencePattern.BySetPosition = RRuleWrapper.BySetPosition;
                    break;
                case FrequencyType.Yearly:
                    recurrencePattern.ByDay = RRuleWrapper.GetByDayList();
                    recurrencePattern.ByMonth = RRuleWrapper.ByMonth;
                    recurrencePattern.ByMonthDay = RRuleWrapper.ByMonthDay;
                    recurrencePattern.BySetPosition = RRuleWrapper.BySetPosition;
                    break;
                default:
                    break;
            }
            if (RRuleWrapper.Count > 0)
                recurrencePattern.Count = RRuleWrapper.Count;
            if (RRuleWrapper.Until.HasValue)
                recurrencePattern.Until = RRuleWrapper.Until.Value;

            //var rRuleResult = new RRuleResult() { RecurrencePatternString = recurrencePattern.ToString(), RecurrencePatternText = recurrencePattern.ToText() };
            var browserLang = Request.Headers["Accept-Language"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();
            var rRuleResult = new RRuleResult() { RecurrencePatternString = recurrencePattern.ToString(), RecurrencePatternText = recurrencePattern.ToText(new CultureInfo(browserLang)) };
            var startTime = RRuleWrapper.StartDate != null ? RRuleWrapper.StartDate : DateTime.Now;
            rRuleResult.RecurrencePatternList = RecurringRuleProcessor.GetAppointments(startTime, startTime.AddYears(maxYears), recurrencePattern.ToString()).Take(maxOccurences + 1).ToList();
            if (rRuleResult.RecurrencePatternList.Count > maxOccurences)
            {
                rRuleResult.RecurrencePatternList.RemoveAt(maxOccurences);
                rRuleResult.HintText = $"there are more occurences, only first {maxOccurences} occurences in the next {maxYears} years at most are listed";
            }

            return new JsonResult(rRuleResult);
        }

    }
}
