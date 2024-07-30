using lscCommon.configLang.queryContract.DependencyInjection.Options;
using lscCommon.configLang.queryContract.Exceptions;
using lscCommon.configLang.queryContract.Shared;
using lscCommon.configLang.queryContract.Validators;
using lscCommon.configLang.queryDomain.Abstractions.Repositories;
using lscCommon.configLang.queryDomain.Constants;
using lscCommon.configLang.queryDomain.Entities;
using MediatR;

namespace UserCases
{
	/// <summary>
	/// Request to get lang by id
	/// </summary>
	public record GetLangByIdQuery(string Id) : IRequest<Result<Lang>> { }

	/// <summary>
	/// Handler for get lang by id request
	/// </summary>
	public class GetLangByIdQueryHandler : IRequestHandler<GetLangByIdQuery, Result<Lang>>
	{
		private readonly ILangRepository langRepository;

		/// <summary>
		/// Handler for get lang by id request
		/// </summary>
		public GetLangByIdQueryHandler(ILangRepository langRepository)
		{
			this.langRepository = langRepository;
		}

		/// <summary>
		/// Handle request
		/// </summary>
		/// <param name="request">Request to handle</param>
		/// <param name="cancellationToken"></param>
		/// <returns>Result with lang data</returns>
		public async Task<Result<Lang>> Handle(GetLangByIdQuery request,
												 CancellationToken cancellationToken)
		{
			var validator = Validator.Create(request);
			// Create validator rule for Id that Id must be valid key
			validator.RuleFor(x => x.Id).MustBeValidKey();
			validator.Validate();

			var findOption = new FindOption
			{
				AllowNullReturn = false,
				IsTracking = false
			};
			// Find lang without allow null return. If lang not found will throw NotFoundException
			var lang = await langRepository.FindByIdAsync(request.Id, findOption, cancellationToken);
			if (lang == null)
			{
				throw new NotFoundException(LangConstant.INVALID_ID_IS_NOTFOUND_MESSAGE);
			}
			return lang!;
		}
	}
}