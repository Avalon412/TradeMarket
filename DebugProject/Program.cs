using Business.Customers;
using Business.Products;
using Business.Products.DTOs;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

var optionsBuilder = new DbContextOptionsBuilder<TradeMarketDbContext>();
optionsBuilder.UseSqlServer(@"Server=AVALON;Database=TradeMarket;Trusted_Connection=True;Encrypt=true;TrustServerCertificate=True");
using var context = new TradeMarketDbContext(optionsBuilder.Options);
var unitOfWork = new UnitOfWork(context);
var service = new ProductService(unitOfWork);

await service.DeleteAsync(8);

Console.WriteLine();