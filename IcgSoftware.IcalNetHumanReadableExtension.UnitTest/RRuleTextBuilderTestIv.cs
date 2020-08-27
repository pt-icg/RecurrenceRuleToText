using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IcgSoftware.IcalNetHumanReadableExtension.UnitTest
{
    [TestClass]
    public class RRuleTextBuilderTestIv : RRuleTextBuilderTestBase
    {
        protected override void ToTextTest(string rRuleString, string readableString)
        {
            Debug.WriteLine("IV: " + readableString);
            var rRuleTextBuilderTest = new RRuleTextBuilderTestHelper(rRuleString, readableString, "");
            Assert.IsTrue(rRuleTextBuilderTest.Result, rRuleTextBuilderTest.ResultMessage);
        }

        [TestMethod]
        public void InvariantCulture()
        {
            ToTextTest("FREQ=YEARLY;BYMONTH=4;BYMONTHDAY=5", "every year on April the 5th");
        }

    }
}
