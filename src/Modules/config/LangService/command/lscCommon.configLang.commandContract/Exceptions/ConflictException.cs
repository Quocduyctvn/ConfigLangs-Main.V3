namespace lscCommon.configLang.commandContract.Exceptions
{
    /// <summary>
    /// Provide conflict exception
    /// </summary>
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message)
        {
        }
    }
}
