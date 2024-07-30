using lscCommon.configLang.queryPresentation.APIs;
using lscCommon.configLang.queryPresentation.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;



namespace lscCommon.configLang.queryPresentation.Abstractions
{
	/// <summary>
	/// Contain configurations for lang endpoints.
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
			group.MapGet("{id}", LangApiV1.GetLangByIdV1).MapToApiVersion(1);
			group.MapGet(RouterConstants.GET_ALL, LangApiV1.GetLangsV1).MapToApiVersion(1);
		}
	}
}
