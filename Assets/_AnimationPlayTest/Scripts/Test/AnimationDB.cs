using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MascotAnimDB", menuName = "Bingo/Mascot Animation DB")]
public class AnimationDB : ScriptableObject, ISerializationCallbackReceiver
/*
// Warning: Unity 的 Inspector 面板和檔案儲存系統（序列化）不支援字典 (Dictionary)。

如果你直接在腳本寫 public Dictionary...，你會發現：
Inspector 面板裡什麼都沒有。
存檔後重開，字典裡的資料會全部消失。

ISerializationCallbackReceiver 提供了兩個關鍵的「掛鉤」：

OnBeforeSerialize (存檔前)：在 Unity 把資料存進硬碟前一刻執行。你可以把字典裡的資料「拆解」回 List 存起來。

OnAfterDeserialize (讀取後)：在 Unity 從硬碟讀完資料後執行。你可以把讀進來的 List「組裝」回字典。
*/
{
    [System.Serializable]
    public class AnimInfo
    {
        public string key;
        public string spineAnimName;
        public bool loop;
    }

    public List<AnimInfo> animList = new List<AnimInfo>();
    public Dictionary<string, AnimInfo> AnimDict = new Dictionary<string, AnimInfo>();

    /// <summary>
    /// Unity 讀取這個檔案，把 List 填滿。
    /// 關鍵點：讀完的一瞬間，Unity 會自動觸發 OnAfterDeserialize。
    /// 把 List 裡的內容一個個丟進 Dictionary。
    /// </summary>
    public void OnAfterDeserialize()
    {
        AnimDict.Clear();
        foreach (var info in animList)
        {
            if (!string.IsNullOrEmpty(info.key) && !AnimDict.ContainsKey(info.key))
                AnimDict.Add(info.key, info);
        }
    }

    public void OnBeforeSerialize() { }
}