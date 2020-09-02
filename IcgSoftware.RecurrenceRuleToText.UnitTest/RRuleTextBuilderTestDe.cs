using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IcgSoftware.RecurrenceRuleToText.UnitTest
{
    [TestClass]
    public class RRuleTextBuilderTestDe : RRuleTextBuilderTestBase
    {
        protected override void ToTextTest(string rRuleString, string readableString)
        {
            Debug.WriteLine("DE: " + readableString);
            var rRuleTextBuilderTest = new RRuleTextBuilderTestHelper(rRuleString, readableString, "de");
            Assert.IsTrue(rRuleTextBuilderTest.Result, rRuleTextBuilderTest.ResultMessage);
        }

        [TestMethod]
        public void LastNumeral()
        {
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-1MO", "monatlich am letzten Montag");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2MO", "monatlich am vorletzten Montag");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-3FR", "monatlich am drittletzten Freitag");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-4SU", "monatlich am viertletzten Sonntag");
        }

        [TestMethod]
        public void JavaRruleParser()
        {
            //aditosoftware tests

            ToTextTest("FREQ=DAILY;INTERVAL=5", "alle 5 Tage");
            ToTextTest("FREQ=WEEKLY;INTERVAL=5", "alle 5 Wochen");
            ToTextTest("FREQ=MONTHLY;INTERVAL=5", "alle 5 Monate");
            ToTextTest("FREQ=YEARLY;INTERVAL=5", "alle 5 Jahre");

            ToTextTest("FREQ=DAILY;INTERVAL=1", "täglich"); //"jeden Tag"
            ToTextTest("FREQ=WEEKLY;INTERVAL=1", "wöchentlich"); //"jede Woche"
            ToTextTest("FREQ=MONTHLY;INTERVAL=1", "monatlich"); //"jeden Monat"
            ToTextTest("FREQ=YEARLY;INTERVAL=1", "jährlich"); //"jedes Jahr"

            ToTextTest("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU", "wöchentlich am Montag, Dienstag");
            ToTextTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU", "alle 2 Wochen am Montag, Dienstag");
            ToTextTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "alle 2 Wochen am Montag, Dienstag, Mittwoch, Sonnabend");

            ToTextTest("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=5", "monatlich am 5.");
            ToTextTest("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=15", "monatlich am 15.");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYMONTHDAY=15", "alle 2 Monate am 15.");

            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=-1", "alle 2 Monate am letzten Montag");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=1", "alle 2 Monate am ersten Montag");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=SA;BYSETPOS=3", "alle 2 Monate am dritten Sonnabend");

            ToTextTest("FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=1", "jährlich am 01. Januar");
            ToTextTest("FREQ=YEARLY;BYMONTH=4;BYMONTHDAY=5", "jährlich am 05. April");

            ToTextTest("FREQ=YEARLY;BYDAY=SU;BYSETPOS=1;BYMONTH=1", "jährlich am ersten Sonntag im Januar");
            ToTextTest("FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4", "jährlich am letzten Mittwoch im April");

            ToTextTest("FREQ=DAILY;INTERVAL=1;COUNT=2", "täglich insgesamt 2 Mal");
            ToTextTest("FREQ=WEEKLY;INTERVAL=1;COUNT=1", "wöchentlich");
            ToTextTest("FREQ=MONTHLY;INTERVAL=1;UNTIL=20181023T220000Z", "monatlich bis zum 23. Oktober 2018");

        }

        [TestMethod]
        public void Custom()
        {
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TH,MO,TU,WE", "jeden Wochentag insgesamt 30 Mal");
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TU,WE,TH", "wöchentlich am Dienstag, Mittwoch, Donnerstag, Freitag insgesamt 30 Mal");
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=SU;BYDAY=FR,TU,WE,TH,SU", "wöchentlich am Sonntag, Dienstag, Mittwoch, Donnerstag, Freitag insgesamt 30 Mal");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=1", "alle 2 Monate am ersten Freitag");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=2", "alle 2 Monate am zweiten Freitag");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=3", "alle 2 Monate am dritten Freitag");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=4", "alle 2 Monate am vierten Freitag");
            ToTextTest("FREQ=YEARLY;INTERVAL=3;BYMONTH=3,4", "alle 3 Jahre im März und April");
        }

        [TestMethod]
        public void RruleJsDemoApp()
        {
            //https://github.com/jakubroztocil/rrule

            //Options
            ToTextTest("RRULE:FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO", "wöchentlich insgesamt 30 Mal");

            //RRule string            
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "wöchentlich am Montag, Mittwoch"); //Schöner wäre am Montag und Mittwoch            
            ToTextTest("RRULE:FREQ=MONTHLY;BYMONTHDAY=10,15;COUNT=20", "monatlich am 10. und 15. insgesamt 20 Mal");
            ToTextTest("RRULE:FREQ=DAILY;INTERVAL=3;COUNT=10", "alle 3 Tage insgesamt 10 Mal");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "monatlich am vorletzten Freitag insgesamt 7 Mal");


            //Text input
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,TU,WE,TH,FR", "jeden Wochentag");
            ToTextTest("RRULE:INTERVAL=2;FREQ=WEEKLY;BYDAY=TU", "alle 2 Wochen am Dienstag");
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "wöchentlich am Montag, Mittwoch");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "monatlich am vorletzten Freitag insgesamt 7 Mal");
            ToTextTest("RRULE:INTERVAL=6;FREQ=MONTHLY", "alle 6 Monate");

            //extra
            ToTextTest("FREQ=MONTHLY;BYMONTHDAY=10,15,21;COUNT=20", "monatlich am 10., 15. und 21. insgesamt 20 Mal");
            ToTextTest("FREQ=MONTHLY;BYDAY=2FR;COUNT=7", "monatlich am 2. Freitag insgesamt 7 Mal");
            ToTextTest("FREQ=WEEKLY;BYDAY=SU,SA,MO,TU,WE,TH,FR", "jeden Tag");
            ToTextTest("RRULE:FREQ=WEEKLY;INTERVAL=2;BYDAY=TU", "alle 2 Wochen am Dienstag");
        }


    }
}
