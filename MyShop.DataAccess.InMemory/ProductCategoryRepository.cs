using Myshop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories ;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory category)
        {
            productCategories.Add(category);
        }
        public void Update(ProductCategory productCategory)
        {
            var productCat = productCategories.Find(c => c.Id == productCategory.Id);

            if (productCat != null)
            {
                productCat = productCategory;
            }
            else
            {
                throw new Exception("Product category not found");
            }
        }

        public ProductCategory Find(string id)
        {
            var productCat = productCategories.Find(p => p.Id == id);

            if (productCat != null)
            {
                return productCat;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string id)
        {
            var productCat = productCategories.Find(p => p.Id == id);
            if (productCat != null)
            {
                productCategories.Remove(productCat);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
