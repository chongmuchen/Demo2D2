using System;
using UnityEngine;

public class BeePatrolState : BaseState
{
    public Vector3 targetPos;
    public Vector3 moveDir;

    public override void OnEnter(Enemy enemy)
    {
        _currentEnemy = enemy;
        _currentEnemy.currentSpeed = _currentEnemy.normalSpeed;
        targetPos = _currentEnemy.GetNewPoint();
    }

    public override void LogicUpdate()
    {
        var found = _currentEnemy.FoundPlayer();
        if (found)
        {
            _currentEnemy.SwitchState(NPCState.Chase);
            return;
        }

        if (Math.Abs(targetPos.x - _currentEnemy.transform.position.x) < 0.1 &&
            Math.Abs(targetPos.y - _currentEnemy.transform.position.y) < 0.1)
        {
            _currentEnemy.wait = true;
            targetPos = _currentEnemy.GetNewPoint();
        }

        moveDir = (targetPos - _currentEnemy.transform.position).normalized;

        if (moveDir.x > 0)
        {
            _currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (moveDir.x < 0)
        {
            _currentEnemy.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {
        if (!_currentEnemy.wait && !_currentEnemy.isHurt && !_currentEnemy.isDead)
        {
            _currentEnemy.rb.linearVelocity = moveDir * _currentEnemy.currentSpeed;
        }
        else
        {
            _currentEnemy.rb.linearVelocity = Vector2.zero;
        }
    }

    public override void OnExit()
    {
    }
}