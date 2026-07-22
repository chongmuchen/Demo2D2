using UnityEngine;

public class Bee : Enemy
{
    [Header("刷新地点")] public float patrolRadius;

    protected override void Awake()
    {
        base.Awake();
        patrolState = new BeePatrolState();
        chaseState = new BeeChaseState();
    }

    public override bool FoundPlayer()
    {
        var obj = Physics2D.OverlapCircle(GetPosByOffset(centerOffset), checkDistance, attackLayer);
        if (obj)
        {
            attacker = obj.transform;
        }

        return obj;
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(GetPosByOffset(centerOffset), checkDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPoint, patrolRadius);
    }

    public override Vector3 GetNewPoint()
    {
        var targetX = Random.Range(-patrolRadius, patrolRadius);
        var targetY = Random.Range(-patrolRadius, patrolRadius);
        return spawnPoint + new Vector3(targetX, targetY, 0f);
    }

    public override void Move()
    {
    }
}