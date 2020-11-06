namespace DevConsole
{
    public class ConsoleMessage
    {
        public string Message { get; }
        public MessageType Type { get; }

        public ConsoleMessage(string message, MessageType type)
        {
            Message = message;
            Type = type;
        }
    }

    public enum MessageType
    {
        Log,
        Warning,
        Error
    }
}