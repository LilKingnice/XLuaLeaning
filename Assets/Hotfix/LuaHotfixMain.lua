print("***********热补丁lua代码主入口**********")

require("Hotfix")
require("HotfixFunctionsReplace")

--require("Assets.Hotfix.HotfixCoroutine")完整路径
require("HotfixCoroutine")--协程

require("Hotfixprop")--属性和索引器


require("HotfixEvent")--事件的热补丁


require("Hotfixgenericity")--泛型热补丁
