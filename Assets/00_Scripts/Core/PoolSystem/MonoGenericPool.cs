using UnityEngine;

public class MonoGenericPool<T> : PoolBase<T>, IPool where T : MonoBehaviour, IPoolable
{
    private GameObject _prefab;

    public MonoGenericPool(GameObject prefab, int initialSize, int poolMaxSize = 200) 
        : base(initialSize, poolMaxSize) 
    {
        _prefab = prefab;
        Debug.Log(_prefab);

        InitializePool(initialSize);
    }

    protected override T CreateInstance()
    {
        if (IsValidToCreate() == false)
            return default(T);

        GameObject instance = Object.Instantiate(_prefab);
        //instance.hideFlags = HideFlags.HideInHierarchy;

        Debug.Assert(instance.GetComponent<T>() != null, 
            $"�߸��� Ÿ�� ĳ����, {instance.GetType()} to {typeof(T).Name}");

        return instance as T;
    }

    IPoolable IPool.GetInstance()
    {
        return GetInstance();
    }

    void IPool.ReturnInstance(IPoolable poolable)
    {
        ReturnInstance(poolable);
    }
}
