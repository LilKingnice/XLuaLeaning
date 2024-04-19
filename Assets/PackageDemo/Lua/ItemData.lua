print("**********读取json数据***************")

--先从AB包中加载jason文件出来
local txt=ABMgr:LoadRes("json","ItemData",typeof(TextAsset))

print(txt.text)

local itemList=Json.decode(txt.text)

--加载出来得到的是一个类似于数组的数据结构
--不方便通过 id来获取里面的内容 所以我们用一张新表转存一次
--而且这张 新道具表都能被使用
--一张用来存储信息的表
--键值对的形式 健是道具ID 值是道具表一行信息

--方式一：只能通过列表的顺序获取到值
print(itemList[1].id..itemList[1].name)


--方式二：通过数值中的id来直接获取想要的值
ItemData ={}
--关键，转存到lua中的一张全局表中
for _, value in pairs(itemList) do
    ItemData[value.id]=value
end
--从只能通过位置来读取值，转换到可以通过数值中的id直接获取值
print(ItemData[105].name..ItemData[105].type..ItemData[105].tips)


