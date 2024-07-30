using lscCommon.configLang.commandDomain.Abstractions.Entities;

namespace lscCommon.configLang.commandDomain.Abstractions.Aggregates
{
    /// <summary>
    /// Aggregate root
    /// </summary>
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
    {
    }
}
