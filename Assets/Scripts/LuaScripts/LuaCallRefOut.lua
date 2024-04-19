print("*********lua调用C#带有refout参数的方法***********")


Luarefout = CS.RefOutClass

local test=Luarefout()

--ref参数 会以多返回值的方式返回给lua
--默认第一个返回值就是函数的返回值
--如果需要多传参数，需要按顺序每个值都要传值，不需要传值的就用0代替（lua默认没传值的都用0代替）
local a,b,c=test:RefFunc(1,0,0,1)

print("a的值为函数返回值:"..a)
print("b的值为ref:"..b)
print("c的值为ref:"..c)


print("************out**********************")
--out参数的传值可以省略
local a,b,c = test:OutFunc(5,1)
print("a的值为函数返回值:"..a)
print("b的值为out:"..b)
print("c的值为out:"..c)


--混合使用refout
print("*********混合使用refout******************")
--ref参数需要用0占位，out可以不传值，第一个值为函数返回值

local a,b,c = test:RefOutFunc(5,2,3)
print("a的值为函数返回值:"..a)
print("b的值为refout:"..b)
print("c的值为refout:"..c)


print("**************************************************")

local b,c = test:NoneReturnFunc(5,2,8)
print("b的值为:"..b)
print("c的值为:"..c)