using System;
using UnityEngine;

public class SavePoint : MonoBehaviour, IIteractable
{
    public SpriteRenderer _spriteRenderer;
    public Sprite darkSprite;
    public Sprite lightSprite;
    public bool isDone;
    public VoidEventSO LoadGameEvent;

    public void Awake()
    {
        // _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _spriteRenderer.sprite = isDone ? lightSprite : darkSprite;
    }

    public void TriggerAction()
    {
        if (!isDone)
        {
            isDone = true;
            _spriteRenderer.sprite = lightSprite;
            LoadGameEvent.RaiseEvent();
            this.gameObject.tag = "Untagged";
        }
    }
}