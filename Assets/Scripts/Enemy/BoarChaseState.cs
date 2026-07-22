using UnityEngine;

public class BoarChaseSate : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        _currentEnemy = enemy;
        Debug.Log("Boar Chase");
        _currentEnemy.currentSpeed = _currentEnemy.chaseSpeed;
        _currentEnemy.anim.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        bool touchFrontWall = _currentEnemy.faceDir.x < 0
            ? _currentEnemy.physics.touchLeftWall
            : _currentEnemy.physics.touchRightWall;
        if (!_currentEnemy.physics.isGround || touchFrontWall)
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
