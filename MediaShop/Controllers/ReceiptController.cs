using MediaShop.Models;
using MediaShop.Repositories;
using System;
using System.Collections.Generic;

namespace MediaShop.Controllers
{
    // Denna klass hanterar receipt-data som skickas mellan databasen (repository) och GUI (windows forms).
    class ReceiptController
    {
        private ReceiptRepository receiptRepository;

        public ReceiptController()
        {
            receiptRepository = new ReceiptRepository();
        }

        //=============== Base Functions ===============//

        public Receipt GetByDate(string date)
        {
            return receiptRepository.GetByDate(date);
        }

        public List<Receipt> GetAll()
        {
            return receiptRepository.GetAll();
        }

        public bool Add(Receipt receipt)
        {
            return receiptRepository.Add(receipt);
        }

        public bool Remove(Receipt receipt)
        {
            return receiptRepository.Remove(receipt);
        }

        public bool Update(Receipt receipt)
        {
            return receiptRepository.Update(receipt);
        }

        //=============== Special Functions ===============//

        // Funktion som filtrerar receipts som endast matchar a specifikt datum-intervall.
        public List<Receipt> GetAllBetweenDates(string from, string to)
        {
            List<Receipt> receipts = GetAll();
            List<Receipt> receiptsFound = new List<Receipt>();
            foreach (Receipt receipt in receipts)
            {
                string date = receipt.date.Substring(0, 8);
                DateTime receiptDate = DateTime.ParseExact(date, "yyyyMMdd", null);
                DateTime fromDate = DateTime.ParseExact(from, "yyyyMMdd", null);
                DateTime toDate = DateTime.ParseExact(to, "yyyyMMdd", null);
                if (DateTime.Compare(receiptDate, fromDate) >= 0 && DateTime.Compare(receiptDate, toDate) <= 0)
                {
                    receiptsFound.Add(receipt);
                }
            }
            return receiptsFound;
        }

    }
}
