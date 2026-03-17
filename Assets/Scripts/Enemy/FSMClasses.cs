using UnityEngine;

public class FSMClasses : MonoBehaviour
{
    State currentState { get; set; }

    private PatrolState patrolState;
    private PursuitState pursuitState;

    public State _currentState { get { return currentState; } set { currentState = value; } }

    private void Awake()
    {
        patrolState = new PatrolState(this);
        pursuitState = new PursuitState(this);

        currentState = patrolState;
    }

    public void ChangeToPatrol() 
    {
        ChangeState(patrolState);
    }

    public void ChangeToPursuit() 
    {
        ChangeState(pursuitState);
    }

    public void ChangeState (State newState) 
    {
        if (currentState == newState)
        {
            return;
        }

        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void UpdateState(bool con) 
    {
        currentState.Update(con);
    }
}

public abstract class State 
{
    protected FSMClasses fsm;
    public State (FSMClasses fsm) 
    { 
        this.fsm = fsm;
    }

    public virtual void Enter () { }

    public virtual void Exit () { }

    public abstract void Update(bool canSeePlayer);
}

public class PatrolState : State
{ 
    public PatrolState (FSMClasses fsm) : base(fsm) { }

    public override void Enter () { }
    public override void Exit () { }
    public override void Update (bool canSeePlayer) 
    {
        if (canSeePlayer == true) 
        {
            fsm.ChangeToPursuit();
        }
    }
}

public class PursuitState : State
{
    public PursuitState (FSMClasses fsm) : base(fsm) { }

    public override void Enter() { }
    public override void Exit() { }
    public override void Update(bool canSeePlayer)
    {
        if (canSeePlayer != true)
        {
            fsm.ChangeToPatrol();
        }
    }
}
