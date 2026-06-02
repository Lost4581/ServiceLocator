using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : IService
{
    private readonly Dictionary<Type, object> _services = new();

    public ServiceLocator(IFadeService fadeService, ISoundPlayer soundPlayer, ISaver saver)
    {
        Register(fadeService);
        Register(soundPlayer);
        Register(saver);
    }

    private void Register(object service)
    {
        _services[service.GetType()] = service;
    }

    public T GetService<T>() where T : class
    {
        if (_services.TryGetValue(typeof(T), out var direct))
            return direct as T;

        foreach (var service in _services.Values)
        {
            if (service is T match)
                return match;
        }

        Debug.LogError($"Сервис {typeof(T).Name} не найден!");
        return null;
    }
}