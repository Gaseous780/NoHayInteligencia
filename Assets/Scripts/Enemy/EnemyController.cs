using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    //private FSMClasses fsm;
    //private LineScript los;
    //public EnemyModel model;

    //private GameObject player;

    //private void Awake()
    //{
    //    fsm = GetComponent<FSMClasses>();
    //    los = GetComponent<LineScript>();
    //    model = GetComponent<EnemyModel>();
    //}

    //private void Start()
    //{
    //    player = GameObject.Find("Player");
    //}

    //private void Update()
    //{

    //    bool seenPlayer;

    //    if (fsm._currentState is AttackState)
    //    {
    //        seenPlayer = los.IsRangeAttack(transform, player.transform);
    //    }
    //    else
    //    {
    //        seenPlayer = los.IsRange(transform, player.transform) == true &&
    //            los.IsAngle(transform, player.transform) == true &&
    //            los.IsObstacle(transform, player.transform) == true;
    //    }

    //    fsm.UpdateState(seenPlayer);

    //    ExecuteState();
    //}

    //public void ExecuteState()
    //{
    //    if (fsm._currentState is PatrolState)
    //    {
    //        model.Patrol();
    //    }
    //    else if (fsm._currentState is PursuitState)
    //    {
    //        model.Pursuit();
    //    }
    //    else if (fsm._currentState is AttackState)
    //    {
    //        model.StartAttack();
    //    }
    //}

    ///////
    private Transform player;
    private LineOfSight los;
    private EnemyDecisionTree desicionTree;
    private EnemyContext context;

    [SerializeField] private float speed=3;
    [SerializeField] private float rotationSpeed=3;
    [SerializeField] private float patrolRotationSpeed = 3;

    [SerializeField] private Material attackMaterial;
    private Material defaultMaterial;
    private MeshRenderer renderer;

    private void Awake()
    {
        los = GetComponent<LineOfSight>();
        desicionTree=GetComponent<EnemyDecisionTree>();
        context=new EnemyContext { self = transform, player = player, los = los };

    }
    public void Update()
    {
        context.player = player;
        desicionTree.Evaluate(this, context);
    }
    public void Pursuit()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        Vector3 moveDirection = direction.normalized;

        transform.position += moveDirection * speed * Time.deltaTime;

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    public void Patrol()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void Attack()
    {
        Debug.Log("Empieza a atacar");
        renderer.material = attackMaterial;
        Debug.Log("Deja de atacar");
        renderer.material = defaultMaterial;
    }


}

