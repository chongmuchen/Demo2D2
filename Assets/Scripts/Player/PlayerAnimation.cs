using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PhysicsCheck physisCheck;
    private PlayerController _playerController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physisCheck = GetComponent<PhysicsCheck>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        SetAnimation();
    }

    public void PlayHurt()
    {
        animator.SetTrigger("hurt");
    }


    public void PlayAttack()
    {
        animator.SetTrigger("attack");
    }


    public void SetAnimation()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("velocityY", rb.linearVelocity.y);
        animator.SetBool("isGround", physisCheck.isGround);
        animator.SetBool("isCrouch", _playerController.isCrouch);
        animator.SetBool("isDead", _playerController.isDead);
        animator.SetBool("isAttack", _playerController.isAttack);
        animator.SetBool("onWall", physisCheck.onWall);
        animator.SetBool("isSlide", _playerController.isSlide);
    }
}