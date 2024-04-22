print("*************泛型热补丁**************")

--泛型类的替换规则就是：C#中写了多少种类型，在lua中就打多少种补丁

xlua.hotfix(CS.Hotfixgenericity(CS.System.String),{
    Test=function (self,str)
        print("lua中打的补丁:"..str)
    end
})

xlua.hotfix(CS.Hotfixgenericity(CS.System.Int32),{
    Test=function (self,str)
        print("lua中打的补丁:"..str)
    end
})
