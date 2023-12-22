using Transport.Exceptions;

namespace Transport
{
    /// <summary>
    /// Represents a ticket of transport.
    /// </summary>
    public class Ticket
    {
        public Guid Id { get; }
        public DateTimeOffset IssueDate { get; }
        public DateTimeOffset? CompostDate { get; private set; }
        public DateTimeOffset? EndOfValidityDate { get; private set; }

        /// <summary>
        /// Initialize a new instance of <see cref="Ticket"/>
        /// </summary>
        /// <param name="id">The ticket identifier</param>
        /// <param name="issueDate">The isuue date of the ticket</param>
        public Ticket(Guid id, DateTimeOffset issueDate)
        {
            Id = id;
            IssueDate = issueDate;
        }
        /// <summary>
        /// Initialize a new instance of <see cref="Ticket"/>
        /// </summary>
        /// <param name="id">The ticket identifier</param>
        /// <param name="issueDate">The isuue date of the ticket</param>
        /// <param name="compostDate">The compost date</param>
        /// <param name="endOfValidityDate">The end of validity of the ticket</param>
        public Ticket(Guid id, DateTimeOffset issueDate, DateTimeOffset? compostDate, DateTimeOffset? endOfValidityDate) : this(id, issueDate)
        {
            CompostDate = compostDate;
            EndOfValidityDate = endOfValidityDate;
        }

        /// <summary>
        /// Determine if the ticket is valid or not.
        /// </summary>
        /// <param name="dateOfCheck">The date of checking of validity</param>
        /// <returns>True if ticket is composted and not expired, else false</returns>
        public bool IsValid(DateTimeOffset dateOfCheck) => CompostDate.HasValue && DetermineValidityDate(CompostDate!.Value) > dateOfCheck;

        /// <summary>
        /// Compost the ticket and activate its using.
        /// </summary>
        /// <param name="compostDate"></param>
        /// <exception cref="ExpiredTicketException"></exception>
        public void Compost(DateTimeOffset compostDate)
        {
            if (!IsValid(compostDate))
                throw new ExpiredTicketException(Id);

            CompostDate = compostDate;
            EndOfValidityDate = DetermineValidityDate(compostDate);
        }

        private DateTimeOffset DetermineValidityDate(DateTimeOffset baseDate) => baseDate.AddHours(2);
    }
}