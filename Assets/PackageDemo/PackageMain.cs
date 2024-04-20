using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 用于xlua背包项目的C#主入口
/// </summary>
public class PackageMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaManager.Instance.Init();
        LuaManager.Instance.DoingLuaFile("PackageMain");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
