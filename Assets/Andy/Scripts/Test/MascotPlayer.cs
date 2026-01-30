using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
public class MascotPlayer : MonoBehaviour
{
    public AnimDataMap dataMap;
    private Animator _animator;

    // Key: StateHash, Value: 該動畫所有的觸發點清單
    private Dictionary<int, List<ActionPoint>> _allActions = new();

    private class ActionPoint
    {
        public float TriggerTime; // 0~1
        public Action Callback;
        public bool IsTriggered;
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        SetupAnimationActions();
    }

    void Start()
    {

        this.UpdateAsObservable().Subscribe(_ =>
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)) PlayAnimation("A");
            if (Input.GetKeyDown(KeyCode.RightArrow)) PlayAnimation("B");
            if (Input.GetKeyDown(KeyCode.LeftArrow)) PlayAnimation("C");
        });
    }


    void SetupAnimationActions()
    {
        // --- 動畫 A 的多點控制 ---
        InsertAction("A", 0.3f, () => Debug.Log("A 播到 30%"));
        InsertAction("A", 0.6f, () => Debug.Log("A 播到 60%"));
        InsertAction("A", 1.0f, () => OnAnimEnd("A")); // 1.0 就是結束點

        // --- 動畫 B 的控制 ---
        InsertAction("B", 0.5f, () => Debug.Log("B 的中場休息"));
        InsertAction("B", 1.0f, () => OnAnimEnd("B"));
    }

    public void InsertAction(string stateName, float time, Action callback)
    {
        int hash = Animator.StringToHash(stateName);
        if (!_allActions.ContainsKey(hash)) _allActions[hash] = new List<ActionPoint>();

        _allActions[hash].Add(new ActionPoint
        {
            TriggerTime = time,
            Callback = callback
        });
    }

    void Update()
    {
        var info = _animator.GetCurrentAnimatorStateInfo(0);
        if (!_allActions.TryGetValue(info.shortNameHash, out var points)) return;

        // 這裡用 %1.0f 處理 Loop，如果是單次動畫會停在 0.99... 左右
        float progress = info.normalizedTime % 1.0f;

        // 抓取 normalizedTime 的整數部分來判斷是否進入了「新的一輪」
        int currentLoopCount = (int)info.normalizedTime;

        foreach (var p in points)
        {
            // 觸發條件：進度超過設定時間 且 尚未觸發
            if (!p.IsTriggered && progress >= p.TriggerTime)
            {
                p.Callback?.Invoke();
                p.IsTriggered = true;
            }
        }

        // 當動畫進入新的一輪 (Loop) 時，重置所有觸發狀態
        // 或者當切換到不同動畫時 (這部分可由 Play 函式處理)
        if (progress < 0.05f)
        {
            foreach (var p in points) p.IsTriggered = false;
        }
    }

    void OnAnimEnd(string name)
    {
        Debug.Log($"{name} 播完囉，執行接續邏輯");
        // 例如：if (name == "A") Play("B");
    }

    void PlayAnimation(string state)
    {
        _animator.CrossFade(state, 0.1f);
    }
}