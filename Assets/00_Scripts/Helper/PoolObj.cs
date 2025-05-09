using UnityEngine;

public class PoolObj : MonoBehaviour, IPoolable
{
    public void OnPop()
    {
        Debug.Log("Pop");
    }

    public void OnPush()
    {
        Debug.Log("Push");
    }

    public void Test()
    {
        Debug.Log("poool, pool");
    }
}
