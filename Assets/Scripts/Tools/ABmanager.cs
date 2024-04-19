using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// AB包管理器
/// </summary>
public class ABmanager : BaseSingletonWtihMonoAuto<ABmanager>
{
    //主包
    private AssetBundle mainAB = null;
    
    //依赖包获取用的配置文件
    private AssetBundleManifest manifest = null;
    
    //AB包不能重复加载，所以使用字典存储加载过的AB包
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// AB包的存放路径方便修改
    /// </summary>
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    /// <summary>
    /// 拼接最终主包名方便修改
    /// </summary>
    /// <returns></returns>
    private string MainABName
    {
        get
        {
        #if UNITY_IOS
            return "IOS";
        #elif UNITY_ANDROID
            return "Android";
        #else
            return "PC";
        #endif 
        }
    }

    /// <summary>
    /// Load all dependencies package
    /// </summary>
    /// <param name="abName"></param>
    public void LoadAllDependencies(string abName)
    {
        //加载AB包
        if (mainAB==null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //获取依赖包的信息
        AssetBundle ab = null;
        string[] strs = manifest.GetAllDependencies(abName);
        foreach (var package in strs)
        {
            if (!abDic.ContainsKey(package))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + package);
                abDic.Add(package, ab);
            }
        }
        //加载所有资源来源包,如果没有加载过就马上加载
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }

    
    /// <summary>
    /// 同步加载，遇到包中有同名文件的时候无法分清
    /// </summary>
    /// <param name="abName">资源包名</param>
    /// <param name="resName">资源文件名</param>
    public Object LoadRes(string abName,string resName)
    {
        //加载所需AB包
        LoadAllDependencies(abName);
        // //加载资源(三种方式)
        // return abDic[abName].LoadAsset(resName);
        
        //优化:为了外部方便使用，在加载资源时先判断一下 资源是不是GameObject
        //如果是 直接实例化后再返回给外部
        Object obj = abDic[abName].LoadAsset(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    /// <summary>
    /// 按照类型查找，同步加载。这种方法在后面的XLua会用得很多
    /// </summary>
    /// <param name="abName">资源包名</param>
    /// <param name="resName">资源文件名</param>
    /// <param name="type">限定类型</param>
    /// <returns></returns>
    public Object LoadRes(string abName, string resName, System.Type type)
    {
        //加载所需AB包
        LoadAllDependencies(abName);
        
        Object obj = abDic[abName].LoadAsset(resName,type);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    /// <summary>
    /// 泛型同步加载
    /// </summary>
    /// <param name="abName">AB包名</param>
    /// <param name="resName">资源文件名</param>
    /// <typeparam name="T">限定类型</typeparam>
    /// <returns></returns>
    public T LoadRes<T>(string abName, string resName)where T : Object
    {
        //加载所需AB包
        LoadAllDependencies(abName);
        
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }
    
    
    /// <summary>
    /// 异步加载的方法,根据名字加载
    /// </summary>
    /// <param name="abName">AB包名</param>
    /// <param name="resName">资源文件名</param>
    /// <param name="callback">委托函数</param>
    public void AsyncLoadRes(string abName, string resName,UnityAction<Object> callback)
    {
        StartCoroutine(IELoadResAsync(abName, resName, callback));
    }

    private IEnumerator IELoadResAsync(string abName, string resName,UnityAction<Object> callback)
    {
        //加载所需AB包
        LoadAllDependencies(abName);

        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        
        //异步加载结束后通过委托传递给外部
        if (abr.asset is GameObject)
            callback(Instantiate(abr.asset));
        else
            callback(abr.asset);

    }

    /// <summary>
    /// 异步加载的方法,根据Type加载
    /// </summary>
    /// <param name="abName">AB包名</param>
    /// <param name="resName">资源文件名</param>
    /// <param name="type">限定类型</param>
    /// <param name="callback">委托函数</param>
    public void AsyncLoadRes(string abName, string resName,System.Type type,UnityAction<Object> callback)
    {
        StartCoroutine(IELoadResAsync(abName, resName,type, callback));
    }

    private IEnumerator IELoadResAsync(string abName, string resName,System.Type type,UnityAction<Object> callback)
    {
        //加载所需AB包
        LoadAllDependencies(abName);

        //根据type获取对象
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName,type);
        yield return abr;
        
        //异步加载结束后通过委托传递给外部
        if (abr.asset is GameObject)
            callback(Instantiate(abr.asset));
        else
            callback(abr.asset);

    }

      /// <summary>
      /// 泛型异步加载
      /// </summary>
      /// <param name="abName">AB包名</param>
      /// <param name="resName">资源文件名</param>
      /// <param name="callback">委托函数</param>
    public void AsyncLoadRes<T>(string abName, string resName,UnityAction<T> callback)where T : Object
    {
        StartCoroutine(IELoadResAsync(abName, resName, callback));
    }

    private IEnumerator IELoadResAsync<T>(string abName, string resName,UnityAction<T> callback)where T : Object
    {
        //加载所需AB包
        LoadAllDependencies(abName);

        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        
        //异步加载结束后通过委托传递给外部
        if (abr.asset is GameObject)
            callback(Instantiate(abr.asset)as T);
        else
            callback(abr.asset as T);

    }
    

    /// <summary>
    /// 单个包的卸载
    /// </summary>
    /// <param name="abName">AB包名</param>
    public void UnLoad(string abName)
    {
        if (abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }
    
    
    /// <summary>
    /// 所有包的卸载
    /// </summary>
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}
