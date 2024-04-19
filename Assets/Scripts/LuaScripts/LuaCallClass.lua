print("***********lua调用C#中的类*****************")

local obj = CS.UnityEngine.GameObject()--创建一个新物体
local obj2 = CS.UnityEngine.GameObject("名字")

--声明全局变量，减少局部变量创建，（优化方式）
GameObject = CS.UnityEngine.GameObject
local obj3 = GameObject("obj3")

--静态方法只需要用点调用即可
local obj4 = GameObject.Find("obj3")

--创建了全局变量的要调用成员方法也只需要用点即可
Debug = CS.UnityEngine.Debug
--报错：Debug("obj3当前位置"..obj3.transform.position)
--报错：Debug("obj3当前位置"+obj3.transform.position)
Debug.Log(obj4.transform.position)


--如果使用对象中的成员方法就需要用冒号
Vector3 = CS.UnityEngine.Vector3
obj3.transform:Translate(Vector3.right)
Debug.LogWarning(obj3.transform.position)
Debug.LogError(obj3.transform.position)


--自定义类 使用方法
--方法一:没有命名空间的
local test = CS.Test1()
test:speak("NIHAO111")
--方法二：有命名空间的
local test2 = CS.luacallcsharp.Test2()
test2:speak("NIHAO222")


--通过xlua的typeof添加组件
local obj5=GameObject("添加组件")
obj5:AddComponent(typeof(CS.LuCall))
obj5:AddComponent(typeof(CS.UnityEngine.Rigidbody))
obj5:AddComponent(typeof(CS.UnityEngine.BoxCollider))

