using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    public static bool IsAwakened { get; private set; }
    public static bool IsStarted { get; private set; }
    public static bool IsDestroyed { get; private set; }

    static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                if (IsDestroyed) return null;

                _instance = FindExistingInstance() ?? CreateNewInstance();
            }
            return _instance;
        }
    }

    private static T FindExistingInstance()
    {
        T[] existingInstances = FindObjectsOfType<T>();

        // No instance found
        if (existingInstances == null || existingInstances.Length == 0)
            return null;

        return existingInstances[0];
    }

    private static T CreateNewInstance()
    {
        var containerGO = new GameObject("__" + typeof(T).Name + " (Singleton)");
        return containerGO.AddComponent<T>();
    }

    #region Singleton Life-time Management
    protected virtual void SingletonAwakened() { }
    protected virtual void SingletonStarted() { }
    protected virtual void SingletonDestroyed() { }
    protected virtual void NotifyInstanceRepeated()
    {
        Destroy(GetComponent<T>());
    }
    #endregion

    #region Unity3d Messages - DO NOT OVERRRIDE / IMPLEMENT THESE METHODS in child classes!
    void Awake()
    {
        T thisInstance = GetComponent<T>();

        // Initialize the singleton if the script is already in the scene in a GameObject
        if (_instance == null)
        {
            _instance = thisInstance;
            DontDestroyOnLoad(_instance.gameObject);
        }

        else if (thisInstance != _instance)
        {
            Debug.LogWarning(string.Format(
                "Found a duplicated instance of a Singleton with type {0} in the GameObject {1}",
                GetType(), gameObject.name));

            NotifyInstanceRepeated();
            return;
        }


        if (!IsAwakened)
        {
            SingletonAwakened();
            IsAwakened = true;
        }

    }

    void Start()
    {
        // do not start it twice
        if (IsStarted)
            return;
        SingletonStarted();
        IsStarted = true;
    }

    void OnDestroy()
    {
        // Here we are dealing with a duplicate so we don't need to shut the singleton down
        if (this != _instance)
            return;
        IsDestroyed = true;
        SingletonDestroyed();
    }

    #endregion
}
