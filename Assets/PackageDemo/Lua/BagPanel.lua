--一个面板一张表
BagPanel = {}

--"成员变量"
--面板对象
BagPanel.panelObj = nil --主面板
--各个控件
BagPanel.btnClose = nil --关闭按钮
BagPanel.togEquip = nil --装备按钮
BagPanel.togItem = nil  --道具按钮
BagPanel.togGem = nil   --宝石按钮

BagPanel.svBag = nil    --背包栏
BagPanel.Content = nil  --内容方块

--用来存储当前 显示的格子
BagPanel.items = {}
--用来管理当前显示的页签，避免重复刷新()
BagPanel.currentPage=-1


--"成员方法"
--初始化方法
function BagPanel:Init()
    --基本步骤：
    --1.实例化
    --2.找控件
    --3.添加事件
    if self.BagPanel == nil then
        self.panelObj = ABMgr:LoadRes("ui", "BagPanel", typeof(GameObject))
        
        self.panelObj.transform:SetParent(Canvas, false)
        --先招面板再找三个选项框
        self.btnClose = self.panelObj.transform:Find("bg"):Find("btnClose"):GetComponent("Button")
        local group = self.panelObj.transform:Find("bg"):Find("ToggleGroup")
        --一定要记得要获取到物体身上的Toggle组件
        self.togEquip = group.transform:Find("togEquip"):GetComponent(typeof(Toggle))
        self.togItem = group.transform:Find("togItem"):GetComponent(typeof(Toggle))
        self.togGem = group.transform:Find("togGem"):GetComponent(typeof(Toggle))

        --scroll view
        self.svBag = self.panelObj.transform:Find("bg"):Find("svBagBackGround"):Find("svBag"):GetComponent(typeof(
        ScrollRect))
        self.Content = self.svBag.transform:Find("Viewport"):Find("Content")

        --加事件
        --关闭按钮
        self.btnClose.onClick:AddListener(function()
            self:HideMe()
        end)


        --单选框事件，注意这里需要在C#代码中添加[CSharpCallLua]特性
        --切换页签
        --装备按钮监听
        self.togEquip.onValueChanged:AddListener(function(Page)
            if Page == true then
                self:ChangePage(1)
            end
        end)

        --道具按钮监听
        self.togItem.onValueChanged:AddListener(function(Page)
            if Page == true then
                self:ChangePage(2)
            end
        end)

        --宝石按钮监听
        self.togGem.onValueChanged:AddListener(function(Page)
            if Page == true then
                self:ChangePage(3)
            end
        end)
    end
end

--显示隐藏
function BagPanel:ShowMe()
    self:Init()
    self.panelObj:SetActive(true)
    --第一次打开背包时，初始化更新装备栏
    if self.currentPage==-1 then
        self:ChangePage(1)
    end
end

function BagPanel:HideMe()
    self.panelObj:SetActive(false)
end

--切换页签的逻辑处理
--Page 装备1 道具2 宝石3
function BagPanel:ChangePage(Page)
    print("当前界面为"..Page)
    --判断如果是当前页签就不更新
    if Page==currentPage then
        return
    end
    --切页逻辑 根据玩家信息
    --更新之前 把老的格子删除  BagPanel.items
    --再根据当前选择的类型来创建新的格子  BagPanel.items

    --删除格子的数据
    for i = 1, #self.items do
        --直接删除
        --GameObject.Destroy(self.items[i].obj)

        --面向对象
        self.items[i]:Destroy()
    end
    self.items={}

    --创建格子的逻辑
    local currentItem=nil
    if Page == 1 then
        currentItem=PlayerData.equips
    elseif Page == 2 then
        currentItem=PlayerData.items
    else
        currentItem=PlayerData.gems
    end

    --创建格子
    for i = 1, #currentItem do
        --[[非面向对象的处理方式

        --有格子资源 直接加载格子资源实例化出来改变图片和文本以及位置即可
        local grid={}
        --创建一张临时的新表 代表格子对象 里面的属性 存储对应想要的信息
        grid.obj=ABMgr:LoadRes("ui","item")

        --设置父对象
        grid.obj.transform:SetParent(self.Content,false)

        --继续设置他的位置,这里我在Unity中设置了LayOut所以不用设置位置
        --gride.obj.transform.localPosition=Vector3()
        
        --先找到控件
        grid.imgIcon = grid.obj.transform:Find("icon"):GetComponent(typeof(Image))
        grid.Text = grid.obj.transform:Find("count"):GetComponent(typeof(Text))

        --读取图标
        --先利用ItemData定义的根据id找数据的方式获取数据
        local data=ItemData[currentItem[i].id]
        --根据名字 先添加图集 再添加图集中的 图标信息
        --根据图集中的命名规则来切割字符串 本项目的命名规则是icon_id
        local strs=string.split(data.icon,"_")--得到icon和id
        --加载图集
        local spriteAtlas=ABMgr:LoadRes("ui",strs[1],typeof(SpriteAtlas))
        --加载图标
        grid.imgIcon.sprite=spriteAtlas:GetSprite(strs[2])
        --读取数量
        grid.Text.text=currentItem[i].num
        ]]--


        --面向对象的处理方式ItemGrid
        local grid=ItemGrid:new()

        grid:Init(self.Content)
        grid:InitData(currentItem[i])



        table.insert(self.items,grid)--添加到新创建的表中
    end
end
