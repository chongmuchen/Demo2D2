using System;
using UnityEngine;

public class Chest : MonoBehaviour, IIteractable
{
    public SpriteRenderer spriteRenderer;

    public Sprite openSprite;
    public Sprite closedSprite;
    public bool isDone;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? openSprite : closedSprite;
    }

    public void TriggerAction()
    {
        if (!isDone)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isDone = true;
        spriteRenderer.sprite = openSprite;
        this.gameObject.tag = "Untagged";
    }
}