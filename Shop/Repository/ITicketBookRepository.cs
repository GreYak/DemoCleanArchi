namespace Shop.Repository
{
    /// <summary>
    /// Manage the <see cref="TicketBook"/> persistancy.
    /// </summary>
    public interface ITicketBookRepository
    {
        /// <summary>
        /// Create a <see cref="TicketBook"/>.
        /// </summary>
        /// <param name="ticketBook"></param>
        /// <returns><see cref="Task"/></returns>
        Task CreateTicketBookAsync(TicketBook ticketBook);

        /// <summary>
        /// Get the <see cref="TicketBook"/> identified by its Id.
        /// </summary>
        /// <param name="ticketBookId">The <see cref="TicketBook"/> identifier.</param>
        /// <returns>The <see cref="TicketBook"/> if found, else null.</returns>
        Task<TicketBook?> GetTicketBookByIdAsync(Guid ticketBookId);
    }
}
