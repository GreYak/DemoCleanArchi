using Demo.Application.Abstraction;
using Demo.Application.Exceptions;
using Transport;
using Transport.Contracts;
using Transport.Repositories;

namespace Demo.Application
{
    public class TransportApplicationService : ITransportApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITicketRespository _ticketRepository;
        private readonly IControllerRepository _controllerRepository;


        public TransportApplicationService(IUserRepository userRepository, ITicketRespository ticketRepository, IControllerRepository controllerRepository)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
            _controllerRepository = controllerRepository;
        }

        /// <inheritdoc/>
        public async Task UserTakesTransportAsync(Guid userId, DateTimeOffset contextualDate)
        {
            User user = await _userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException(nameof(User), userId);

            Ticket ticket;
            try
            {
                ticket = user.UseTicket();
                ticket.Compost(contextualDate);
            }
            catch (Exception ex)
            {
                throw new DomainException(nameof(ShoppingApplicationService.UserBuyTicketBookAsync), ex);
            }

            await _userRepository.SaveAsync(user);
            await _ticketRepository.UpdateAsync(ticket);
        }

        /// <inheritdoc/>
        public async Task ControlUserInTransportAsync(Guid controllerId, Guid userId, DateTimeOffset dateOfControl)
        {
            User user = await _userRepository.GetUserByIdAsync(userId) ?? throw new NotFoundException(nameof(User), userId);
            Controller controller = await _controllerRepository.GetControllerByIdAsync(controllerId) ?? throw new NotFoundException(nameof(Controller), controllerId);

            try
            {
                var controlService = new ControlService(ref user, ref controller);
                controlService.DoControl(dateOfControl);
            }
            catch (Exception ex)
            {
                throw new DomainException(nameof(ShoppingApplicationService.UserBuyTicketBookAsync), ex);
            }

            await _userRepository.SaveAsync(user);
            await _controllerRepository.SaveAsync(controller);
        }
    }
}
