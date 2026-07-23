using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioEventSO audioSO;
    public AudioClip clip;
    public bool PlayOnEnable;

    public void OnEnable()
    {
        if (PlayOnEnable)
        {
            PlayAudioClip();
        }
    }

    public void PlayAudioClip()
    {
        audioSO.OnEventRaised(clip);
    }
}