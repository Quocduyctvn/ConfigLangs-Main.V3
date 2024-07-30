using lscCommon.configLang.queryPresentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using UserCases;

namespace lscCommon.configLang.queryPresentation.APIs
{
	/// <summary>
	/// Api version 1 of lang
	/// </summary>
	public class LangApiV1 : ApplicationApi
	{
		/// <summary>
		/// Api version 1 for get lang by id, use for minimal API
		/// </summary>
		/// <param name="id">ID of lang</param>
		/// <param name="mediator">Mediator to mediate request</param>
		/// <returns>Action result with lang as data</returns>
		public static async Task<IResult> GetLangByIdV1(string id, IMediator mediator)
		{
			var query = new GetLangByIdQuery(id);
			var result = await mediator.Send(query);
			return TypedResults.Ok(result);
		}

		/// <summary>
		/// Api version 1 for get all lang, use for minimal API
		/// </summary>
		/// <param name="mediator">Mediator to mediate request</param>
		/// <returns>Action result with list lang as data</returns>
		public static async Task<IResult> GetLangsV1(IMediator mediator)
		{
			var query = new GetAllLangsQuery();
			var result = await mediator.Send(query);
			return TypedResults.Ok(result);
		}
	}
}