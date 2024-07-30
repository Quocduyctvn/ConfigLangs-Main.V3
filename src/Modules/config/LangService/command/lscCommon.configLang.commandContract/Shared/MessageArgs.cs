namespace lscCommon.configLang.commandContract.Shared
{
    /// <summary>
    /// Define message arguments contain argument name and argument value
    /// </summary>
    public class MessageArgs
    {
        public string ArgName { set; get; }
        public string ArgValue { set; get; }

        public MessageArgs(string Name, object Value)
        {
            ArgName = Name;
            ArgValue = string.Format("{0}", Value);
        }
    }
}
