using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/PoolDefaultData")]
public class BasePoolDataSO : ScriptableObject
{
    [SerializeField] private int initialSize;
    [SerializeField] private int maxSize = 200;

    public int GetInitialSize => initialSize;
    public int GetMaxSize => maxSize;
}
