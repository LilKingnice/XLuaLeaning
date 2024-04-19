//这个代码主要用来处理lua调用C#的一些逻辑

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XLua;
using Object = UnityEngine.Object;

#region 调用类或者枚举

/// <summary>
/// 测试类（无命名空间）
/// </summary>
public class Test1
{
    public void speak(string str)
    {
        Debug.Log("Test1说："+str);
    }
}

namespace luacallcsharp
{
    public class Test2
    {
        public void speak(string str)
        {
            Debug.Log("Test2说："+str);
        }
    }
}

/// <summary>
/// 测试枚举
/// </summary>
public enum Customenmu
{
    Idle,
    Running,
    Walking,
    Talking
}

#endregion


#region 调用List、Dictionary

public class Structure
{
    public int[] array = new int[5] { 5, 6, 22, 10, 6 };
    public List<int> list = new List<int>();
    public Dictionary<int,string> dic = new Dictionary<int,string>();
}

#endregion


#region 扩展方法

[LuaCallCSharp]
public static class Tool
{
    //扩展方法固定写法this 类名 参数（相当于实例化了）
    public static void Move(this Unextend test)
    {
        Debug.Log(test.name+"移动");
    }
    
    //给lua使用的判断空的方法(写的是Object的扩展方法)
         //扩展方法必须在静态类和静态方法中
         //对应：（Lua一些特殊处理）
         public static bool IsNull(this Object obj)
         {
             // if (obj == null)
             //     return true;
             // return false;
     
             return obj == null;
         }
}

public class Unextend
{
    public string name;
    
    public void Speak(string str)
    {
        Debug.Log(str);
        
    }
    
    public static void Eat()
    {
        //Debug.Log(name+"吃东西");//不能在 static 上下文中访问非 static 字段 'name'
        Debug.Log("吃东西");
    }
}

#endregion


#region ref和out

public class RefOutClass
{
    public int RefFunc(int a, ref int b,ref int c,int d)
    {
        b = a+d;
        c = a+d;
        Debug.Log("lua传入a的值为:"+a+"lua传入d的值为"+d);
        return 100;
    }

    public int OutFunc(int a ,out int b, out int c,int d)
    {
        b = a - d;
        c = a - d;
        return 200;
    
    }

    public int RefOutFunc(int a , ref int b, out int c,int d)
    {
        b = a *d;
        c = a *d;
        return 300;
    }

    public void NoneReturnFunc(int a, ref int b, out int c, int d)
    {
        b = a *d;
        c = a *d;
    }
}

#endregion

#region 调用重载函数

public class CustomOverride
{
    public void Myreturn()
    {
        Debug.Log("无参无返回值");
    }

    public int Myreturn(int a)
    {
        return a;
    }

    public int Myreturn(int a, int b)
    {
        return a + b;
    }

    public float Myreturn(float a)
    {
        return a;
    }
}


#endregion

#region 调用委托

public class CustomDel
{
    public UnityAction luacallAction;

    public event UnityAction luacallEvent;

    public void DoEvent()
    {
        if (luacallEvent != null)
            luacallEvent();
    }

    /// <summary>
    /// 封装一个lua清除事件的方法(lua中不具备清除事件的能力)
    /// </summary>
    public void ClearEvent()
    {
        luacallEvent = null;
    }

}
#endregion


#region Lua一些特殊处理

public class LuaSpecial
{
    //二维数组访问处理
    public int[,] luaArray = new int[3, 2] { { 1, 5 }, { 3, 7 } ,{8 , 9}};
    

}

#endregion


#region 系统类型加特性

//固定写法
/// <summary>
/// lua处理系统类型添加特性的情况
/// 这种方式的好处就是可以使用一个静态类统一管理所有特性的添加，
/// 可以把所有的委托或者接口需要添加特性都放到这里处理
/// </summary>
public static class AddAttribute
{
    [CSharpCallLua]
    public static List<Type> CsharpCallLuaList = new List<Type>(){
        typeof(UnityAction<float>)
    };
    
    [LuaCallCSharp]
    public static List<Type> LuaCallCsharpList = new List<Type>(){
        typeof(GameObject),
        typeof(Rigidbody)
    };
}

#endregion

#region lua调用泛型相关

public class CallGeneric
{
    public interface ITest
    {
        
    }

    public class GFather
    {
    }

    public class GChild : GFather
    {
    }

    public void TestFunc1<T>(T a, T b) where T : GFather
    {
        Debug.Log("有参数有约束的泛型方法");
    }

    //从这里开始lua无法直接调用
    public void TestFunc2<T>(T a)
    {
        Debug.Log("有参数没有约束的泛型方法"+a);

    }

    public void TestFunc3<T>() where T : GFather
    {
        Debug.Log("没有参数有约束的泛型方法");

    }

    public void TestFunc4<T>(T a) where T : ITest
    {
        Debug.Log("有参数有约束，但是约束不是类的方法");

    }
}


#endregion


public class LuCall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
