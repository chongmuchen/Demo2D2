using System;
using UnityEngine;

public class BeeChaseState : BaseState
{
    public Vector3 targetPos;
    public Vector3 moveDir;
    private Attack _attack;

    public override void OnEnter(Enemy enemy)
    {
        _currentEnemy = enemy;
        _currentEnemy.currentSpeed = _currentEnemy.chaseSpeed;
        _attack = _currentEnemy.GetComponent<Attack>();
        _currentEnemy.lostTimeCounter = _currentEnemy.lostTime;
    }

    public override void LogicUpdate()
    {
        if (_currentEnemy.lostTimeCounter <= 0)
        {
            _currentEnemy.SwitchState(NPCState.Patrol);
        }

        targetPos = _currentEnemy.attacker.position + new Vector3(0, 1.5f);
        if (Math.Abs(_currentEnemy.transform.position.x - _currentEnemy.attacker.position.x) < _attack.attackRange &&
            Math.Abs(_currentEnemy.transform.position.y - _currentEnemy.attacker.position.y) < _attack.attackRange)
            _currentEnemy.rb.linearVelocity = Vector3.zero;
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
        if (!_currentEnemy.isHurt && !_currentEnemy.isDead)
        {
            _currentEnemy.rb.linearVelocity = moveDir * _currentEnemy.currentSpeed;
        }
    }

    public override void OnExit()
    {
        _currentEnemy.anim.SetBool("run", false);
    }
}