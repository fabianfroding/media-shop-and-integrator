using MediaShop.Models;
using MediaShop.Repositories;
using System.Collections.Generic;

namespace MediaShop.Controllers
{
    // Denna klass hanterar produkt-data som skickas mellan databasen (repository) och GUI (windows forms).
    class ProductController
    {
        private ProductRepository productRepository;

        public ProductController()
        {
            productRepository = new ProductRepository();
        }

        public List<Product> GetAll()
        {
            return productRepository.GetAllFromDB(); //.GetAll();
        }

        public Product GetById(int id)
        {
            return productRepository.GetById(id);
        }

        public bool Add(Product product)
        {
            return productRepository.Add(product);
        }

        public bool Remove(Product product)
        {
            return productRepository.Remove(product);
        }

        public bool Update(Product product)
        {
            return productRepository.Update(product);
        }

        public bool ExportProducts(string path)
        {
            return productRepository.ExportProducts(path);
        }

        public bool ImportProducts(string path)
        {
            productRepository.ImportProducts(path);
            return true;
        }
    }
}
