using System.IO;
using UnityEngine;
using XLua;

/// <summary>
/// Lua管理器，需要保证唯一性，继承没有MonoBehaviour的单例基类
/// </summary>
public class LuaManager : BaseSingletonWithoutMono<LuaManager>
{
    private LuaEnv env;


    /// <summary>
    /// 返回Lua中的 _G 表
    /// </summary>
    public LuaTable Global
    {
        get
        {
            return env.Global;
        }
    }


    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //如果已经初始化就直接返回
        if (env!=null)
            return;
        env = new LuaEnv();
        
        //env.AddLoader(MyCustomLoader);
        //env.AddLoader(MyCustomABLoader);//从AB包中加载Lua文件，日常开发中不常使用，只有最后导出项目再启用
        
        env.AddLoader(PackageLoader);
        //env.AddLoader(MyCustomABLoader);//从AB包中加载
    }

    
    
    /// <summary>
    /// 外部传入文件名即可，无需重新传入require函数
    /// </summary>
    /// <param name="fileName">lua文件名</param>
    public void DoingLuaFile(string fileName)
    {
        Debug.Log("正在执行"+fileName);
        string str = string.Format("require('{0}')", fileName);
        DoString(str);
    }
    
    /// <summary>
    /// lua文件重定向本地指定文件夹
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public byte[] MyCustomLoader(ref string fileName)
    {
        //自定义的路径
        string path=Application.dataPath+"/Scripts"+"/LuaScripts/"+fileName+".lua";
        //Debug.Log(path);
        
        //先判断是否包含这个文件
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);//将指定路径所有内容返回一个字节数组
        }
        else
        {
            Debug.Log($"重定向失败，文件名为{fileName}");
        }
        return null;
    }

    /// <summary>
    /// 背包项目的lua重定向本地指定文件夹
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public byte[] PackageLoader(ref string fileName)
    {
        //背包系统lua脚本的自定义的路径
        //string path=Application.dataPath+"/PackageDemo"+"/Lua/"+fileName+".lua";
        string path=Application.dataPath+"/Hotfix/"+fileName+".lua";//hotfix热更新脚本位置定义。，
        
        //Debug.Log(path);
        
        //先判断是否包含这个文件
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);//将指定路径所有内容返回一个字节数组
        }
        else
        {
            Debug.Log($"重定向失败，文件名为{fileName}");
        }
        return null;
    }
    

    /// <summary>
    /// 从AB包中加载lua文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public byte[] MyCustomABLoader(ref string fileName)
    {

        #region 不使用abmanager

        // Debug.Log("不使用ABmanager读取包中的lua成功！");
        // //加载AB包
        // string path = Application.streamingAssetsPath +"/lua";//后面加上装文件的小ab包的名字
        // AssetBundle abpack = AssetBundle.LoadFromFile(path);
        //
        // //加载Lua文件
        // TextAsset tx = abpack.LoadAsset<TextAsset>(fileName+".lua");//因为文件名是xxx.lua.txt，所以这里的.lua属于名字的一部分
        //
        // //最后返回lua文件中的byte数组
        // return tx.bytes;//提供了获取所有的byte的数组的方法

        #endregion
        
        #region 使用ABmanager
        Debug.Log("使用ABmanager读取包中的lua成功！");
        TextAsset lua=ABmanager.Instance.LoadRes<TextAsset>("lua",fileName+".lua");
        if (lua != null)
            return lua.bytes;
        else
        {
            Debug.LogError("MyCustomABLoader加载失败，文件名为："+fileName);
        }
        return null;
        #endregion
    }

    
    /// <summary>
    /// 执行需要执行的lua语言
    /// </summary>
    /// <param name="str">lua文件名</param>
    public void DoString(string str)
    {
        if (env==null)
        {
            Debug.LogError("解析器未初始化！不能执行DoString操作");
            return;
        }
        env.DoString(str);
    }

    /// <summary>
    /// lua垃圾回收
    /// </summary>
    public void Tick()
    {
        if (env==null)
        {
            Debug.LogError("解析器未初始化！不能执行Tick操作");
            return;
        }
        env.Tick();
    }
    
    /// <summary>
    /// 销毁解析器
    /// </summary>
    public void Dispose()
    {
        if (env==null)
        {
            Debug.LogError("解析器未初始化！不能执行Dispose操作");
            return;
        }
        env.Dispose();
        env = null;
    }
    
}
