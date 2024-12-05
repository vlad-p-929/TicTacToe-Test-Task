using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        public static T GetOrCreateService<T>() where T : new()
        {
            if (!GetService<T>(out var service))
            {
                service = CreateService<T>();
            }

            return service;
        }

        public static T CreateService<T>() where T : new()
        {
            var type = typeof(T);
            if (Services.ContainsKey(type))
            {
                throw new InvalidOperationException($"Service of type {type} is already registered.");
            }

            Services[type] = new T();

            return (T)Services[type];
        }
        
        public static void RegisterService<T>(T service)
        {
            var type = typeof(T);
            if (Services.ContainsKey(type))
            {
                throw new InvalidOperationException($"Service of type {type} is already registered.");
            }

            Services[type] = service;
        }

        public static bool GetService<T>(out T neededService)
        {
            neededService = default;

            if (Services.TryGetValue(typeof(T), out var service))
            {
                neededService = (T)service;
                return true;
            }

            return false;
        }

        public static void Reset()
        {
            Services.Clear();
        }
    }
}