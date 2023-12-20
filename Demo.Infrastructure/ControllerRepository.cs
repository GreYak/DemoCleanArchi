using Transport;
using Transport.Repository;

namespace Demo.Infrastructure
{
    public class ControllerRepository : IControllerRepository
    {
        public Task<Controller?> GetControllerByIdAsync(Guid controllerId)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Controller controller)
        {
            throw new NotImplementedException();
        }
    }
}
