using lscCommon.configLang.commandContract.Shared;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// Extensions for string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Replace arguments in message by argument values
        /// </summary>
        /// <param name="msgText"></param>
        /// <param name="args"></param>
        public static string FillArgs(this string msgText, List<MessageArgs> args)
        {
            // Check if message is null or empty
            if (string.IsNullOrEmpty(msgText)) return msgText;

            // Replace all argument names with argument values
            foreach (var arg in args)
                while (msgText.IndexOf(arg.ArgName, StringComparison.OrdinalIgnoreCase) >= 0)
                    msgText = Regex.Replace(msgText, arg.ArgName, arg.ArgValue, RegexOptions.IgnoreCase);
            return msgText;
        }
    }
}