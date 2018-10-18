using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance != null) return instance;

            instance = FindObjectOfType<T>();
            if (instance != null) return instance;

            GameObject obj = new GameObject { name = typeof(T).Name };
            instance = obj.AddComponent<T>();

            return instance;
        }
    }

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}