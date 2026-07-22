using System;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D _collider;
    public LayerMask groundLayer;
    [Header("参数检测")] public bool manual;
    public float checkRaduis;

    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    [Header("状态")] public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            rightOffset = new Vector2((_collider.bounds.size.x + _collider.offset.x) / 2, _collider.bounds.size.y / 2);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }

    // Update is calle d once per frame
    void Update()
    {
        Check();
    }

    void Check()
    {
        isGround = Physics2D.OverlapCircle(GetPositionByOffset(bottomOffset), checkRaduis, groundLayer);
        touchLeftWall = Physics2D.OverlapCircle(GetPositionByOffset(leftOffset), checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle(GetPositionByOffset(rightOffset), checkRaduis, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(GetPositionByOffset(bottomOffset), checkRaduis);
        Gizmos.DrawWireSphere(GetPositionByOffset(leftOffset), checkRaduis);
        Gizmos.DrawWireSphere(GetPositionByOffset(rightOffset), checkRaduis);
    }

    private Vector2 GetPositionByOffset(Vector2 offset)
    {
        var dirX = 1;
        if (transform.localScale.x < 0)
        {
            dirX = -1;
        }
        return (Vector2)transform.position + new Vector2(offset.x * dirX, offset.y);
    }
}