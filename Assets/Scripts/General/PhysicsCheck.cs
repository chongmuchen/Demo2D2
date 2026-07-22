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
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}