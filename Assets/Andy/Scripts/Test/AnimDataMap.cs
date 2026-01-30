using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimDataMap", menuName = "Scriptable Objects/AnimDataMap")]
public class AnimDataMap : ScriptableObject
{
    [Serializable]
    public class AnimEvent
    {
        [Range(0, 1)] public float triggerNormalizedTime; // 0~1 的位置
        public string eventName; // 用來標記這個事件（例如 "Footstep"）
    }

    [Serializable]
    public class StateData
    {
        public string stateName;
        public int hash;
        public float length;
        public List<AnimEvent> customEvents = new List<AnimEvent>(); // 這裡可以插入多個 Action
    }

    public List<StateData> states = new List<StateData>();
}
