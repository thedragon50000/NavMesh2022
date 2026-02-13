using UnityEngine;
using UnityEditor;
using Spine.Unity;

// 這會幫所有掛著 SkeletonAnimation 的物件加上自訂介面
[CustomEditor(typeof(SkeletonAnimation))]
public class SpineTestEditor : Editor
{
    private string _testAnimName = "idle";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // 保留原本的 Spine 介面

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("寶的測試工具", EditorStyles.boldLabel);

        _testAnimName = EditorGUILayout.TextField("動畫名稱", _testAnimName);

        if (GUILayout.Button("立即播放測試"))
        {
            var sa = (SkeletonAnimation)target;
            if (sa.AnimationState != null)
            {
                sa.AnimationState.SetAnimation(0, _testAnimName, true);
                Debug.Log($"<color=cyan>正在測試播放：{_testAnimName}</color>");
            }
        }
    }
}