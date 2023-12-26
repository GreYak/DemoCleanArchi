using Demo.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Shop;
using Transport;
using Transport.Repositories;

namespace Demo.Infrastructure
{
    public class ControllerRepository : IControllerRepository
    {
        private readonly DemoDbContext _dbContext = new DemoDbContext();

        /// <inheritdoc/>
        public async Task<Controller?> GetControllerByIdAsync(Guid controllerId)
        {
            var controllerDb = await _dbContext.Controllers.AsNoTracking().SingleOrDefaultAsync(c => c.Id == controllerId);
            return controllerDb?.ToTransportDomain();
        }

        /// <inheritdoc/>
        public async Task SaveAsync(Controller controller)
        {
            ArgumentNullException.ThrowIfNull(controller);

            if (controller.FraudsterIds.Count > 0)
            {
                var controllerDb = await _dbContext.Controllers.SingleAsync(c => c.Id == controller.Id);
                controllerDb.Fraudsters = _dbContext.Users.Where(u => controller.FraudsterIds.Contains(u.Id));
            }

            if (controller.ControlledTickets.Count > 0)
            {
                await _dbContext.Tickets
                    .Where(t => controller.ControlledTickets.ContainsKey(t.Id))
                    .ForEachAsync(t => t.ControlDate = controller.HasControlled(t.Id));
            }

            _dbContext.SaveChanges();
        }
    }
}
