using System;
using UnityEngine;

namespace DevConsole
{
    public static class TestCommands
    {
        [Command("hello")]
        public static void Hello() => DevConsoleUI.Instance.Log("hello");

        [Command("add")]
        public static void Add(int a, int b) => DevConsoleUI.Instance.Log($"{a} + {b} = {a + b}");

        [Command("roll")]
        public static void Roll(string dice)
        {
            var args = dice.Split('d');
            try
            {
                var amount = Int32.Parse(args[0]);
                var faces = Int32.Parse(args[1]);
                var result = 0;
                for (var i = 0; i != amount; i++)
                {
                    result += UnityEngine.Random.Range(0, faces) + 1;
                }
                DevConsoleUI.Instance.Log(result.ToString());
            }
            catch (Exception e)
            {
                DevConsoleUI.Instance.Log("Invalid argument");
            }
        }
    }
}