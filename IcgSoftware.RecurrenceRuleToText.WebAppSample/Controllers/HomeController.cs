using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IcgSoftware.RecurrenceRuleToText.WebAppSample.Models;
using Ical.Net.DataTypes;
using System.Globalization;
using System.Resources;

namespace IcgSoftware.RecurrenceRuleToText.WebAppSample.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            List<RRuleHumanReadableSample> list = new List<RRuleHumanReadableSample>();
            List<string> samples = new List<string>()
            {
                "FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU",
                "FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4",
                "FREQ=WEEKLY;BYDAY=MO,WE"
            };
            foreach (var sample in samples)
            {
                var recurrencePattern = new RecurrencePattern(sample);
                list.Add(new RRuleHumanReadableSample() { Language = RRuleTextBuilder.GetDefaultCulture().EnglishName, RRule = sample, HumanReadable = recurrencePattern.ToText() });
            }

            var defaultCulture = RRuleTextBuilder.GetDefaultCulture();
            foreach (var cultureInfo in RRuleTextBuilder.GetAvailableCultures())
            {
                if (cultureInfo.TwoLetterISOLanguageName == defaultCulture.TwoLetterISOLanguageName)
                    continue;
                foreach (var sample in samples)
                {
                    var recurrencePattern = new RecurrencePattern(sample);
                    list.Add(new RRuleHumanReadableSample() { Language = cultureInfo.EnglishName, RRule = sample, HumanReadable = recurrencePattern.ToText(cultureInfo) });
                }
            }

            return View(list);
        }

    }
}
