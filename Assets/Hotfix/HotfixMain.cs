using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XLua;

[Hotfix]
class HotfixWithOutMono
{
    public HotfixWithOutMono()
    {
        Debug.Log("Hotfix构造函数");
    }

    public void Speak(string str)
    {
        Debug.Log(str);
    }

    //析构函数（在Unity中不会常用）
    ~HotfixWithOutMono()
    {

    }
}

[Hotfix]
public class Hotfixgenericity<T>
{
    public void Test(T str)
    {
        Debug.Log(str);
    }
}


[Hotfix]
public class HotfixMain : MonoBehaviour
{
    HotfixWithOutMono test;

    #region 属性和索引器
    public int Age
    {
        get
        {
            return 0;
        }
        set
        {
            Debug.Log(value);
        }
    }

    int[] array = new int[] { 1, 2, 3 };
    public int this[int index]
    {
        get
        {
            if (index >= array.Length || index < 0)
            {
                Debug.Log("索引器不正确");
                return 0;
            }
            return array[index];
        }
        set
        {
            if (index >= array.Length || index < 0)
            {
                Debug.Log("索引器不正确");
                return;
            }
            array[index] = value;
        }
    }
    #endregion

    #region 事件
    event UnityAction myEvent;
    private void EventTest()
    {

    }
    #endregion


    void Start()
    {
        LuaManager.Instance.Init();
        LuaManager.Instance.DoingLuaFile("LuaHotfixMain");

        Debug.Log(Add(2, 5));

        Speak("hot hot hotfix!!");

        test = new HotfixWithOutMono();
        test.Speak("这是没有mono的类");


        //StartCoroutine(nameof(HotfixCoroutine));

        this.Age = 3;
        Debug.Log(this.Age);

        this[99] = 100;
        Debug.Log(this[22]);


        myEvent += EventTest;
        myEvent -= EventTest;


        Hotfixgenericity<string> t1=new Hotfixgenericity<string>();
        t1.Test("原本的string泛型方法");

        Hotfixgenericity<int> t2=new Hotfixgenericity<int>();
        t2.Test(555);
        
    }



    void Update()
    {

    }

    IEnumerator HotfixCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("协程调用");
        }
    }
    public int Add(int a, int b)
    {
        return a + b;
    }

    public static void Speak(string str)
    {
        Debug.Log(str);
    }
}
