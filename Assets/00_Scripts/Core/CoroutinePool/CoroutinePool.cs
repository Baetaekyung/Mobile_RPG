using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinePool : MonoSingleton<CoroutinePool>
{
    private Dictionary<string, Func<IEnumerator>> _cachedCoroutines = new();
    private Dictionary<string, Coroutine> _activeCoroutines = new();

    public void RegisterRoutine(string key, Func<IEnumerator> routineFactory)
    {
        _cachedCoroutines[key] = routineFactory;
    }

    public void StartRegisterRoutine(string key, Action callback = null)
    {
        if (_cachedCoroutines.TryGetValue(key, out var routineFactory))
            StartRoutine(key, routineFactory(), callback);
        else
            Debug.LogWarning($"등록되지 않은 코루틴을 실행하려함, 실행이름: {key}");
    }

    public void StartRoutine(string key, IEnumerator routine, Action callback = null)
    {
        StopRoutine(key);
        Coroutine coroutine = StartCoroutine(RunWithCallback(key, routine, callback));
        _activeCoroutines[key] = coroutine;
    }

    private IEnumerator RunWithCallback(string key, IEnumerator routine, Action callback)
    {
        yield return routine;

        _activeCoroutines.Remove(key);
        callback?.Invoke();
    }

    public void StopRoutine(string key)
    {
        if (_activeCoroutines.TryGetValue(key, out var activeRoutine))
        {
            StopCoroutine(activeRoutine);
            _activeCoroutines.Remove(key);
        }
    }

    public void StopAllRoutines()
    {
        foreach (var routine in _activeCoroutines.Values)
        {
            StopCoroutine(routine);
        }

        _activeCoroutines.Clear();
    }
}
