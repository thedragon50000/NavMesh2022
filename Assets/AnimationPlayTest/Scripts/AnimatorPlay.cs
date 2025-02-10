using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AnimatorPlay : MonoBehaviour

{
    private Animator _animator;
    // AnimatorStateInfo stateInfo;


    // 將動畫狀態名稱的哈希值存儲為靜態常量
    private static readonly int IdleStateHash = Animator.StringToHash("A");
    private static readonly int RunStateHash = Animator.StringToHash("B");
    private static readonly int JumpStateHash = Animator.StringToHash("C");
    private static readonly int Idle = Animator.StringToHash("New State");

    private readonly Dictionary<int, string> _stateAnim = new();

    private void Awake()
    {
        _stateAnim.Add(IdleStateHash, "A");
        _stateAnim.Add(RunStateHash, "B");
        _stateAnim.Add(JumpStateHash, "C");
        _stateAnim.Add(Idle, "New State");
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        
        Observable.EveryUpdate()
            .Select(_ => _animator.GetCurrentAnimatorStateInfo(0))
            .Where(_ => _.normalizedTime >= 1.0f)   // 最好再加一個bool
            .Subscribe(_ => { Debug.Log("動畫播放完畢！"); });
        // Observable.EveryUpdate()
        //     .Where(_ => Input.GetKeyDown(KeyCode.LeftArrow))
        //     .Take(1) // 只執行一次，防止多次觸發
        //     .Subscribe(_ => { animator.Play(JumpStateHash, 0, 0); });
    }

    void Update()
    {
        
        // 根據輸入直接播放哈希值對應的動畫
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _animator.Play(IdleStateHash, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _animator.Play(RunStateHash, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _animator.Play(JumpStateHash, 0, 0);
        }
    }

    // 當動畫狀態退出時調用
    public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _stateAnim.TryGetValue(stateInfo.shortNameHash, out var state);
        OnAnimationComplete(state);
    }

    // 自定義回調處理邏輯
    private void OnAnimationComplete(string animationName)
    {
        // 在這裡執行動畫結束後的任務
        Debug.Log($"Animation {animationName} completed!");

        _animator.Play(Idle);
    }
}