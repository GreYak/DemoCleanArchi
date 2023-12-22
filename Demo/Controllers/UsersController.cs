using Demo.Application.Abstraction;
using Demo.Application.Exceptions;
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

        public UsersController(ILogger<UsersController> logger, IShoppingApplicationService shoppingService, ITransportApplicationService transportService)
        {
            _logger = logger;
            _shoppingService = shoppingService;
            _transportService = transportService;
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
                await _transportService.UserTakesTransportAsync(userId, DateTimeOffset.Now);
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
