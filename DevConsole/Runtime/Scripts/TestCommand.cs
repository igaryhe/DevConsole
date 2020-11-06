using DevConsole;
using UnityEngine;

public class TestCommand : MonoBehaviour
{
    public void Log(string[] args)
    {
        DevConsoleBehaviour.Instance.Log(new ConsoleMessage(string.Join(" ", args), MessageType.Log));
    }
}
