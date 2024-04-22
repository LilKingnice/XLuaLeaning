print("************事件的热补丁*********************")

func={}

xlua.hotfix(CS.HotfixMain,{
    add_myEvent=function (self,del)
        print(del)
        print("添加事件函数")
        --此处添加事件需要注意不能使用lua代码添加del，因为del是C#的事件，
        --这样又会被lua热补丁修改会造成死循环
        
        --后面测试的就算是使用这种方式添加lua中的方法都不行，也会卡死
        --self:myEvent("+",hotfixeventtest)

        --如需要添加事件直接往里面写即可
        hotfixeventtest()
    end,
    remove_myEvent=function (self,del)
        print(del)
        print("移除事件函数")
    end
})


function hotfixeventtest()
    print("hotfix event test！！")
end
