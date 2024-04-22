print("**************协程的热补丁替换****************")

--要在Lua中调用协程一定会使用到它
util = require("xlua.util")
--[[标准写法
xlua.hotfix(类，{
    函数名=函数,
    函数名=函数,
    函数名=函数
    .....
})
]]--


xlua.hotfix(CS.HotfixMain, {
    HotfixCoroutine = function(self)
        --（通过cs_generator）返回一个真正的经过了xlua处理过的协程函数
        return util.cs_generator(function()
            while true do
                coroutine.yield(CS.UnityEngine.WaitForSeconds(2))
                print("lua打补丁后的协程函数")
            end
        end)
    end
})
