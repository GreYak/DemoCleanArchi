using Demo.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Repository;

namespace Demo.Infrastructure
{
    public class TicketBookRepository : ITicketBookRepository
    {
        private readonly DemoDbContext _dbContext = new DemoDbContext();

        public Task CreateTicketBookAsync(TicketBook ticketBook)
        {
            throw new NotImplementedException();
        }

        public async Task<TicketBook?> GetTicketBookByIdAsync(Guid ticketBookId)
        {
            var ticketBookDb = await _dbContext.TicketBooks.SingleOrDefaultAsync(tb => tb.Id == ticketBookId);
            return ticketBookDb?.ToShopDomain();
        }
    }
}