print("***********拓展方法*****************")


ExtendMethod = CS.Unextend

--调用静态方法
ExtendMethod.Eat()

local obj = ExtendMethod()

obj:Speak("说话")
obj.name="KUNKUN"

obj:Move()
