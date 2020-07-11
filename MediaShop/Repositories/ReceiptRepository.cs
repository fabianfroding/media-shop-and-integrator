using MediaShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MediaShop.Repositories
{
    // Denna klass innehåller samma logik som ProductRepository, fast istället för Receipt-objektet.
    // För att förstå hur denna klass fungerar, hänvisas ProductRepostiory klassen.
    class ReceiptRepository : IReceiptRepository
    {
        private string dbPath = @"..\..\Repositories\Data\Receipts\";

        public Receipt GetByDate(string date)
        {
            DirectoryInfo di = new DirectoryInfo(dbPath);
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo fi in files)
            {
                if (fi.Name.Replace(".txt", "") == date)
                {
                    Receipt receipt = new Receipt();
                    receipt.date = fi.Name.Replace(".txt", "");
                    List<string> lines = File.ReadAllLines(fi.FullName).ToList();
                    foreach (string line in lines)
                    {
                        if (line != "")
                        {
                            string[] entries = line.Split('|');
                            receipt.products.Add(GetParsedProduct(entries));
                        }
                    }
                    return receipt;
                }
            }
            return null;
        }

        public List<Receipt> GetAll()
        {
            List<Receipt> receipts = new List<Receipt>();
            DirectoryInfo di = new DirectoryInfo(dbPath);
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo fi in files)
            {
                Receipt receipt = new Receipt();
                receipt.date = fi.Name.Replace(".txt", "");
                List<string> lines = File.ReadAllLines(fi.FullName).ToList();
                foreach (string line in lines)
                {
                    if (line != "")
                    {
                        string[] entries = line.Split('|');
                        receipt.products.Add(GetParsedProduct(entries));
                    }
                }
                receipts.Add(receipt);
            }
            return receipts;
        }

        public bool Add(Receipt receipt)
        {
            FileInfo fi = new FileInfo(dbPath + receipt.date + ".txt");
            using (StreamWriter sw = fi.CreateText())
            {
                foreach (Product product in receipt.products)
                {
                    string data = product.id + "|" + product.name + "|" + product.price + "|" + product.stock + "|" + product.productType;
                    sw.WriteLine(data);
                }
            }
            return true;
        }

        public bool Remove(Receipt receipt)
        {
            DirectoryInfo di = new DirectoryInfo(dbPath);
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo fi in files)
            {
                if (fi.Name.Replace(".txt", "") == receipt.date)
                {
                    File.Delete(fi.FullName);
                    return true;
                }
            }
            return false;
        }

        public bool Update(Receipt receipt)
        {
            DirectoryInfo di = new DirectoryInfo(dbPath);
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo fi in files)
            {
                if (fi.Name.Replace(".txt", "") == receipt.date)
                {
                    File.WriteAllText(fi.FullName, String.Empty);
                    using (StreamWriter sw = new StreamWriter(fi.FullName))
                    {
                        foreach (Product product in receipt.products)
                        {
                            string data = product.id + "|" + product.name + "|" + product.price + "|" + product.stock + "|" + product.productType;
                            sw.WriteLine(data);
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private Product GetParsedProduct(string[] entries)
        {
            Product product = new Product();
            int.TryParse(entries[0], out int productId);
            product.id = productId;
            product.name = entries[1];
            double.TryParse(entries[2], out double productPrice);
            product.price = productPrice;
            int.TryParse(entries[3], out int productStock);
            product.stock = productStock;
            Enum.TryParse(entries[4], out Product.ProductType productType);
            product.productType = productType;

            return product;
        }
    }
}
