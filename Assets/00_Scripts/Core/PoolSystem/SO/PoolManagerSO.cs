using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/PoolManager")]
public class PoolManagerSO : ScriptableObject
{
    private Dictionary<Type, IPool> _poolDictionary = new(); 

    public void InitializePool<T>(BasePoolDataSO data) 
        where T : IPoolable, new()
    {
        GenericPool<T> newPool = new GenericPool<T>(data.GetInitialSize, data.GetMaxSize);

        _poolDictionary.Add(typeof(T), newPool); 
    }

    public void InitializePool<T>(GameObjectPoolDataSO data)
        where T : MonoBehaviour, IPoolable
    {
        MonoGenericPool<T> newPool = 
            new MonoGenericPool<T>(data.GetGameObject, data.GetInitialSize, data.GetMaxSize);

        _poolDictionary.Add(typeof(T), newPool);
    }

    public IPool GetPool<T>()
        where T : IPoolable
    {
        if(_poolDictionary.TryGetValue(typeof(T), out var pool))
            return pool;

        Debug.LogWarning($"Ǯ�� �ʱ�ȭ���� ����!, Ÿ�� �̸�: [{typeof(T).Name}]");

        return default(IPool);
    }

    public T GetInstance<T>() where T : IPoolable
    {
        IPool pool = GetPool<T>();

        T instance = (T)pool.GetInstance();

        return instance;
    }

    public void ReturnInstance<T>(T obj) where T : IPoolable
    {
        IPool pool = GetPool<T>();

        pool.ReturnInstance(obj);
    }
}
