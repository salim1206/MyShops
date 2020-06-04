using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShops.DataAccess.InMemory
{
   public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }
        public void Comit()
        {
            cache["products"] = products;
        }
        public void insert(Product P)
        {
            products.Add(P);
        }
        public void Update(Product P)
        {
           Product ProducttoBeUpdated = products.FirstOrDefault(p => p.Id == P.Id);
            if (ProducttoBeUpdated != null)
            {
                ProducttoBeUpdated = P;
            }
            else
            {
                throw new Exception("product Not Found");
            }
        }
        public Product Find(String ID)
        {
            Product product = products.FirstOrDefault(p => p.Id == ID);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("product Not Found");
            }

        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(String ID)
        {
            Product ProducttoBeRemoved = products.FirstOrDefault(p => p.Id == ID);
            if (ProducttoBeRemoved != null)
            {
                products.Remove(ProducttoBeRemoved);
            }
            else
            {
                throw new Exception("product Not Found");
            }

        }
    }
}
