using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Fields
    private static T s_instance;
    private static readonly object Lock = new object();
    private static bool s_applicationIsQuitting = false;

    private bool _isPurged = false;

    #endregion

    #region Properties

    public static T Instance
    {
        get
        {
            if (s_applicationIsQuitting)
            {
                lock (Lock)
                {
                    return s_instance ? s_instance : null;
                }
            }

            lock (Lock)
            {
                if (s_instance == null)
                {
                    LazyInitialize();
                }
            }

            lock (Lock)
            {
                return s_instance;
            }
        }
    }

    #endregion

    #region Unity Events

    protected virtual bool Awake()
    {
        return Init();
    }

    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed,
    ///     it will create a buggy ghost object that will stay on the Editor scene
    ///     even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (!_isPurged)
        {
            s_applicationIsQuitting = true;
        }
    }

    #endregion

    #region Private Methods

    protected virtual bool Init()
    {
        if (s_instance == null)
        {
            LazyInitialize();
        }
        else
        {
            if (s_instance == this)
            {
                return true;
            }

            Destroy(this.gameObject);
            return false;
        }

        return true;
    }

    private static void LazyInitialize()
    {
        var type = typeof(T);

        var instances = (T[])FindObjectsOfType(type);
        switch (instances.Length)
        {
            case 0:
                var go = new GameObject($"[{type}]");
                s_instance = go.AddComponent<T>();
                DontDestroyOnLoad(go);
                break;
            //case > 1:
            //    Debug.LogError("There is more than 1 instance of this class in scene view , please check it");
            //    s_instance = instances[0];
            //    DontDestroyOnLoad(s_instance.gameObject);
            //    return;
            default:
                s_instance = instances[0];
                DontDestroyOnLoad(s_instance.gameObject);
                break;
        }
    }
    #endregion
}
