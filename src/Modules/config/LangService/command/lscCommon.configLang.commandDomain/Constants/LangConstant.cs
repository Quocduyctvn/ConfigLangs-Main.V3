using lscCommon.configLang.commandDomain.Entities;

namespace lscCommon.configLang.commandDomain.Constants
{
	/// <summary>
	/// Contain constants for Lang
	/// </summary>
	public class LangConstant
	{
		/// <summary>
		/// The maximum length allowed for the Id property.
		/// </summary>
		public const int ID_MAX_LENGTH = 64;

		/// <summary>
		/// The maximum length allowed for the Description property.
		/// </summary>
		public const int DESCRIPTION_MAX_LENGTH = 255;

		/// <summary>
		/// The maximum length allowed for the Vietnamese (Vn) property.
		/// </summary>
		public const int VN_MAX_LENGTH = 255;

		/// <summary>
		/// The maximum length allowed for the English (En) property.
		/// </summary>
		public const int EN_MAX_LENGTH = 255;

		/// <summary>
		/// Error message indicating that an Id already exists.
		/// </summary>
		public static readonly string IS_EXIST = $"{nameof(Lang.Id)} already exists.";

		/// <summary>
		/// Error message indicating that the Id must not include Vietnamese characters.
		/// </summary>
		public static readonly string INVALID_ID_PROPERTY_MESSAGE = $"{nameof(Lang.Id)} must not include Vietnamese characters.";

		// <summary>
		/// Error message indicating that the Id must not be null or empty.
		/// </summary>
		public static readonly string INVALID_ID_NOTNULL_OR_EMPTY_MESSAGE = $"{nameof(Lang.Id)} must not be null or empty.";

		// <summary>
		/// Error message indicating that the Vn must not be null or empty.
		/// </summary>
		public static readonly string INVALID_VN_NOT_EMPTY_MESSAGE = $"{nameof(Lang.Vn)} must not be empty.";
	}
}