using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトンMonoBehaviour
/// </summary>
/// <typeparam name="T">シングルトンにしたい型</typeparam>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// インスタンス
    /// </summary>
    private static T instance = null;

    /// <summary>
    /// インスタンス
    /// </summary>
    public static T Instance
    {
        get
        {
            if (!HasInstance())
            {
                CreateInstance();
            }
            return instance;
        }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    protected SingletonMonoBehaviour()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>オーバーライドしたら必ず呼ぶこと</remarks>
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(instance);
        }
        else if (this != instance)
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDestroy()
    {
        instance = null;
    }

    /// <summary>
    /// インスタンスの作成
    /// </summary>
    public static void CreateInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<T>();

            if (instance == null)
            {
                GameObject obj = new GameObject(typeof(T).Name);
                instance = obj.AddComponent<T>();
            }

            DontDestroyOnLoad(instance);
        }
    }

    /// <summary>
    /// インスタンスの破棄
    /// </summary>
    public static void DestroyInstance()
    {
        if (instance != null)
        {
            Destroy(instance);
            instance = null;
        }
    }

    /// <summary>
    /// インスタンスが作成されているかを取得
    /// </summary>
    /// <returns>インスタンスが作成されているか (true: いる, false: いない)</returns>
    public static bool HasInstance()
    {
        return instance != null;
    }

}
