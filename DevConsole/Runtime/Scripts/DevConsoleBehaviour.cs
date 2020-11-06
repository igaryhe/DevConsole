using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.String;

namespace DevConsole
{
    public class DevConsoleBehaviour : Singleton<DevConsoleBehaviour>
    {
        [SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];
        [Header("UI")]
        [SerializeField] private GameObject console;
        [SerializeField] private TextMeshProUGUI output;
        [SerializeField] private TMP_InputField input;
        private DevConsole _devConsole;
        private DevConsole DevConsole
        {
            get
            {
                if (_devConsole != null) return _devConsole;
                return _devConsole = new DevConsole(commands);
            }
        }
    
        public void Toggle(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            console.SetActive(!console.activeSelf);
        }

        public void Log(ConsoleMessage message)
        {
            Log(message.Message, message.Type);
        }

        public void Log(string message, MessageType type)
        {
            switch (type)
            {
                case MessageType.Log:
                    output.text += message + "\n";
                    break;
                case MessageType.Error:
                    output.text += "<color=red>" + message + "</color>" + "\n";
                    break;
                case MessageType.Warning:
                    output.text += "<color=yellow>" + message + "</color>" + "\n";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void ProcessCommand(string inputString)
        {
            Log("> " + inputString, MessageType.Log);
            DevConsole.ProcessCommand(inputString);
            input.text = Empty;
            input.ActivateInputField();
        }
    }
}