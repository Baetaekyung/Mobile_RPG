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
        Debug.Log("Ǯ�� ������ ���ο� ��ü�� �����ϰ� Ǯ ����� �÷Ƚ��ϴ�.");

        return newInstance;
    }

    public virtual void ReturnToPool(T obj)
    {
        Debug.Assert(obj != null, $"NULL�� ��ü�� Ǯ�� ��������, Ǯ [{typeof(T).Name}]");

        obj.OnPush();

        _pool.Enqueue(obj);
    }

    protected abstract T CreateInstance();

    protected virtual bool IsValidToCreate()
    {
        if (_poolMaxSize >= _currentPoolSize)
        {
            Debug.Log("Ǯ ũ�Ⱑ �ִ� ũ�⸦ �ʰ��Ͽ� �߰��� �� ����, Ǯ �ִ�ũ�⸦ �ٽ� �����ϼ���.");
            Debug.Log($"Ǯ [{typeof(T).Name}]");

            return false;
        }

        return true;
    }
}
