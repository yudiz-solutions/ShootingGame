using UnityEngine;

public enum EnemyState
{
    Patrolling,
    Attacking,
    Death
}

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyState CurrentState { get; private set; }

    public IEnemyState currentState;
    private RoamingNPC enemyAI;

    private void Awake()
    {
        enemyAI = GetComponent<RoamingNPC>();
    }

    private void Start()
    {
        TransitionToState(EnemyState.Patrolling);
    }

    public void TransitionToState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        CurrentState = newState;

        switch (newState)
        {
            case EnemyState.Patrolling:
                currentState = new PatrollingState(this, enemyAI);
                break;
            case EnemyState.Attacking:
                currentState = new AttackingState(this, enemyAI);
                break;
            case EnemyState.Death:
                currentState = new DeathState(this, enemyAI);
                break;
        }

        currentState.Enter();
    }
    
    public T GetCurrentState<T>() where T : IEnemyState
    {
        if (currentState is T)
        {
            return (T)currentState;
        }
        return default;
    }


    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}