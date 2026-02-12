using UnityEditor;
using UnityEngine;

public class MyTool
{
    [MenuItem("MyTools/Do Something")]
    static void DoSomething()
    {
        Debug.Log("工具開始執行！");
    }
}