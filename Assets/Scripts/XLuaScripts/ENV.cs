using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// Lua 解析器
/// </summary>
public class ENV : MonoBehaviour
{
    void Start()
    {
        //创建一个lua解析器
        LuaEnv env = new LuaEnv();

        //使用lua解析器输出
        env.DoString("print('你好世界')");

        env.DoString("require('FirstLua')");
        
        //env.Tick();
        
        //env.Dispose();
    }
}
