print("************lua中处理一些特殊问题***************")


--无法为系统类或者第三方库代码加上两大特性
--在Unity中监听一个Slider的值

GameObject = CS.UnityEngine.GameObject

UI = CS.UnityEngine.UI

--先获取到场景中的对象
local slider = GameObject.Find("Slider")
local slider2 = GameObject.Find("Slider2")

--print(slider)
local sliderscript = slider:GetComponent(typeof(UI.Slider))
--print(sliderscript)

--添加Slider的事件监听
sliderscript.onValueChanged:AddListener(function (f)
	print(f)
end)

