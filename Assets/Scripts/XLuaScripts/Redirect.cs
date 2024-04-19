using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

/// <summary>
/// 重定向读取lua代码
/// </summary>
public class Redirect : MonoBehaviour
{
    void Start()
    {
        LuaEnv env = new LuaEnv();
        
        env.AddLoader(MyCustomLoader);

        env.DoString("require('Second')");
    }

    public byte[] MyCustomLoader(ref string filepath)
    {
        //自定义路径
        string path=Application.dataPath+"/Scripts"+"/LuaScripts/"+filepath+".lua";
        Debug.Log(path);
        
        //先判断是否包含这个文件
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);//将指定路径所有内容返回一个字节数组
        }
        else
        {
            Debug.Log($"重定向失败，文件名为{filepath}");
        }
        return null;
    }
}
