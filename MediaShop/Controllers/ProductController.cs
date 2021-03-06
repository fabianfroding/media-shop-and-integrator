﻿using MediaShop.Models;
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
            return productRepository.GetByIdFromDB(id); //GetById(id);
        }

        public bool Add(Product product)
        {
            return productRepository.Add(product) && productRepository.AddToDB(product);
        }

        public bool Remove(Product product)
        {
            return productRepository.Remove(product) && productRepository.RemoveFromDB(product);
        }

        public bool Update(Product product)
        {
            return productRepository.UpdateInDB(product); //Update(product);
        }

        public bool ExportProducts(string path)
        {
            return productRepository.ExportProductsFromDB(path);
        }

        public bool ImportProducts(string path)
        {
            productRepository.ImportProductsToDB(path); //ImportProducts(path);
            return true;
        }
    }
}
