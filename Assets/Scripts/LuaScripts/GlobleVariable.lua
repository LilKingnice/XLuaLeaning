print("*****C#调用lua——GlobalVariable********")

--所有声明了的全局变量都会存储在_G表中
GlobalNumber=123
GlobalBool=true
GlobalFloat=1.22
GlobalString="abcdefg"

--local oooooo = 1

--查看_G表,键（名字）值（类型和编号）
--for k,_ in pairs(_G) do
--	print(k)
--end




