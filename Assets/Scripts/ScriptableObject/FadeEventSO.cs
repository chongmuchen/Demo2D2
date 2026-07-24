using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FadeEventSO", menuName = "Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public UnityAction<Color, float, bool> OnEventRaised;

    public void FadeIn(float duration)
    {
        RaiseEvent(Color.black, duration, true);
    }


    public void FadeOut(float duration)
    {
        RaiseEvent(Color.clear, duration, false);
    }

    public void RaiseEvent(Color targetColor, float duration, bool fadeIn)
    {
        OnEventRaised?.Invoke(targetColor, duration, fadeIn);
    }
}