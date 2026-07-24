using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
    public Image fadeImage;
    public FadeEventSO fadeEventSO;

    private void OnFadeEvent(Color color, float duration, bool fadeIn)
    {
        fadeImage.DOBlendableColor(color, duration);
    }

    private void OnEnable()
    {
        fadeEventSO.OnEventRaised += OnFadeEvent;
    }

    private void OnDisable()
    {
        fadeEventSO.OnEventRaised -= OnFadeEvent;
    }
}