using System.Collections.Generic;
using UnityEngine;

public abstract class PoolBase<T> where T : IPoolable
{
    protected Queue<T> _pool;

    protected int _poolMaxSize;
    protected int _currentPoolSize = 0;

    public PoolBase(int initialSize, int poolMaxSize = 200)
    {
        _poolMaxSize = poolMaxSize;
        _currentPoolSize = 0;

        InitializePool(initialSize);
    }

    protected virtual void InitializePool(int initialSize)
    {
        _pool = new Queue<T>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            T instance = CreateInstance();
            _pool.Enqueue(instance);
        }
    }

    public virtual T GetInstance()
    {
        if(_pool.Count > 0)
        {
            T instance = _pool.Dequeue();
            instance.OnPop();

            return instance;
        }

        T newInstance = CreateInstance();
        newInstance.OnPop();
        Debug.Log("풀이 가득차 새로운 객체를 생성하고 풀 사이즈를 늘렸습니다.");

        return newInstance;
    }

    public virtual void ReturnToPool(T obj)
    {
        Debug.Assert(obj != null, $"NULL인 객체를 풀에 넣으려함, 풀 [{typeof(T).Name}]");

        obj.OnPush();

        _pool.Enqueue(obj);
    }

    protected abstract T CreateInstance();

    protected virtual bool IsValidToCreate()
    {
        if (_poolMaxSize >= _currentPoolSize)
        {
            Debug.Log("풀 크기가 최대 크기를 초과하여 추가할 수 없음, 풀 최대크기를 다시 설정하세요.");
            Debug.Log($"풀 [{typeof(T).Name}]");

            return false;
        }

        return true;
    }
}
