using UnityEngine;

public class Bee : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseState();
    } 
}