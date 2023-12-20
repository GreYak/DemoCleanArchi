using Transport.Exceptions;

namespace Transport.Contracts
{
    /// <summary>
    /// Service to manage a conrol
    /// </summary>
    public class ControlService
    {
        private User _user;
        private Controller _controller;

        public ControlService(ref User user, ref Controller controller)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }

        /// <summary>
        /// Execute the control
        /// </summary>
        /// <param name="controlDate">Date of the control</param>
        /// <exception cref="NoMoreTicketForUserException"></exception>
        public void DoControl(DateTimeOffset controlDate)
        {
            if (!_user.HasCurrentTicket)
            {
                if (_user.HasTicketInPocket)
                {
                    _user.UseTicket();
                }
            }

            if (_user.CurrentTicket?.IsValid(controlDate)!= true)
            {
                _controller.NoticeFraudster(_user.Id);
                _user.BuyTickets(new List<Ticket> { new Ticket(Guid.NewGuid(), controlDate) }) ;
                _user.UseTicket();
            }
 
             _controller.Compost(_user.CurrentTicket!.Id, controlDate);
        }
    }
}
