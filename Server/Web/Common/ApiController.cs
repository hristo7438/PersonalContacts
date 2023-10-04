using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Common
{
	[ApiController]
	[Route("[controller]")]
	public abstract class ApiController : ControllerBase
	{
		protected readonly IMediator _mediator;

		public ApiController(IMediator mediator)
		{
			this._mediator = mediator;
		}

		public const string Id = "{id}";

		protected Task<ActionResult<TResult>> Send<TResult>(IRequest<TResult> request)
			=> this._mediator.Send(request).ToActionResult();

		protected Task<ActionResult> Send(IRequest<Result> request)
			=> this._mediator.Send(request).ToActionResult();

		protected Task<ActionResult<TResult>> Send<TResult>(IRequest<Result<TResult>> request)
			=> this._mediator.Send(request).ToActionResult();
	}
}