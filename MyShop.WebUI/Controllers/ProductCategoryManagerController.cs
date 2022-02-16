using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using Myshop.Core.Models;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository <ProductCategory> context;
        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productcatory = new ProductCategory();
            return View(productcatory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory cat)
        {
            if (!ModelState.IsValid)
            {
                return View(cat);
            }
            else
            {
                context.Insert(cat);
                context.Commit();

                return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult Edit(string id)
        {
           var productCategory = context.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCateg, string id)
        {
            var categoryEdit = context.Find(id);

            if (categoryEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCateg);
                }

                categoryEdit.Category = productCateg.Category;
                context.Commit();
                return RedirectToAction(nameof(Index));

            }
        }
        public ActionResult Delete(string id)
        {
            var category = context.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(category);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            var product = context.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();

                return RedirectToAction(nameof(Index));
            }
        }

    }
}