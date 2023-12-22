namespace Transport
{

    /// <summary>
    /// Represents a controller in transport
    /// </summary>
    public class Controller
    {
        public Guid Id { get; }
        private readonly Dictionary<Guid, DateTimeOffset> _controlledTickets;
        private readonly List<Guid> _fraudster;

        /// <summary>
        /// New instance of <see cref="Controller"/>
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="tickets">The <see cref="Ticket"/>s composing the book</param>
        public Controller(Guid id, IEnumerable<Guid> fraudster, Dictionary<Guid, DateTimeOffset> controlledTickets)
        {
            Id = id;
            _fraudster = new List<Guid>(fraudster ?? Array.Empty<Guid>());
            _controlledTickets = controlledTickets is null ? new Dictionary<Guid, DateTimeOffset>() : new Dictionary<Guid, DateTimeOffset>(controlledTickets);
        }

        /// <summary>
        /// Compost of a user's ticket.
        /// </summary>
        /// <param name="ticketId">The composted ticket identifier</param>
        /// <param name="controlDate">The date of control</param>
        public void Compost(Guid ticketId, DateTimeOffset controlDate)
        {
            if (!_controlledTickets.ContainsKey(ticketId))
                _controlledTickets.Add(ticketId, controlDate);
        }

        /// <summary>
        /// Get the date of a control.
        /// </summary>
        /// <param name="ticketId">The identifier of the controlled ticket.</param>
        /// <returns>The date of the control is controlled, else null.</returns>
        public DateTimeOffset? HasControlled(Guid ticketId) => _controlledTickets.TryGetValue(ticketId, out DateTimeOffset controlDate) ? controlDate : null;

        /// <summary>
        /// Notice a user as fraudster
        /// </summary>
        /// <param name="id">The user identifier</param>
        internal void NoticeFraudster(Guid id)
        {
            _fraudster.Add(id);
        }

        /// <summary>
        /// The fraudsters Ids.
        /// </summary>
        public IReadOnlyList<Guid> FraudsterIds => _fraudster.AsReadOnly();

        /// <summary>
        /// The controlled tickets and their dates.
        /// </summary>
        public IReadOnlyDictionary<Guid, DateTimeOffset> ControlledTickets => _controlledTickets.AsReadOnly();
    }
}
