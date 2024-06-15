using System;
using System.Collections.Generic;

namespace Core.Scripts.DI
{
    public class DIContainer
    {
        private readonly DIContainer m_parent;
        private readonly Dictionary<(string, Type), DIRegistartion> m_registrations = new();
        private readonly HashSet<(string, Type)> m_resultions = new();
        public DIContainer(DIContainer parent = null)
        {
            m_parent = parent;
        }
        /// <summary>
        /// Регистрация синглтона
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        public void RegisterSingleton<T>(Func<DIContainer, T> factory)
        {
            RegisterSingleton(null, factory);
        }
        /// <summary>
        /// Регистрация синглтона
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        public void RegisterSingleton<T>(string tag, Func<DIContainer, T> factory)
        {
            Register((tag, typeof(T)), factory, true);
        }
        /// <summary>
        /// Регистрация фабрики
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        public void RegisterTransient<T>(Func<DIContainer, T> factory)
        {
            RegisterTransient(null, factory);
        }
        /// <summary>
        /// Регистрация фабрики
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        public void RegisterTransient<T>(string tag, Func<DIContainer, T> factory)
        {
            Register((tag, typeof(T)), factory, false);
        }
        /// <summary>
        /// Регистрация инстанса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        public void RegisterInstance<T>(T instance)
        {
            RegisterInstance(null, instance);
        }
        /// <summary>
        /// Регистрация инстанса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        public void RegisterInstance<T>(string tag, T instance)
        {
            var key = (tag, typeof(T));
            if (m_registrations.ContainsKey(key)) throw new Exception($"DI: Already has factory entry for type: {key.Item2.FullName} or tag: {key.Item1}");
            m_registrations[key] = new DIRegistartion
            {
                Instance = instance,
                IsSingleton = true
            };
        }
        public T Resolve<T>(string tag = null)
        {
            var key = (tag, typeof(T));
            if(m_resultions.Contains(key))
            {
                throw new Exception($"Cycling dependency for tag {key.tag}, and type {key.Item2.FullName}");
            }
            m_resultions.Add(key);
            try
            {
                if (m_registrations.TryGetValue(key, out DIRegistartion registartion))
                {
                    if (registartion.IsSingleton)
                    {
                        if (registartion.Instance == null && registartion.Factory != null)
                        {
                            registartion.Instance = registartion.Factory(this);
                        }
                        return (T)registartion.Instance;
                    }

                    return (T)registartion.Factory(this);
                }
                if (m_parent != null)
                {
                    return m_parent.Resolve<T>(tag);
                }
            }
            finally
            {
                m_resultions.Remove(key);
            }

            throw new Exception($"There is no factory registered for key: {key}");
        }
        /// <summary>
        /// Регистрация синглтона или фабрики
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        private void Register<T>((string, Type) key, Func<DIContainer, T> factory, bool isSingleton)
        {
            if (m_registrations.ContainsKey(key)) throw new Exception($"DI: Already has factory entry for type: {key.Item2.FullName} or tag: {key.Item1}");
            m_registrations[key] = new DIRegistartion
            {
                Factory = _ => factory(_),
                IsSingleton = isSingleton
            };
        }
    }
}