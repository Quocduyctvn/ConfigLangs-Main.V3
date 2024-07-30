using lscCommon.configLang.commandPresentation.Abstractions;
using lscCommon.configLang.commandPresentation.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserCases;

namespace lscCommon.configLang.commandPresentation.APIs
{
	/// <summary>
	/// API version 1 for Lang operations.
	/// </summary>
	public class LangApiV1 : ApplicationApi
	{
		/// <summary>
		/// API endpoint for creating a Lang using minimal API.
		/// </summary>
		/// <param name="command">Request to create a Lang.</param>
		/// <param name="mediator">Mediator for handling the request.</param>
		/// <returns>Result of the action.</returns>
		public static async Task<IResult> CreateLangV1([FromBody] CreateLangCommand command,
													   IMediator mediator)
		{
			var result = await mediator.Send(command);
			return TypedResults.Ok(result);
		}

		/// <summary>
		/// API endpoint for updating a Lang using minimal API.
		/// </summary>
		/// <param name="id">Identifier of the Lang to update.</param>
		/// <param name="request">Update request containing new Lang data.</param>
		/// <param name="mediator">Mediator for handling the request.</param>
		/// <returns>Result of the action.</returns>
		public static async Task<IResult> UpdateLangV1(string id,
													   [FromBody] UpdateLangRequestDTO request,
													   IMediator mediator)
		{
			var command = new UpdateLangCommand
			{
				Id = id,
				Description = request.Description,
				Vn = request.Vn,
				En = request.En
			};

			var result = await mediator.Send(command);
			return TypedResults.Ok(result);
		}
	}
}