using lscCommon.configLang.queryDomain.Abstractions.Repositories;
using lscCommon.configLang.queryDomain.Entities;

namespace lscCommon.configLang.queryPersistence.Repositories
{
	/// <summary>
	/// Implementation of ILangRepository
	/// </summary>
	public class LangRepository : GenericRepository<Lang, string>, ILangRepository
	{
		/// <summary>
		/// Implementation of ILangRepository
		/// </summary>
		public LangRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}