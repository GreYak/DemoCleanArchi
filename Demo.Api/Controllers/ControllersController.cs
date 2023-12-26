using Demo.Application.Abstraction;
using Demo.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControllersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITransportApplicationService _transportService;

        public ControllersController(ILogger<UsersController> logger, IShoppingApplicationService shoppingService, ITransportApplicationService transportService)
        {
            _logger = logger;
            _transportService = transportService;
        }

        [HttpPost, Route("{controllerId}/control/{userId}")]
        public async Task<IActionResult> UserBuyBookTicket(Guid controllerId, Guid userId)
        {
            try
            {
                await _transportService.ControlUserInTransportAsync(controllerId, userId, DateTimeOffset.Now);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (DomainException)
            {
                return BadRequest();
            }
        }
    }
}
