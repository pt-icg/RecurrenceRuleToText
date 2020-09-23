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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecurrenceRuleWebApp.Models;

namespace RecurrenceRuleWebApp.Controllers
{
    //public class HomeController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }

    //    public IActionResult Privacy()
    //    {
    //        return View();
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //    }
    //}
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly int maxOccurences = 50;
        private readonly int maxYears = 20;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index0()
        {
            //RecurringRule recurringRule = new RecurringRule() { BaseTime = new DateTime(2020, 9, 3, 19, 30, 0, DateTimeKind.Local), Rule = "FREQ=WEEKLY;INTERVAL=3;BYDAY=TH,SU" };
            //RecurringRule recurringRule = new RecurringRule() { BaseTime = new DateTime(2020, 9, 3, 19, 30, 0, DateTimeKind.Local), Rule = "FREQ=MONTHLY;INTERVAL=3;BYMONTHDAY=4,6;UNTIL=20200930T000000" };
            //RecurringRule recurringRule = new RecurringRule() { BaseTime = new DateTime(2020, 9, 3, 19, 30, 0, DateTimeKind.Local), Rule = "FREQ=MONTHLY;INTERVAL=3;UNTIL=20200930T000000;BYDAY=TU;BYSETPOS=2" };
            //RecurringRule recurringRule = new RecurringRule() { BaseTime = new DateTime(2020, 9, 3, 19, 30, 0, DateTimeKind.Local), Rule = "FREQ=YEARLY;COUNT=10;BYMONTH=2;BYMONTHDAY=2" };
            //RecurringRule recurringRule = new RecurringRule() { BaseTime = new DateTime(2020, 9, 3, 19, 30, 0, DateTimeKind.Local), Rule = "FREQ=YEARLY;COUNT=10;BYMONTH=1,12" };
            RecurringRule recurringRule = new RecurringRule() { BaseTime = new DateTime(2020, 9, 3, 19, 30, 0, DateTimeKind.Local), Rule = "FREQ=YEARLY;COUNT=10;BYDAY=WE;BYMONTH=3;BYSETPOS=3" };

            var startTime = recurringRule.BaseTime;
            Tuple<RecurrencePattern, List<DateTime>> tuple = RecurringRuleProcessor.GetRecurrencePatternAppointments(startTime, startTime.AddYears(maxYears), recurringRule.Rule);
            recurringRule.JsonRecurrencePattern = JsonConvert.SerializeObject(tuple.Item1);
            recurringRule.Output = tuple.Item2;
            return View(recurringRule);
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult GetRecurringRuleText(String RecurringRule)
        //{
        //    if (String.IsNullOrWhiteSpace(RecurringRule))
        //        return new JsonResult("");
        //    var recurrencePattern = new RecurrencePattern(RecurringRule);
        //    return new JsonResult(recurrencePattern.ToText());
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        [HttpPost]
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
                    break;
                case FrequencyType.Secondly:
                    break;
                case FrequencyType.Minutely:
                    break;
                case FrequencyType.Hourly:
                    break;
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
                rRuleResult.HintText = $"there are more occurences, only first {maxOccurences} occurences are listed";
            }

            return new JsonResult(rRuleResult);
        }

    }

}

