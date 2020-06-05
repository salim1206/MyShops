using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShops.DataAccess.InMemory;


namespace MyShop.WebUI.Controllers
{
    public class ProductManegerController : Controller
    {
        ProductRepository context;
        public ProductManegerController()
        {
            context = new ProductRepository();
        }

        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult CreateProduct()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.insert(product);
                context.Comit();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit (string Id)
        {
            Product product = context.Find(Id);
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
        public ActionResult Edit(Product product, string ID)
        {
            Product productToBeEdited = context.Find(ID);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToBeEdited.Name = product.Name;
                productToBeEdited.Price = product.Price;
                productToBeEdited.Image = product.Image;
                productToBeEdited.Category = product.Category;


                context.Comit();
                return RedirectToAction("Index");
            }

        }
        public ActionResult Delete(string Id)
        {
            Product product = context.Find(Id);
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
        public ActionResult ConfirmDelete(string ID)
        {
            Product productToBeRemoved = context.Find(ID);
            if (productToBeRemoved == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(ID);
                context.Comit();
                return RedirectToAction("Index");
            }

        }


    }
}