using UnityEngine;

public class GenericPool<T> : PoolBase<T> where T : IPoolable, new()
{
    public GenericPool(int initialSize, int poolMaxSize = 200) 
        : base(initialSize, poolMaxSize) { }

    protected override T CreateInstance()
    {
        if (IsValidToCreate() == false)
            return default(T);

        T newInstance = new T();
        _currentPoolSize++;

        return newInstance;
    }
}
