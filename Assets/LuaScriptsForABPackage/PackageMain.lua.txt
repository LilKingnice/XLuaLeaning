print("********背包-Main主入口*********")

--初始化所有准备好的类名
require("initAll")

--初始化道具信息
require("ItemData")

--玩家信息读取方式：
--1.从本地读取 本地存储的几种方式：PlayerPrefs 和 Json 或者2进制
--2.从网络端读取

require("PlayerData")
--想在其他地方使用PlayerData就可以在这里先调用一次Init方法初始化
PlayerData:Init()


require("MainPanel")
--MainPanel:Init()
MainPanel:ShowMe()--优化
require("ItemGrid")--基于面向对象思想创建的格子脚本

require("BagPanel")
--BagPanel:ShowMe()


