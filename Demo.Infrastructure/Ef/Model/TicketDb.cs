namespace Demo.Infrastructure.Ef.Model
{
    public class TicketDb
    {
        public Guid Id { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset? CompostDate { get; set; }
        public DateTimeOffset? EndOfValidityDate { get; set; }
        public DateTimeOffset? ControlDate { get; set; }

        internal Shop.Ticket ToShopDomain()
        {
            return new Shop.Ticket(Id);
        }

        internal Transport.Ticket ToTransportDomain()
        {
            return new Transport.Ticket(Id, IssueDate, CompostDate, EndOfValidityDate);
        }
    }
}
