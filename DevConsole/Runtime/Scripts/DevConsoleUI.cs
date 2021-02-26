using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.InputSystem;
using static System.String;

namespace DevConsole
{
    public class DevConsoleUI : Singleton<DevConsoleUI>
    {
        private TextMeshProUGUI _output;
        private TMP_InputField _input;
        private List<string> _history;

        public DevConsoleUI()
        {
            _history = new List<string>();
            DevConsole.RegisterCommands();
        }

        private void Start()
        {
            _output = GetComponentInChildren<TextMeshProUGUI>();
            _input = GetComponentInChildren<TMP_InputField>();
        }

        private void Update()
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame) ExecuteCommand(_input.text);
        }

        public void Log(string message) => _output.text += $"{message}\n";
        public void LogWarning(string message) => _output.text += $"<color=yellow>{message}</color>\n";
        public void LogError(string message) => _output.text += $"<color=red>{message}</color>\n";

        private void ExecuteCommand(string input)
        {
            var (command, args) = ParseCommand(input);
            _history.Add(input);
            Log($"> {input}");
            DevConsole.ExecuteCommand(command, args);
            _input.text = Empty;
            _input.ActivateInputField();
        }

        private static (string, string[]) ParseCommand(string input)
        {
            var inputSplit = input.Split(' ');
            var command = inputSplit[0];
            var args = inputSplit.Skip(1).ToArray();
            return (command, args);
        }
    }
}