using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class ModelAnimationController : MonoBehaviour
{
    public Animator animator;  // 模型上的 Animator 組件
    public AnimationClip clip; // 你想播放的動畫片段
    private PlayableGraph _playableGraph;

    void Start()
    {
        // 創建 PlayableGraph
        _playableGraph = PlayableGraph.Create("ModelAnimationGraph");

        // 創建 AnimationPlayableOutput 並連接到 Animator
        var output = AnimationPlayableOutput.Create(_playableGraph, "Animation", animator);

        // 創建 AnimationClipPlayable，將動畫片段載入 Playable
        var clipPlayable = AnimationClipPlayable.Create(_playableGraph, clip);

        // 將 AnimationClipPlayable 連接到輸出
        output.SetSourcePlayable(clipPlayable);

        // 啟動 PlayableGraph
        _playableGraph.Play();
    }

    void OnDestroy()
    {
        // 銷毀 PlayableGraph 以釋放資源
        _playableGraph.Destroy();
    }
}