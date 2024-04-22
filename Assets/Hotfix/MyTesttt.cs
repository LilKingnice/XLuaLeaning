using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyTesttt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // var i_test1 = new IndexedCustomList<string>();
        // i_test1[1] = "kunkun";
        // Debug.Log("string类型索引器第一个是" + i_test1[1]);

        // var i_test2 = new IndexedCustomList<int>();
        // i_test2[2] = 5;
        // Debug.Log("int类型索引器第一个是" + i_test2[2]);


        IndexWeek indexWeek=new IndexWeek();
        Debug.Log(indexWeek[4]);//星期五
        Debug.Log(indexWeek["星期一"]);//0
        Debug.Log(indexWeek["星期六"]);//5

    }

    // Update is called once per frame
    void Update()
    {

    }


}

#region 使用索引器
// public class IndexedCustomList<T>
// {
//     T[] temps = new T[10];

//     public T this[int index]
//     {
//         get => temps[index];
//         set => temps[index] = value;
//     }
// }
#endregion


#region 实际使用

// public class Customer
// {
//     public string Name{ get; set; }
//     public int? PhoneNumb { get; set; }
//     public string Address { get; set; }
// }

public class IndexWeek
{
    string[] weekarray=
    {
        "星期一","星期二","星期三",
        "星期四","星期五","星期六","星期日"
    };
    public string this[int index]
    {
        get{
            return weekarray[index];
        }
    }

    public int this[string index]
    {
        get{
            return Array.IndexOf(weekarray,index);
        }
    }
}



#endregion