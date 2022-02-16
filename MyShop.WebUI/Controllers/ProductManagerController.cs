﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myshop.Core.Models;
using MyShop.DataAccess.InMemory;
using Myshop.Core.ViewModels;


namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Product> context;

        InMemoryRepository<ProductCategory> productCategories;
       
        public ProductManagerController()
        {
            context = new InMemoryRepository<Product>();
            productCategories = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string id)
        {
            var productEdit = context.Find(id);
           
            if (productEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if(!ModelState.IsValid)
                {
                    return View(product);
                }
                
                    productEdit.Name = product.Name;
                    productEdit.Category = product.Category;
                    productEdit.Description = product.Description;
                    productEdit.Image = product.Image;
                    productEdit.Price = product.Price;

                    context.Commit(); 
                    return RedirectToAction(nameof(Index)); 
                
            }
        }
        public ActionResult Delete(string id)
        {
            var product = context.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
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