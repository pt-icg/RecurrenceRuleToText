using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using Ical.Net.DataTypes;

namespace IcgSoftware.IcalNetHumanReadableExtension.UnitTest
{
    [TestClass]
    public class RRuleTextBuilderTest
    {

        [TestMethod]
        public void JavaRRuleParser()
        {

            RRuleTextBuilderTestHelper.ToStringTest("FREQ=DAILY;INTERVAL=5", "every 5 days");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;INTERVAL=5", "every 5 weeks");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=MONTHLY;INTERVAL=5", "every 5 months");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=YEARLY;INTERVAL=5", "every 5 years");

            RRuleTextBuilderTestHelper.ToStringTest("FREQ=DAILY;INTERVAL=1", "every day");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;INTERVAL=1", "every week");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=MONTHLY;INTERVAL=1", "every month");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=YEARLY;INTERVAL=1", "every year");

            RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU", "every week on Monday, Tuesday");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU", "every 2 weeks on Monday, Tuesday");
            //RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "Every 2 weeks on Mon, Tue, Wed, Sat");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "every 2 weeks on Monday, Tuesday, Wednesday, Saturday");

            RRuleTextBuilderTestHelper.ToStringTest("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=5", "every month on the 5th");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=15", "every month on the 15th");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=MONTHLY;INTERVAL=2;BYMONTHDAY=15", "every 2 months on the 15th");

            RRuleTextBuilderTestHelper.ToStringTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=-1", "every 2 months on last Monday"); //jakubroztocil liefert: every 2 months on Monday
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=1", "every 2 months on first Monday"); //jakubroztocil liefert: every 2 months on Monday
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=SA;BYSETPOS=3", "every 2 months on third Saturday"); //jakubroztocil liefert: every 2 months on Saturday

            RRuleTextBuilderTestHelper.ToStringTest("FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=1", "every year on January 01");  //jakubroztocil liefert: every January on the 1st
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=YEARLY;BYMONTH=4;BYMONTHDAY=5", "every year on April 05"); //jakubroztocil liefert: every April on the 5th

            RRuleTextBuilderTestHelper.ToStringTest("FREQ=YEARLY;BYDAY=SU;BYSETPOS=1;BYMONTH=1", "every year on first Sunday of January"); //jakubroztocil liefert: every January on Sunday
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4", "every year on last Wednesday of April"); //jakubroztocil liefert: every April on Wednesday

            RRuleTextBuilderTestHelper.ToStringTest("FREQ=DAILY;INTERVAL=1;COUNT=2", "every day for 2 times");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;INTERVAL=1;COUNT=1", "every week"); //jakubroztocil liefert: every week for 1 time
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=MONTHLY;INTERVAL=1;UNTIL=20181023T220000Z", "every month until October 23, 2018");
        }

        [TestMethod]
        public void JavaRRuleParserG()
        {

            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=DAILY;INTERVAL=5", "alle 5 Tage");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;INTERVAL=5", "alle 5 Wochen");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=MONTHLY;INTERVAL=5", "alle 5 Monate");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=YEARLY;INTERVAL=5", "alle 5 Jahre");

            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=DAILY;INTERVAL=1", "jeden Tag");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;INTERVAL=1", "jede Woche");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=MONTHLY;INTERVAL=1", "jeden Monat");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=YEARLY;INTERVAL=1", "jedes Jahr");

            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU", "jede Woche am Montag, Dienstag");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU", "alle 2 Wochen am Montag, Dienstag");
            //RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "Every 2 weeks on Mon, Tue, Wed, Sat");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "alle 2 Wochen am Montag, Dienstag, Mittwoch, Sonnabend");

            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=5", "jeden Monat am 5.");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=15", "jeden Monat am 15.");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=MONTHLY;INTERVAL=2;BYMONTHDAY=15", "alle 2 Monate am 15.");

            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=-1", "alle 2 Monate am letzten Montag"); //jakubroztocil liefert: every 2 months on Monday
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=1", "alle 2 Monate am ersten Montag"); //jakubroztocil liefert: every 2 months on Monday
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=MONTHLY;INTERVAL=2;BYDAY=SA;BYSETPOS=3", "alle 2 Monate am dritten Sonnabend"); //jakubroztocil liefert: every 2 months on Saturday

            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=1", "jedes Jahr am 01. Januar");  //jakubroztocil liefert: every January on the 1st
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=YEARLY;BYMONTH=4;BYMONTHDAY=5", "jedes Jahr am 05. April"); //jakubroztocil liefert: every April on the 5th

            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=YEARLY;BYDAY=SU;BYSETPOS=1;BYMONTH=1", "jedes Jahr am ersten Sonntag im Januar"); //jakubroztocil liefert: every January on Sunday
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4", "jedes Jahr am letzten Mittwoch im April"); //jakubroztocil liefert: every April on Wednesday

            //RRuleTextBuilderTestHelper.ToStringTestG("FREQ=DAILY;INTERVAL=1;COUNT=2", "jeden Tag für 2 Wiederholungen");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=DAILY;INTERVAL=1;COUNT=2", "jeden Tag 2 Mal");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;INTERVAL=1;COUNT=1", "jede Woche"); //jakubroztocil liefert: every week for 1 time
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=MONTHLY;INTERVAL=1;UNTIL=20181023T220000Z", "jeden Monat bis zum 23. Oktober 2018");

        }


        [TestMethod]
        public void Custom1()
        {
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TH,MO,TU,WE", "every weekday for 30 times");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TU,WE,TH", "every week on Tuesday, Wednesday, Thursday, Friday for 30 times");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=SU;BYDAY=FR,TU,WE,TH,SU", "every week on Sunday, Tuesday, Wednesday, Thursday, Friday for 30 times"); //jakubroztocil liefert (keine Beachtung von Wochenanfang): every week on Tuesday, Wednesday, Thursday, Friday, Sunday for 30 times
        }


        [TestMethod]
        public void Custom1G()
        {

            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TH,MO,TU,WE", "jeden Wochentag 30 Mal");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TU,WE,TH", "jede Woche am Dienstag, Mittwoch, Donnerstag, Freitag 30 Mal");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=SU;BYDAY=FR,TU,WE,TH,SU", "jede Woche am Sonntag, Dienstag, Mittwoch, Donnerstag, Freitag 30 Mal");
        }



        [TestMethod]
        public void Jakubroztocil()
        {

            //Options
            RRuleTextBuilderTestHelper.ToStringTest("RRULE:FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO", "every week for 30 times");//"every week for 30 times");

            //RRule string
            RRuleTextBuilderTestHelper.ToStringTest("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "every week on Monday, Wednesday");
            RRuleTextBuilderTestHelper.ToStringTest("RRULE:FREQ=MONTHLY;BYMONTHDAY=10,15;COUNT=20", "every month on the 10th and 15th for 20 times");
            RRuleTextBuilderTestHelper.ToStringTest("RRULE:FREQ=DAILY;INTERVAL=3;COUNT=10", "every 3 days for 10 times");
            RRuleTextBuilderTestHelper.ToStringTest("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "every month on the 2nd last Friday for 7 times");


            //Text input
            RRuleTextBuilderTestHelper.ToStringTest("RRULE:FREQ=WEEKLY;BYDAY=MO,TU,WE,TH,FR", "every weekday");
            RRuleTextBuilderTestHelper.ToStringTest("RRULE:INTERVAL=2;FREQ=WEEKLY;BYDAY=TU", "every 2 weeks on Tuesday");
            RRuleTextBuilderTestHelper.ToStringTest("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "every week on Monday, Wednesday");
            RRuleTextBuilderTestHelper.ToStringTest("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "every month on the 2nd last Friday for 7 times");
            RRuleTextBuilderTestHelper.ToStringTest("RRULE:INTERVAL=6;FREQ=MONTHLY", "every 6 months");

            //extra
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=MONTHLY;BYMONTHDAY=10,15,21;COUNT=20", "every month on the 10th, 15th and 21st for 20 times");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=MONTHLY;BYDAY=2FR;COUNT=7", "every month on the 2nd Friday for 7 times");
            RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;BYDAY=SU,SA,MO,TU,WE,TH,FR", "every day");
            RRuleTextBuilderTestHelper.ToStringTest("RRULE:FREQ=WEEKLY;INTERVAL=2;BYDAY=TU", "every 2 weeks on Tuesday");

        }

        [TestMethod]
        public void JakubroztocilG()
        {

            //Options
            RRuleTextBuilderTestHelper.ToStringTestG("RRULE:FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO", "jede Woche 30 Mal");//"every week for 30 times");

            //RRule string
            //RRuleTextBuilderTestHelper.ToStringTestG("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "jede Woche am Montag und Mittwoch");
            RRuleTextBuilderTestHelper.ToStringTestG("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "jede Woche am Montag, Mittwoch"); //Schöner wäre am Montag und Mittwoch
            RRuleTextBuilderTestHelper.ToStringTestG("RRULE:FREQ=MONTHLY;BYMONTHDAY=10,15;COUNT=20", "jeden Monat am 10. und 15. 20 Mal");
            RRuleTextBuilderTestHelper.ToStringTestG("RRULE:FREQ=DAILY;INTERVAL=3;COUNT=10", "alle 3 Tage 10 Mal");
            RRuleTextBuilderTestHelper.ToStringTestG("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "jeden Monat am 2. letzten Freitag 7 Mal");


            //Text input
            RRuleTextBuilderTestHelper.ToStringTestG("RRULE:FREQ=WEEKLY;BYDAY=MO,TU,WE,TH,FR", "jeden Wochentag");
            RRuleTextBuilderTestHelper.ToStringTestG("RRULE:INTERVAL=2;FREQ=WEEKLY;BYDAY=TU", "alle 2 Wochen am Dienstag");
            RRuleTextBuilderTestHelper.ToStringTestG("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "jede Woche am Montag, Mittwoch");
            RRuleTextBuilderTestHelper.ToStringTestG("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "jeden Monat am 2. letzten Freitag 7 Mal");
            RRuleTextBuilderTestHelper.ToStringTestG("RRULE:INTERVAL=6;FREQ=MONTHLY", "alle 6 Monate");

            //extra
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=MONTHLY;BYMONTHDAY=10,15,21;COUNT=20", "jeden Monat am 10., 15. und 21. 20 Mal");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=MONTHLY;BYDAY=2FR;COUNT=7", "jeden Monat am 2. Freitag 7 Mal");
            RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;BYDAY=SU,SA,MO,TU,WE,TH,FR", "jeden Tag");
            RRuleTextBuilderTestHelper.ToStringTestG("RRULE:FREQ=WEEKLY;INTERVAL=2;BYDAY=TU", "alle 2 Wochen am Dienstag");
        }

        private class RRuleTextBuilderTestHelper
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

            public RRuleTextBuilderTestHelper(string rRuleString, string readableString, string culture)
            {

                RRuleString = rRuleString;
                ReadableString = readableString;
                string rRuleStringC = RRuleTextBuilder.GetRRuleStringCorrection(rRuleString);
                recurrencePattern = new RecurrencePattern(rRuleStringC);
                RecreatedRRuleString = recurrencePattern.ToString();
                if (culture == "de")
                    ReadableResult = recurrencePattern.ToText(new CultureInfo(culture));
                else
                    ReadableResult = recurrencePattern.ToText();
            }

            public bool Result { get => ReadableString == ReadableResult; }
            public string ResultMessage { get => $"{ReadableResult} (missed: {ReadableString})"; }

            public static void ToStringTest(string rRuleString, string readableString)
            {
                var rRuleTextBuilderTest = new RRuleTextBuilderTestHelper(rRuleString, readableString);
                Assert.IsTrue(rRuleTextBuilderTest.Result, rRuleTextBuilderTest.ResultMessage);
            }

            public static void ToStringTestG(string rRuleString, string readableString)
            {
                var rRuleTextBuilderTest = new RRuleTextBuilderTestHelper(rRuleString, readableString, "de");
                Assert.IsTrue(rRuleTextBuilderTest.Result, rRuleTextBuilderTest.ResultMessage);
            }

        }

    }

}
