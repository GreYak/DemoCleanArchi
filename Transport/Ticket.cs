using Transport.Exceptions;

namespace Transport
{
    public class Ticket
    {
        public Guid Id { get; }
        public DateTimeOffset IssueDate { get; }
        public DateTimeOffset? _compostDate;
        public DateTimeOffset? EndOfValidityDate { get; private set; }

        private Ticket(Guid id, DateTimeOffset issueDate)
        {
            Id = id;
            IssueDate = issueDate;
        }
        public Ticket(Guid id, DateTimeOffset issueDate, DateTimeOffset? compostDate, DateTimeOffset? endOfValidityDate) : this(id, issueDate)
        {
            _compostDate = compostDate;
            EndOfValidityDate = endOfValidityDate;
        }


        public static Ticket New(DateTimeOffset issueDate)
        {
            return new Ticket(Guid.NewGuid(), issueDate);
        }

        public bool IsComposted => _compostDate.HasValue;

        public bool IsValid(DateTimeOffset dateOfCheck) => IsComposted && DetermineValidityDate(_compostDate!.Value) > dateOfCheck;

        public void Compost(DateTimeOffset compostDate)
        {
            if (!IsValid(compostDate))
                throw new ExpiredTicketException(Id);

            _compostDate = compostDate;
            EndOfValidityDate = DetermineValidityDate(compostDate);
        }

        private DateTimeOffset DetermineValidityDate(DateTimeOffset baseDate) => baseDate.AddHours(2);
    }
}