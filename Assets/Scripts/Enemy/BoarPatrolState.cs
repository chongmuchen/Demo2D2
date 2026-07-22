using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        _currentEnemy = enemy;
    }

    public override void LogicUpdate()
    {
        if (_currentEnemy.FoundPlayer())
        {
            _currentEnemy.SwitchState(NPCState.Chase);
        }

        if (!_currentEnemy.physics.isGround || _currentEnemy.physics.touchFrontWall)
        {
            _currentEnemy.wait = true;
            _currentEnemy.anim.SetBool("walk", false);
        }
        else
        {
            _currentEnemy.anim.SetBool("walk", true);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        _currentEnemy.anim.SetBool("walk", false);
    }
}