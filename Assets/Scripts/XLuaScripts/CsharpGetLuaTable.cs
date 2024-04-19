using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XLua;

/// <summary>
/// 定义一个映像类，用来接收lua传过来的表
/// </summary>
class CustomClass
{
    public int testInt;
    public bool testBool;
    public string testString;
    public UnityAction testFun;
}

/// <summary>
/// 定义一个映像接口，用来接收lua传过来的表，一定要记得声明为public！！！！
/// </summary>
[CSharpCallLua]
public interface ICustominterface
{
    int testInt { get; set; }
    bool testBool { get; set; }
    string testString { get; set; }
    UnityAction testFun { get; set; }
}

/// <summary>
/// lua中所有表的映射
/// </summary>
public class CsharpGetLuaTable : MonoBehaviour
{
    void Start()
    {
        //初始化
        LuaManager.Instance.Init();
        LuaManager.Instance.DoingLuaFile("Main");

        #region List和Dictionary的映射
        Debug.LogWarning("***********int类型的list*************");
        List<int> list = LuaManager.Instance.Global.Get<List<int>>("list1");
        foreach (var item in list)
        {
            Debug.Log(item);
        }

        Debug.LogWarning("************object类型的list************");
        List<object> list2 = LuaManager.Instance.Global.Get<List<object>>("list2");
        foreach (var item in list2)
        {
            Debug.Log(item);
        }
        
        
        Debug.LogWarning("***********int类型的Dic*************");
        Dictionary<string,int> dic = LuaManager.Instance.Global.Get<Dictionary<string,int>>("dic1");
        foreach (var item in dic.Keys)
        {
            Debug.Log(item+"_"+dic[item]);
        }

        Debug.LogWarning("************object类型的Dic************");
        Dictionary<object,object> dic2 = LuaManager.Instance.Global.Get<Dictionary<object,object>>("dic2");

        foreach (var item in dic2.Keys)
        {
            Debug.Log(item+"_"+dic2[item]);
        }

        
        //两个都是值拷贝（浅拷贝），不会修改lua表中的值

        #endregion

        #region lua映像到C#类（值拷贝）

        Debug.LogWarning("******lua映像到C#类*****");
        CustomClass customclass = LuaManager.Instance.Global.Get<CustomClass>("myluaclass");
        
        Debug.Log(customclass.testInt);
        Debug.Log(customclass.testBool);
        Debug.Log(customclass.testString);
        customclass.testFun();

        #endregion

        #region lua映像到C#接口（引用拷贝）
        Debug.LogWarning("******lua映像到C#接口*****");
        ICustominterface icustominterface = LuaManager.Instance.Global.Get<ICustominterface>("myluainterface");
        
        Debug.Log(icustominterface.testInt);
        Debug.Log(icustominterface.testBool);
        Debug.Log(icustominterface.testString);
        icustominterface.testFun();

        Debug.LogWarning("************执行修改操作*************");
        icustominterface.testInt = 500;
        Debug.Log(icustominterface.testInt);
        
        #endregion
        
        #region luatable

        Debug.LogWarning("********lua表映射luatable*********");
        //这里之前获取之前生成的lua映射类的表
        LuaTable luatable = LuaManager.Instance.Global.Get<LuaTable>("myluaclass");
        Debug.Log(luatable.Get<int>("testInt"));
        Debug.Log(luatable.Get<bool>("testBool"));
        Debug.Log(luatable.Get<string>("testString"));
        luatable.Get<LuaFunction>("testFun").Call();

        Debug.LogWarning("******修改后的值*********");
        luatable.Set("testInt",600);
        Debug.Log(luatable.Get<int>("testInt"));


        #endregion

    }
    
}

