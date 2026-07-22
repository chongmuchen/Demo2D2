using System;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicsCheck physics;
    protected BaseState patrolState;
    protected BaseState currentState;
    protected BaseState chaseState;

    public Transform attacker;

    [Header("基本参数")] public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public float hurtForce;

    [Header("计时器")] public float waitTime;
    public float waitTimeCounter;
    public bool wait;

    [Header("状态")] public bool isHurt;
    public bool isDead;

    [Header("检测")] public Vector2 centerOffset;
    public Vector2 checkSize;
    public Vector2 checkDistance;
    public LayerMask attackLayer;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physics = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        TimeCounter();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait)
        {
            Move();
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        currentState.PhysicsUpdate();
    }

    public virtual void Move()
    {
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

    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, checkDistance, attackLayer);
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
            _ => null
        };
        if (newState != null)
        {
            currentState.OnExit();
            currentState = newState;
            currentState.OnEnter(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset, 0.2f);
    } 
}