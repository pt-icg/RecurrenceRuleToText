using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using Ical.Net;
using Ical.Net.DataTypes;
using IcgSoftware.IntToOrdinalNumber;


namespace IcgSoftware.RecurrenceRuleToText
{

    //weekstart WKST noch nicht in Wochentagen
    public static class RRuleTextBuilder
    {

        public static string ToText(this RecurrencePattern recurrencePattern)
        {
            return ToText(recurrencePattern, GetDefaultCulture());
        }

        public static string ToText(this RecurrencePattern recurrencePattern, CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
            {
                cultureInfo = GetDefaultCulture();
            }
            var rRuleTextBuilderIntern = new RRuleTextBuilderIntern(recurrencePattern, cultureInfo);
            return rRuleTextBuilderIntern.ToText();
        }

        public static bool IsWeekdays(this RecurrencePattern recurrencePattern)
        {
            List<DayOfWeek> weekDaysR = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
            List<DayOfWeek> weekDaysP = recurrencePattern.ByDay.Select(i => i.DayOfWeek).ToList();
            return weekDaysR.Except(weekDaysP).ToList().Count == 0 && weekDaysP.Except(weekDaysR).ToList().Count == 0;
        }

        public static bool IsEveryDay(this RecurrencePattern recurrencePattern)
        {
            List<DayOfWeek> weekDaysR = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
            List<DayOfWeek> weekDaysP = recurrencePattern.ByDay.Select(i => i.DayOfWeek).ToList();
            return weekDaysR.Except(weekDaysP).ToList().Count == 0 && weekDaysP.Except(weekDaysR).ToList().Count == 0;
        }

        public static CultureInfo GetDefaultCulture()
        {
            var cultureInfo = CultureInfo.DefaultThreadCurrentUICulture;
            if (cultureInfo == null)
                cultureInfo = CultureInfo.CurrentUICulture;
            return cultureInfo;
        }

        public static IEnumerable<CultureInfo> GetAvailableCultures()
        {
            List<CultureInfo> result = new List<CultureInfo>();

            ResourceManager rm = new ResourceManager(typeof(Language));

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            foreach (CultureInfo culture in cultures)
            {
                try
                {
                    //if (culture.Equals(CultureInfo.InvariantCulture)) continue; //do not use "==", won't work
                    ResourceSet rs = rm.GetResourceSet(culture, true, false);
                    if (rs != null)
                        result.Add(culture);
                }
                catch (CultureNotFoundException)
                {
                    //NOP
                }
            }
            return result;
        }

        public static string GetRRuleStringCorrection(string RRuleString)
        {
            //there's a problem for FREQ not in front position e.g. "RRULE:INTERVAL=2;FREQ=WEEKLY;BYDAY=TU"
            string rRuleValues = RRuleString;
            string leadingValue = null;
            if (RRuleString.Contains(':'))
            {
                List<string> list = RRuleString.Split(':').ToList();
                leadingValue = list.Where(i => i.ToUpper().StartsWith("RRULE")).First();
                rRuleValues = list.Where(i => !i.ToUpper().StartsWith("RRULE")).First();
            }
            if (rRuleValues == null) //other problem
                return RRuleString;
            List<string> rRuleValuesList = rRuleValues.Split(';').ToList();
            string freqValue = rRuleValuesList.Where(i => i.ToUpper().StartsWith("FREQ")).First();
            if (freqValue == null) //other problem or default year?
                return RRuleString;
            rRuleValuesList.Remove(freqValue);
            string result = !string.IsNullOrWhiteSpace(leadingValue) ? $"{leadingValue}:" : "";
            result += freqValue + ";" + string.Join(";", rRuleValuesList);
            return result;
        }

        private class RRuleTextBuilderIntern
        {
            private readonly RecurrencePattern recurrencePattern;
            private readonly CultureInfo ordinalCulture;

