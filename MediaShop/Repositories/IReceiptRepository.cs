using MediaShop.Models;
using System.Collections.Generic;

namespace MediaShop.Repositories
{
    interface IReceiptRepository
    {
        Receipt GetByDate(string date);
        List<Receipt> GetAll();
        bool Add(Receipt receipt);
        bool Remove(Receipt receipt);
        bool Update(Receipt receipt);
    }
}
