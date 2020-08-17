using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaShop.Test
{
    [TestClass]
    public class MediaShopTest
    {
        [TestMethod]
        public void Test_SQLServerConnection()
        {
            // TODO: Cannot acces Repositories folder in MediaShop reference...
            // https://www.youtube.com/watch?v=rW6LvPP4VvA

            // AAA:
            // Arrange
            bool connected = false;

            // Act
            try
            {
                // dbConnection.Open();
                connected = true;
            }
            catch (Exception e)
            {
                connected = false;
                Console.WriteLine(e.ToString());
            }

            // Assert

        }
    }
}
