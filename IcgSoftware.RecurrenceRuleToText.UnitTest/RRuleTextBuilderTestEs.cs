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
    public class RRuleTextBuilderTestEs : RRuleTextBuilderTestBase
    {
        protected override void ToTextTest(string rRuleString, string readableString)
        {
            Debug.WriteLine("ES: " + readableString);
            var rRuleTextBuilderTest = new RRuleTextBuilderTestHelper(rRuleString, readableString, "es");
            Assert.IsTrue(rRuleTextBuilderTest.Result, rRuleTextBuilderTest.ResultMessage);
        }

        [TestMethod]
        public void LastNumeral()
        {
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-1MO", "cada mes el último lunes");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2MO", "cada mes el penúltimo lunes");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-3FR", "cada mes el tercer último viernes");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-4SU", "cada mes el cuarto último domingos");
        }

        [TestMethod]
        public void JavaRruleParser()
        {
            //aditosoftware tests

            ToTextTest("FREQ=DAILY;INTERVAL=5", "cada 5 dias");
            ToTextTest("FREQ=WEEKLY;INTERVAL=5", "cada 5 semanas");
            ToTextTest("FREQ=MONTHLY;INTERVAL=5", "cada 5 meses");
            ToTextTest("FREQ=YEARLY;INTERVAL=5", "cada 5 años");

            ToTextTest("FREQ=DAILY;INTERVAL=1", "cada día"); 
            ToTextTest("FREQ=WEEKLY;INTERVAL=1", "cada semana");
            ToTextTest("FREQ=MONTHLY;INTERVAL=1", "cada mes");
            ToTextTest("FREQ=YEARLY;INTERVAL=1", "cada año");

            ToTextTest("FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU", "cada semana los lunes, martes");
            ToTextTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU", "cada 2 semanas los lunes, martes");
            ToTextTest("FREQ=WEEKLY;INTERVAL=2;BYDAY=MO,TU,WE,SA", "cada 2 semanas los lunes, martes, miércoles, sábados");

            ToTextTest("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=5", "cada mes el 5º");
            ToTextTest("FREQ=MONTHLY;INTERVAL=1;BYMONTHDAY=15", "cada mes el 15º");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYMONTHDAY=15", "cada 2 meses el 15º");

            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=-1", "cada 2 meses el último lunes");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=MO;BYSETPOS=1", "cada 2 meses el primer lunes");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=SA;BYSETPOS=3", "cada 2 meses el tercer sábados");

            ToTextTest("FREQ=YEARLY;BYMONTH=1;BYMONTHDAY=1", "cada año el 1º de enero");
            ToTextTest("FREQ=YEARLY;BYMONTH=4;BYMONTHDAY=5", "cada año el 5º de abril");

            ToTextTest("FREQ=YEARLY;BYDAY=SU;BYSETPOS=1;BYMONTH=1", "cada año el primer domingos de enero");
            ToTextTest("FREQ=YEARLY;BYDAY=WE;BYSETPOS=-1;BYMONTH=4", "cada año el último miércoles de abril");

            ToTextTest("FREQ=DAILY;INTERVAL=1;COUNT=2", "cada día un total de 2 veces"); 
            ToTextTest("FREQ=WEEKLY;INTERVAL=1;COUNT=1", "cada semana");
            ToTextTest("FREQ=MONTHLY;INTERVAL=1;UNTIL=20181023T220000Z", "cada mes hasta el martes, 23 de octubre de 2018");

        }

        [TestMethod]
        public void Custom()
        {
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TH,MO,TU,WE", "todos los días de la semana un total de 30 veces");
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO;BYDAY=FR,TU,WE,TH", "cada semana los martes, miércoles, jueves, viernes un total de 30 veces");            
            ToTextTest("FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=SU;BYDAY=FR,TU,WE,TH,SU", "cada semana los domingos, martes, miércoles, jueves, viernes un total de 30 veces");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=1", "cada 2 meses el primer viernes");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=2", "cada 2 meses el segundo viernes");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=3", "cada 2 meses el tercer viernes");
            ToTextTest("FREQ=MONTHLY;INTERVAL=2;BYDAY=FR;BYSETPOS=4", "cada 2 meses el cuarto viernes");
        }

        [TestMethod]
        public void RruleJsDemoApp()
        {
            //https://github.com/jakubroztocil/rrule

            //Options
            ToTextTest("RRULE:FREQ=WEEKLY;COUNT=30;INTERVAL=1;WKST=MO", "cada semana un total de 30 veces");

            //RRule string            
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "cada semana los lunes, miércoles"); //Schöner wäre am Montag und Mittwoch            
            ToTextTest("RRULE:FREQ=MONTHLY;BYMONTHDAY=10,15;COUNT=20", "cada mes los 10º y 15º un total de 20 veces");
            ToTextTest("RRULE:FREQ=DAILY;INTERVAL=3;COUNT=10", "cada 3 dias un total de 10 veces");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "cada mes el penúltimo viernes un total de 7 veces");


            //Text input
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,TU,WE,TH,FR", "todos los días de la semana");
            ToTextTest("RRULE:INTERVAL=2;FREQ=WEEKLY;BYDAY=TU", "cada 2 semanas los martes");
            ToTextTest("RRULE:FREQ=WEEKLY;BYDAY=MO,WE", "cada semana los lunes, miércoles");
            ToTextTest("RRULE:FREQ=MONTHLY;BYDAY=-2FR;COUNT=7", "cada mes el penúltimo viernes un total de 7 veces");
            ToTextTest("RRULE:INTERVAL=6;FREQ=MONTHLY", "cada 6 meses");

            //extra
            ToTextTest("FREQ=MONTHLY;BYMONTHDAY=10,15,21;COUNT=20", "cada mes los 10º, 15º y 21º un total de 20 veces");
            ToTextTest("FREQ=MONTHLY;BYDAY=2FR;COUNT=7", "cada mes el 2º viernes un total de 7 veces");
            ToTextTest("FREQ=WEEKLY;BYDAY=SU,SA,MO,TU,WE,TH,FR", "cada día");
            ToTextTest("RRULE:FREQ=WEEKLY;INTERVAL=2;BYDAY=TU", "cada 2 semanas los martes");
        }


    }
}
