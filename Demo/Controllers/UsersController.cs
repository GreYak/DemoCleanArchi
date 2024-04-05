using Demo.Application.Abstraction;
using Demo.Application.Exceptions;
using Demo.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IShoppingApplicationService _shoppingService;
        private readonly ITransportApplicationService _transportService;
        private readonly IExecutionContext _executionContext;

        public UsersController(ILogger<UsersController> logger, IShoppingApplicationService shoppingService, ITransportApplicationService transportService, IExecutionContext executionContext)
        {
            _logger = logger;
            _shoppingService = shoppingService;
            _transportService = transportService;
            _executionContext = executionContext;
        }

        [HttpPost, Route("{userId}/buy-ticketbook")]
        public async Task<IActionResult> UserBuyBookTicket(Guid userId, [FromBody]Guid ticketBookId)
        {
            try
            {
                await _shoppingService.UserBuyTicketBookAsync(userId, ticketBookId);
                return NoContent();
            }
            catch (Exception ex) when (ex is NotFoundException || ex is DomainException)
            {
                return BadRequest();
            }
        }

        [HttpPost, Route("{userId}/take-transport")]
        public async Task<IActionResult> UserTransport(Guid userId)
        {
            try
            {
                await _transportService.UserTakesTransportAsync(userId, _executionContext.ReferenceDateTime);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DomainException ex)
            {
                return BadRequest();
            }
        }
    }
}