            public RRuleTextBuilderIntern(RecurrencePattern recurrencePattern, CultureInfo cultureInfo)
            {
                this.recurrencePattern = recurrencePattern;
                Language.Culture = cultureInfo;
                //default language is "en" but ToOrdinalNumber has a default dot
                ordinalCulture = cultureInfo.Equals(CultureInfo.InvariantCulture) ? new CultureInfo("en") : cultureInfo;
            }

            public string ToText()
            {
                var frequency = BuildFrequency();
                var day = "";
                var count = "";

                switch (recurrencePattern.Frequency)
                {

                    case FrequencyType.Daily:
                        break;
                    case FrequencyType.Weekly:
                        if (recurrencePattern.IsWeekdays())
                        {
                            frequency = "";
                            day = Language.WeeklyEveryWeekday;
                        }
                        else if (recurrencePattern.IsEveryDay())
                        {
                            frequency = "";
                            day = Language.WeeklyEveryDay;
                        }
                        else
                            day += BuildWeeklyDays();
                        break;
                    case FrequencyType.Monthly:
                        if (recurrencePattern.ByMonthDay.Count > 0)
                            day += BuildMonthlyOnDay();
                        else if (recurrencePattern.BySetPosition.Count > 0 && recurrencePattern.ByDay.Count > 0)
                            day += BuildMonthlyOnNumberedDay();
                        else if (recurrencePattern.ByDay.Count > 0)
                            day += BuildMonthlyOnWeekDay();
                        break;
                    case FrequencyType.Yearly:
                        if (recurrencePattern.ByMonth.Count > 0 && recurrencePattern.ByMonthDay.Count > 0)
                            day += BuildYearlyOnDay();
                        else if (recurrencePattern.BySetPosition.Count > 0 && recurrencePattern.ByDay.Count > 0 && recurrencePattern.ByMonth.Count > 0)
                            day += BuildYearlyOnNumbered();
                        break;
                }

                // Endings
                if (recurrencePattern.Until != DateTime.MinValue)
                {

                    String ending = BuildUntilDateEnding();
                    if (ending != null)
                        count = ending;
                }
                else if (recurrencePattern.Count != int.MinValue)
                {
                    String ending = BuildCountEnding();
                    if (ending != null)
                        count = ending;
                }

                var result = String.Format(Language.PhraseStructure, frequency, day, count).Replace("  ", " ").Trim();
                try
                {
                    return bool.Parse(Language.PhraseFirstCharToUpper) ? FirstCharToUpper(result) : result;
                }
                catch (Exception)
                {
                    return result;
                }
            }

            private string BuildFrequency()
            {
                String value = null;
                if (recurrencePattern.Interval == 1)
                {
                    switch (recurrencePattern.Frequency)
                    {
                        //not supported
                        //case FrequencyType.None:
                        //    break;
                        //case FrequencyType.Secondly:
                        //    break;
                        //case FrequencyType.Minutely:
                        //    break;
                        //case FrequencyType.Hourly:
                        //    break;
                        //default:
                        //    break;

                        case FrequencyType.Daily:
                            value = Language.Daily;
                            break;
                        case FrequencyType.Weekly:
                            value = Language.Weekly;
                            break;
                        case FrequencyType.Monthly:
                            value = Language.Monthly;
                            break;
                        case FrequencyType.Yearly:
                            value = Language.Yearly;
                            break;
                    }
                }
                else
                {
                    switch (recurrencePattern.Frequency)
                    {
                        case FrequencyType.Daily:
                            value = String.Format(Language.DailyEveryNDays, recurrencePattern.Interval);
                            break;
                        case FrequencyType.Weekly:
                            value = String.Format(Language.WeeklyEveryNWeeks, recurrencePattern.Interval);
                            break;
                        case FrequencyType.Monthly:
                            value = String.Format(Language.MonthlyEveryNMonths, recurrencePattern.Interval);
                            break;
                        case FrequencyType.Yearly:
                            value = String.Format(Language.YearlyEveryNYears, recurrencePattern.Interval);
                            break;
                    }
                }

                if (value == null)
                    return "";
                return value;
            }

