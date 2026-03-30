using UnityEngine;

public class EnemyControllerSB : MonoBehaviour
{
    public enum Mode
    {
        Seek,
        Flee,
        Arrive,
        Persue,
        Evade,
        Wander
    }
    [SerializeField] private Mode mode;
    [SerializeField] private Transform player;
    private Rigidbody playerRB;
    [SerializeField] float speed = 3f;
    [SerializeField] float rotationSpeed = 5f;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 dir = Vector3.zero;
        switch (mode)
        {
            case Mode.Seek:
                dir = SteeringBehaviours.Seek(transform, player.position);
                break;
            case Mode.Flee:
                dir = SteeringBehaviours.Flee(transform, player.position);
                break;
            case Mode.Arrive:
                dir = SteeringBehaviours.Arrive(transform, player.position, 5f);
                break;
            case Mode.Persue:
                dir = SteeringBehaviours.Pursue(transform, player,playerRB, 5f);
                break ;
            case Mode.Evade:
                dir = SteeringBehaviours.Evade(transform, player, playerRB,0.5f);
                break;
                case Mode.Wander:
                dir = SteeringBehaviours.Wander();
                break;
        }
        Move(dir);

    }
    private void Move(Vector3 dir)
    {
        transform.position += dir * speed * Time.deltaTime;

        if(dir!= Vector3.zero)
        {
            transform.forward=Vector3.Lerp(transform.forward, dir, Time.deltaTime * rotationSpeed);
        }
    }
}
