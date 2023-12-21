using Transport;

namespace Demo.Infrastructure.Ef.Model
{
    public class TicketDb
    {
        public Guid Id { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset? CompostDate { get; set; }
        public DateTimeOffset? EndOfValidityDate { get; set; }
        public DateTimeOffset? ControlDate { get; set; }

        //public Guid TicketBookId { get; set; }
        //public TicketBookDb TicketBook { get; set; }


        internal void Feed(Ticket currentTicket)
        {
            ArgumentNullException.ThrowIfNull(currentTicket);

            IssueDate = currentTicket.IssueDate;
            CompostDate = currentTicket.CompostDate;
            EndOfValidityDate = currentTicket.EndOfValidityDate;
        }

        internal Transport.Ticket ToTransportDomain()
        {
            return new Transport.Ticket(Id, IssueDate, CompostDate, EndOfValidityDate);
        }
    }
}
