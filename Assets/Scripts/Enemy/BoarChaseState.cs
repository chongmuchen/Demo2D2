using UnityEngine;

public class BoarChaseSate : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        _currentEnemy = enemy;
        _currentEnemy.currentSpeed = _currentEnemy.chaseSpeed;
        _currentEnemy.anim.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        if (!_currentEnemy.physics.isGround || _currentEnemy.physics.touchFrontWall)
        {
            _currentEnemy.transform.localScale = new Vector3(_currentEnemy.faceDir.x, 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
    }
}