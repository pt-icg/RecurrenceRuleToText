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
        public void JavaRruleParserEn()
        {
            //aditosoftware tests

            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=DAILY;INTERVAL=5", "every 5 days");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=WEEKLY;INTERVAL=5", "every 5 weeks");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=5", "every 5 months");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=YEARLY;INTERVAL=5", "every 5 years");

            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=DAILY;INTERVAL=1", "every day");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=WEEKLY;INTERVAL=1", "every week");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=1", "every month");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=YEARLY;INTERVAL=1", "every year");

            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU", "every week on Monday, Tuesday");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU", "every 2 weeks on Monday, Tuesday");
            //RRuleTextBuilderTestHelper.ToStringTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "Every 2 weeks on Mon, Tue, Wed, Sat");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "every 2 weeks on Monday, Tuesday, Wednesday, Saturday");

            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=5", "every month on the 5th");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=15", "every month on the 15th");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=2;BYMONTHDAY=15", "every 2 months on the 15th");

            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=-1", "every 2 months on last Monday"); //jakubroztocil liefert: every 2 months on Monday
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=1", "every 2 months on first Monday"); //jakubroztocil liefert: every 2 months on Monday
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=2;BYDAY=SA;BYSETPOS=3", "every 2 months on third Saturday"); //jakubroztocil liefert: every 2 months on Saturday

            //RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=1", "every year on January 01");  //jakubroztocil liefert: every January on the 1st
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=1", "every year on January the 1st");  //jakubroztocil liefert: every January on the 1st            
            //RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=YEARLY;BYMONTH=4;BYMONTHDAY=5", "every year on April 05"); //jakubroztocil liefert: every April on the 5th
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=YEARLY;BYMONTH=4;BYMONTHDAY=5", "every year on April the 5th"); //jakubroztocil liefert: every April on the 5th

            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=YEARLY;BYDAY=SU;BYSETPOS=1;BYMONTH=1", "every year on first Sunday of January"); //jakubroztocil liefert: every January on Sunday
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4", "every year on last Wednesday of April"); //jakubroztocil liefert: every April on Wednesday

            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=DAILY;INTERVAL=1;COUNT=2", "every day for 2 times");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=WEEKLY;INTERVAL=1;COUNT=1", "every week"); //jakubroztocil liefert: every week for 1 time
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=1;UNTIL=20181023T220000Z", "every month until October 23, 2018");
        }

        [TestMethod]
        public void JavaRruleParserDe()
        {
            //aditosoftware tests

            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=DAILY;INTERVAL=5", "alle 5 Tage");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=WEEKLY;INTERVAL=5", "alle 5 Wochen");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=5", "alle 5 Monate");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=YEARLY;INTERVAL=5", "alle 5 Jahre");

            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=DAILY;INTERVAL=1", "jeden Tag");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=WEEKLY;INTERVAL=1", "jede Woche");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=1", "jeden Monat");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=YEARLY;INTERVAL=1", "jedes Jahr");

            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU", "jede Woche am Montag, Dienstag");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU", "alle 2 Wochen am Montag, Dienstag");
            //RRuleTextBuilderTestHelper.ToStringTestG("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "Every 2 weeks on Mon, Tue, Wed, Sat");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "alle 2 Wochen am Montag, Dienstag, Mittwoch, Sonnabend");

            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=5", "jeden Monat am 5.");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=15", "jeden Monat am 15.");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=2;BYMONTHDAY=15", "alle 2 Monate am 15.");

            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=-1", "alle 2 Monate am letzten Montag");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=1", "alle 2 Monate am ersten Montag");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=2;BYDAY=SA;BYSETPOS=3", "alle 2 Monate am dritten Sonnabend");

            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=1", "jedes Jahr am 01. Januar");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=YEARLY;BYMONTH=4;BYMONTHDAY=5", "jedes Jahr am 05. April"); 

            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=YEARLY;BYDAY=SU;BYSETPOS=1;BYMONTH=1", "jedes Jahr am ersten Sonntag im Januar"); 
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4", "jedes Jahr am letzten Mittwoch im April");

            //RRuleTextBuilderTestHelper.ToStringTestG("FREQ=DAILY;INTERVAL=1;COUNT=2", "jeden Tag für 2 Wiederholungen");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=DAILY;INTERVAL=1;COUNT=2", "jeden Tag insgesamt 2 Mal");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=WEEKLY;INTERVAL=1;COUNT=1", "jede Woche"); 
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=1;UNTIL=20181023T220000Z", "jeden Monat bis zum 23. Oktober 2018");

        }

        [TestMethod]
        public void JavaRruleParserFr()
        {
            //aditosoftware tests

            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=DAILY;INTERVAL=5", "tous les 5 jours");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=WEEKLY;INTERVAL=5", "toutes les 5 semaines");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=5", "tous les 5 mois");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=YEARLY;INTERVAL=5", "tous les 5 ans");

            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=DAILY;INTERVAL=1", "tous les jours");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=WEEKLY;INTERVAL=1", "toutes les semaines");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=1", "tous les mois"); // "chaque mois");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=YEARLY;INTERVAL=1", "tous les ans"); //"chaque année");

            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU", "toutes les semaines le lundi, mardi");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU", "toutes les 2 semaines le lundi, mardi");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "toutes les 2 semaines le lundi, mardi, mercredi, samedi");            

            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=5", "tous les mois le 5e");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=15", "tous les mois le 15e");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=2;BYMONTHDAY=15", "tous les 2 mois le 15e");

            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=-1", "tous les 2 mois le dernier lundi"); 
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=1", "tous les 2 mois le premier lundi"); 
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=2;BYDAY=SA;BYSETPOS=3", "tous les 2 mois le troisième samedi");

            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=1", "tous les ans le 1er janvier");  
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=YEARLY;BYMONTH=4;BYMONTHDAY=5", "tous les ans le 5e avril");
            
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=YEARLY;BYDAY=SU;BYSETPOS=1;BYMONTH=1", "tous les ans le premier dimanche de janvier"); 
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4", "tous les ans le dernier mercredi de avril"); 

            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=DAILY;INTERVAL=1;COUNT=2", "tous les jours à 2 reprises");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=WEEKLY;INTERVAL=1;COUNT=1", "toutes les semaines");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=1;UNTIL=20181023T220000Z", "tous les mois jusqu'au 23 octobre 2018");
        }


        [TestMethod]
        public void CustomEn()
        {
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TH,MO,TU,WE", "every weekday for 30 times");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TU,WE,TH", "every week on Tuesday, Wednesday, Thursday, Friday for 30 times");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=SU;BYDAY=FR,TU,WE,TH,SU", "every week on Sunday, Tuesday, Wednesday, Thursday, Friday for 30 times"); //jakubroztocil liefert (keine Beachtung von Wochenanfang): every week on Tuesday, Wednesday, Thursday, Friday, Sunday for 30 times
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=1", "every 2 months on first Friday");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=2", "every 2 months on second Friday");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=3", "every 2 months on third Friday");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=4", "every 2 months on fourth Friday");
        }


        [TestMethod]
        public void CustomDe()
        {
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TH,MO,TU,WE", "jeden Wochentag insgesamt 30 Mal");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TU,WE,TH", "jede Woche am Dienstag, Mittwoch, Donnerstag, Freitag insgesamt 30 Mal");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=SU;BYDAY=FR,TU,WE,TH,SU", "jede Woche am Sonntag, Dienstag, Mittwoch, Donnerstag, Freitag insgesamt 30 Mal");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=1", "alle 2 Monate am ersten Freitag");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=2", "alle 2 Monate am zweiten Freitag");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=3", "alle 2 Monate am dritten Freitag");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=4", "alle 2 Monate am vierten Freitag");
        }


        [TestMethod]
        public void CustomFr()
        {
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TH,MO,TU,WE", "tous les jours à 30 reprises");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TU,WE,TH", "toutes les semaines le mardi, mercredi, jeudi, vendredi à 30 reprises");            
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=SU;BYDAY=FR,TU,WE,TH,SU", "toutes les semaines le dimanche, mardi, mercredi, jeudi, vendredi à 30 reprises");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=1", "tous les 2 mois le premier vendredi");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=2", "tous les 2 mois le deuxième vendredi");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=3", "tous les 2 mois le troisième vendredi");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=4", "tous les 2 mois le quatrième vendredi");
        }


        [TestMethod]
        public void RruleJsDemoAppEn()
        {
            //https://github.com/jakubroztocil/rrule

            //Options
            RRuleTextBuilderTestHelper.ToStringTestEn("RRULE:FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO", "every week for 30 times");

            //RRule string
            RRuleTextBuilderTestHelper.ToStringTestEn("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "every week on Monday, Wednesday");
            RRuleTextBuilderTestHelper.ToStringTestEn("RRULE:FREQ=MONTHLY;BYMONTHDAY=10,15;COUNT=20", "every month on the 10th and 15th for 20 times");
            RRuleTextBuilderTestHelper.ToStringTestEn("RRULE:FREQ=DAILY;INTERVAL=3;COUNT=10", "every 3 days for 10 times");
            RRuleTextBuilderTestHelper.ToStringTestEn("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "every month on the 2nd last Friday for 7 times");


            //Text input
            RRuleTextBuilderTestHelper.ToStringTestEn("RRULE:FREQ=WEEKLY;BYDAY=MO,TU,WE,TH,FR", "every weekday");
            RRuleTextBuilderTestHelper.ToStringTestEn("RRULE:INTERVAL=2;FREQ=WEEKLY;BYDAY=TU", "every 2 weeks on Tuesday");
            RRuleTextBuilderTestHelper.ToStringTestEn("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "every week on Monday, Wednesday");
            RRuleTextBuilderTestHelper.ToStringTestEn("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "every month on the 2nd last Friday for 7 times");
            RRuleTextBuilderTestHelper.ToStringTestEn("RRULE:INTERVAL=6;FREQ=MONTHLY", "every 6 months");

            //extra
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;BYMONTHDAY=10,15,21;COUNT=20", "every month on the 10th, 15th and 21st for 20 times");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=MONTHLY;BYDAY=2FR;COUNT=7", "every month on the 2nd Friday for 7 times");
            RRuleTextBuilderTestHelper.ToStringTestEn("FREQ=WEEKLY;BYDAY=SU,SA,MO,TU,WE,TH,FR", "every day");
            RRuleTextBuilderTestHelper.ToStringTestEn("RRULE:FREQ=WEEKLY;INTERVAL=2;BYDAY=TU", "every 2 weeks on Tuesday");
        }

        [TestMethod]
        public void RruleJsDemoAppDe()
        {
            //https://github.com/jakubroztocil/rrule

            //Options
            RRuleTextBuilderTestHelper.ToStringTestDe("RRULE:FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO", "jede Woche insgesamt 30 Mal");

            //RRule string
            //RRuleTextBuilderTestHelper.ToStringTestG("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "jede Woche am Montag und Mittwoch");
            RRuleTextBuilderTestHelper.ToStringTestDe("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "jede Woche am Montag, Mittwoch"); //Schöner wäre am Montag und Mittwoch
            RRuleTextBuilderTestHelper.ToStringTestDe("RRULE:FREQ=MONTHLY;BYMONTHDAY=10,15;COUNT=20", "jeden Monat am 10. und 15. insgesamt 20 Mal");
            RRuleTextBuilderTestHelper.ToStringTestDe("RRULE:FREQ=DAILY;INTERVAL=3;COUNT=10", "alle 3 Tage insgesamt 10 Mal");
            RRuleTextBuilderTestHelper.ToStringTestDe("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "jeden Monat am 2. letzten Freitag insgesamt 7 Mal");


            //Text input
            RRuleTextBuilderTestHelper.ToStringTestDe("RRULE:FREQ=WEEKLY;BYDAY=MO,TU,WE,TH,FR", "jeden Wochentag");
            RRuleTextBuilderTestHelper.ToStringTestDe("RRULE:INTERVAL=2;FREQ=WEEKLY;BYDAY=TU", "alle 2 Wochen am Dienstag");
            RRuleTextBuilderTestHelper.ToStringTestDe("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "jede Woche am Montag, Mittwoch");
            RRuleTextBuilderTestHelper.ToStringTestDe("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "jeden Monat am 2. letzten Freitag insgesamt 7 Mal");
            RRuleTextBuilderTestHelper.ToStringTestDe("RRULE:INTERVAL=6;FREQ=MONTHLY", "alle 6 Monate");

            //extra
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;BYMONTHDAY=10,15,21;COUNT=20", "jeden Monat am 10., 15. und 21. insgesamt 20 Mal");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=MONTHLY;BYDAY=2FR;COUNT=7", "jeden Monat am 2. Freitag insgesamt 7 Mal");
            RRuleTextBuilderTestHelper.ToStringTestDe("FREQ=WEEKLY;BYDAY=SU,SA,MO,TU,WE,TH,FR", "jeden Tag");
            RRuleTextBuilderTestHelper.ToStringTestDe("RRULE:FREQ=WEEKLY;INTERVAL=2;BYDAY=TU", "alle 2 Wochen am Dienstag");
        }

        [TestMethod]
        public void RruleJsDemoAppFr()
        {
            //https://github.com/jakubroztocil/rrule

            //Options
            RRuleTextBuilderTestHelper.ToStringTestFr("RRULE:FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO", "toutes les semaines à 30 reprises");

            //RRule string
            RRuleTextBuilderTestHelper.ToStringTestFr("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "toutes les semaines le lundi, mercredi"); 
            RRuleTextBuilderTestHelper.ToStringTestFr("RRULE:FREQ=MONTHLY;BYMONTHDAY=10,15;COUNT=20", "tous les mois les 10e et 15e à 20 reprises");
            RRuleTextBuilderTestHelper.ToStringTestFr("RRULE:FREQ=DAILY;INTERVAL=3;COUNT=10", "tous les 3 jours à 10 reprises");
            RRuleTextBuilderTestHelper.ToStringTestFr("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "tous les mois le 2e dernier vendredi à 7 reprises");

            //Text input
            RRuleTextBuilderTestHelper.ToStringTestFr("RRULE:FREQ=WEEKLY;BYDAY=MO,TU,WE,TH,FR", "tous les jours");
            RRuleTextBuilderTestHelper.ToStringTestFr("RRULE:INTERVAL=2;FREQ=WEEKLY;BYDAY=TU", "toutes les 2 semaines le mardi");
            RRuleTextBuilderTestHelper.ToStringTestFr("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "toutes les semaines le lundi, mercredi");
            RRuleTextBuilderTestHelper.ToStringTestFr("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "tous les mois le 2e dernier vendredi à 7 reprises");
            RRuleTextBuilderTestHelper.ToStringTestFr("RRULE:INTERVAL=6;FREQ=MONTHLY", "tous les 6 mois");

            //extra
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;BYMONTHDAY=10,15,21;COUNT=20", "tous les mois les 10e, 15e et 21e à 20 reprises");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=MONTHLY;BYDAY=2FR;COUNT=7", "tous les mois le 2e vendredi à 7 reprises");
            RRuleTextBuilderTestHelper.ToStringTestFr("FREQ=WEEKLY;BYDAY=SU,SA,MO,TU,WE,TH,FR", "tous les jours");
            RRuleTextBuilderTestHelper.ToStringTestFr("RRULE:FREQ=WEEKLY;INTERVAL=2;BYDAY=TU", "toutes les 2 semaines le mardi");
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
                ReadableResult = recurrencePattern.ToText(new CultureInfo(culture));
            }

            public bool Result { get => ReadableString == ReadableResult; }
            public string ResultMessage { get => $"{ReadableResult} (missed: {ReadableString})"; }

            public static void ToStringTestEn(string rRuleString, string readableString)
            {                
                System.Diagnostics.Debug.WriteLine("EN: " + readableString);
                var rRuleTextBuilderTest = new RRuleTextBuilderTestHelper(rRuleString, readableString, "en"); 
                Assert.IsTrue(rRuleTextBuilderTest.Result, rRuleTextBuilderTest.ResultMessage);
            }

            public static void ToStringTestDe(string rRuleString, string readableString)
            {
                System.Diagnostics.Debug.WriteLine("DE: " + readableString);
                var rRuleTextBuilderTest = new RRuleTextBuilderTestHelper(rRuleString, readableString, "de");
                Assert.IsTrue(rRuleTextBuilderTest.Result, rRuleTextBuilderTest.ResultMessage);
            }

            public static void ToStringTestFr(string rRuleString, string readableString)
            {
                System.Diagnostics.Debug.WriteLine("FR: " + readableString);
                var rRuleTextBuilderTest = new RRuleTextBuilderTestHelper(rRuleString, readableString, "fr");
                Assert.IsTrue(rRuleTextBuilderTest.Result, rRuleTextBuilderTest.ResultMessage);
            }

        }

    }

}
