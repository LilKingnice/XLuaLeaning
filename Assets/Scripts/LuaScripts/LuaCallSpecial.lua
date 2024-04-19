print("***********************Lua调用的特殊处理**********************")

--二维数组遍历
print("***********************二维数组的遍历**********************")

local Array = CS.LuaSpecial()


print("行："..Array.luaArray:GetLength(0))
print("列："..Array.luaArray:GetLength(1))

--获取元素
--错误示范:无法通过这样进行数组内容的获取
--print(Array.luaArray[1][1])

print(Array.luaArray:GetValue(0,0))
print(Array.luaArray:GetValue(1,1))

for i=0,Array.luaArray:GetLength(0)-1 do
	for j=0,Array.luaArray:GetLength(1)-1 do
		print("遍历数组："..Array.luaArray:GetValue(i,j))
	end
end

--lua调用C#-nil和null比较(判空)
print("*********nil和null比较*****************")

GameObject = CS.UnityEngine.GameObject
Rigidbody=CS.UnityEngine.Rigidbody

local obj = GameObject("测试添加刚体")
local rig=obj:GetComponent(typeof(Rigidbody))

print(rig)

--判断空的三种方式

--第一种：使用Equals进行空值判断，但是如果一开始就为空就不适用
--if rig:Equals(nil) then

--第二种：在main文件中创建一个全局的判断函数进行空值判断，比较推荐的做法
--if IsNull(rig) then

--第三种：在C#代码中创建一个判断空的方法
if rig:IsNull() then
	rig=obj:AddComponent(typeof(Rigidbody))
end
print(rig)




