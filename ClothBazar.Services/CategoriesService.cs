﻿using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClothBazar.Services
{
    public class CategoriesService
    {
        public Category GetCategory(int ID)
        {

            using (var context = new CBContext())
            {
                return context.Categories.Find(ID);
            }
        }
        public List<Category>GetCategories()
        {
            
            using (var context = new CBContext())
            {
                return context.Categories.Include(x => x.Products).ToList();
            }
        }
        public List<Category> GetFeaturedCategories()
        {

            using (var context = new CBContext())
            {
                return context.Categories.Where(x=>x.isFeatured && x.ImageURL != null).ToList();
            }
        }
        public void SaveCategory(Category category)
        {
            // object
            using (var context = new CBContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            // object
            using (var context = new CBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteCategory(int ID)
        {
            // object
            using (var context = new CBContext())
            {
                var category = context.Categories.Find(ID);
                context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                //both working same above or below
                //context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
