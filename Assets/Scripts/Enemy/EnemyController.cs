using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private FSMClasses fsm;
    private LineScript los;
    private EnemyModel model;

    private GameObject player;

    private void Awake()
    {
        fsm = GetComponent<FSMClasses>();
        los = GetComponent<LineScript>();
        model = GetComponent<EnemyModel>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {

        bool seenPlayer;

        if (fsm._currentState is AttackState)
        {
            seenPlayer = los.IsRangeAttack(transform, player.transform);
        }
        else
        {
            seenPlayer = los.IsRange(transform, player.transform) == true &&
                los.IsAngle(transform, player.transform) == true &&
                los.IsObstacle(transform, player.transform) == true;
        }

        fsm.UpdateState(seenPlayer);

        ExecuteState();
    }

    public void ExecuteState()
    {
        if (fsm._currentState is PatrolState)
        {
            model.Patrol();
        }
        else if (fsm._currentState is PursuitState)
        {
            model.Pursuit();
        }
        else if (fsm._currentState is AttackState)
        {
            model.StartAttack();
        }
    }
}
