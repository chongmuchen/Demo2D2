using System;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D _collider;
    private PlayerController _playerController;
    private Rigidbody2D _rb;
    public LayerMask groundLayer;
    [Header("参数检测")] public bool manual;
    public bool isPlayer;
    public float checkRaduis;

    public Vector2 bottomOffset;
    public Vector2 frontOffset;
    public Vector2 backOffset;

    [Header("状态")] public bool isGround;
    public bool onWall;
    public bool touchFrontWall;
    public bool touchBackWall;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        if (!manual)
        {
            frontOffset = new Vector2(-(_collider.bounds.size.x + _collider.offset.x) / 2, _collider.bounds.size.y / 2);
            backOffset = new Vector2(-frontOffset.x, backOffset.y);
        }

        if (isPlayer)
        {
            _playerController = GetComponent<PlayerController>();
        }
    }

    // Update is calle d once per frame
    void Update()
    {
        Check();
    }

    void Check()
    {
        isGround = Physics2D.OverlapCircle(GetPosByOffset(bottomOffset), checkRaduis, groundLayer);
        touchFrontWall = Physics2D.OverlapCircle(GetPosByOffset(frontOffset), checkRaduis, groundLayer);
        touchBackWall = Physics2D.OverlapCircle(GetPosByOffset(backOffset), checkRaduis, groundLayer);
        if (isPlayer)
        {
            var forward = (_playerController.inputDirection.x > 0 && transform.localScale.x > 0) ||
                          (_playerController.inputDirection.x < 0 && transform.localScale.x < 0);
            onWall = (touchFrontWall || touchBackWall) && _rb.linearVelocity.y < 0 && forward;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(GetPosByOffset(bottomOffset), checkRaduis);
        Gizmos.DrawWireSphere(GetPosByOffset(frontOffset), checkRaduis);
        Gizmos.DrawWireSphere(GetPosByOffset(backOffset), checkRaduis);
    }

    public Vector2 GetPosByOffset(Vector2 offset)
    {
        var dirX = 1;
        if (transform.localScale.x < 0)
        {
            dirX = -1;
        }

        return (Vector2)transform.position + new Vector2(offset.x * dirX, offset.y);
    }
}