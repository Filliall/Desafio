using AutoMapper;
using DebtManagement.Application.Commands;
using DebtManagement.Application.Dtos;
using DebtManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DebtManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DebtsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDebt([FromBody] CreateDebtCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetDebt), new { debtNumber = result.DebtNumber }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDebts([FromQuery] DateTime? referenceDate)
        {
            var query = new GetDebtsQuery { ReferenceDate = referenceDate };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{debtNumber}")]
        public async Task<IActionResult> GetDebt(string debtNumber)
        {
            var query = new GetDebtQuery { DebtNumber = debtNumber };
            var debt = await _mediator.Send(query);

            if (debt == null) return NotFound();

            return Ok(_mapper.Map<DebtDto>(debt));
        }
    }
}
