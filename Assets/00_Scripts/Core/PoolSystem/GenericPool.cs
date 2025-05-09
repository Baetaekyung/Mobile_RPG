using UnityEngine;

public class GenericPool<T> : PoolBase<T>, IPool where T : IPoolable, new()
{
    public GenericPool(int initialSize, int poolMaxSize = 200) 
        : base(initialSize, poolMaxSize)
    {
        InitializePool(initialSize);
    }

    IPoolable IPool.GetInstance()
    {
        return GetInstance();
    }

    void IPool.ReturnInstance(IPoolable poolable)
    {
        ReturnInstance(poolable);
    }

    protected override T CreateInstance()
    {
        if (IsValidToCreate() == false)
            return default(T);

        T newInstance = new T();
        _currentPoolSize++;

        return newInstance;
    }
}
