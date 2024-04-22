--常用别名都在这里定位
--这里事先定义好常用的一些类的引用，提高效率
--准备我们自己之前导入的脚本

--面向对象
require("Object")

--字符串拆分
require("SplitTools")

--Json解析
Json=require("JsonUtility")


--Unity相关的
GameObject = CS.UnityEngine.GameObject
Resources = CS.UnityEngine.Resources
Transform = CS.UnityEngine.Transform
RectTransform = CS.UnityEngine.RectTransform
--文本文件读取相关
TextAsset = CS.UnityEngine.TextAsset



Vector3 = CS.UnityEngine.Vector3
Vector2 = CS.UnityEngine.Vector2

--UI相关
--图集对象类
SpriteAtlas = CS.UnityEngine.U2D.SpriteAtlas

UI = CS.UnityEngine.UI

Image = UI.Image
Text = UI.Text
Button = UI.Button

Toggle = UI.Toggle
ScrollRect = UI.ScrollRect

--找一次 都可以调用
Canvas = GameObject.Find("Canvas").transform

--自己写的C#脚本相关
--直接得到AB包管理器的单例对象
ABMgr = CS.ABmanager.Instance



