using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClothBazar.Services
{
    public class ProductsService
    {
        public Product GetProduct(int ID)
        {

            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.ID == ID).Include(x => x.category).FirstOrDefault();
            }
        }
        public List<Product> GetProducts(List<int> IDs)
        {

            using (var context = new CBContext())
            {
                return context.Products.Where(x => IDs.Contains(x.ID)).ToList();
            }
        }
        public List<Product>GetProducts()
        {
            
            using (var context = new CBContext())
            {
                return context.Products.Include(x=>x.category).ToList();
            }
        }
        public void SaveProduct(Product product)
        {
            // object
            using (var context = new CBContext())
            {
                context.Entry(product.category).State = System.Data.Entity.EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {
            // object
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteProduct(int ID)
        {
            // object
            using (var context = new CBContext())
            {
                var product = context.Products.Find(ID);
                context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                //both working same above or below
                //context.products.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
