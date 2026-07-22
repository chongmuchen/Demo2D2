using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(PhysicsCheck), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicsCheck physics;
    protected BaseState patrolState;
    protected BaseState currentState;
    protected BaseState chaseState;
    protected BaseState skillState;

    public Transform attacker;

    [Header("基本参数")] public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;

    [Header("计时器")] public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;

    [Header("状态")] public bool isHurt;
    public bool isDead;

    [Header("检测")] public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;
    public Vector3 spawnPoint;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physics = GetComponent<PhysicsCheck>();
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
        spawnPoint = transform.position;
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        TimeCounter();
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();
        if (!isDead && !isHurt && !wait)
        {
            Move();
        }
        else
        {
            if (!isHurt)
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
        }
    }

    public virtual void Move()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("PreMove") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("SnailRecover"))
            rb.linearVelocity = new Vector2(faceDir.x * currentSpeed, rb.linearVelocity.y);
    }

    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, transform.localScale.y, transform.localScale.z);
            }
        }

        if (!FoundPlayer())
        {
            lostTimeCounter -= Time.deltaTime;
        }
    }

    public void OnTakeDamage(Transform attackerTrans)
    {
        attacker = attackerTrans;
        if (attackerTrans.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (attackerTrans.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        isHurt = true;
        anim.SetTrigger("hurt");
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void OnDie()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        anim.SetBool("dead", true);
        isDead = true;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }

    public virtual bool FoundPlayer()
    {
        return Physics2D.BoxCast(GetPosByOffset(centerOffset), checkSize, 0f, faceDir, checkDistance, attackLayer);
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.Skill => skillState,
            _ => null
        };
        if (newState != null)
        {
            currentState.OnExit();
            currentState = newState;
            currentState.OnEnter(this);
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (faceDir.x == 0)
        {
            faceDir = new Vector3(-transform.localScale.x, 0, 0);
        }

        Vector2 center = GetPosByOffset(centerOffset) + (Vector2)faceDir * (checkDistance * 0.5f);
        Vector2 size = new Vector2(checkSize.x + checkDistance, checkSize.y);
        Gizmos.DrawWireCube(center, size);
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

    public virtual Vector3 GetNewPoint()
    {
        return transform.position;
    }
}