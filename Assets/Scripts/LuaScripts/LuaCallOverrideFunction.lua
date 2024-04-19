print("************lua调用C#中的重载方法*******************")

Override = CS.CustomOverride

local test = Override()

test:Myreturn()

print("一个int的重载"..test:Myreturn(10))
print("两个int的重载"..test:Myreturn(10,20))

--由于lua中只有number类型，所以会造成精度丢失的问题。这里的返回结果为0
print("一个float的重载"..test:Myreturn(1.5))


--用反射解决精度丢失问题

--先使用typeof获取类型
local m1=typeof(CS.CustomOverride):GetMethod("Myreturn",{typeof(CS.System.Int32)})
local m2=typeof(CS.CustomOverride):GetMethod("Myreturn",{typeof(CS.System.Single)})

--然后使用xlua中的tofunction转换成可以使用的成员函数
local f1 = xlua.tofunction(m1)
local f2 = xlua.tofunction(m2)

--成员方法 第一个参数传入调用对象
--如果是静态函数就直接传入参数即可
print(f1(test,10))
print(f2(test,1.5))

