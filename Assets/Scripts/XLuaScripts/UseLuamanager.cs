using System;
using UnityEngine;
using UnityEngine.Events;
using XLua;


//自定义无参无返回值
public delegate void CustomCall();

//自定义有参有返回值
[CSharpCallLua]
public delegate int CustomCall2(int a);

//自定义多返回值委托,lua代码中返回多少个参数这里写多少个out，第一个不用写out应为是默认返回的第一个
[CSharpCallLua]
public delegate int CustomCall3(int a, out int b, out bool c, out string d);
//自定义多返回值委托,lua代码相当于传入四个参数进行修改，因为第一个是默认的返回值所以不用写ref
[CSharpCallLua]
public delegate int CustomCall4(int a, ref int b, ref bool c, ref string d);

//自定义变长参数委托
//一般这个数组需要根据实际的变长参数的使用类型传值，可以稍微提升一点性能
[CSharpCallLua]
//public delegate void CustomCall5(string a, params int[] obj);
public delegate void CustomCall5(string a, params object[] obj);
/// <summary>
/// C#调用Lua
/// </summary>
public class UseLuamanager : MonoBehaviour
{
    void Start()
    {
        
        
        LuaManager.Instance.Init();
        
        //LuaManager.Instance.DoString("require('Main')");
        //LuaManager.Instance.DoString("require('ABLuaTest')");
        
        LuaManager.Instance.DoingLuaFile("Main");
        
        //不使用单例模式就需要自己手动创建
        // LuaEnv env = new LuaEnv();
        // int GlobalNumber=env.Global.Get<int>("GlobalNumber");


        #region 全局变量调用
        // int GlobalNumber1=LuaManager.Instance.Global.Get<int>("GlobalNumber");
        // Debug.Log("GlobalNumber1："+GlobalNumber1);
        //
        // //修改值
        // LuaManager.Instance.Global.Set("GlobalNumber",555);
        // int GlobalNumber2 = LuaManager.Instance.Global.Get<int>("GlobalNumber");
        // Debug.Log("GlobalNumber2："+GlobalNumber2);
        //
        // bool GlobalBool=LuaManager.Instance.Global.Get<bool>("GlobalBool");
        // Debug.Log("GlobalBool："+GlobalBool);
        //
        // double GlobalFloat=LuaManager.Instance.Global.Get<double>("GlobalFloat");
        // Debug.Log("GlobalFloat："+GlobalFloat);
        //
        // string GlobalString=LuaManager.Instance.Global.Get<string>("GlobalString");
        // Debug.Log("GlobalString："+GlobalString);
        #endregion


        #region 无参无返回值
        
        //无参无返回
        CustomCall mycall = LuaManager.Instance.Global.Get<CustomCall>("func1");
        mycall();
        //Unity内置无返回值
        UnityAction ucall = LuaManager.Instance.Global.Get<UnityAction>("func1");
        ucall();
        //C#内置无返回值
        Action ccall=LuaManager.Instance.Global.Get<Action>("func1");
        ccall();
        //XLua提供的获取函数的方法，一般来说少用
        LuaFunction lf = LuaManager.Instance.Global.Get<LuaFunction>("func1");
        lf.Call();
        
        #endregion

        
        
        #region 有参有返回值
        //自定义有参有返回值
        CustomCall2 mycall2 = LuaManager.Instance.Global.Get<CustomCall2>("func2");
        Debug.Log(mycall2(10));
        
        //C#内置有返回值
        Func<int,int> ccall2=LuaManager.Instance.Global.Get<Func<int,int>>("func2");
        Debug.Log(ccall2(20));
        
        //XLua提供的获取函数的方法，一般来说少用
        LuaFunction lf2 = LuaManager.Instance.Global.Get<LuaFunction>("func2");
        //lf2.Call();//返回的是一个数组,由于只有一个返回值，所以返回第一个即可
        Debug.Log(lf2.Call(30)[0]);
        #endregion
        
        
        #region 多返回值

        //自定义委托调用：使用out关键字
        CustomCall3 mycall3 = LuaManager.Instance.Global.Get<CustomCall3>("func3");
        int b1;
        bool c1;
        string d1;
        Debug.Log("_第一个默认返回元素："+mycall3(100, out b1,out c1,out d1)+"_第二个out元素："+b1+"_第三个out元素："+c1+"_第四个out元素："+d1);

        //自定义委托调用：使用ref关键字
        CustomCall4 mycall4 = LuaManager.Instance.Global.Get<CustomCall4>("func3");
        int b2=0;
        bool c2=false;
        string d2="";
        Debug.Log("_第一个默认返回元素："+mycall4(200, ref b2,ref c2,ref d2)+"_第二个ref元素："+b2+"_第三ref个元素："+c2+"_第四个ref元素："+d2);
        
        //XLua提供的获取函数的方法，一般来说少用
        LuaFunction l3 = LuaManager.Instance.Global.Get<LuaFunction>("func3");
        //lf.Call();//返回的是一个数组,由于只有一个返回值，所以返回第一个即可
        object[] objs = l3.Call(30);
        for (int i = 0; i < objs.Length; i++)
        {
            Debug.Log("第"+i+"个返回值："+objs[i]);
        }
        #endregion

        
        #region 变长函数
        
        CustomCall5 mycall5 = LuaManager.Instance.Global.Get<CustomCall5>("func4");
        
        //mycall5("自定义的变长参数返回值",11,22,33,44,55,66);定死变长函数为int数组
        
        mycall5("变长", "12", "11", true, 55);
        
        //XLua提供的变长参数调用方法
        LuaFunction l4 = LuaManager.Instance.Global.Get<LuaFunction>("func4");
        l4.Call("XLua返回值", 111, 222, 333, 444, 555, 666);

        #endregion

    }


}
