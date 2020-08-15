using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// サウンドユーティリティ
/// </summary>
public static class SoundUtil
{
    /// <summary>
    /// ボリュームをデシベルに変換して取得
    /// </summary>
    /// <param name="vol">ボリューム (0.0-1.0)</param>
    /// <returns>ボリューム [dB]</returns>
    public static float CalcGetVolumeToDb(float vol)
    {
        if (vol <= 0.0f)
        {
            return float.MinValue;
        }
        else
        {
            return 20.0f * Mathf.Log10(Mathf.Min(vol, 1.0f));
        }
    }

    /// <summary>
    /// デシベルをボリュームに変換して取得
    /// </summary>
    /// <param name="dbVol">ボリューム [dB]</param>
    /// <returns>ボリューム (0.0-1.0)</returns>
    public static float CalcGetDbToVolume(float dbVol)
    {
        return Mathf.Pow(10.0f, Mathf.Min(dbVol, 0.0f) / 20.0f);
    }

    /// <summary>
    /// ラウドネスを考慮したボリュームを計算して取得
    /// </summary>
    /// <remarks>
    /// volLvRatioが半分のときに10dB差がつくボリュームを返す
    ///
    /// volLvRatio  volume  db
    /// 0.00f       0.000f     -inf[dB]
    /// 0.05f       0.007f  -43.219[dB]
    /// 0.10f       0.022f  -33.219[dB]
    /// 0.15f       0.043f  -27.370[dB]
    /// 0.20f       0.069f  -23.219[dB]
    /// 0.25f       0.100f  -20.000[dB]
    /// 0.30f       0.135f  -17.370[dB]
    /// 0.35f       0.175f  -15.146[dB]
    /// 0.40f       0.218f  -13.219[dB]
    /// 0.45f       0.265f  -11.520[dB]
    /// 0.50f       0.316f  -10.000[dB]
    /// 0.55f       0.370f   -8.625[dB]
    /// 0.60f       0.428f   -7.370[dB]
    /// 0.65f       0.489f   -6.215[dB]
    /// 0.70f       0.553f   -5.146[dB]
    /// 0.75f       0.620f   -4.150[dB]
    /// 0.80f       0.690f   -3.219[dB]
    /// 0.85f       0.763f   -2.345[dB]
    /// 0.90f       0.839f   -1.520[dB]
    /// 0.95f       0.918f   -0.740[dB]
    /// 1.00f       1.000f    0.000[dB]
    /// </remarks>
    /// <param name="volLvRatio">ボリュームレベル比率 (0.0-1.0)</param>
    /// <returns>ラウドネスを考慮したボリューム (0.0-1.0)</returns>
    public static float CalcGetVolume(float volLvRatio)
    {
        if (volLvRatio <= 0.0f)
        {
            return 0.0f;
        }
        else if (volLvRatio >= 1.0f)
        {
            return 1.0f;
        }
        else
        {
            return Mathf.Pow(10.0f, -Mathf.Log(1.0f / volLvRatio, 2.0f) / 2.0f);
        }
    }

    /// <summary>
    /// ラウドネスを考慮したボリュームを計算して取得 (AudioMixer用)
    /// </summary>
    /// <remarks>
    /// volLvRatioが半分のときに10dB差がつくボリュームを返す
    ///
    /// volLvRatio  db
    /// 0.00f          -inf[dB]
    /// 0.05f       -43.219[dB]
    /// 0.10f       -33.219[dB]
    /// 0.15f       -27.370[dB]
    /// 0.20f       -23.219[dB]
    /// 0.25f       -20.000[dB]
    /// 0.30f       -17.370[dB]
    /// 0.35f       -15.146[dB]
    /// 0.40f       -13.219[dB]
    /// 0.45f       -11.520[dB]
    /// 0.50f       -10.000[dB]
    /// 0.55f        -8.625[dB]
    /// 0.60f        -7.370[dB]
    /// 0.65f        -6.215[dB]
    /// 0.70f        -5.146[dB]
    /// 0.75f        -4.150[dB]
    /// 0.80f        -3.219[dB]
    /// 0.85f        -2.345[dB]
    /// 0.90f        -1.520[dB]
    /// 0.95f        -0.740[dB]
    /// 1.00f         0.000[dB]
    /// </remarks>
    /// <param name="volLvRatio">ボリュームレベル比率 (0.0-1.0)</param>
    /// <returns>ラウドネスを考慮したボリューム [dB] (-80.0-0.0)</returns>
    public static float CalcGetVolumeForAudioMixer(float volLvRatio)
    {
        if (volLvRatio <= 0.0f)
        {
            return -80.0f;
        }
        else if (volLvRatio >= 1.0f)
        {
            return 0.0f;
        }
        else
        {
            return -10.0f * Mathf.Log(1.0f / volLvRatio, 2.0f);
        }
    }
}
