using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PhysicsCheck physics;
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

    private void Awake()
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
        bool touchFrontWall = faceDir.x < 0 ? physics.touchLeftWall : physics.touchRightWall;
        if (touchFrontWall)
        {
            // transform.localScale = new Vector3(faceDir.x, transform.localScale.y, transform.localScale.z);
            wait = true;
            anim.SetBool("walk", false);
        }

        TimeCounter();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead)
        {
            Move();
        }
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

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter();
    }
}