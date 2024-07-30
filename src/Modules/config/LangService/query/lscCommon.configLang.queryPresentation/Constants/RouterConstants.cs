namespace lscCommon.configLang.queryPresentation.Constants
{
	/// <summary>
	/// Class contains route constants used for API routing.
	/// </summary>
	public static class RouterConstants
	{
		public const string LANG_ROUTE = "api/v{v:apiVersion}/langs/";
		public const string LANG_ROUTE_MINIMAL = "/minimal/langs/";
		public const string LANG_ROUTE_MINIMAL_GET_ALL = "/minimal/langs/get-all";
		public const string GET_ALL = "get-all";
	}
}
