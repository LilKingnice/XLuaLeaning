print("************属性和索引器的热补丁*****************")


xlua.hotfix(CS.HotfixMain,{
    --如果是属性进行热补丁重定向
    --set_属性名 设置属性的方法
    --get_属性名 得到属性的方法
    set_Age=function (self,v)
        print("Lua重定向"..v)
    end,
    get_Age=function (self)
        return 100
    end,

    --索引器固定写法
    --set_Item 通过索引器设置
    --get_Item 通过索引器获取
    set_Item=function (self,index,v)
        print("Lua重定向索引器,索引值："..index.."值"..v)
    end,
    get_Item=function (self,index)
        print("Lua重定向索引器")
        return 9999
    end




})