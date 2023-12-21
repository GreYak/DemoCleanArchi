using Demo.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;
using Transport;
using Transport.Repository;

namespace Demo.Infrastructure
{
    public class ControllerRepository : IControllerRepository
    {
        private readonly DemoDbContext _dbContext = new DemoDbContext();

        /// <inheritdoc/>
        public async Task<Controller?> GetControllerByIdAsync(Guid controllerId)
        {
            var controllerDb = await _dbContext.Controllers.SingleOrDefaultAsync(c => c.Id == controllerId);
            return controllerDb?.ToTransportDomain();
        }

        /// <inheritdoc/>
        public Task SaveAsync(Controller controller)
        {
            throw new NotImplementedException();
        }
    }
}
