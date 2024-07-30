using Asp.Versioning;
using lscCommon.configLang.commandPresentation.Abstractions;
using lscCommon.configLang.commandPresentation.Constants;
using lscCommon.configLang.commandPresentation.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserCases;

namespace lscCommon.configLang.commandPresentation.Controllers.v1
{
	/// <summary>
	/// Controller version 1 for Lang APIs.
	/// </summary>
	[ApiVersion(1)]  // Specify API version
	[Route(RouterConstants.LANG_ROUTE)]
	public class LangController : ApiController
	{
		private readonly IMediator mediator;

		public LangController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		/// <summary>
		/// API endpoint for creating a Lang (version 1).
		/// </summary>
		/// <param name="command">Request to create a Lang.</param>
		/// <returns>Action result.</returns>
		[MapToApiVersion(1)]
		[HttpPost]
		public async Task<IActionResult> CreateLangV1(CreateLangCommand command)
		{
			var result = await mediator.Send(command);
			return Ok(result);
		}

		/// <summary>
		/// Api version 1 for update lang
		/// </summary>
		/// <param name="id">Id of lang need to be updated</param>
		/// <param name="request">Request body contains content to update</param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> UpdateLangV1(string id, [FromBody] UpdateLangRequestDTO request)
		{
			var command = new UpdateLangCommand
			{
				Id = id,
				Description = request.Description,
				Vn = request.Vn,
				En = request.En
			};
			var result = await mediator.Send(command);
			return Ok(result);
		}

	}
}
