using UnityEngine.Events;

namespace DevConsole
{
    [System.Serializable]
    public class CommandEvent : UnityEvent<string[]> {}
}