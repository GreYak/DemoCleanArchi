using Demo.Infrastructure.Ef;
using Demo.Infrastructure.Ef.Model;
using Microsoft.EntityFrameworkCore;
using Shop;
using Shop.Repository;

namespace Demo.Infrastructure
{
    public class TicketBookRepository : ITicketBookRepository
    {
        private readonly DemoDbContext _dbContext;

        public TicketBookRepository(DemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateTicketBookAsync(TicketBook ticketBook)
        {
            ArgumentNullException.ThrowIfNull(ticketBook);

            var tickets = _dbContext.Tickets.Where(t => ticketBook.TicketIds.Contains(t.Id));

            var ticketBookDb = new TicketBookDb()
            {
                Id = ticketBook.Id,
                IssueDate = ticketBook.IssueDate
            };
            ticketBookDb.Tickets.AddRange(tickets);
            await _dbContext.TicketBooks.AddAsync(ticketBookDb);
                
            _dbContext.SaveChanges();
        }

        public async Task<TicketBook?> GetTicketBookByIdAsync(Guid ticketBookId)
        {
            var ticketBookDb = await _dbContext.TicketBooks.AsNoTracking()
                .Include(tb => tb.Tickets)
                .SingleOrDefaultAsync(tb => tb.Id == ticketBookId);
            return ticketBookDb?.ToShopDomain();
        }
    }
}