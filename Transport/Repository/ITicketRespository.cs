namespace Transport.Repository
{
    /// <summary>
    /// Manage the <see cref="Ticket"/> persistancy.
    /// </summary>
    public interface ITicketRespository
    {
        /// <summary>
        /// Create or update a <see cref="Ticket"/>
        /// </summary>
        /// <param name="ticket">The <see cref="Ticket"/> to save</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateAsync(Ticket ticket);
    }
}
