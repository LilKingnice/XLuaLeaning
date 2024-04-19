--C#调用Lua
--Lua中的表映射到C#中的类对象中


print("*********luaclass***********")
myluaclass={
	testInt=2,
	testBool=true,
	testString="luaclass",
	testFun = function()
		print("lua中的myluaclass")
	end
}



--lua表映射C#接口
myluainterface={
	testInt=10,
	testBool=false,
	testString="luainterface",
	testFun = function()
		print("lua中的myluainterface")
	end
}
