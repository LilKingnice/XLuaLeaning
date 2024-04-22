print("************多函数替换******************")


--lua当中 热补丁代码多函数调用的固定写法
--[[
xlua.hotfix(类，{
    函数名=函数,
    函数名=函数,
    函数名=函数
    .....
})
]]--

--给HotfixMain类（有Mono的类）打热补丁
xlua.hotfix(CS.HotfixMain, {
    Update = function(self)
        --print(os.time())
    end,
    Add = function(self, a, b)
        return a + b + 200
    end,
    Speak = function(a)
        a = "多函数替换"
        print(a)
    end
})


--给没有Mono的类打热补丁

xlua.hotfix(CS.HotfixWithOutMono, {
    --构造函数的固定写法！！！
    [".ctor"] = function()
        print("构造函数打lua热补丁")
    end,

    Speak = function(self, a)
        print("KUNKUN说" .. a)
    end

    --析构函数的固定写法，在Unity中不是很常用了解即可
    --Finalize = function()

    --end
})
