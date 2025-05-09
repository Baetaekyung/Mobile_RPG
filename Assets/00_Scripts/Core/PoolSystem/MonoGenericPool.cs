using UnityEngine;

public class MonoGenericPool<T> : PoolBase<T> where T : MonoBehaviour, IPoolable
{
    private T _prefab;

    public MonoGenericPool(T prefab, int initialSize, int poolMaxSize = 200) 
        : base(initialSize, poolMaxSize) 
    {
        _prefab = prefab;
    }

    protected override T CreateInstance()
    {
        if (IsValidToCreate() == false)
            return default(T);

        T instance = Object.Instantiate(_prefab);

        return instance;
    }
}
