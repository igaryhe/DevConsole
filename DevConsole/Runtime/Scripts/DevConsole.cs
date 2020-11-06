using System.Collections.Generic;
using System.Linq;

namespace DevConsole
{
    public class DevConsole
    {
        private readonly IEnumerable<IConsoleCommand> _commands;

        public DevConsole(IEnumerable<IConsoleCommand> commands)
        {
            _commands = commands;
        }

        public void ProcessCommand(string input)
        {
            var inputSplit = input.Split(' ');
            var commandInput = inputSplit[0];
            var args = inputSplit.Skip(1).ToArray();
            ProcessCommand(commandInput, args);
        }

        private void ProcessCommand(string commandInput, string[] args)
        {
            foreach (var command in _commands)
            {
                if (!commandInput.Equals(command.Command)) continue;
                command.Process(args);
                return;
            }
        }
    }
}