using Demo.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Transport;
using Transport.Repositories;

namespace Demo.Infrastructure
{
    public class TicketRepository : ITicketRespository
    {
        private readonly DemoDbContext _dbContext;

        public TicketRepository(DemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Ticket ticket)
        {
            ArgumentNullException.ThrowIfNull(ticket);

            var ticketDb = await _dbContext.Tickets.SingleAsync(t => t.Id == ticket.Id);
            ticketDb.Feed(ticket);

            _dbContext.SaveChanges();
        }
    }
}
