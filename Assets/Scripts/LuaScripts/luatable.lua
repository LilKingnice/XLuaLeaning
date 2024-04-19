--C#调用Lua中的List和Dictionary
--List和Dictionary映射table

print("****映射lua表****")
--List
list1={1,2,3,4,5,6}
list2={"我是List",true,nil,999}


--Dictionary

dic1={
	["1"]=1,
	["2"]=2,
	["3"]=3,
	["4"]=4,
}

dic2={
	[true] =0,
	[false] =1,
	["这是字典"] =11111,
	["hhh"] =true,
}




