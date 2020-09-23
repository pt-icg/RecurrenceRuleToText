using Ical.Net;
using Ical.Net.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecurrenceRuleWebApp.Models
{
    public class RRuleWrapper
    {
        public string Guid { get; set; }
        public DateTime StartDate { get; set; }
        public string Frequency { get; set; }
        public string event_recurring { get; set; }
        public int Interval { get; set; }
        public List<string> ByDayValue { get; set; }
        public int Count { get; set; }
        public List<int> ByMonth { get; set; }
        public List<int> ByMonthDay { get; set; }
        public List<int> BySetPosition { get; set; }
        public DateTime? Until { get; set; }

        public List<WeekDay> GetByDayList()
        {
            var result = new List<WeekDay>();
            foreach (var d in ByDayValue)
            {
                switch (d)
                {
                    case "MO":
                        result.Add(new WeekDay(DayOfWeek.Monday));
                        break;
                    case "TU":
                        result.Add(new WeekDay(DayOfWeek.Tuesday));
                        break;
                    case "WE":
                        result.Add(new WeekDay(DayOfWeek.Wednesday));
                        break;
                    case "TH":
                        result.Add(new WeekDay(DayOfWeek.Thursday));
                        break;
                    case "FR":
                        result.Add(new WeekDay(DayOfWeek.Friday));
                        break;
                    case "SA":
                        result.Add(new WeekDay(DayOfWeek.Saturday));
                        break;
                    case "SU":
                        result.Add(new WeekDay(DayOfWeek.Sunday));
                        break;
                }
            }

            return result;
        }

        public FrequencyType GetFrequencyType()
        {

            switch (Frequency)
            {
                case "daily":
                    return FrequencyType.Daily;
                case "weekly":
                    return FrequencyType.Weekly;
                case "monthly":
                    return FrequencyType.Monthly;
                case "yearly":
                    return FrequencyType.Yearly;
                default:
                    return FrequencyType.None;
            }

            /*
                 public enum FrequencyType
                {
                    None = 0,
                    Secondly = 1,
                    Minutely = 2,
                    Hourly = 3,
                    Daily = 4,
                    Weekly = 5,
                    Monthly = 6,
                    Yearly = 7
                }

             */
        }


    }
}
