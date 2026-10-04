using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnalaizerClassLibrary;
using System;
using System.Data;

namespace AnalaizerClassTest
{
    [TestClass]
    public class AnalaizerClassTest
    {
        public TestContext TestContext { get; set; }
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnalaizerTestDB;Integrated Security=True;";
        [TestMethod]
        [DataSource("System.Data.SqlClient", ConnectionString, "CreateStackTests", DataAccessMethod.Sequential)]
        public void CreateStack_ReadDataFromSource_ReturnsCorrectStack()
        {
            string input = TestContext.DataRow["InputExpression"].ToString();
            string expectedOutput = TestContext.DataRow["ExpectedResult"].ToString();

            AnalaizerClass.expression = input;

            var result = AnalaizerClass.CreateStack();
            var expectedStack = expectedOutput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            Assert.IsNotNull(result, "The result stack should not be null.");
            Assert.AreEqual(expectedStack.Length, result.Count, "The number of elements in the stack does not match the expected count.");

            CollectionAssert.AreEqual(expectedStack, result, "The elements in the stack do not match the expected values.");
        }
    }
}
