using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL.Factories
{
    public class TradeMarketDbContextDesignFactory : IDesignTimeDbContextFactory<TradeMarketDbContext>
    {
        public TradeMarketDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TradeMarketDbContext>();
            optionsBuilder.UseSqlServer(@"Server=AVALON;Database=TradeMarket;Trusted_Connection=True;Encrypt=true;TrustServerCertificate=True");

            return new TradeMarketDbContext(optionsBuilder.Options);
        }
    }
}
