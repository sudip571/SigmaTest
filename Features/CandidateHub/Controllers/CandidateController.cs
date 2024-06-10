using CandidateHub.Attributes;
using CandidateHub.Features.Candidates.Commands;
using CandidateHub.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sigma.Shared.Controllers;
using Swashbuckle.AspNetCore.Annotations;
using Sigma.Shared.Responses; 

namespace CandidateHub.Controllers
{
    [ApiExplorerSettings(GroupName = SwaggerHelper.V1.CandidateHubAPIGroup)]
    [CandidateHubRoute("")]
    public class CandidateController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CandidateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(
         Summary = "Create or update  candidate.",
         Description = "Create or update  candidate.",
         OperationId = "Candidate.CreateAndUpdateCandidate",
         Tags = new[] { "Sigma - Candidate" })]
        public async Task<IActionResult> CreateAndUpdateCandidate([FromBody] CreateAndUpdateCandidate.Command command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return response.HttpResponses();
        }
    }
}
