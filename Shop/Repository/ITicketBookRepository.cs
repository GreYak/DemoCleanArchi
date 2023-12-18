namespace Shop.Repository
{
    public interface ITicketBookRepository
    {
        Task CreateTicketBookAsync(TicketBook ticketBook);
        Task<TicketBook?> GetTicketBookByIdAsync(Guid ticketBookId);
    }
}
