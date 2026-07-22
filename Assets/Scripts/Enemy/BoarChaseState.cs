using UnityEngine;

public class BoarChaseSate : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        _currentEnemy = enemy;
        Debug.Log("Boar Chase");
    }

    public override void LogicUpdate()
    {
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
    }
}