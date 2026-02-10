using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainAudioManager : MonoBehaviour
{
    // 背景音乐及音效音频
    public AudioSource[] bgmAudio;
    public AudioSource[] effectsAudio;
    // 音量控制滑动条
    public Slider bgmSlider;
    public Slider effectsSlider;

    void Update()
    {
        // 控制所有背景音乐的音量
        foreach (AudioSource audioSource in bgmAudio)
        {
            if (audioSource != null)
            {
                audioSource.volume = bgmSlider.value;
            }
        }

        // 控制所有音效的音量
        foreach (AudioSource audioSource in effectsAudio)
        {
            if (audioSource != null)
            {
                audioSource.volume = effectsSlider.value;
            }
        }
    }
}
