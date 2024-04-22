--用来控制UI中的MainPanel

--只要是一个新的对象(面板)我们就新建一张表
MainPanel={}

--不是必须写的 因为lua的特性 不存在声明变量的概念
-- 关联的面板对象
MainPanel.panelObj=nil--用来装面板对象自身
MainPanel.btnRole=nil--用来装主面板下的role按钮
MainPanel.btnSkill=nil--用来装主面板的skill按钮


--初始化该面板 实例化对象 控件事件监听
function MainPanel:Init()
    --只有对象没有被实例化过才需要实例化，节省性能
    if self.panelObj ==nil then

        --1.实例化面板对象 调用的是InitAll代码中获取到的ABmanager
        self.panelObj=ABMgr:LoadRes("ui","MainPanel",typeof(GameObject))
        --第一个参数表示父对象，第二个参数表示是否保持在世界坐标上的位置
        self.panelObj.transform:SetParent(Canvas,false)
        --2.找到对应控件
        --找到子对象和其对应button脚本
        self.btnRole = self.panelObj.transform:Find("btnRole"):GetComponent(typeof(Button))
        
        --3.为空间加上事件监听 进行点击等等的逻辑处理
        --无法获取到自己的信息
        --self.btnRole.onClick:AddListener(self.BtnRoleClick)
        --推荐直接用这种方式（面向对象）
        self.btnRole.onClick:AddListener(function()
            self:BtnRoleClick()
        end)
        
    end

end

--调用背包面板打开的函数(BagPanel)
--一定不要忘记写冒号，冒号就是表示是谁在调用
function MainPanel:BtnRoleClick()
    print("nihaonihaonihao")
    print(self.panelObj)--"打印当前获取到的对象"

    BagPanel:ShowMe()
end

function MainPanel:ShowMe()
    --面板显示就自动实例化
    self:Init()
    self.panelObj:SetActive(true)

    --背包面板逻辑

end

function MainPanel:HideMe()
    self.panelObj:SetActive(false)

end


