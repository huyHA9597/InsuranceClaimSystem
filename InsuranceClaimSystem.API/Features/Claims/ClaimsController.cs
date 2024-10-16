using Ardalis.Result.AspNetCore;
using InsuranceClaimSystem.API.Features.Claims.CreateClaim;
using InsuranceClaimSystem.API.Features.Claims.GetAllClaims;
using InsuranceClaimSystem.API.Features.Claims.GetAllClaimsByStatus;
using InsuranceClaimSystem.API.Features.Claims.UpdateClaimStatus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace InsuranceClaimSystem.API.Features.Claims
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateClaimRequest request) =>
            (await _mediator.Send(request)).ToActionResult(this);

        [HttpGet]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<ClaimResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ClaimResponse>>> Get() =>
        (await _mediator.Send(new GetAllClaimsRequest())).ToActionResult(this);

        [HttpGet("{status:alpha}")]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<ClaimResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ClaimResponse>>> GetByStatus(string status) =>
            (await _mediator.Send(new GetAllClaimsByStatusRequest(status))).ToActionResult(this);

        [HttpPut]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Update([FromBody] UpdateClaimStatusRequest request) =>
            (await _mediator.Send(request)).ToActionResult(this);
    }
}
