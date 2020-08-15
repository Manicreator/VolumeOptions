using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// スライダー通知
/// </summary>
public class SliderNotifier : MonoBehaviour, IPointerUpHandler
{
    /// <summary>
    /// デフォルトの値
    /// </summary>
    public const float DefaultValue = 0.8f;

    /// <summary>
    /// スライダー
    /// </summary>
    private Slider Slider { get; set; }

    /// <summary>
    /// 値が変更された
    /// </summary>
    private Subject<float> onValueChanged = new Subject<float>();

    /// <summary>
    /// 値が変更された
    /// </summary>
    public IObservable<float> OnValueChanged => onValueChanged;

    /// <summary>
    /// 値を変更中
    /// </summary>
    private Subject<float> onValueChanging = new Subject<float>();

    /// <summary>
    /// 値を変更中
    /// </summary>
    public IObservable<float> OnValueChanging => onValueChanging;

    /// <summary>
    /// 入力フィールド
    /// </summary>
    [SerializeField]
    private InputField inputField = null;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        Debug.Log(gameObject.name);
        Slider = GetComponent<Slider>();
        Slider.onValueChanged.AddListener(value => onValueChanging.OnNext(value));
        Slider.onValueChanged.AddListener(ApplySliderValue);
        inputField.onEndEdit.AddListener(ApplyInputFieldText);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        Slider.value = DefaultValue;
    }

    /// <summary>
    /// マウスボタンを離した
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        onValueChanged.OnNext(Slider.value);
    }

    /// <summary>
    /// 入力フィールドのテキストを適用
    /// </summary>
    /// <param name="text">入力フィールドのテキスト</param>
    private void ApplyInputFieldText(string text)
    {
        float value = float.Parse(text);
        Slider.value = value;
    }

    /// <summary>
    /// スライダーの値を適用
    /// </summary>
    /// <param name="value">スライダーの値</param>
    public void ApplySliderValue(float value)
    {
        inputField.text = value.ToString();
    }
}
