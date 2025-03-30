using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public enum SingletonType
    {
        DEFAULT,
        DONTDESTROY,
    }

    public enum SingletonDebugType
    {
        ALLOW_DEBUG,
        DISALLOW_DEBUG
    }

    private static T _instance;

    private static bool isShuttingDown = false;

    [SerializeField] private SingletonType      singletonType;
    [SerializeField] private SingletonDebugType debugType;

    public static T Instance
    {
        get
        {
            if(isShuttingDown)
            {
                Debug.LogWarning($"인스턴스를 발견 할 수 없음. Type: {typeof(T).Name}");
                return default;
            }

            if(_instance == null)
            {
                _instance = FindAnyObjectByType<T>();

                if(_instance == null)
                {
                    GameObject newInstance = new GameObject($"[RuntimeSingleton] {typeof(T).Name}");
                    _instance = newInstance.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }

        gameObject.name = $"[Singleton] {typeof(T).Name}";

        if(singletonType == SingletonType.DONTDESTROY)
            DontDestroyOnLoad(gameObject);
        if(debugType == SingletonDebugType.DISALLOW_DEBUG)
            gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInHierarchy;
    }

    private void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            isShuttingDown = true;
        }
    }
}
