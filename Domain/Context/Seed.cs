using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Context
{
    public static class Seed
    {
        public static void SeedData(this ShopDbContext dbContext)
        {
            if (!dbContext.Suppliers.Any())
            {
                dbContext.Suppliers.AddRange(
                    new Supplier { Name = "Samsung", Email = "samsung@samsung.com" },
                    new Supplier { Name = "Apple", Email = "apple@apple.com" },
                    new Supplier { Name = "Sony", Email = "sony@sony.com" },
                    new Supplier { Name = "Lenovo", Email = "lenovo@lenovo.com" },
                    new Supplier { Name = "Logitech", Email = "logitech@logitech.com" },
                    new Supplier { Name = "Trust", Email = "trust@trust.com" }
                );
                dbContext.SaveChanges();
            }

            if (!dbContext.Products.Any())
            {
                var suppliers = dbContext.Suppliers.ToList();
                dbContext.Products.AddRange(
                    new Product { SupplierId = suppliers.Single(s => s.Name == "Samsung").Id, Name = "Samsug S15", Category = Category.Smartphone, Prezzo = 900m },
                    new Product { SupplierId = suppliers.Single(s => s.Name == "Samsung").Id, Name = "Samsung TV", Category = Category.TV, Prezzo = 1400m },
                    new Product { SupplierId = suppliers.Single(s => s.Name == "Apple").Id, Name = "MacBook Pro", Category = Category.Notebook, Prezzo = 2700m },
                    new Product { SupplierId = suppliers.Single(s => s.Name == "Apple").Id, Name = "iPhone 15", Category = Category.Smartphone, Prezzo = 1300m },
                    new Product { SupplierId = suppliers.Single(s => s.Name == "Lenovo").Id, Name = "Notebook", Category = Category.Notebook, Prezzo = 700m },
                    new Product { SupplierId = suppliers.Single(s => s.Name == "Lenovo").Id, Name = "Lenovo TV 32 pol", Category = Category.TV, Prezzo = 500m },
                    new Product { SupplierId = suppliers.Single(s => s.Name == "Lenovo").Id, Name = "Lenovo TV 27 pol", Category = Category.TV, Prezzo = 350m },
                    new Product { SupplierId = suppliers.Single(s => s.Name == "Lenovo").Id, Name = "Lenovo TV 21 pol", Category = Category.TV, Prezzo = 200m },
                    new Product { SupplierId = suppliers.Single(s => s.Name == "Trust").Id, Name = "Webcam", Category = Category.Accessories, Prezzo = 35m },
                    new Product { SupplierId = suppliers.Single(s => s.Name == "Logitech").Id, Name = "Stereo", Category = Category.Accessories, Prezzo = 60m }
                );
                
                
                dbContext.SaveChanges();
            }

        }
    }
}
