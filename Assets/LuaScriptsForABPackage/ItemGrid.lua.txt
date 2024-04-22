--面向对象思想
--写所有的格子的行为，还可以加一些以后的逻辑（格子拖动，详细面板）
--生成一个table 继承Object 主要目的是要它里面实现的 继承方法subClass
Object:subClass("ItemGrid")

--"成员变量"
ItemGrid.obj = nil
ItemGrid.imgIcon = nil
ItemGrid.Text = nil

--成员函数
--实例化格子对象
function ItemGrid:Init(father)
    self.obj = ABMgr:LoadRes("ui", "item") --使用AB包管理器读取文件
    --设置父对象
    self.obj.transform:SetParent(father, false)

    --获得物体身上的组件
    self.imgIcon = self.obj.transform:Find("icon"):GetComponent(typeof(Image))
    self.Text = self.obj.transform:Find("count"):GetComponent(typeof(Text))
end

--初始化格子信息
--data 是外面传入的参数 道具信息  里面包含了id和num
function ItemGrid:InitData(data)
    local itemData = ItemData[data.id]

    --根据名字 先添加图集 再添加图集中的 图标信息
    --根据图集中的命名规则来切割字符串 本项目的命名规则是icon_id
    local strs = string.split(itemData.icon, "_")
    local spriteAtlas = ABMgr:LoadRes("ui", strs[1], typeof(SpriteAtlas))

    --加载图标
    self.imgIcon.sprite = spriteAtlas:GetSprite(strs[2])
    --读取数量
    self.Text.text = data.num
end

--加自己的逻辑

--删除对象函数
function ItemGrid:Destroy()
    GameObject.Destroy(self.obj)
    self.obj=nil
end