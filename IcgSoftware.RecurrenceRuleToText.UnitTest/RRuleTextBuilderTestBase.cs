﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using Ical.Net.DataTypes;

namespace IcgSoftware.RecurrenceRuleToText.UnitTest
{
    abstract public class RRuleTextBuilderTestBase
    {

        abstract protected void ToTextTest(string rRuleString, string readableString);

        abstract protected void ToTextTestWithShortWeekday(string rRuleString, string readableString);
        
        protected class RRuleTextBuilderTestHelper
        {
            private RecurrencePattern recurrencePattern;
            public string RRuleString;
            public string ReadableString;
            public string ReadableResult;
            public string RecreatedRRuleString;

            public RRuleTextBuilderTestHelper(string rRuleString, string readableString)
            {
                RRuleString = rRuleString;
                ReadableString = readableString;
                string rRuleStringC = RRuleTextBuilder.GetRRuleStringCorrection(rRuleString);
                recurrencePattern = new RecurrencePattern(rRuleStringC);
                RecreatedRRuleString = recurrencePattern.ToString();
                ReadableResult = recurrencePattern.ToText();
            }

            public RRuleTextBuilderTestHelper(string rRuleString, string readableString, string culture, DisplayOptions options = null)
            {
                options = options ?? new DisplayOptions();
                
                RRuleString = rRuleString;
                ReadableString = readableString;
                string rRuleStringC = RRuleTextBuilder.GetRRuleStringCorrection(rRuleString);
                recurrencePattern = new RecurrencePattern(rRuleStringC);
                RecreatedRRuleString = recurrencePattern.ToString();
                ReadableResult = recurrencePattern.ToText(new CultureInfo(culture), options);
            }

            public bool Result { get => ReadableString == ReadableResult; }
            public string ResultMessage { get => $"{ReadableResult} (missed: {ReadableString})"; }

        }

    }

}
