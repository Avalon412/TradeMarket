using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(TradeMarketDbContext dbContext)
            : base(dbContext) 
        { 

        }
    }
}
