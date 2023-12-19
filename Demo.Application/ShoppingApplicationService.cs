using Demo.Application.Abstraction;
using Demo.Application.Dtos.Commands;
using Demo.Application.Exceptions;
using Shop;
using Shop.Repository;

namespace Demo.Application
{
    public class ShoppingApplicationService : IShoppingApplicationService
    {
        private readonly ITicketBookRepository _ticketBookRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingApplicationService(ITicketBookRepository ticketBookRepository, IUserRepository userRepository)
        {
            _ticketBookRepository = ticketBookRepository;
            _userRepository = userRepository;
        }

        /// <inheritdoc/>
        public async Task AddingTicketBookInStoreAsync(CreateTicketBookCommand ticketBookCreationCommand, DateTimeOffset contextualDate)
        {

            if (ticketBookCreationCommand?.IsValid() != true)
                throw new InvalidParamException(nameof(ticketBookCreationCommand), ticketBookCreationCommand);

            TicketBook? ticketBook = await _ticketBookRepository.GetTicketBookByIdAsync(ticketBookCreationCommand.TicketBookId);
            if (ticketBook != null)
            {
                throw new AlreadyExistException(nameof(TicketBook), ticketBookCreationCommand.TicketBookId);
            }

            await _ticketBookRepository.CreateTicketBookAsync(ticketBookCreationCommand.ToDomain(contextualDate));
        }

        /// <inheritdoc/>
        public async Task UserBuyTicketBookAsync(Guid userId, Guid ticketBookId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId) ?? new User(userId);
            TicketBook ticketBook = await _ticketBookRepository.GetTicketBookByIdAsync(ticketBookId) ?? throw new NotFoundException(nameof(TicketBook), ticketBookId);

            user.Buy(ticketBook);

            await _userRepository.SaveAsync(user);
        }
    }
}