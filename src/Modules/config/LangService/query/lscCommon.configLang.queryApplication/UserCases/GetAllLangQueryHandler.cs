using lscCommon.configLang.queryContract.Shared;
using lscCommon.configLang.queryDomain.Abstractions.Repositories;
using lscCommon.configLang.queryDomain.Entities;
using MediatR;
using Entities = lscCommon.configLang.queryDomain.Entities;

namespace UserCases
{
	/// <summary>
	/// Request to get all Lang
	/// </summary>
	public class GetAllLangsQuery : IRequest<Result<List<Entities.Lang>>>
	{
	}

	/// <summary>
	/// Handler for get all lang request
	/// </summary>
	public class GetAllLangQueryHandler : IRequestHandler<GetAllLangsQuery, Result<List<Lang>>>
	{
		private readonly ILangRepository langRepository;

		/// <summary>
		/// Handler for get all lang request
		/// </summary>
		public GetAllLangQueryHandler(ILangRepository langRepository)
		{
			this.langRepository = langRepository;
		}

		/// <summary>
		/// Handle request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<Result<List<Lang>>> Handle(GetAllLangsQuery request, CancellationToken cancellationToken)
		{
			{
				var langs = langRepository.FindAll().ToList();
				return await Task.FromResult(langs);
			}
		}
	}
}