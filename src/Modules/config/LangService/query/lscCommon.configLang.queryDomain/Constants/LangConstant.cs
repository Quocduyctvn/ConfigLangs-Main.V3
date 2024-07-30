using lscCommon.configLang.queryDomain.Entities;

namespace lscCommon.configLang.queryDomain.Constants
{
	/// <summary>
	/// Contain constants for Lang
	/// </summary>
	public class LangConstant
	{
		/// <summary>
		/// Error message indicating that the Id must not include Vietnamese characters.
		/// </summary>
		public static readonly string ID_CANNOT_INCLUDE_VIETNAMESE_CHARACTERS_MESSAGE = $"{nameof(Lang.Id)} must not include Vietnamese characters.";

		/// <summary>
		/// Error message indicating that the Lang with the specified Id was not found.
		/// </summary>
		public static readonly string INVALID_ID_IS_NOTFOUND_MESSAGE = $"Lang with the specified {nameof(Lang.Id)} was not found.";

	}
}