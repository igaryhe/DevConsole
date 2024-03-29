﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevConsole
{
    internal static class DevConsole
    {
        private static Dictionary<string, MethodInfo> methodInfoCache;
        internal static void RegisterCommands()
        {
            methodInfoCache = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.IsDynamic)
                .SelectMany(assembly => assembly.GetExportedTypes())
                .SelectMany(type => type.GetMethods(BindingFlags.Static | BindingFlags.Public))
                .Where(method => method.GetCustomAttribute<CommandAttribute>() != null)
                .ToDictionary(method => method.GetCustomAttribute<CommandAttribute>().Alias ?? method.Name,
                    StringComparer.OrdinalIgnoreCase);
        }

        public static void ExecuteCommand(string methodName, params string[] args)
        {
            if (methodInfoCache == null || !methodInfoCache.ContainsKey(methodName))
            {
                DevConsoleUI.LogError($"{methodName}: Not registered.");
                return;
            }
            var methodInfo = methodInfoCache[methodName];
            var parametersInfo = methodInfo.GetParameters();
            if (parametersInfo.Length != args.Length)
            {
                DevConsoleUI.LogError($"{methodName}: Requires {parametersInfo.Length} args, while {args.Length} were provided.");
                return;
            }
            var parameters = new object[parametersInfo.Length];
            try
            {
                for (var i = 0; i < args.Length; i++)
                    parameters[i] = Convert.ChangeType(args[i], parametersInfo[i].ParameterType,
                        System.Globalization.CultureInfo.InvariantCulture);
                methodInfo.Invoke(null, parameters);
            }
            catch (FormatException)
            {
                DevConsoleUI.LogError($"{methodName}: Parameters type error.");
            }
        }
    }
}