using Asp.Versioning;
using lscCommon.configLang.queryPresentation.Abstractions;
using lscCommon.configLang.queryPresentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserCases;

namespace lscCommon.configLang.queryPresentation.Controllers.v1
{
	/// <summary>
	/// Controller version 1 for lang apis
	/// </summary>
	[ApiVersion(1)]
	[Route(RouterConstants.LANG_ROUTE)]
	public class LangController : ApiController
	{
		private readonly IMediator mediator;

		public LangController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		/// <summary>
		/// Api version 1 for get lang by id
		/// </summary>
		/// <param name="id">ID of lang</param>	
		/// <returns>Action result with lang as data</returns>
		[MapToApiVersion(1)]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetLangByIdV1(string id)
		{
			var query = new GetLangByIdQuery(id);
			var result = await mediator.Send(query);
			return Ok(result);
		}

		/// <summary>
		/// Api version 1 for get all langs
		/// </summary>
		/// <returns>Action result with list of Langs as data</returns>
		[MapToApiVersion(1)]
		[HttpGet(RouterConstants.GET_ALL)]
		public async Task<IActionResult> GetAllLangsV1()
		{
			var query = new GetAllLangsQuery();
			var result = await mediator.Send(query);
			return Ok(result);
		}
	}
}