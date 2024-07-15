using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIContainer 
{
    private readonly DIContainer parentContainer;
    private readonly Dictionary<(string, Type), DIRegistration> registrations = new();
    private HashSet<(string, Type)> resolutions = new();

    public DIContainer(DIContainer parentContainer = null)
    {
        this.parentContainer = parentContainer;
    }

    public void RegisterSingleton<T>(Func<DIContainer, T> factory)
    {
        RegisterSingleton(null, factory);
    }
    public void RegisterSingleton<T>(string tag, Func<DIContainer, T> factory)
    {
        var key = (tag, typeof(T));
        Register(key, factory, true);
    }
    public void RegisterTransient<T>(Func<DIContainer, T> factory)
    {
        RegisterTransient(null, factory);
    }
    public void RegisterTransient<T>(string tag, Func<DIContainer, T> factory)
    {
        var key = (tag, typeof(T));
        Register(key, factory, false);
    }
    public void RegisterInstance<T>(T instance)
    {
        RegisterInstance(null, instance);
    }
    public void RegisterInstance<T>(string tag, T instance)
    {
        var key = (tag, typeof(T));

        if (registrations.ContainsKey(key))
        {
            throw new Exception($"Factory ({key.Item1} - {key.Item2}) already registered$");
        }

        registrations[key] = new DIRegistration
        {
            IsSingleton = true,
            Instance = instance
        };
    }
    public T Resolve<T>(string tag = null)
    {
        var key = (tag, typeof (T));

        if (resolutions.Contains(key))
        {
            throw new Exception($"Cyclic dependency for ({tag} - {typeof(T)})");
        }

        resolutions.Add(key);

        try
        {
            if (registrations.TryGetValue(key, out var registration))
            {
                if (registration.IsSingleton)
                {
                    if (registration.Instance == null && registration.Factory != null)
                    {
                        registration.Instance = registration.Factory(this);
                    }

                    return (T)registration.Instance;
                }

                return (T)registration.Factory(this);
            }

            if (parentContainer != null)
            {
                parentContainer.Resolve<T>(tag);
            }
        }
        finally
        {
            registrations.Remove(key);
        }

        throw new Exception($"Couldn't find dependency ({tag} - {typeof(T)}");
    }
    public void Register<T>((string, Type) key, Func<DIContainer, T> factory, bool isSingleton)
    {
        if (registrations.ContainsKey(key))
        {
            throw new Exception($"Factory ({key.Item1} - {key.Item2}) already registered$");
        }

        registrations[key] = new DIRegistration
        {
            Factory = c => factory(c),
            IsSingleton = isSingleton
        };
    }
}
