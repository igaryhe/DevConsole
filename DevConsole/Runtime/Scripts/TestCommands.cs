namespace DevConsole
{
    public class TestCommands
    {
        [Command("hello")]
        public static void Hello() => DevConsoleUI.Instance.Log("hello");

        [Command("add")]
        public static void Add(int a, int b) => DevConsoleUI.Instance.Log($"{a} + {b} = {a + b}");
    }
}