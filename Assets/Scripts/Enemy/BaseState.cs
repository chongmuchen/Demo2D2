public abstract class BaseState
{
    public abstract void OnEnter();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void OnExit();
}