namespace DevConsole
{
    public interface IConsoleCommand
    {
        string Command { get; }
        CommandEvent Event { get; }
        void Process(string[] args);
    }
}