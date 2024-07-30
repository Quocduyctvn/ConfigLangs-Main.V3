using lscCommon.configLang.queryDomain.Abstractions.Entities;

namespace lscCommon.configLang.queryDomain.Abstractions.Aggregates
{
    /// <summary>
    /// Aggregate root
    /// </summary>
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
    {
    }
}
