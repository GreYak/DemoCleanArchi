using Demo.Application.Abstraction;
using Demo.Application.Dtos.Commands;
using Demo.Application.Exceptions;
using Shop;
using Shop.Repository;
using Transport;
using ITransportUserRepository = Transport.Repository.IUserRepository;
using IShopUserRepository = Shop.Repository.IUserRepository;
using ShoppingUser = Shop.User;
using TransportUser = Transport.User;
using TransportTicket = Transport.Ticket;

namespace Demo.Application
{
    public class ShoppingApplicationService : IShoppingApplicationService
    {
        private readonly ITicketBookRepository _ticketBookRepository;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly ITransportUserRepository _transportUserRepository;

        public ShoppingApplicationService(ITicketBookRepository ticketBookRepository, IShopUserRepository shopUserRepository, ITransportUserRepository transportUserRepository)
        {
            _ticketBookRepository = ticketBookRepository;
            _shopUserRepository = shopUserRepository;
            _transportUserRepository = transportUserRepository;
        }

        /// <inheritdoc/>
        public async Task AddingTicketBookInStoreAsync(CreateTicketBookCommand ticketBookCreationCommand, DateTimeOffset contextualDate)
        {

            if (ticketBookCreationCommand?.IsValid() != true)
                throw new InvalidParamException(nameof(ticketBookCreationCommand), ticketBookCreationCommand);

            TicketBook? ticketBook = await _ticketBookRepository.GetTicketBookByIdAsync(ticketBookCreationCommand.TicketBookId);
            if (ticketBook != null)
                throw new AlreadyExistException(nameof(TicketBook), ticketBookCreationCommand.TicketBookId);

            try
            {
                ticketBook = ticketBookCreationCommand.ToDomain(contextualDate);
            }
            catch (Exception ex) 
            { 
                throw new DomainException(nameof(ShoppingApplicationService.AddingTicketBookInStoreAsync), ex);
            }

            await _ticketBookRepository.CreateTicketBookAsync(ticketBook);
        }

        /// <inheritdoc/>
        public async Task UserBuyTicketBookAsync(Guid userId, Guid ticketBookId)
        {
            ShoppingUser shoppingUser = await _shopUserRepository.GetUserByIdAsync(userId) ?? new ShoppingUser(userId);
            TransportUser transportUser = await _transportUserRepository.GetUserByIdAsync(userId) ?? new TransportUser(userId);
            TicketBook ticketBook = await _ticketBookRepository.GetTicketBookByIdAsync(ticketBookId) ?? throw new NotFoundException(nameof(TicketBook), ticketBookId);

            try 
            { 
                shoppingUser.Buy(ticketBook);
                transportUser.BuyTickets(ticketBook.Tickets.Select(t => new TransportTicket(t.Id, ticketBook.IssueDate)));
            }
            catch (Exception ex) 
            { 
                throw new DomainException(nameof(ShoppingApplicationService.UserBuyTicketBookAsync), ex);
            }

            await _shopUserRepository.SaveAsync(shoppingUser);
            await _transportUserRepository.SaveAsync(transportUser);
        }
    }
}