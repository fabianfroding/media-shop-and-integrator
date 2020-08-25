using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace MediaShopTest
{
    [TestClass]
    public class DBConnectionTest
    {
        [TestMethod]
        public void TestDBConnection()
        {
            SqlConnection dbConnection = new SqlConnection(
                                        "user id=FABIANLAPTOP\\Fabian2;" +
                                       "password=password;" +
                                        "server=localhost\\SQLEXPRESS2;" +
                                       "Trusted_Connection=yes;" +
                                       "database=MediaShop; " +
                                       "connection timeout=30"
            );

            bool b = false;

            try
            {
                dbConnection.Open();
                b = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Assert.IsTrue(b);

        }
    }
}
