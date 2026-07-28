using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
    public static FadeCanvas Instance { get; private set; }

    public Image fadeImage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void FadeIn(float duration)
    {
        fadeImage.DOBlendableColor(Color.black, duration);
    }

    public void FadeOut(float duration)
    {
        fadeImage.DOBlendableColor(Color.clear, duration);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
