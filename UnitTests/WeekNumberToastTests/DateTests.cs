using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeekNumberToast;

namespace WeekNumberToastTests
{
    [TestClass]
    public class DateTests
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }


        [DataTestMethod]
        [DataRow("1/1/2000", 52)]
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
        [DeploymentItem("DateTestData.xlsx")]
        [DataSource("MyExcelDataSource")]
        public void MyTestMethod2()
        {
            Assert.AreEqual(TestContext.DataRow["Val1"], TestContext.DataRow["Val2"]);
        }

        [TestMethod()]
        [DeploymentItem("DateTestData.xlsx")]
        [DataSource("System.Data.Odbc", @"Dsn=Excel Files;dbq=DateTestData.xlsx;defaultdir=.\; driverid=790;maxbuffersize=2048;pagetimeout=5", "Sheet1", DataAccessMethod.Sequential)]
        public void MyTest()
        {

        }
    }

}
