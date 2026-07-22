using UnityEngine;

public class SnailSkillState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        _currentEnemy = enemy;
        _currentEnemy.currentSpeed = _currentEnemy.chaseSpeed;
        _currentEnemy.anim.SetBool("hide", true);
        _currentEnemy.anim.SetTrigger("skill");
        _currentEnemy.lostTimeCounter = _currentEnemy.lostTime;
        _currentEnemy.GetComponent<Character>().invulnerable = true;
        _currentEnemy.GetComponent<Character>().invulnerableCounter =
            _currentEnemy.lostTimeCounter;
    }

    public override void LogicUpdate()
    {
        if (_currentEnemy.lostTimeCounter <= 0)
        {
            _currentEnemy.SwitchState(NPCState.Patrol);
        }
        _currentEnemy.GetComponent<Character>().invulnerableCounter =
            _currentEnemy.lostTimeCounter;
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        _currentEnemy.anim.SetBool("hide", false);
        _currentEnemy.GetComponent<Character>().invulnerable = false;
    }
}