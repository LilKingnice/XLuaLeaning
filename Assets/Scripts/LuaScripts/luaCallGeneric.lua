print("*********Lua调用C# 泛型函数相关知识点***********")

local obj = CS.CallGeneric()

local child = CS.CallGeneric.GChild()
local father = CS.CallGeneric.GFather()

--支持有约束有参数的泛型函数
--方法一和方法二都可以实现
--obj:TestFunc1(child, father)

--方法二
obj:TestFunc1(father, child)

--lua中不支持 没有约束的泛型函数
--obj:TestFun2(child)

--lua中不支持 有约束 但是没有参数的泛型函数
--obj:TestFun3()

--lua中不支持 非class的约束
--obj:TestFun4(child)




--补充知识 让上面 不支持使用的泛型函数 变得能用
--得到通用函数  
--设置泛型类型再使用
--xlua.get_generic_method(类, "函数名")
local testFunc2 = xlua.get_generic_method(CS.CallGeneric, "TestFunc2")
local testFunc2_R = testFunc2(CS.System.Int32)
--调用
--成员方法  第一个参数 传调用函数的对象
--静态方法 不用传
testFunc2_R(obj, 1)


--有一定的使用限制（其他的未实现）
--Mono打包 这种方式支持使用
--il2cpp打包  如果泛型参数是引用类型才可以使用
--il2cpp打包  如果泛型参数是值类型，除非C#那边已经调用过了 同类型的泛型参数 lua中才能够被使用

