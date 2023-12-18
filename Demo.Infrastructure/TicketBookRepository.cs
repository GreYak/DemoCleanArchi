using Shop;
using Shop.Repository;

namespace Demo.Infrastructure
{
    public class TicketBookRepository : ITicketBookRepository
    {
        public Task CreateTicketBookAsync(TicketBook ticketBook)
        {
            throw new NotImplementedException();
        }

        public Task<TicketBook?> GetTicketBookByIdAsync(Guid ticketBookId)
        {
            throw new NotImplementedException();
        }
    }
}