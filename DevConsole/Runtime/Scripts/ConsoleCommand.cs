using UnityEngine;

namespace DevConsole
{
    [CreateAssetMenu(fileName = "New Command", menuName = "Command")]
    public class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField] private string command = string.Empty;
        [SerializeField] private string manual = string.Empty;
        [SerializeField] private CommandEvent commandEvent;
    
        public string Command => command;
        public string Manual => manual;
        public CommandEvent Event => commandEvent;

        public void Process(string[] args)
        {
            Event.Invoke(args);
        }
    }
}