            private string BuildWeeklyDays()
            {
                if (recurrencePattern.ByDay.Count == 0)
                    return "";

                List<DayOfWeek> sorted = GetSortedDayOfWeeks(recurrencePattern.ByDay.Select(i => i.DayOfWeek).ToList(), recurrencePattern.FirstDayOfWeek);
                return String.Format(Language.WeeklyDays, string.Join(", ", sorted.Select(i => GetDayOfWeekString(i))));
            }


            private string BuildMonthlyOnDay()
            {
                if (recurrencePattern.ByMonthDay.Count == 0)
                    return "";

                if (recurrencePattern.ByMonthDay.Count == 1)
                    return string.Format(Language.MonthlyOnDay, recurrencePattern.ByMonthDay.First().ToOrdinalNumber(ordinalCulture));
                else
                    return string.Format(Language.MonthlyOnDayPlural, GetDayChain(recurrencePattern.ByMonthDay));
            }


            private String GetDayChain(List<int> dayList)
            {
                switch (dayList.Count)
                {
                    case 0:
                    case 1:
                        return "";
                    case 2:
                        return string.Join($" {Language.DayChainSeparator} ", dayList.OrderBy(i => i).Select(i => i.ToOrdinalNumber(ordinalCulture)).ToList());
                    default:
                        var sorted = dayList.OrderBy(i => i).ToList();
                        return string.Join(", ", sorted.Take(sorted.Count - 1).Select(i => i.ToOrdinalNumber(ordinalCulture)).ToList()) + $" {Language.DayChainSeparator} " + sorted.Last().ToOrdinalNumber(ordinalCulture);
                }

            }

            //private String GetDayChain(List<WeekDay> dayList)
            //{
            //    switch (dayList.Count)
            //    {
            //        case 0:
            //        case 1:
            //            return "";
            //        case 2:
            //            return string.Join(" and ", dayList.OrderBy(i => i).Select(i => i.Ordinal()).ToList());
            //        default:
            //            var sorted = dayList.OrderBy(i => i).ToList();
            //            return string.Join(", ", sorted.Take(sorted.Count - 1).Select(i => i.Ordinal()).ToList()) + " and " + sorted.Last().Ordinal();
            //    }

            //}


            private String BuildMonthlyOnNumberedDay()
            {
                if (recurrencePattern.ByDay.Count == 0 || recurrencePattern.BySetPosition.Count == 0)
                    return "";

                var dayValue = TranslateSetPosNumber(recurrencePattern.BySetPosition.First());
                DayOfWeek dayOfWeek = recurrencePattern.ByDay.Select(i => i.DayOfWeek).First();
                dayValue += " " + GetDayOfWeekString(dayOfWeek);
                return String.Format(Language.MonthlyOnNumberedDay, dayValue);
            }


            private string BuildMonthlyOnWeekDay()
            {
                if (recurrencePattern.ByDay.Count == 0)
                    return "";

                if (recurrencePattern.ByDay.Count == 1)
                {
                    WeekDay wd = recurrencePattern.ByDay.First();                    
                    if (wd.Offset < 0)
                    {
                        string last;
                        switch (wd.Offset)
                        {
                            case -1:
                                last = String.Format(Language.LastNumeral, GetDayOfWeekString(wd.DayOfWeek));
                                break;
                            case -2:
                                last = String.Format(Language.SecondLastNumeral, GetDayOfWeekString(wd.DayOfWeek));
                                break;
                            case -3:
                                last = String.Format(Language.ThirdLastNumeral, GetDayOfWeekString(wd.DayOfWeek));
                                break;
                            case -4:
                                last = String.Format(Language.FourthLastNumeral, GetDayOfWeekString(wd.DayOfWeek));
                                break;
                            default:
                                last = String.Format("{0} {1}", Math.Abs(wd.Offset).ToOrdinalNumber(ordinalCulture), GetDayOfWeekString(wd.DayOfWeek)).Trim();
                                break;
                        }
                        return String.Format(Language.MonthlyOnWeekDayNumeral, last);
                    }
                    else
                    {
                        var value = String.Format("{0} {1}", Math.Abs(wd.Offset).ToOrdinalNumber(ordinalCulture), GetDayOfWeekString(wd.DayOfWeek)).Trim();
                        return String.Format(Language.MonthlyOnWeekDay, value);
                    }                        

                }
                else
                {
                    return " (not implemented) ";
                }

            }

