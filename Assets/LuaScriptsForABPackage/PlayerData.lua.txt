print("*********玩家数据读取和存储*******************")

PlayerData ={}
--我们目前只做背包功能 所以只需要它的道具信息即可

PlayerData.equips={}--装备信息
PlayerData.items={}--道具信息
PlayerData.gems={}--宝石信息


--为玩家数据写了一个初始化方法，后期可以直接改变数据来源
function PlayerData:Init()

    --存储物品信息，不管存到哪里都只会存储物品的id和数量
    --示例代码，随便添加一点东西进去
    table.insert(self.equips,{id=1,num=5})
    table.insert(self.equips,{id=2,num=1})

    table.insert(self.items,{id=3,num=8})
    table.insert(self.items,{id=4,num=2})

    table.insert(self.gems,{id=5,num=11})
    table.insert(self.gems,{id=6,num=12})
end

--要想在代码中调用就需要先调用一次Init()方法
--PlayerData:Init()
--print(PlayerData.equips[1].id.." "..PlayerData.equips[1].num)
