using System;
using UnityEngine;

public class Boar : Enemy
{
    protected virtual void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseSate();
    }
}