            private String TranslateSetPosNumber(int number)
            {
                if (number == -1)
                    return Language.Last;
                if (number == 1)
                    return Language.First;
                if (number == 2)
                    return Language.Second;
                if (number == 3)
                    return Language.Third;
                if (number == 4)
                    return Language.Fourth;
                return "";
            }

            private String BuildYearlyOnDay()
            {
                if (recurrencePattern.ByMonth.Count == 0 || recurrencePattern.ByMonthDay.Count == 0)
                    return "";

                DateTime day = new DateTime(DateTime.Today.Year, recurrencePattern.ByMonth.First(), recurrencePattern.ByMonthDay.First());
                return String.Format(Language.YearlyOnDay, day.ToString(Language.YearlyOnDayMonthPattern, Language.Culture), day.Day.ToOrdinalNumber(ordinalCulture)).Trim();
            }


            private String BuildYearlyOnNumbered()
            {
                if (recurrencePattern.BySetPosition.Count == 0 && recurrencePattern.ByDay.Count == 0 && recurrencePattern.ByMonth.Count == 0)
                    return "";

                DayOfWeek dayOfWeek = recurrencePattern.ByDay.Select(i => i.DayOfWeek).First();
                DateTime day = new DateTime(DateTime.Today.Year, recurrencePattern.ByMonth.First(), 1);
                return String.Format(Language.YearlyOnNumbered,
                    TranslateSetPosNumber(recurrencePattern.BySetPosition.First()),
                    GetDayOfWeekString(dayOfWeek),
                    day.ToString(Language.YearlyOnNumberedMonthFormat, Language.Culture));
            }

            private String BuildUntilDateEnding()
            {
                if (recurrencePattern.Until == DateTime.MinValue)
                    return null;

                return String.Format(Language.UntilDateEnding, recurrencePattern.Until.ToString(Language.UntilDateEndingDateFormat, Language.Culture));
            }

            private String BuildCountEnding()
            {
                if (recurrencePattern.Count == int.MinValue)
                    return null;

                if (recurrencePattern.Count == 1)
                    return null;

                return String.Format(Language.CountEnding, recurrencePattern.Count);
            }


            private static List<DayOfWeek> GetSortedDayOfWeeks(List<DayOfWeek> list, DayOfWeek firstDayOfWeek)
            {
                return list.OrderBy(x => (x - firstDayOfWeek + 7) % 7).ToList();
            }

            public string FirstCharToUpper(string input)
            {
                switch (input)
                {
                    case null: throw new ArgumentNullException(nameof(input));
                    case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                    default: return input.First().ToString().ToUpper() + input.Substring(1);
                }
            }

            public string GetDayOfWeekString(DayOfWeek dayOfWeek)
            {
                switch (dayOfWeek)
                {
                    case DayOfWeek.Monday:
                        return Language.Monday;
                    case DayOfWeek.Tuesday:
                        return Language.Tuesday;
                    case DayOfWeek.Wednesday:
                        return Language.Wednesday;
                    case DayOfWeek.Thursday:
                        return Language.Thursday;
                    case DayOfWeek.Friday:
                        return Language.Friday;
                    case DayOfWeek.Saturday:
                        return Language.Saturday;
                    case DayOfWeek.Sunday:
                    default:
                        return Language.Sunday;
                }
            }

        }

    }

}
