using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DevConsole
{
    public class DevConsoleUI : Singleton<DevConsoleUI>
    {
        private TextMeshProUGUI output;
        private TMP_InputField input;
        private List<string> history;
        private int backlogIdx;

        public DevConsoleUI()
        {
            history = new List<string>();
            DevConsole.RegisterCommands();
        }

        private void Awake()
        {
            output = GetComponentInChildren<TextMeshProUGUI>();
            input = GetComponentInChildren<TMP_InputField>();
        }

        private void OnEnable()
        {
            input.ActivateInputField();
            input.caretWidth = 12;
        }

        private void OnDisable()
        {
            input.DeactivateInputField();
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void Execute() => ExecuteCommand(input.text);

        public void Up()
        {
            if (backlogIdx < 0) return;
            input.text = history[backlogIdx];
            if (backlogIdx > 0) backlogIdx--;
            input.caretPosition = input.text.Length;
        }

        public void Down()
        {
            if (backlogIdx > history.Count - 1) return;
            input.text = history[backlogIdx];
            if (backlogIdx < history.Count - 1) backlogIdx++;
            input.caretPosition = input.text.Length;
        }

        public void Log(string message) => output.text += $"{message}\n";
        public void LogWarning(string message) => output.text += $"<color=yellow>{message}</color>\n";
        public void LogError(string message) => output.text += $"<color=red>{message}</color>\n";

        private void ExecuteCommand(string cmd)
        {
            var (command, args) = ParseCommand(cmd);
            history.Add(cmd);
            backlogIdx = history.Count - 1;
            Log($"> {cmd}");
            DevConsole.ExecuteCommand(command, args);
            input.text = "";
            input.ActivateInputField();
        }

        private static (string, string[]) ParseCommand(string input)
        {
            var split = input.Split(' ');
            var command = split[0];
            var args = split.Skip(1).ToArray();
            return (command, args);
        }
    }
}