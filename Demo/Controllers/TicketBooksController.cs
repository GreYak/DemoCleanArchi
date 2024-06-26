﻿using Demo.Application.Abstraction;
using Demo.Application.Dtos.Commands;
using Demo.Application.Exceptions;
using Demo.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketBooksController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IShoppingApplicationService _shoppingService;
        private readonly IExecutionContext _executionContext;

        public TicketBooksController(ILogger<UsersController> logger, IShoppingApplicationService shoppingService)
        {
            _logger = logger;
            _shoppingService = shoppingService;
        }

        [HttpPost, Route("{ticketBookId}/buy-ticketbook")]
        public async Task<IActionResult> UserBuyBookTicket(Guid ticketBookId, [FromBody] IEnumerable<Guid> tickets)
        {
            try
            {
                await _shoppingService.AddingTicketBookInStoreAsync(new CreateTicketBookCommand(ticketBookId, tickets), _executionContext.ReferenceDateTime);
                return NoContent();
            }
            catch (Exception ex) when (ex is AlreadyExistException || ex is DomainException || ex is InvalidParamException)
            {
                return BadRequest();
            }
        }
    }
}
