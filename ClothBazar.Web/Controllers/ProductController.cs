using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsService productsService = new ProductsService();
        CategoriesService categoryService = new CategoriesService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string search)
        {
            //var products = productsService.GetProducts();
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.Products = productsService.GetProducts();
            if (!string.IsNullOrEmpty(search))
            {
                model.SearchTerm = search;
                model.Products = model.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();
            model.AvailableCategories = categoryService.GetCategories();
            return PartialView(model);
            //CategoriesService categoryService = new CategoriesService();
            //var categories = categoryService.GetCategories();
            //return PartialView(categories);
        }
        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
           
            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            //newProduct.CategoryID = model.CategoryID;
            newProduct.category= categoryService.GetCategory(model.CategoryID);
            productsService.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();
            var product = productsService.GetProduct(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.category != null ? product.category.ID : 0;

            model.AvailableCategories = categoryService.GetCategories();

            return PartialView(model);
            //var product = productsService.GetProduct(ID);
            //return PartialView(product);
        }
        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = productsService.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.category = categoryService.GetCategory(model.CategoryID);

            productsService.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
            //productsService.UpdateProduct(product);
            //return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productsService.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }
    }
}