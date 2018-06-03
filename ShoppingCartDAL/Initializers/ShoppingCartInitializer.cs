﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCartDAL.EF;
using ShoppingCartDAL.Models;

namespace ShoppingCartDAL.Initializers
{
    public static class ShoppingCartInitializer
    {
        public static void InitializeData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ShoppingCartDbContext>();
            InitializeData(context);
        }

        public static void InitializeData(ShoppingCartDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            ClearData(context);
            SeedData(context);
        }

        public static void ClearData(ShoppingCartDbContext context)
        {
            ExecuteDeleteSQL(context, "ShoppingCartItems");
            ExecuteDeleteSQL(context, "Products");
            ResetIdentity(context, "ShoppingCartItems");
            ResetIdentity(context, "Products");
        }

        public static void ExecuteDeleteSQL(ShoppingCartDbContext context, string tableName)
        {
            context.Database.ExecuteSqlCommand($"Delete from {tableName}");
        }

        public static void ResetIdentity(ShoppingCartDbContext context, string tableName)
        {
            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT (\"{tableName}\", RESEED, 0);");
        }

        public static IEnumerable<Product> GetProducts()
        {

            yield return new Product
            {
                Name = "Butter",
                Price = 0.80M
            };
            yield return new Product
            {
                Name = "Milk",
                Price = 1.15M
            };
            yield return new Product
            {
                Name = "Bread",
                Price = 1M
            };
        }

        public static void SeedData(ShoppingCartDbContext context)
        {
            try
            {
                if (context.Products.Any()) return;
                context.Products.AddRange(GetProducts());
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
