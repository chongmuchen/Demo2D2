using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayAudioEventSO", menuName = "Event/PlayAudioEventSO")]
public class PlayAudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;

    public void RaiseEvent(AudioClip clip)
    {
        OnEventRaised?.Invoke(clip);
    }
}