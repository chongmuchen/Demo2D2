using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physisCheck;
    private CapsuleCollider2D _collider;
    private PlayerAnimation _playerAnimation;

    private Vector2 originalOffset;
    private Vector2 originalSize;

    [Header("基本参数")] public float speed;
    private float walkSpeed;
    private float runSpeed;
    public float jumpForce;
    public float wallJumpForce;
    public Vector2 inputDirection;
    public float slideDistance;
    public float slideSpeed;

    [Header("状态")] public bool isCrouch;
    public bool isDead;
    public bool isHurt;
    public float hurtForce;
    public bool isAttack;
    public bool isSlide;
    public bool wallJumping;
    [Header("物理材质")] public PhysicsMaterial2D normalMaterial;
    public PhysicsMaterial2D wallMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        physisCheck = GetComponent<PhysicsCheck>();
        _collider = GetComponent<CapsuleCollider2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();

        originalOffset = _collider.offset;
        originalSize = _collider.size;

        walkSpeed = speed / 2;
        runSpeed = speed;
        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.Attack.started += PlayAttack;
        inputControl.GamePlay.Slice.started += Slide;
        inputControl.GamePlay.WalkButtom.performed += ctx =>
        {
            if (physisCheck.isGround)
                speed = walkSpeed;
        };
        inputControl.GamePlay.WalkButtom.canceled += ctx =>
        {
            if (physisCheck.isGround)
                speed = runSpeed;
        };
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    // Update is called once per frame
    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        CheckState();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
            Move();
    }

    public void Move()
    {
        if (!isCrouch && !wallJumping)
        {
            rb.linearVelocity = new Vector2(inputDirection.x * speed, rb.linearVelocity.y);
        }

        float faceDir = transform.localScale.x;
        if (inputDirection.x > 0)
        {
            faceDir = 1;
        }
        else if (inputDirection.x < 0)
        {
            faceDir = -1;
        }

        transform.localScale = new Vector3(faceDir, 1f, 1f);
        isCrouch = inputDirection.y < -0.5f && physisCheck.isGround;
        if (isCrouch)
        {
            _collider.offset = new Vector2(-0.05f, 0.85f);
            _collider.size = new Vector2(0.7f, 1.7f);
        }
        else
        {
            _collider.offset = originalOffset;
            _collider.size = originalSize;
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (physisCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        else if (physisCheck.onWall)
        {
            rb.AddForce(new Vector2(-inputDirection.x, 2) * wallJumpForce, ForceMode2D.Impulse);
            wallJumping = true;
        }
    }

    private void PlayAttack(InputAction.CallbackContext ctx)
    {
        if (physisCheck.isGround)
        {
            _playerAnimation.PlayAttack();
            isAttack = true;
        }
    }


    private void Slide(InputAction.CallbackContext obj)
    {
        if (!isSlide)
        {
            isSlide = true;
            var targetPos = new Vector2(transform.position.x + transform.localScale.x * slideDistance,
                transform.position.y);
            StartCoroutine(TriggerSlice(targetPos));
        }
    }

    public IEnumerator TriggerSlice(Vector3 target)
    {
        do
        {
            yield return null;
            if (!physisCheck.isGround)
            {
                break;
            }

            if (physisCheck.touchFrontWall)
            {
                isSlide = false;
                break;
            }

            var nextPos = new Vector2(transform.position.x + transform.localScale.x * slideSpeed,
                transform.position.y);
            rb.MovePosition(nextPos);
        } while (Math.Abs(transform.position.x - target.x) > 0.1);

        isSlide = false;
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.linearVelocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.GamePlay.Disable();
    }

    public void CheckState()
    {
        _collider.sharedMaterial = physisCheck.isGround ? normalMaterial : wallMaterial;
        if (physisCheck.onWall)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y / 2);
        }

        if (wallJumping && rb.linearVelocity.y <= 0f)
        {
            wallJumping = false;
        }
    }
}