using MediaShop.Models;
using System.Collections.Generic;

namespace MediaShop.Repositories
{
    interface IProductRepository
    {
        Product GetById(int id);
        List<Product> GetAll();
        bool Add(Product product);
        bool Remove(Product product);
        bool Update(Product product);
    }
}
