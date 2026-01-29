using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.Playables;
using UnityEngine.Timeline;
// using DG

public class ModelAnimationController : MonoBehaviour
{
    public Animator animator;  // 模型上的 Animator 組件
    // 你想播放的動畫片段
    public AnimationClip clip0;
    public AnimationClip clip1;

    public AudioClip walkSfx;
    public AudioSource audio;

    private PlayableGraph _playableGraph;

    void Start()
    {
        // 創建 PlayableGraph
        _playableGraph = PlayableGraph.Create("ModelAnimationGraph");

        // OneMotion();
        TwoMotionMixer();
        // AudioPlay();
    }

    private void AudioPlay()
    {
        // Warning:沒有資源，不能播
        // 正確的寫法是這樣：
        var audioClipPlayable = AudioClipPlayable.Create(_playableGraph, walkSfx, true); // true 代表循環播放

        // 就像動畫一樣，它也需要一個 Output 接出去
        var output2 = AudioPlayableOutput.Create(_playableGraph, "AudioOut", audio);
        output2.SetSourcePlayable(audioClipPlayable);
    }

    private void OneMotion()
    {
        // 創建 AnimationPlayableOutput 並連接到 Animator
        var output = AnimationPlayableOutput.Create(_playableGraph, "Animation", animator);

        // 創建 AnimationClipPlayable，將動畫片段載入 Playable
        var clipPlayable = AnimationClipPlayable.Create(_playableGraph, clip1);

        // 將 AnimationClipPlayable 連接到輸出
        output.SetSourcePlayable(clipPlayable);

        // 啟動 PlayableGraph
        _playableGraph.Play();
    }

    private void TwoMotionMixer()
    {
        // 1. 創建 Mixer
        var mixer = AnimationMixerPlayable.Create(_playableGraph, 2); // 準備 2 個插槽

        // 2.創建 AnimationPlayableOutput 並連接到 Animator
        var output = AnimationPlayableOutput.Create(_playableGraph, "Animation", animator);

        // 3. 創建兩捲錄影帶並插到 Mixer 上
        var idleClip = AnimationClipPlayable.Create(_playableGraph, clip0);
        var walkClip = AnimationClipPlayable.Create(_playableGraph, clip1);

        _playableGraph.Connect(idleClip, 0, mixer, 0); // 錄影帶 0 號插到 Mixer 的 0 號口
        _playableGraph.Connect(walkClip, 0, mixer, 1); // 錄影帶 1 號插到 Mixer 的 1 號口

        // 4. 把 Mixer 接到螢幕 (Output)
        output.SetSourcePlayable(mixer);

        // 5. 動態調權重 (這就是你可以發揮騷操作的地方)
        mixer.SetInputWeight(0, 0.5f); // Idle 佔 50%
        mixer.SetInputWeight(1, 0.5f); // Walk 佔 50%

        // // 也可以用DoTween做動態權重
        // DOVirtual.Float(0f, 1f, 1f, (v) =>
        // {
        //     mixer.SetInputWeight(0, 1f - v); // 逛街權重 1 -> 0
        //     mixer.SetInputWeight(1, v);      // 抽獎權重 0 -> 1
        // }).SetEase(Ease.InOutQuad);

        // 啟動 PlayableGraph
        _playableGraph.Play();
    }

    void OnDestroy()
    {
        // 銷毀 PlayableGraph 以釋放資源
        _playableGraph.Destroy();
    }
}