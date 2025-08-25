using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public static class DataSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // ==== Product Categories ====
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { Id = 1, CategoryName = "Beverages" },
                new ProductCategory { Id = 2, CategoryName = "Snacks" },
                new ProductCategory { Id = 3, CategoryName = "Dairy" }
            );

            // ==== Products ====
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, ProductCategoryId = 1, ProductName = "Cola", Price = 2.50m },
                new Product { Id = 2, ProductCategoryId = 1, ProductName = "Juice", Price = 3.00m },
                new Product { Id = 3, ProductCategoryId = 1, ProductName = "Water", Price = 1.20m },
                new Product { Id = 4, ProductCategoryId = 2, ProductName = "Chips", Price = 1.80m },
                new Product { Id = 5, ProductCategoryId = 2, ProductName = "Chocolate", Price = 2.20m },
                new Product { Id = 6, ProductCategoryId = 3, ProductName = "Milk", Price = 1.50m },
                new Product { Id = 7, ProductCategoryId = 3, ProductName = "Cheese", Price = 4.50m }
            );

            // ==== Persons ====
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "John", Surname = "Doe", BirthDate = new DateTime(1990, 5, 20) },
                new Person { Id = 2, Name = "Anna", Surname = "Smith", BirthDate = new DateTime(1985, 10, 10) },
                new Person { Id = 3, Name = "Mark", Surname = "Johnson", BirthDate = new DateTime(2000, 3, 15) },
                new Person { Id = 4, Name = "Sophia", Surname = "Williams", BirthDate = new DateTime(1995, 8, 25) }
            );

            // ==== Customers ====
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, PersonId = 1, DiscountValue = 5 },
                new Customer { Id = 2, PersonId = 2, DiscountValue = 10 },
                new Customer { Id = 3, PersonId = 3, DiscountValue = 0 },
                new Customer { Id = 4, PersonId = 4, DiscountValue = 15 }
            );

            // ==== Receipts ====
            modelBuilder.Entity<Receipt>().HasData(
                new Receipt { Id = 1, CustomerId = 1, OperationDate = new DateTime(2025, 08, 15), IsCheckedOut = true },
                new Receipt { Id = 2, CustomerId = 2, OperationDate = new DateTime(2025, 08, 12), IsCheckedOut = true },
                new Receipt { Id = 3, CustomerId = 3, OperationDate = new DateTime(2025, 08, 18), IsCheckedOut = false },
                new Receipt { Id = 4, CustomerId = 4, OperationDate = new DateTime(2025, 08, 10), IsCheckedOut = true }
            );

            // ==== Receipt Details ====
            modelBuilder.Entity<ReceiptDetail>().HasData(
                new ReceiptDetail { Id = 1, ReceiptId = 1, ProductId = 1, UnitPrice = 2.50m, DiscountUnitPrice = 2.25m, Quantity = 2 },
                new ReceiptDetail { Id = 2, ReceiptId = 1, ProductId = 4, UnitPrice = 1.80m, DiscountUnitPrice = 1.62m, Quantity = 1 },

                new ReceiptDetail { Id = 3, ReceiptId = 2, ProductId = 2, UnitPrice = 3.00m, DiscountUnitPrice = 2.70m, Quantity = 3 },
                new ReceiptDetail { Id = 4, ReceiptId = 2, ProductId = 6, UnitPrice = 1.50m, DiscountUnitPrice = 1.35m, Quantity = 2 },

                new ReceiptDetail { Id = 5, ReceiptId = 3, ProductId = 3, UnitPrice = 1.20m, DiscountUnitPrice = 1.20m, Quantity = 5 },
                new ReceiptDetail { Id = 6, ReceiptId = 3, ProductId = 5, UnitPrice = 2.20m, DiscountUnitPrice = 2.00m, Quantity = 1 },

                new ReceiptDetail { Id = 7, ReceiptId = 4, ProductId = 7, UnitPrice = 4.50m, DiscountUnitPrice = 4.00m, Quantity = 2 },
                new ReceiptDetail { Id = 8, ReceiptId = 4, ProductId = 1, UnitPrice = 2.50m, DiscountUnitPrice = 2.25m, Quantity = 1 }
            );
        }
    }
}
