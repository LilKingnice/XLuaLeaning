print("热补丁lua逻辑代码")


--每个热补丁在使用之前的都需要进行四步操作：
-- 1. 加特性（给哪个类加热补丁就在哪个类前面加特性）
-- 2. 加宏（仅第一次打补丁进行操作）
-- 3.生成代码（每次修改了C#代码后都要执行操作，修改lua代码无需执行）
-- 4.hotfix 注入（每次修改了C#代码后都要执行操作，修改lua代码无需执行）


--lua当中 热补丁代码固定写法
--xlua.hotfix(类，“函数名”，lua函数)

xlua.hotfix(CS.HotfixMain, "Add", function(self, a, b)--如果调用的是成员函数，第一个参数需要填self（相当于把自己传入进来了）
    return a + b +1000--修改了原本应该输出的内容
end)

xlua.hotfix(CS.HotfixMain, "Speak", function(a)--如果调用的是静态函数，不用考虑填self
    a="lua中修改输出"--修改了原本应该输出的内容
    print(a)
end)
