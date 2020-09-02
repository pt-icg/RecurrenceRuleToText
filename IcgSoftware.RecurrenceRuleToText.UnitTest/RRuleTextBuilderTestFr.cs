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
    public class RRuleTextBuilderTestFr : RRuleTextBuilderTestBase
    {
        protected override void ToTextTest(string rRuleString, string readableString)
        {
            Debug.WriteLine("FR: " + readableString);
            var rRuleTextBuilderTest = new RRuleTextBuilderTestHelper(rRuleString, readableString, "fr");
            Assert.IsTrue(rRuleTextBuilderTest.Result, rRuleTextBuilderTest.ResultMessage);
        }

        [TestMethod]
        public void LastNumeral()
        {
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-1MO", "tous les mois le dernier lundi");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2MO", "tous les mois l'avant-dernier lundi");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-3FR", "tous les mois le troisième vendredi dernier");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-4SU", "tous les mois le quatrième dimanche dernier");
        }

        [TestMethod]
        public void JavaRruleParser()
        {
            //aditosoftware tests

            ToTextTest("FREQ=DAILY;INTERVAL=5", "tous les 5 jours");
            ToTextTest("FREQ=WEEKLY;INTERVAL=5", "toutes les 5 semaines");
            ToTextTest("FREQ=MONTHLY;INTERVAL=5", "tous les 5 mois");
            ToTextTest("FREQ=YEARLY;INTERVAL=5", "tous les 5 ans");

            ToTextTest("FREQ=DAILY;INTERVAL=1", "tous les jours");
            ToTextTest("FREQ=WEEKLY;INTERVAL=1", "toutes les semaines");
            ToTextTest("FREQ=MONTHLY;INTERVAL=1", "tous les mois"); // "chaque mois");
            ToTextTest("FREQ=YEARLY;INTERVAL=1", "tous les ans"); //"chaque année");

            ToTextTest("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU", "toutes les semaines le lundi, mardi");
            ToTextTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU", "toutes les 2 semaines le lundi, mardi");
            ToTextTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "toutes les 2 semaines le lundi, mardi, mercredi, samedi");

            ToTextTest("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=5", "tous les mois le 5e");
            ToTextTest("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=15", "tous les mois le 15e");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYMONTHDAY=15", "tous les 2 mois le 15e");

            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=-1", "tous les 2 mois le dernier lundi");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=1", "tous les 2 mois le premier lundi");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=SA;BYSETPOS=3", "tous les 2 mois le troisième samedi");

            ToTextTest("FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=1", "tous les ans le 1er janvier");
            ToTextTest("FREQ=YEARLY;BYMONTH=4;BYMONTHDAY=5", "tous les ans le 5e avril");

            ToTextTest("FREQ=YEARLY;BYDAY=SU;BYSETPOS=1;BYMONTH=1", "tous les ans le premier dimanche de janvier");
            ToTextTest("FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4", "tous les ans le dernier mercredi de avril");

            ToTextTest("FREQ=DAILY;INTERVAL=1;COUNT=2", "tous les jours à 2 reprises");
            ToTextTest("FREQ=WEEKLY;INTERVAL=1;COUNT=1", "toutes les semaines");
            ToTextTest("FREQ=MONTHLY;INTERVAL=1;UNTIL=20181023T220000Z", "tous les mois jusqu'au 23 octobre 2018");
        }

        [TestMethod]
        public void Custom()
        {
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TH,MO,TU,WE", "tous les jours à 30 reprises");
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TU,WE,TH", "toutes les semaines le mardi, mercredi, jeudi, vendredi à 30 reprises");
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=SU;BYDAY=FR,TU,WE,TH,SU", "toutes les semaines le dimanche, mardi, mercredi, jeudi, vendredi à 30 reprises");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=1", "tous les 2 mois le premier vendredi");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=2", "tous les 2 mois le deuxième vendredi");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=3", "tous les 2 mois le troisième vendredi");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=4", "tous les 2 mois le quatrième vendredi");
            ToTextTest("FREQ=YEARLY;INTERVAL=3;BYMONTH=3,4", "tous les 3 ans en mars et avril");
        }

        [TestMethod]
        public void RruleJsDemoApp()
        {
            //https://github.com/jakubroztocil/rrule

            //Options
            ToTextTest("RRULE:FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO", "toutes les semaines à 30 reprises");

            //RRule string
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "toutes les semaines le lundi, mercredi");
            ToTextTest("RRULE:FREQ=MONTHLY;BYMONTHDAY=10,15;COUNT=20", "tous les mois les 10e et 15e à 20 reprises");
            ToTextTest("RRULE:FREQ=DAILY;INTERVAL=3;COUNT=10", "tous les 3 jours à 10 reprises");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "tous les mois l'avant-dernier vendredi à 7 reprises");

            //Text input
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,TU,WE,TH,FR", "tous les jours");
            ToTextTest("RRULE:INTERVAL=2;FREQ=WEEKLY;BYDAY=TU", "toutes les 2 semaines le mardi");
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "toutes les semaines le lundi, mercredi");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "tous les mois l'avant-dernier vendredi à 7 reprises");
            ToTextTest("RRULE:INTERVAL=6;FREQ=MONTHLY", "tous les 6 mois");

            //extra
            ToTextTest("FREQ=MONTHLY;BYMONTHDAY=10,15,21;COUNT=20", "tous les mois les 10e, 15e et 21e à 20 reprises");
            ToTextTest("FREQ=MONTHLY;BYDAY=2FR;COUNT=7", "tous les mois le 2e vendredi à 7 reprises");
            ToTextTest("FREQ=WEEKLY;BYDAY=SU,SA,MO,TU,WE,TH,FR", "tous les jours");
            ToTextTest("RRULE:FREQ=WEEKLY;INTERVAL=2;BYDAY=TU", "toutes les 2 semaines le mardi");
        }

    }
}
