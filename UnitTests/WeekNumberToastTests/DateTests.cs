using System;
using System.Data.Odbc;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeekNumberToast;
using WeekNumberToast.Helpers;

namespace WeekNumberToastTests
{
    [TestClass]
    public class DateTests
    {
        public TestContext TestContext { get; set; }


        [DataTestMethod]
        [DataRow("12/30/2019", 1)]
        [DataRow("12/31/2019", 1)]
        [DataRow("1/1/2020", 1)]
        [DataRow("1/2/2020", 1)]
        [DataRow("12/28/2020", 53)]
        [DataRow("12/29/2020", 53)]
        [DataRow("12/30/2020", 53)]
        [DataRow("12/31/2020", 53)]
        [DataRow("1/1/2021", 53)]
        [DataRow("1/2/2021", 53)]
        public void WeekNumberIsCorrect(string dateString, int expectedWeek)
        {
            // **** Arrange ****

            var date2Test = DateTime.Parse(dateString);

            // **** Act ****

            var weekNumber = date2Test.GetIso8601WeekOfYear();


            // **** Assert ****

            Assert.AreEqual(expectedWeek, weekNumber);
        }

        [TestMethod()]
        [DeploymentItem("DateTestData.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\DateTestData.csv",
            "DateTestData#csv",
            DataAccessMethod.Sequential)]
        public void WeekNumberTest()
        {
            // **** Arrange ****

            var date2Test = DateTime.Parse(TestContext.DataRow["Date"].ToString());

            // **** Act ****

            var weekNumber = date2Test.GetIso8601WeekOfYear();


            // **** Assert ****

            Assert.AreEqual(TestContext.DataRow["ISOWeek"], weekNumber);
        }

    }

}
