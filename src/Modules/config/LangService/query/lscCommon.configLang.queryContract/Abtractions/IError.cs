using lscCommon.configLang.queryContract.Enumerations;
using lscCommon.configLang.queryContract.Errors;
using System.Text.Json.Serialization;

namespace lscCommon.configLang.queryContract.Abtractions
{
    /// <summary>
    /// Represent application error
    /// </summary>
    [JsonDerivedType(typeof(StackTraceError))]
    [JsonDerivedType(typeof(Error))]
    public interface IError
    {
        /// <summary>
        /// Indicate which type of error
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ErrorType Type { get; }

        /// <summary>
        /// Code of error
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Use for providing more information
        /// </summary>
        public List<string>? Details { get; }
    }
}
