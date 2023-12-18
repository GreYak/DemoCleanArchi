using Demo.Application.Model.Commands;
using Microsoft.Extensions.Logging;
using Shop;
using Shop.Repository;

namespace Demo.Application
{
    public class ShoppingApplicationService
    {
        private readonly ILogger _logger;
        private readonly ITicketBookRepository _ticketBookRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingApplicationService(ILogger logger, ITicketBookRepository ticketBookRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _ticketBookRepository = ticketBookRepository;
            _userRepository = userRepository;
        }

        public async Task CreateTicketBook(CreateTicketBookDto ticketBookCreationCommand)
        {
            ArgumentNullException.ThrowIfNull(nameof(ticketBookCreationCommand));
            if (!ticketBookCreationCommand.IsValid())
                throw new ArgumentException(nameof(ticketBookCreationCommand));

            TicketBook? ticketBook = await _ticketBookRepository.GetTicketBookByIdAsync(ticketBookCreationCommand.TicketBookId);
            if (ticketBook is not null)
            {
                var error = $"{nameof(TicketBook)} with id '{ticketBookCreationCommand.TicketBookId}' already exixts.";
                _logger.LogWarning(error);
                throw new ApplicationException(error);
            }

            await _ticketBookRepository.CreateTicketBookAsync(ticketBookCreationCommand.ToDomain(DateTimeOffset.Now));  // TODO.
        }

        public async Task UserBuyTicketBookAsync(Guid userId, Guid ticketBookId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId) ?? new User(userId);
            TicketBook ticketBook = await _ticketBookRepository.GetTicketBookByIdAsync(ticketBookId) ?? throw new ArgumentException(nameof(ticketBookId));

            user.Buy(ticketBook);

            await _userRepository.SaveAsync(user);
        }
    }
}