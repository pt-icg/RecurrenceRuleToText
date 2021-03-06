﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecurrenceRuleWebAppVue.Models
{
    public class RRuleResult
    {
        public string ErrorText { get; set; } = "";
        public string RecurrencePatternString { get; set; } = "";
        public string RecurrencePatternText { get; set; } = "";
        public List<DateTime> RecurrencePatternList { get; set; } = new List<DateTime>();
        public string HintText { get; set; } = "";
    }
}
