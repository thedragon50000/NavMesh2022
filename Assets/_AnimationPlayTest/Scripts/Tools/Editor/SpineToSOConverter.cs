using UnityEngine;
using UnityEditor;
using Spine;
using Spine.Unity;
using System.Collections.Generic;

public class SpineToSOConverter : EditorWindow
{
    public AnimationDB targetSO;

    [MenuItem("Tools/Bingo/Spine動畫自動彙整")]
    public static void ShowWindow()
    {
        GetWindow<SpineToSOConverter>("Spine 轉換工具");
    }

    private void OnGUI()
    {
        targetSO = (AnimationDB)EditorGUILayout.ObjectField("目標 SO 檔案", targetSO, typeof(AnimationDB), false);

        if (GUILayout.Button("抓取選中物件的動畫並存入 SO"))
        {
            ExportAnimations();
        }
    }

    private void ExportAnimations()
    {
        if (targetSO == null) { Debug.LogError("寶，先拉入目標 SO 呀！"); return; }

        // 取得目前在 Hierarchy 選中的物件
        GameObject selected = Selection.activeGameObject;
        if (selected == null) { Debug.LogError("沒選中物件喔！"); return; }

        SkeletonAnimation sa = selected.GetComponent<SkeletonAnimation>();
        if (sa == null) { Debug.LogError("選中的物件沒有 SkeletonAnimation！"); return; }

        // 取得 Spine 所有的動畫資料
        var skeletonData = sa.SkeletonDataAsset.GetSkeletonData(true);
        targetSO.animList.Clear();

        foreach (Spine.Animation anim in skeletonData.Animations)
        {
            targetSO.animList.Add(new AnimationDB.AnimInfo
            {
                key = anim.Name, // 預設 Key 跟動畫名一樣，你可以之後手改
                spineAnimName = anim.Name,
                loop = anim.Name.Contains("idle") // 聰明小功能：如果是 idle 就預設 loop
            });
        }

        // 標記 SO 已修改，這樣 Unity 才會存檔
        EditorUtility.SetDirty(targetSO);
        AssetDatabase.SaveAssets();

        Debug.Log($"<color=green>成功！已從 {selected.name} 匯入 {skeletonData.Animations.Count} 個動畫到 SO！</color>");
    }
}