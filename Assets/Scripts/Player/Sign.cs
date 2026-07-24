using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class Sign : MonoBehaviour
{
    private PlayerInputControl _playerInputControl;
    private Animator anim;
    public GameObject spireSign;
    public Transform playerTrans;
    private bool canPress;
    private IIteractable targetItem;

    private void Awake()
    {
        anim = spireSign.GetComponent<Animator>();
        _playerInputControl = new PlayerInputControl();
        _playerInputControl.Enable();
        _playerInputControl.GamePlay.Confirm.started += OnConfirm;
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
    }

    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if (canPress)
        {
            targetItem.TriggerAction();
            GetComponent<AudioDefination>()?.PlayAudioClip();
        }
    }


    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        if (actionChange == InputActionChange.ActionStarted)
        {
            var d = ((InputAction)obj).activeControl.device;
            switch (d.device)
            {
                case Keyboard:
                    anim.Play("Keyboard");
                    break;
                case DualShockGamepad:
                    anim.Play("ps");
                    break;
            }
        }
    }

    private void Update()
    {
        spireSign.GetComponent<SpriteRenderer>().enabled = canPress;
        spireSign.transform.localScale = playerTrans.localScale;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = other.GetComponent<IIteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canPress = false;
    }
}