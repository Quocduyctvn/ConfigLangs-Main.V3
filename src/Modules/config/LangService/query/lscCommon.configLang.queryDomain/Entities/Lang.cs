using lscCommon.configLang.queryContract.Validators;
using lscCommon.configLang.queryDomain.Abstractions.Aggregates;

namespace lscCommon.configLang.queryDomain.Entities
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

		public override void Validate()
		{
			// Create validator for this lang
			var validator = Validator.Create(this);
			// Create validator rule for Id that Id must be valid key
			validator.RuleFor(x => x.Id).MustBeValidKey();
			// Validate all rules of this lang
			validator.Validate();
		}
	}
}
