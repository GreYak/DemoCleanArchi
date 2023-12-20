using Demo.Application.Dtos.Commands;
using Demo.Application.Exceptions;

namespace Demo.Application.Abstraction
{
    /// <summary>
    /// Manage the shopping use-cases.
    /// </summary>
    public interface IShoppingApplicationService
    {
        /// <summary>
        /// Adding a new ticket-book in the store.
        /// </summary>
        /// <param name="ticketBookCreationCommand"><see cref="CreateTicketBookCommand"/></param>
        /// <param name="contextualDate">The contextual date of action.</param>
        /// <returns><see cref="Task"/></returns>
        /// <exception cref="InvalidParamException">When <see cref="CreateTicketBookCommand"/>isn't valid.</exception>
        /// <exception cref="AlreadyExistException">When ticketbook already exists.</exception>
        /// <exception cref="DomainException"></exception> 
        Task AddingTicketBookInStoreAsync(CreateTicketBookCommand ticketBookCreationCommand, DateTimeOffset contextualDate);

        /// <summary>
        /// Manage the buying of a ticketbook by an user.
        /// </summary>
        /// <param name="userId">The id of the buyer</param>
        /// <param name="ticketBookId">The id of the ticketbook to buy</param>
        /// <returns><see cref="Task"/></returns>
        /// <exception cref="NotFoundException">When ticketbook doesn't exist.</exception>
        /// <exception cref="DomainException"></exception> 
        Task UserBuyTicketBookAsync(Guid userId, Guid ticketBookId);
    }
}
