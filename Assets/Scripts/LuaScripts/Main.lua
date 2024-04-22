--把这里当作一个lua脚本运行的入口
print("lua程序入口Main")

--C#调用lua
--require("GlobleVariable")--C#获取lua中的变量

--require("luafunction")--C#获取lua中的函数


--require("luatable")--C#调用Lua中的List和Dictionary


--require("luaclass")--Lua中的表映射到C#中的类对象中

--lua调用C#

--require("LuaCallClass")--lua调用c#类
--require("LuaCallEnum")--lua调用c#枚举

--require("LuaCallStructure")--lua调用c#数据结构

--require("LuaCallFunction")--lua调用c#扩展方法

--require("LuaCallRefOut")--lua调用C#带有refout参数的方法


--require("LuaCallOverrideFunction")--lua调用C#中的重载方法


--require("LuaCallDelegate")--lua调用C#中的委托和事件

--新增一个用于全局判断空的函数
function IsNull(str)
	if str==nil or str:Equals(nil)then
		return true
	end
	return false
end

--require("LuaCallSpecial")--lua调用的一些特殊处理

--require("LuaCallSpecialTreatment")--lua中的一些特殊调用(特性)


--require("luaCallCoroutine")--lua调用C#协程


require("luaCallGeneric")--lua调用C#泛型