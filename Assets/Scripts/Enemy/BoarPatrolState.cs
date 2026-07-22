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

        bool touchFrontWall = _currentEnemy.faceDir.x < 0
            ? _currentEnemy.physics.touchLeftWall
            : _currentEnemy.physics.touchRightWall;
        if (!_currentEnemy.physics.isGround || touchFrontWall)
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
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        _currentEnemy.anim.SetBool("walk", false);
    }
}