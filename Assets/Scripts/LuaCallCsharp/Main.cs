using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaManager.Instance.Init();
        //使用于之前学习lua调用C#的入口
        LuaManager.Instance.DoingLuaFile("main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
