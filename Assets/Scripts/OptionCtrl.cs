using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// オプション制御
/// </summary>
public class OptionCtrl : MonoBehaviour
{
    /// <summary>
    /// Masterスライダー通知
    /// </summary>
    [SerializeField]
    private SliderNotifier masterSliderNotifier = null;

    /// <summary>
    /// BGMスライダー通知
    /// </summary>
    [SerializeField]
    private SliderNotifier bgmSliderNotifier = null;

    /// <summary>
    /// SEスライダー通知
    /// </summary>
    [SerializeField]
    private SliderNotifier seSliderNotifier = null;

    /// <summary>
    /// Voiceスライダー通知
    /// </summary>
    [SerializeField]
    private SliderNotifier voiceSliderNotifier = null;

    /// <summary>
    /// BGMサンプル
    /// </summary>
    [SerializeField]
    private AudioSource bgmSample = null;

    /// <summary>
    /// SEサンプル
    /// </summary>
    [SerializeField]
    private AudioSource seSample = null;

    /// <summary>
    /// ボイスサンプル
    /// </summary>
    [SerializeField]
    private AudioSource voiceSample = null;

    /// <summary>
    /// 素朴な手法を使うか (true: 使う, false: 使わない)
    /// </summary>
    [SerializeField]
    private bool isNaive = false;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        masterSliderNotifier.OnValueChanging.Subscribe(value =>
        {
            float volume = (isNaive) ? (value) : (SoundUtil.CalcGetVolume(value));
            SoundManager.Instance.SetVolume(SoundCategory.Master, volume);
        });
        bgmSliderNotifier.OnValueChanging.Subscribe(value =>
        {
            float volume = (isNaive) ? (value) : (SoundUtil.CalcGetVolume(value));
            SoundManager.Instance.SetVolume(SoundCategory.Bgm, volume);
        });
        seSliderNotifier.OnValueChanging.Subscribe(value =>
        {
            float volume = (isNaive) ? (value) : (SoundUtil.CalcGetVolume(value));
            SoundManager.Instance.SetVolume(SoundCategory.Se, volume);
        });
        voiceSliderNotifier.OnValueChanging.Subscribe(value =>
        {
            float volume = (isNaive) ? (value) : (SoundUtil.CalcGetVolume(value));
            SoundManager.Instance.SetVolume(SoundCategory.Voice, volume);
        });
        seSliderNotifier.OnValueChanged.Subscribe(value =>
        {
            seSample.Play();
        });
        voiceSliderNotifier.OnValueChanged.Subscribe(value =>
        {
            voiceSample.Play();
        });

        bgmSample.Play();
    }
}
