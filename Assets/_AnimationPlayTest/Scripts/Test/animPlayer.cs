using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using TMPro;
using DG.Tweening;
using System.Linq;
public class animPlayer : baseCharacterAnimation
{

    void Start()
    {
        this.UpdateAsObservable().Subscribe(_ =>
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                PlayAnimation("A");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                PlayAnimation("B");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PlayAnimation("C");
            }
        });
    }


    protected override void SetupAnimationActions()
    {
        // --- 動畫 A 的多點控制 ---
        InsertAction("A", 0.3f, () => Debug.Log("A 播到 30%"));
        InsertAction("A", 0.6f, () => Debug.Log("A 播到 60%"));
        InsertAction("A", 0.4f, () => { bPlayNextMoveLock = false; });
        InsertAction("A", 1.0f, () => OnAnimEnd("A")); // 1.0 就是結束點

        // --- 動畫 B 的控制 ---
        InsertAction("B", 0.5f, () => Debug.Log("B 的中場休息"));
        InsertAction("B", 0.4f, () => { bPlayNextMoveLock = false; });
        InsertAction("B", 1.0f, () => OnAnimEnd("B"));

        InsertAction("C", 0.3f, () => { bPlayNextMoveLock = false; });
        InsertAction("C", 1.0f, () => OnAnimEnd("C"));

    }
}