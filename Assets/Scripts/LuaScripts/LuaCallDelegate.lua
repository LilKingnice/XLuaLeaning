print("***********lua调用C#中的委托和事件************")

local obj = CS.CustomDel()

--委托的本质就是是用来装函数的
--使用C#中的委托 就是用来装lua函数的
local fun = function( )
	print("Lua函数Fun")
end

--Lua中没有复合运算符 不能+=
--如果第一次往委托中加函数 因为是nil 不能直接+
--所以第一次 要先用等于添加一个函数进去
print("*********开始添加函数***********")
obj.luacallAction = fun
--obj.del = obj.del + fun
obj.luacallAction = obj.luacallAction + fun

--第二种写法：直接声明临时函数的写法
--不建议这样写 最好最好还是 先声明函数再加
obj.luacallAction = obj.luacallAction + function()
	print("临时申明的函数")
end

--委托执行
obj.luacallAction()
print("*********开始退订函数***********")
obj.luacallAction = obj.luacallAction - fun
obj.luacallAction = obj.luacallAction - fun
--委托执行
obj.luacallAction()


print("*********清空***********")
--清空所有存储的函数
obj.luacallAction = nil
--清空过后得先等
obj.luacallAction = fun
--调用
obj.luacallAction()

print("*********Lua调用C#事件相关知识点***********")
local fun2 = function()
	print("事件加的函数")
end


print("*********事件添加函数***********")
--事件加减函数  和 委托非常不一样
--lua中使用C#事件 加函数 
--有点类似使用成员方 冒号事件名("+", 函数变量)
obj:luacallEvent("+", fun2)

--第二种写法：直接声明临时函数的写法
--最好不要这样写
obj:luacallEvent("+", function()
	print("事件加的匿名函数")
end)

obj:DoEvent()
print("*********事件减函数***********")
obj:luacallEvent("-", fun2)
obj:DoEvent()

print("*********事件清楚***********")
--清事件 不能直接设空，需要调用C#预先定义好的方法
obj:ClearEvent()
obj:DoEvent()