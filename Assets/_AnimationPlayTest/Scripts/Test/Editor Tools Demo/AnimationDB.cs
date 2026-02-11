using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

// 確保這裡完全沒有 using System.Net;
[CreateAssetMenu(fileName = "MascotAnimDB", menuName = "Bingo/Mascot Animation DB")]
public class AnimationDB : ScriptableObject, ISerializationCallbackReceiver
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