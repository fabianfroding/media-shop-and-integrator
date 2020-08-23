using MediaShop.Models;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace MediaShop.Repositories
{
    class ProductRepository : IProductRepository
    {
        private static string _dbPath = @"..\..\Repositories\Data\dbProducts.txt";

        private ProductContext _context;

        // TODO: Move this and test-method to a UnitTest project.
        /**
        SqlConnection dbConnection = new SqlConnection(
                                        "user id=FABIANLAPTOP\\Fabian2;" +
                                       "password=password;" +
                                        "server=localhost\\SQLEXPRESS2;" +
                                       "Trusted_Connection=yes;" +
                                       "database=MediaShop; " +
                                       "connection timeout=30"
        );
        **/

        public ProductRepository()
        {
            Product.idCounter = FindMaxId();
            _context = new ProductContext();
        }

        /**
        public void TestSQLConnection()
        {
            try
            {
                dbConnection.Open();
                Debug.WriteLine("Connection successful.");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Connection failed.");
                Console.WriteLine(e.ToString());
            }
        }
        **/

        // Hämtar en produkt från textfilen baserat på produktens id.
        public Product GetById(int id)
        {
            List<string> lines = File.ReadAllLines(_dbPath).ToList();

            foreach (string line in lines)
            {
                if (line != "")
                {
                    string[] entries = line.Split('|');
                    int.TryParse(entries[0], out int productId);

                    if (id == productId)
                    {
                        return GetParsedProduct(entries);
                    }
                }
            }
            return null;
        }

        public Product GetByIdFromDB(int id)
        {
            return _context.Products.Single(p => p.id == id);
        }

        // Hämtar alla produkter från textfilen.
        public List<Product> GetAll()
        {
            List<Product> _products = new List<Product>();
            List<string> lines = File.ReadAllLines(_dbPath).ToList();

            foreach(string line in lines)
            {
                if (line != "")
                {
                    string[] entries = line.Split('|');
                    _products.Add(GetParsedProduct(entries));
                }
            }
            return _products;
        }

        public List<Product> GetAllFromDB()
        {
            return _context.Products.ToList<Product>();
        }

        // Lägger till en produkt till textfilen.
        public bool Add(Product product)
        {
            string data = product.id + "|" + product.name + "|" + product.price + "|" + product.stock + "|" + product.productType;

            StreamWriter sw = File.AppendText(_dbPath);
            sw.WriteLine(data);
            sw.Close();

            return true;
        }

        public bool AddToDB(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return true;
        }

        // Tar bort en produkt från textfilen.
        public bool Remove(Product product)
        {
            int id = product.id;
            List<string> lines = File.ReadAllLines(_dbPath).ToList();
            List<String> newLines = new List<String>();

            // Copy all existing lines except for the one that matches id.
            foreach (string line in lines)
            {
                string[] entries = line.Split('|');
                int.TryParse(entries[0], out int productId);
                if (id != productId)
                {
                    newLines.Add(line);
                }
            }

            // Remove all content from file.
            File.WriteAllText(_dbPath, String.Empty);

            // Rewrite the copied content to file.
            StreamWriter sw = File.AppendText(_dbPath);
            foreach (string line in newLines)
            {
                sw.WriteLine(line);
            }
            sw.Close();

            return true;
        }

        public bool RemoveFromDB(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        // Uppdaterar en produkt i textfilen.
        // (Produkten tas först bort, sedan läggs den modifiera produkten in på nytt).
        public bool Update(Product product)
        {
            if (Remove(product) && Add(product))
            {
                return true;
            }
            return false;
        }

        public bool UpdateInDB(Product product)
        {
            Product oldProduct = _context.Products.Single(p => p.id == product.id);

            if (oldProduct != null)
            {
                oldProduct.name = product.name;
                oldProduct.price = product.price;
                oldProduct.stock = product.stock;
                oldProduct.productType = product.productType;

                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool ExportProducts(string path)
        {
            List<string> lines = File.ReadAllLines(_dbPath).ToList();
            File.WriteAllText(path, String.Empty);
            StreamWriter sw = File.AppendText(path);
            foreach (string line in lines)
            {
                sw.WriteLine(line);
            }
            sw.Close();
            return true;
        }

        public bool ExportProductsFromDB(string exportPath)
        {
            File.WriteAllText(exportPath, String.Empty);
            StreamWriter sw = File.AppendText(exportPath);

            foreach (Product p in GetAllFromDB())
            {
                sw.WriteLine(p.id + "|" + p.name + "|" + p.price + "|" + p.stock + "|" + p.productType);
            }

            sw.Close();
            return true;
        }

        public bool ImportProducts(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo fi = di.GetFiles("*.txt")[0];
            List<string> lines = File.ReadAllLines(fi.FullName).ToList();
            File.WriteAllText(_dbPath, String.Empty);
            StreamWriter sw = File.AppendText(_dbPath);
            foreach (string line in lines)
            {
                sw.WriteLine(line);
            }
            sw.Close();
            return true;
        }

        public bool ImportProductsToDB(string filePath)
        {
            ClearDB();

            DirectoryInfo di = new DirectoryInfo(filePath);
            FileInfo fi = di.GetFiles("*.txt")[0];
            List<string> lines = File.ReadAllLines(fi.FullName).ToList();

            foreach (string line in lines)
            {
                Product product = GetParsedProduct(line.Split('|'));
                Debug.WriteLine(product.ToString());
                _context.Add(product);
            }
            
            _context.SaveChanges();
            return true;
        }

        // Funktion för att konvertera data från textfilen till en Product-objekt.
        // En Products fält separeras med | symbolen.
        private Product GetParsedProduct(string[] entries)
        {
            Product product = new Product();
            // Commented out so that id is auto-generated to db.
            //int.TryParse(entries[0], out int productId);
            //product.id = productId;
            product.name = entries[1];
            double.TryParse(entries[2], out double productPrice);
            product.price = productPrice;
            int.TryParse(entries[3], out int productStock);
            product.stock = productStock;
            Enum.TryParse(entries[4], out Product.ProductType productType);
            product.productType = productType;

            return product;
        }

        // Funktion som hanterar att varje produkt har ett unikt id.
        private int FindMaxId()
        {
            int maxId = 0;

            List<string> lines = File.ReadAllLines(_dbPath).ToList();
            foreach (string line in lines)
            {
                if (line != "")
                {
                    string[] entries = line.Split('|');
                    int.TryParse(entries[0], out int productId);

                    if (productId > maxId)
                    {
                        maxId = productId;
                    }
                }
            }

            return maxId;
        }

        private void ClearDB()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM Product");
        }
    }
}
