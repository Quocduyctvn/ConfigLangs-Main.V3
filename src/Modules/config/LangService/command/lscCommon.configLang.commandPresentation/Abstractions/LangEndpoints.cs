
using lscCommon.configLang.commandPresentation.APIs;
using lscCommon.configLang.commandPresentation.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace lscCommon.configLang.commandPresentation.Abstractions
{
	/// <summary>
	/// Contain configurations for Lang endpoints.
	/// </summary>
	public static class LangEndpoints
	{
		/// <summary>
		/// Map lang endpoints
		/// </summary>
		/// <param name="app"></param>
		public static void MapLangEndpoints(this IEndpointRouteBuilder app)
		{
			var group = app.MapGroup(RouterConstants.LANG_ROUTE_MINIMAL);
			group.MapPost("", LangApiV1.CreateLangV1).MapToApiVersion(1);
			group.MapPut("{id}", LangApiV1.UpdateLangV1).MapToApiVersion(1);
		}
	}
}