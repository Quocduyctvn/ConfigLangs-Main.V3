using lscCommon.configLang.commandDomain.Abstractions.Repositories;
using lscCommon.configLang.commandDomain.Entities;

namespace lscCommon.configLang.commandPersistence.Repositories
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
