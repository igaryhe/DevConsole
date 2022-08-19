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
        private Canvas canvas;
        private TMP_InputField input;
        private List<string> history;
        private int backlogIdx;

        public bool isOpened => canvas.enabled;
        public bool justClosed; // a dirty hack

        public DevConsoleUI()
        {
            history = new List<string>();
            DevConsole.RegisterCommands();
        }

        protected override void Awake()
        {
            output = GetComponentInChildren<TextMeshProUGUI>();
            input = GetComponentInChildren<TMP_InputField>();
            canvas = GetComponent<Canvas>();
            input.caretWidth = 12;
            input.onValueChanged.AddListener(delegate{ OnValueChanged(); });
            input.onSubmit.AddListener(delegate{ ExecuteCommand(input.text); });
        }

        public void Toggle()
        {
            canvas.enabled = !canvas.enabled;
            if (canvas.enabled)
            {
                input.ActivateInputField();
            }
            else
            {
                input.DeactivateInputField();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        private void Hide()
        {
            input.text = "";
            input.DeactivateInputField();
            EventSystem.current.SetSelectedGameObject(null);
            canvas.enabled = false;
            justClosed = true;
        }

        private void OnValueChanged()
        {
            if (input.text.Length == 0 || input.text.Last() != '`') return;
            if (input.text == "`")
            {
                Hide();
                return;
            }
            input.text = input.text[..^1];
            ExecuteCommand(input.text);
            Hide();
        }

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

        public static void Log(string message) => Instance.output.text += $"{message}\n";
        public static void LogWarning(string message) => Instance.output.text += $"<color=yellow>{message}</color>\n";
        public static void LogError(string message) => Instance.output.text += $"<color=red>{message}</color>\n";

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