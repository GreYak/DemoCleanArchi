using Demo.Application.Abstraction;
using Demo.Application.Exceptions;
using Demo.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControllersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITransportApplicationService _transportService;
        private readonly IExecutionContext _executionContext;

        public ControllersController(ILogger<UsersController> logger, IShoppingApplicationService shoppingService, ITransportApplicationService transportService, IExecutionContext executionContext)
        {
            _logger = logger;
            _transportService = transportService;
            _executionContext = executionContext;
        }

        [HttpPost, Route("{controllerId}/control/{userId}")]
        public async Task<IActionResult> UserBuyBookTicket(Guid controllerId, Guid userId)
        {
            try
            {
                await _transportService.ControlUserInTransportAsync(controllerId, userId, _executionContext.ReferenceDateTime);
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
