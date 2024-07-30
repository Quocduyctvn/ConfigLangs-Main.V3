namespace lscCommon.configLang.commandContract.DependencyInjection.Options
{
    /// <summary>
    /// Define option for finding domain entity
    /// </summary>
    public class FindOption
    {
        /// <summary>
        /// Allow entity to be tracking by database context or not
        /// </summary>
        public bool IsTracking { get; set; }

        /// <summary>
        /// Allow method to return null value or not. If not, when catching a null value will throw NotFoundException
        /// </summary>
        public bool AllowNullReturn { get; set; }
    }
}
