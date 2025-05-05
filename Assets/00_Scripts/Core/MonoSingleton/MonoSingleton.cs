using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static bool isShuttingDown = false;

    [SerializeField] private ESingletonType      singletonType;
    [SerializeField] private ESingletonDebugType debugType;

    public static T Inst
    {
        get
        {
            if(isShuttingDown)
            {
                Debug.LogWarning($"게임이 종료되었다. Type: {typeof(T).Name}");
                return default(T);
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
            Debug.LogWarning($"[Two singleton] singleton name: {typeof(T).Name}");

            Destroy(gameObject);
            return;
        }

        gameObject.name = $"[Singleton] {typeof(T).Name}";

        if(singletonType == ESingletonType.DONTDESTROY)
            DontDestroyOnLoad(gameObject);

        if(debugType == ESingletonDebugType.DISALLOW_DEBUG)
            gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
    }

    private void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
            isShuttingDown = true;
    }

    public enum ESingletonType
    {
        DEFAULT,
        DONTDESTROY,
    }

    public enum ESingletonDebugType
    {
        ALLOW_DEBUG,
        DISALLOW_DEBUG
    }
}
