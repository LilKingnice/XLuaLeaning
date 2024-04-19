print("***********C#调用lua——函数调用**************")
--各种函数调用

--无参无返回
func1=function()
	print("无参无返回")
end

--有参有返回
function func2(t)
	print("有参有返回")
	return t+1
end

--多返回
function func3()
	print("多返回值")
	return 2,234,true,"nihao"
end

--变长参数

function func4( a,... )
	print("变长参数")
	print(a)
	arg={...}
	for k,v in pairs(arg) do
		print(k,v)
	end
end

--func4("nihao",55,true,"world")

