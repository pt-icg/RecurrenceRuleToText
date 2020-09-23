using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecurrenceRuleWebApp.Models
{
    public class RecurringRuleProcessor
    {
        public DateTime Start { get; set; }
        public RecurrencePattern RecurrencePattern { get; set; }

        public HashSet<Occurrence> GetOccurrences(DateTime startTime, DateTime endTime)
        {
            var calendarEvent = new CalendarEvent
            {
                Start = new CalDateTime(Start),
                RecurrenceRules = new List<RecurrencePattern> { RecurrencePattern },
            };

            return calendarEvent.GetOccurrences(startTime.AddMilliseconds(-1), endTime);
        }

        public static List<DateTime> GetAppointments(DateTime startTime, DateTime endTime, RecurrencePattern RecurrencePattern)
        {
            var recurringRuleProcessor = new RecurringRuleProcessor() { Start = startTime, RecurrencePattern = RecurrencePattern };
            return recurringRuleProcessor.GetOccurrences(startTime, endTime).Select(i => i.Period.StartTime.Value).ToList();
        }

        public static List<DateTime> GetAppointments(DateTime startTime, DateTime endTime, string recurrencePatternValue)
        {
            var recurringRuleProcessor = new RecurringRuleProcessor() { Start = startTime, RecurrencePattern = new RecurrencePattern(recurrencePatternValue) };
            return recurringRuleProcessor.GetOccurrences(startTime, endTime).Select(i => i.Period.StartTime.Value).ToList();
        }

        public static Tuple<RecurrencePattern, List<DateTime>> GetRecurrencePatternAppointments(DateTime startTime, DateTime endTime, string recurrencePatternValue)
        {
            var recurringRuleProcessor = new RecurringRuleProcessor() { Start = startTime, RecurrencePattern = new RecurrencePattern(recurrencePatternValue) };
            return new Tuple<RecurrencePattern, List<DateTime>>(recurringRuleProcessor.RecurrencePattern, recurringRuleProcessor.GetOccurrences(startTime, endTime).Select(i => i.Period.StartTime.Value).ToList());
        }

        public static RecurrencePattern GetRecurrencePattern(DateTime startTime, string recurrencePatternValue)
        {
            var recurringRuleProcessor = new RecurringRuleProcessor() { Start = startTime, RecurrencePattern = new RecurrencePattern(recurrencePatternValue) };
            return recurringRuleProcessor.RecurrencePattern;
        }


        public static List<Occurrence> GetOccurrences(DateTime startTime, DateTime endTime, string recurrencePatternValue)
        {
            var recurringRuleProcessor = new RecurringRuleProcessor() { Start = startTime, RecurrencePattern = new RecurrencePattern(recurrencePatternValue) };
            return recurringRuleProcessor.GetOccurrences(startTime, endTime).ToList();
        }

    }
}
