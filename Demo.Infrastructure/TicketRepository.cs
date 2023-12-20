using Transport;
using Transport.Repository;

namespace Demo.Infrastructure
{
    public class TicketRepository : ITicketRespository
    {
        public Task SaveAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
