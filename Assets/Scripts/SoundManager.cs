using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// サウンドマネージャー
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    /// <summary>
    /// マスターボリューム名
    /// </summary>
    private const string MasterVolumeName = "MasterVolume";

    /// <summary>
    /// BGMボリューム名
    /// </summary>
    private const string BgmVolumeName = "BgmVolume";

    /// <summary>
    /// SEボリューム名
    /// </summary>
    private const string SeVolumeName = "SeVolume";

    /// <summary>
    /// ボイスボリューム名
    /// </summary>
    private const string VoiceVolumeName = "VoiceVolume";

    /// <summary>
    /// AudioMixer
    /// </summary>
    [SerializeField]
    private AudioMixer audioMixer = null;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public SoundManager()
    {
        
    }

    /// <summary>
    /// ボリュームの取得
    /// </summary>
    /// <param name="soundCategory">サウンドカテゴリ</param>
    /// <returns>ボリューム (0-1)</returns>
    public float GetVolume(SoundCategory soundCategory)
    {
        string volumeName = null;
        switch (soundCategory)
        {
            case SoundCategory.Master: volumeName = MasterVolumeName; break;
            case SoundCategory.Bgm: volumeName = BgmVolumeName; break;
            case SoundCategory.Se: volumeName = SeVolumeName; break;
            case SoundCategory.Voice: volumeName = VoiceVolumeName; break;
            default: Debug.LogWarning($"Invalid SoundCategory. [soundCategory={soundCategory}]"); break;
        }

        if (!string.IsNullOrEmpty(volumeName))
        {
            audioMixer.GetFloat(volumeName, out float dbVolume);
            float volume = SoundUtil.CalcGetDbToVolume(dbVolume);
            return volume;
        }
        else
        {
            return 0.0f;
        }
    }

    /// <summary>
    /// ボリュームの設定
    /// </summary>
    /// <param name="soundCategory">サウンドカテゴリ</param>
    /// <param name="volume">ボリューム (0-1)</param>
    public void SetVolume(SoundCategory soundCategory, float volume)
    {
        string volumeName = null;
        switch (soundCategory)
        {
            case SoundCategory.Master: volumeName = MasterVolumeName; break;
            case SoundCategory.Bgm: volumeName = BgmVolumeName; break;
            case SoundCategory.Se: volumeName = SeVolumeName; break;
            case SoundCategory.Voice: volumeName = VoiceVolumeName; break;
            default: Debug.LogWarning($"Invalid SoundCategory. [soundCategory={soundCategory}]"); break;
        }

        if (!string.IsNullOrEmpty(volumeName))
        {
            float dbVolume = SoundUtil.CalcGetVolumeToDb(volume);
            audioMixer.SetFloat(volumeName, dbVolume);
        }
    }
}
