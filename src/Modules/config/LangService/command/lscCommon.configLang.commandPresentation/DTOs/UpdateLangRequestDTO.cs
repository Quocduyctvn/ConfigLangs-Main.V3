namespace lscCommon.configLang.commandPresentation.DTOs
{
	/// <summary>
	/// DTO for request incoming from end user
	/// </summary>
	public class UpdateLangRequestDTO
	{
		public string? Description { get; set; }
		public string? Vn { get; set; }
		public string? En { get; set; }
	}
}
