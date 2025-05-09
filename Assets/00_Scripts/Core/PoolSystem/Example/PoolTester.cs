using UnityEngine;

public class PoolTester : MonoBehaviour
{
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private GameObjectPoolDataSO poolData;

    private PoolObj _poolObj;

    private void Start()
    {
        poolManager.InitializePool<PoolObj>(poolData);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            _poolObj = poolManager.GetInstance<PoolObj>();
            _poolObj.Test();
        }
        else if(Input.GetKeyDown(KeyCode.M))
        {
            if(_poolObj != null)
            {
                poolManager.ReturnInstance(_poolObj);
            }    
        }
    }
}
