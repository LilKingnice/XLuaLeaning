print("***********lua调用C#数据结构***************")

--lua调用C#数据结构，一些基本语法需要遵循C#

print("***********Array数组***************")

--一定要记得，local搭配(),全局搭配纯名字
local test = CS.Structure()

--获取数组长度
print(test.array.Length)
print(test.array[2])


--lua遍历数组，需要注意的点：
--1.遵循C#语法下标从0开始
--2.获取数组长度之后需要减一
for i=0,test.array.Length-1 do
	print(test.array[i])
end


--创建数组：
local array2 = CS.System.Array.CreateInstance(typeof(CS.System.Int32),10)
--print(array2.Length)

for i=0,array2.Length-1 do
	array2[i]=i
end

for i=0,array2.Length-1 do
	print("新建数组："..array2[i])
end


print("***********List***************")
--一定要注意点和冒号的区别！！！
--我自己的理解是调用的时候使用点，要执行或者传值的时候用冒号
test.list:Add(1)
test.list:Add(2)
test.list:Add(3)
test.list:Add(4)
print("testlist总数："..test.list.Count)
--遍历访问以及其他方法的使用，都是差不多的类比即可


--List创建分为两种
--老版本（了解即可）
local createListold = CS.System.Collections.Generic["List`1[System.String]"]()
print(createListold)--获取类型
createListold:Add("新创建一个列表")
print(createListold[0])


--新版本
--相当于得到了一个泛型createListnew只是一个“别名”，到别处还需要加括号使用
local createListnew = CS.System.Collections.Generic.List(CS.System.String)
local newList=createListnew()
newList:Add("新版本创建的List")
print(newList[0])
print(newList)



print("***********Dictionary***************")
--字典的遍历：
test.dic:Add(1,"nihao")
test.dic:Add(2,"世界")
test.dic:Add(3,"！")

for k,v in pairs(test.dic)do
	print("字典数据"..k,v)
end


--字典的创建：
--也有老版本（做了解）

--新版本
local createDicnew = CS.System.Collections.Generic.Dictionary(CS.System.String,CS.UnityEngine.Vector3)
local newDic=createDicnew()
--newDic:Add("Dictionary：右",CS.UnityEngine.Vector3.right)
newDic:Add("Dictionary：上",CS.UnityEngine.Vector3.up)
--newDic:Add("Dictionary：下",CS.UnityEngine.Vector3.down)


--！！这里是一个坑，lua中这样获取不到字典的值！！
--print(newDic["Dictionary：上"])--返回nil

--正确获取值的方式
print(newDic:TryGetValue("Dictionary：上"))--使用C#语法

--XLua语法
print(newDic:get_Item("Dictionary：上"))
newDic:set_Item("Dictionary：上",CS.UnityEngine.Vector3.down)
print(newDic:get_Item("Dictionary：上"))




