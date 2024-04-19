print("*********Lua调用C#枚举*************")

GameObject = CS.UnityEngine.GameObject
PrimitiveType = CS.UnityEngine.PrimitiveType

local cube=GameObject.CreatePrimitive(PrimitiveType.Cube)
--修改名字
--修改属性


--自定义枚举

myEnum=CS.Customenmu
Debug=CS.UnityEngine.Debug

--得到枚举的值
Debug.Log(myEnum.Idle)
Debug.Log(myEnum.Running)
Debug.Log(myEnum.Walking)

print(myEnum.__CastFrom("Running"))--得到枚举的值和位置
print(myEnum.__CastFrom(2))--得到枚举的值和位置


