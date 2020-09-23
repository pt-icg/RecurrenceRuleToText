using Ical.Net.DataTypes;
using IcgSoftware.RecurrenceRuleToText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecurrenceRuleWebApp.Models
{
    public class RecurringRule
    {
        public DateTime BaseTime { get; set; }
        public string Rule { get; set; }
        //public string RRule => $"DTSTART={BaseTime.ToString("yyyyMMdd")};{Rule}";
        //public string RRule => $"DTSTART={BaseTime.ToString("O")};{Rule}"; //ToString("O") ISO 8601
        public string RRule => $"DTSTART={BaseTime.ToString("s")};{Rule}"; //ToString("O") ISO 8601
        public List<DateTime> Output { get; set; } = new List<DateTime>();

        //public RecurrencePattern RecurrencePattern { get; set; }
        public string JsonRecurrencePattern { get; set; }
        //public JsonResult JsonResultRecurrencePattern { get; set; }


        public string RecurringRuleText => GetRecurringRuleText();

        private string GetRecurringRuleText()
        {
            if (String.IsNullOrWhiteSpace(Rule))
                return "";
            var recurrencePattern = new RecurrencePattern(Rule);
            return recurrencePattern.ToText();
        }
    }
}
