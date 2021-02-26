using UnityEngine;
using UnityEngine.InputSystem;

namespace DevConsole
{
    public class Toggle : MonoBehaviour
    {
        [SerializeField] private GameObject devConsole;

        private void Update()
        {
            if (Keyboard.current.backquoteKey.wasPressedThisFrame)
                devConsole.SetActive(!devConsole.activeSelf);
        }
    }
}
