using lscCommon.configLang.commandContract.Validators;
using lscCommon.configLang.commandDomain.Abstractions.Aggregates;
using lscCommon.configLang.commandDomain.Constants;
using System.Text.RegularExpressions;

namespace lscCommon.configLang.commandDomain.Entities
{
	public class Lang : AggregateRoot<string>
	{
		/// <summary>
		/// Description of Lang
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Name of notice by code VN
		/// </summary>
		public string Vn { get; set; }

		/// <summary>
		/// Name of notice by code EN
		/// </summary>
		public string? En { get; set; }

		/// <summary>
		/// Update value of lang, if data provide is null, get old data.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="description"></param>
		/// <param name="vn"></param>
		/// <param name="en"></param>
		public void Update(string? id = null, string? description = null, string? vn = null, string? en = null)
		{
			Id = id ?? Id;
			Description = description ?? Description;
			Vn = vn ?? Vn;
			En = en ?? En;
		}

		public override void Validate()
		{
			// Create validator for this lang
			var validator = Validator.Create(this);
			// Create validator rule for Id that Id must be valid key
			validator.RuleFor(x => x.Id).MustBeValidKey().NotNullOrEmpty().MaxLength(LangConstant.ID_MAX_LENGTH).Must(CheckVietnamese, LangConstant.INVALID_ID_PROPERTY_MESSAGE); ;

			// Create validator rule for Id that Id must smaller LangConstant.ID_MAX_LENGTH
			validator.RuleFor(x => x.Description).MaxLength(LangConstant.DESCRIPTION_MAX_LENGTH);

			// Create validator rule for Vn that Vn NotNullOrEmpty And must smaller LangConstant.VN_MAX_LENGTH
			validator.RuleFor(x => x.Vn).NotNullOrEmpty().MaxLength(LangConstant.VN_MAX_LENGTH);

			// Create validator rule for En that En must smaller LangConstant.EN_MAX_LENGTH
			validator.RuleFor(x => x.En).MaxLength(LangConstant.EN_MAX_LENGTH);

			// Validate all rules of this lang
			validator.Validate();
		}

		/// <summary>
		/// Checks if the Id string contains any Vietnamese characters.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool CheckVietnamese(string id)
		{
			return !Regex.IsMatch(id, @"[\u00C0-\u1EF9]");
		}
	}
}
