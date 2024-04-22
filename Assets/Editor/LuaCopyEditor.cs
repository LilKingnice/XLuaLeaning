using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// XLua栏目扩展，用于自动复制所有lua文件和修改文件扩展以及打包操作
/// </summary>
public class LuaCopyEditor : Editor
{
    [MenuItem("XLua/快速打包lua")]
    public static void CopyLuaToTxt()
    {
        //获取
        string path = Application.dataPath + "/PackageDemo/Lua/";
        //判断路径是否存在
        if (!Directory.Exists(path))
            return;
        string[] strs = Directory.GetFiles(path, "*.lua");//根据字符串筛选后缀名的文件

        //拷贝
        //定义新文件夹
        string newPath = Application.dataPath + "/LuaScriptsForABPackage/";
        if (!Directory.Exists(newPath))
            Directory.CreateDirectory(newPath);//文件夹不存在就创建
        else
        {
            string[] files = Directory.GetFiles(newPath, "*.txt");

            foreach (var item in files)
            {
                File.Delete(item);
            }
            Debug.Log("文件夹已清空！");
        }

        //新文件名
        string filename;
        List<string> newFileNames=new List<string>();
        foreach (var item in strs)
        {
            filename = newPath + item.Substring(item.LastIndexOf("/") + 1) + ".txt";//找到最后一个斜杠的后面一位
            newFileNames.Add(filename);//把每一个找到的对象都添加到List中
            File.Copy(item, filename);//拷贝文件
        }

        //到处成功后刷新一下Unity面板
        AssetDatabase.Refresh();

        //刷新过后修改所有文件的ab包
        foreach (var item in newFileNames)
        {
            //UnityAPI:修改传入路径 必须是相对Assets文件夹中的 Assets/..../..../
            AssetImporter importer=AssetImporter.GetAtPath(item.Substring(item.IndexOf("Assets")));
            if(importer!=null)
                importer.assetBundleName="lua";
        }
    }
}
