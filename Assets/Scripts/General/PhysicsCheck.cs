using System;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D _collider;
    public LayerMask groundLayer;
    [Header("参数检测")] public bool manual;
    public float checkRaduis;

    public Vector2 bottomOffset;
    public Vector2 frontOffset;

    [Header("状态")] public bool isGround;
    public bool touchFrontWall;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            frontOffset = new Vector2(-(_collider.bounds.size.x + _collider.offset.x) / 2, _collider.bounds.size.y / 2);
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
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(GetPosByOffset(bottomOffset), checkRaduis);
        Gizmos.DrawWireSphere(GetPosByOffset(frontOffset), checkRaduis);
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