using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IcgSoftware.RecurrenceRuleToText.UnitTest
{
    [TestClass]
    public class RRuleTextBuilderTestEn : RRuleTextBuilderTestBase
    {
        protected override void ToTextTest(string rRuleString, string readableString)
        {
            Debug.WriteLine("EN: " + readableString);
            var rRuleTextBuilderTest = new RRuleTextBuilderTestHelper(rRuleString, readableString, "en");
            Assert.IsTrue(rRuleTextBuilderTest.Result, rRuleTextBuilderTest.ResultMessage);
        }
        
        protected override void ToTextTestWithShortWeekday(string rRuleString, string readableString)
        {
            Debug.WriteLine("EN: " + readableString);
            var rRuleTextBuilderTest = new RRuleTextBuilderTestHelper(rRuleString, readableString, "en", new DisplayOptions(){ShortWeekdays = true});
            Assert.IsTrue(rRuleTextBuilderTest.Result, rRuleTextBuilderTest.ResultMessage);
        }

        [TestMethod]
        public void LastNumeral()
        {
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-1MO", "every month on the last Monday");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2MO", "every month on the 2nd last Monday");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-3FR", "every month on the 3rd last Friday");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-4SU", "every month on the 4th last Sunday");
        }

        [TestMethod]
        public void JavaRruleParser()
        {
            //aditosoftware tests

            ToTextTest("FREQ=DAILY;INTERVAL=5", "every 5 days");
            ToTextTest("FREQ=WEEKLY;INTERVAL=5", "every 5 weeks");
            ToTextTest("FREQ=MONTHLY;INTERVAL=5", "every 5 months");
            ToTextTest("FREQ=YEARLY;INTERVAL=5", "every 5 years");

            ToTextTest("FREQ=DAILY;INTERVAL=1", "every day");
            ToTextTest("FREQ=WEEKLY;INTERVAL=1", "every week");
            ToTextTest("FREQ=MONTHLY;INTERVAL=1", "every month");
            ToTextTest("FREQ=YEARLY;INTERVAL=1", "every year");

            ToTextTest("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU", "every week on Monday, Tuesday");
            ToTextTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU", "every 2 weeks on Monday, Tuesday");
            //RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "Every 2 weeks on Mon, Tue, Wed, Sat");
            ToTextTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "every 2 weeks on Monday, Tuesday, Wednesday, Saturday");

            ToTextTest("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=5", "every month on the 5th");
            ToTextTest("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=15", "every month on the 15th");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYMONTHDAY=15", "every 2 months on the 15th");

            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=-1", "every 2 months on last Monday"); //jakubroztocil liefert: every 2 months on Monday
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=1", "every 2 months on first Monday"); //jakubroztocil liefert: every 2 months on Monday
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=SA;BYSETPOS=3", "every 2 months on third Saturday"); //jakubroztocil liefert: every 2 months on Saturday

            ToTextTest("FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=1", "every year on January the 1st");  //jakubroztocil liefert: every January on the 1st            
            ToTextTest("FREQ=YEARLY;BYMONTH=4;BYMONTHDAY=5", "every year on April the 5th"); //jakubroztocil liefert: every April on the 5th

            ToTextTest("FREQ=YEARLY;BYDAY=SU;BYSETPOS=1;BYMONTH=1", "every year on first Sunday of January"); //jakubroztocil liefert: every January on Sunday
            ToTextTest("FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4", "every year on last Wednesday of April"); //jakubroztocil liefert: every April on Wednesday

            ToTextTest("FREQ=DAILY;INTERVAL=1;COUNT=2", "every day for 2 times");
            ToTextTest("FREQ=WEEKLY;INTERVAL=1;COUNT=1", "every week"); //jakubroztocil liefert: every week for 1 time
            ToTextTest("FREQ=MONTHLY;INTERVAL=1;UNTIL=20181023T220000Z", "every month until October 23, 2018");
        }

        [TestMethod]
        public void Custom()
        {
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TH,MO,TU,WE", "every weekday for 30 times");
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TU,WE,TH", "every week on Tuesday, Wednesday, Thursday, Friday for 30 times");
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=SU;BYDAY=FR,TU,WE,TH,SU", "every week on Sunday, Tuesday, Wednesday, Thursday, Friday for 30 times"); //jakubroztocil liefert (keine Beachtung von Wochenanfang): every week on Tuesday, Wednesday, Thursday, Friday, Sunday for 30 times
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=1", "every 2 months on first Friday");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=2", "every 2 months on second Friday");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=3", "every 2 months on third Friday");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=4", "every 2 months on fourth Friday");
            ToTextTest("FREQ=YEARLY;INTERVAL=3;BYMONTH=3,4", "every 3 years in March and April");
        }

        [TestMethod]
        public void RruleJsDemoApp()
        {
            //https://github.com/jakubroztocil/rrule

            //Options
            ToTextTest("RRULE:FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO", "every week for 30 times");

            //RRule string
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "every week on Monday, Wednesday");
            ToTextTest("RRULE:FREQ=MONTHLY;BYMONTHDAY=10,15;COUNT=20", "every month on the 10th and 15th for 20 times");
            ToTextTest("RRULE:FREQ=DAILY;INTERVAL=3;COUNT=10", "every 3 days for 10 times");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "every month on the 2nd last Friday for 7 times");


            //Text input
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,TU,WE,TH,FR", "every weekday");
            ToTextTest("RRULE:INTERVAL=2;FREQ=WEEKLY;BYDAY=TU", "every 2 weeks on Tuesday");
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "every week on Monday, Wednesday");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "every month on the 2nd last Friday for 7 times");
            ToTextTest("RRULE:INTERVAL=6;FREQ=MONTHLY", "every 6 months");

            //extra
            ToTextTest("FREQ=MONTHLY;BYMONTHDAY=10,15,21;COUNT=20", "every month on the 10th, 15th and 21st for 20 times");
            ToTextTest("FREQ=MONTHLY;BYDAY=2FR;COUNT=7", "every month on the 2nd Friday for 7 times");
            ToTextTest("FREQ=WEEKLY;BYDAY=SU,SA,MO,TU,WE,TH,FR", "every day");
            ToTextTest("RRULE:FREQ=WEEKLY;INTERVAL=2;BYDAY=TU", "every 2 weeks on Tuesday");
        }

        [TestMethod]
        public void YearlyDay()
        {
            ToTextTest("FREQ=YEARLY;BYDAY=SU,MO,TU,WE,TH,FR,SA;BYMONTH=1;BYSETPOS=1", "every year on first day of January");
        }
        
        [TestMethod]
        public void YearlyWeekday()
        {
            ToTextTest("FREQ=YEARLY;BYDAY=MO,TU,WE,TH,FR;BYMONTH=1;BYSETPOS=1", "every year on first weekday of January");
        }
        
        [TestMethod]
        public void YearlyWeekendDay()
        {
            ToTextTest("FREQ=YEARLY;BYDAY=SU,SA;BYMONTH=1;BYSETPOS=1", "every year on first weekend days of January");
        }
    }
}
