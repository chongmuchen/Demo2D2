using System;
using UnityEngine;

public class BeeChaseState : BaseState
{
    public bool isAttack;
    public Vector3 targetPos;
    public Vector3 moveDir;
    private Attack _attack;
    [Header("状态")] public float attackTimeCounter;

    public override void OnEnter(Enemy enemy)
    {
        _currentEnemy = enemy;
        _currentEnemy.currentSpeed = _currentEnemy.chaseSpeed;
        _attack = _currentEnemy.GetComponent<Attack>();
        _currentEnemy.lostTimeCounter = _currentEnemy.lostTime;
        _currentEnemy.anim.SetBool("chase", true);
    }

    public override void LogicUpdate()
    {
        if (_currentEnemy.lostTimeCounter <= 0)
        {
            _currentEnemy.SwitchState(NPCState.Patrol);
        }

        targetPos = _currentEnemy.attacker.position + new Vector3(0, 0.8f);
        if (Math.Abs(_currentEnemy.transform.position.x - targetPos.x) < _attack.attackRange &&
            Math.Abs(_currentEnemy.transform.position.y - targetPos.y) < _attack.attackRange)
        {
            isAttack = true;
            if (!_currentEnemy.isHurt)
            {
                _currentEnemy.rb.linearVelocity = Vector3.zero;
            }

            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter < 0)
            {
                _currentEnemy.anim.SetTrigger("attack");
                attackTimeCounter = _attack.attackRate;
            }
        }
        else
        {
            isAttack = false;
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
        if (!isAttack && !_currentEnemy.isHurt && !_currentEnemy.isDead)
        {
            _currentEnemy.rb.linearVelocity = moveDir * _currentEnemy.currentSpeed;
        }
    }

    public override void OnExit()
    {
        _currentEnemy.anim.SetBool("chase", false);
    }
}